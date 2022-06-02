﻿using AmbientAndNotAmbientSounds.Constants;
using AmbientAndNotAmbientSounds.Factories;
using AmbientAndNotAmbientSounds.Services;
using Microsoft.Toolkit.Diagnostics;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AmbientAndNotAmbientSounds.ViewModels
{
    public class SoundListViewModel
    {
        private readonly IMediaPlayerService _player;
        private readonly ISoundDataProvider _provider;
        private readonly ITelemetry _telemetry;
        private readonly ISoundVMFactory _factory;

        public SoundListViewModel(IMediaPlayerService player, 
                                  ISoundDataProvider provider, 
                                  ITelemetry telemetry, 
                                  ISoundVMFactory factory)
        {

            Guard.IsNotNull(player, nameof(player));
            Guard.IsNotNull(provider, nameof(provider));
            Guard.IsNotNull(telemetry, nameof(telemetry));
            Guard.IsNotNull(factory, nameof(factory));



            _player = player;
            _provider = provider;
            _telemetry = telemetry;
            _factory = factory;

            LoadCommand = new AsyncRelayCommand(LoadAsync);
            PlaySoundCommand = new RelayCommand<SoundViewModel>(PlaySound);

            _provider.LocalSoundAdded += OnLocalSoundAdded;
            _provider.LocalSoundDeleted += OnLocalSoundDeleted;
        }

        private void OnLocalSoundDeleted(object sender, string id)
        {
            var forDeletion = Sounds.FirstOrDefault(x => x.Id == id);
            if (forDeletion == null) return;
            _player.DeleteFromPlaylist(forDeletion.Index);
            Sounds.Remove(forDeletion);

            int index = 0;
            foreach (var sound in Sounds)
            {
                sound.Index = index;
                index++;
            }

        }

        private async void OnLocalSoundAdded(object  sender, Models.Sound e)
        {
            var s = _factory.GetSoundVm(e, Sounds.Count);
            Sounds.Add(s);
            await _player.AddToPlaylistAsync(e);
        }


        /// <summary>
        /// The <see cref="IAsyncRelayCommand"/> responsible  for loading  the  viewmodel data.
        /// </summary>
        public IAsyncRelayCommand LoadCommand { get; }


        /// <summary>
        /// The <see cref="IRelayCommand{T}"/> responsible for playing a selected  sound.
        /// </summary>
        public IRelayCommand<SoundViewModel> PlaySoundCommand { get; set; }


        /// <summary>
        /// The list of sounds for this page.
        /// </summary>
        public ObservableCollection<SoundViewModel> Sounds { get; } = new();


        /// <summary>
        /// Loads the list of sounds for this  view model.
        /// </summary>
        /// <returns></returns>
        private async Task LoadAsync()
        {
            if (Sounds.Count > 0) return; // already  initialized

            var soundList = await _provider.GetSoundsAsync();

            int index = 0;
            foreach (var sound in soundList)
            {
                var s = _factory.GetSoundVm(sound, index);
                Sounds.Add(s);
                index++;
            }

            await _player.Initialize(soundList);
        }


        /// <summary>
        /// Loads  the clicked  sound into  the player and plays it.
        /// </summary>
        /// <param name="sound"></param>
        private void PlaySound(SoundViewModel  sound)
        {
            if (sound is null)
            {
                return;
            }

            sound.Play();
            _telemetry.TrackEvent(TelemetryConstants.SoundClicked, new Dictionary<string, string>
            {
                { "id", sound.Name ?? "" }
                
            });
        }
    }
}
