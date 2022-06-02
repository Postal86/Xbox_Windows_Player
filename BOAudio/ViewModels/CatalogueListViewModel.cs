using AmbientAndNotAmbientSounds.Factories;
using AmbientAndNotAmbientSounds.Models;
using AmbientAndNotAmbientSounds.Services;
using Microsoft.Toolkit.Diagnostics;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace AmbientAndNotAmbientSounds.ViewModels
{
    public class CatalogueListViewModel
    {
        private readonly IOnlineSoundDataProviders _dataProvider;
        private readonly ISoundVMFactory _soundVmFactory;

        public CatalogueListViewModel(IOnlineSoundDataProviders dataProvider, ISoundVMFactory soundVmFactory)
        {
            Guard.IsNotNull(dataProvider, nameof(dataProvider));
            Guard.IsNotNull(soundVmFactory, nameof(soundVmFactory));

            _dataProvider = dataProvider;
            _soundVmFactory = soundVmFactory;

            LoadCommand = new AsyncRelayCommand(LoadAsync);
        }

        /// <summary>
        /// The <see cref="IAsyncRelayCommand"/> responsible for loading the viewmodel data.
        /// </summary>
        public IAsyncRelayCommand LoadCommand { get; }

        /// <summary>
        /// The list of sounds for this page.
        /// </summary>
        public ObservableCollection<OnlineSoundViewModel> Sounds { get; } = new();


        private async Task LoadAsync()
        {
            if (Sounds.Count > 0)
            {
                return; 
            }

            IList<Sound> sounds; 

            try
            {
                sounds = await _dataProvider.GetSoundsAsync();  
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            foreach(var sound in sounds)
            {
                if (sound != null)
                {
                    Sounds.Add(_soundVmFactory.GetOnlineSoundVm(sound));
                }
            }
        }
    }
}
