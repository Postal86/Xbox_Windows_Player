using AmbientAndNotAmbientSounds.Constants;
using AmbientAndNotAmbientSounds.Models;
using AmbientAndNotAmbientSounds.Services;
using Microsoft.Toolkit.Diagnostics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmbientAndNotAmbientSounds.ViewModels
{
    public class OnlineSoundViewModel : ObservableObject
    {
        private readonly Sound _sound;
        private readonly IDownloadManager _downloadManager;
        private readonly ISoundDataProvider _soundDataProvider;
        private readonly ITelemetry telemetry;
        private readonly Progress<double> _downloadProgress;
        private double _progressValue;
        private bool _isInstalled;

        public OnlineSoundViewModel(Sound sound, IDownloadManager downloadManager, ISoundDataProvider soundDataProvider, 
            ITelemetry telemetry)
        {

            Guard.IsNotNull(sound, nameof(sound));
            Guard.IsNotNull(downloadManager, nameof(downloadManager));
            Guard.IsNotNull(telemetry, nameof(telemetry));
            Guard.IsNotNull(soundDataProvider, nameof(soundDataProvider));
            
            _sound = sound;
            _downloadManager = downloadManager;
            _soundDataProvider = soundDataProvider;
            this.telemetry = telemetry;

            _downloadProgress = new Progress<double>();
            _downloadProgress.ProgressChanged += OnProgressChanged;
            _soundDataProvider.LocalSoundDeleted += OnSoundDeleted;

            DownloadCommand = new AsyncRelayCommand(DownloadAsync);
            LoadCommand = new AsyncRelayCommand(LoadAsync);
            DeleteCommand = new AsyncRelayCommand(DeleteSound);
        }

        private async void OnSoundDeleted(object  sender, string id)
        {
            if (id == _sound.Id)
            {
                _isInstalled = await _soundDataProvider.IsSoundInstalledAsync(_sound.Id);
                DownloadProgressValue = 0;
            }
        }

        private async void OnProgressChanged(object sender, double e)
        {
            DownloadProgressValue = 0;
            if (e >= 100)
            {
                _isInstalled = await _soundDataProvider.IsSoundInstalledAsync(_sound.Id ?? "");
                DownloadProgressValue = 0;
            }
        }



        /// <summary>
        /// The  sound's attribution
        /// </summary>
        public string? Attribution => _sound.Attribution;


        /// <summary>
        /// Name of the  sound.
        /// </summary>
        public string? Name => _sound.Name;


        /// <summary>
        /// The path for the image to display for the  current  sound.
        /// </summary>
        public string? ImagePath => _sound.ImagePath;

        /// <summary>
        /// This sound's download progress.
        /// </summary>
        public double  DownloadProgressValue
        {
            get => _progressValue;
            set
            {
                SetProperty(ref _progressValue, value);
                OnPropertyChanged(nameof(DownloadProgressVisible));
            }
        }

        /// <summary>
        /// True if download progress  should be visible.
        /// </summary>
        public bool DownloadProgressVisible => DownloadProgressValue >= 0 &&  DownloadProgressValue < 100; 

        /// <summary>
        /// True if the  item is already  installed.
        /// </summary>
        public bool IsInstalled
        {
            get => _isInstalled;
            set
            {
                SetProperty(ref _isInstalled, value);
                OnPropertyChanged(nameof(CanDownload));
            }
        }

        /// <summary>
        /// True if the  item can be  downloaded
        /// </summary>
        public  bool CanDownload => _isInstalled;


        /// <summary>
        /// Command  for downloading this  sound.
        /// </summary>
        public IAsyncRelayCommand DownloadCommand { get; }

        /// <summary>
        /// Command  for loading this  sound.
        /// </summary>
        public IAsyncRelayCommand DeleteCommand { get; }

        /// <summary>
        /// Command  for loading  this sound.
        /// </summary>
        public IAsyncRelayCommand LoadCommand { get; }


        private async  Task DeleteSound()
        {
            telemetry.TrackEvent(TelemetryConstants.CatalogueDeletedClicked, new Dictionary<string, string>
            {
                { "name", _sound.Name ?? ""},
                { "id", _sound.Id ?? ""},
            });

            await _soundDataProvider.DeleteLocalSoundAsync(_sound.Id ?? "");
        }

        private async Task LoadAsync()
        {
            IsInstalled = await _soundDataProvider.IsSoundInstalledAsync(_sound.Id ?? "");
        }

        private Task DownloadAsync()
        {
            if (DownloadProgressValue == 0 &&  CanDownload)
            {
                telemetry.TrackEvent(TelemetryConstants.DownloadClicked, new Dictionary<string, string>
                {
                    { "name", Name ?? ""},
                    { "id", _sound.Id ?? ""},
                });

                return _downloadManager.QueueAndDownloadAsync(_sound, _downloadProgress);
            }

            return Task.CompletedTask;
        }

    }
}
