using System.Collections.Generic;


namespace AmbientAndNotAmbientSounds.Constants
{
    public class UserSettingsConstants
    {
        public const string Volume = "LastUsedVolume";


        public const string TelemetryOn = "TelemetryOn";

        public const string Theme = "themeSetting";

        public const string CataloguePreview = "CataloguePreview";

        public const string Notifications = "NotificationSetting";

        public const string EnableScreenSaver = "EnableScreenSaver";

        public const string DarkScreenSaver = "DarkScreensaver";

        public static readonly Dictionary<string, object> Defaults = new Dictionary<string, object>()
        {
            {Volume, 80d },
            {TelemetryOn, true },
            {Notifications, true },
            {EnableScreenSaver, false },
            {CataloguePreview, false },
            {DarkScreenSaver, false },
            {Theme, "default" }
        };

    }
}
