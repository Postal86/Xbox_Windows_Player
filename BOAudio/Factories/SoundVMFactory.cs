using AmbientAndNotAmbientSounds.Models;
using AmbientAndNotAmbientSounds.Services;
using AmbientAndNotAmbientSounds.ViewModels;
using Microsoft.Toolkit.Diagnostics;



namespace AmbientAndNotAmbientSounds.Factories
{
    public class SoundVMFactory : ISoundVMFactory
    {
        private readonly IDownloadManager   _downloadManager;
        private readonly ISoundDataProvider _soundDataProvider;
        private readonly IMediaPlayerService _player;
        private readonly ITelemetry telemetry;

        public SoundVMFactory(IDownloadManager downloadManager, ISoundDataProvider soundDataProvider, IMediaPlayerService player, ITelemetry telemetry)
        {
            Guard.IsNotNull(downloadManager, nameof(downloadManager));
            Guard.IsNotNull(soundDataProvider, nameof(soundDataProvider));
            Guard.IsNotNull(player, nameof(player));
            Guard.IsNotNull(telemetry, nameof(telemetry));


            _downloadManager = downloadManager;
            _soundDataProvider = soundDataProvider;
            _player = player;
            this.telemetry = telemetry;
        }

        public OnlineSoundViewModel GetOnlineSoundVm(Sound s)
        {
            Guard.IsNotNull(s, nameof(s));
            return new OnlineSoundViewModel(s, _downloadManager, _soundDataProvider, telemetry);
        }

        public SoundViewModel GetSoundVm(Sound s, int index)
        {
            Guard.IsNotNull(s, nameof(s));
            Guard.IsGreaterThan(index, -1, nameof(index));
            return new SoundViewModel(s, _player, index, _soundDataProvider, telemetry);
        }
    }
}
