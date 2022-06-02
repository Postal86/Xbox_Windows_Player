namespace AmbientAndNotAmbientSounds.Constants
{
    public class TelemetryConstants
    {
        // Catalogue
        private const string Catalogue = "catalogue:";
        public  const string DownloadClicked = Catalogue + "downloadClicked";
        public  const string MoreSoundsClicked = Catalogue + "moreSoundsClicked";
        public const string CatalogueDeletedClicked = Catalogue + "deleteClicked";

        // Gallery
        private const string Gallery = "gallery:";
        public const  string SoundClicked = Gallery + "soundClicked";
        public const  string  DeleteClicked = Gallery + "deletedClicked";

        // Timer
        private const string Timer = "timer:";
        public  const string TimeSelected = Timer + "timeSelected";
        public const string  TimerStateChanged = Timer + "timerStateChanged";

        // Playback
        private const string Playback = "playback:";
        public const string  PlaybackStateChanged = Playback + "stateChanged";
        public const string PlaybackRandom = Playback + "randomClicked";

        // Pages 
        private const string Page = "page:";
        public const  string PageNavTo = Page + "navigationTo";

        // ScreenSaver
        private const string ScreenSaver = "ScreenSaver:";
        public  const string ScreenSaverLoaded = ScreenSaver + "loaded";
        public const string ScreenSaverTriggered = ScreenSaver + "triggered";


    }
}
