using AmbientAndNotAmbientSounds.Constants;
using AmbientAndNotAmbientSounds.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.Toolkit.Diagnostics;
using System.Collections.Generic;
using Windows.Globalization;

namespace Xbox_Windows_UWP.Services
{
    public class AppCenterTelemetry : ITelemetry
    {
        private const string AppSecret = ""; // Do  not  commit
        private readonly IUserSettings _userSettings;


        public AppCenterTelemetry(IUserSettings userSettings)
        {
            Guard.IsNotNull(userSettings, nameof(userSettings));

            _userSettings = userSettings;

            AppCenter.SetCountryCode(new  GeographicRegion().CodeTwoLetter);
            AppCenter.Start(AppSecret, typeof(Analytics));
        }


        public void TrackEvent(string eventName, IDictionary<string, string> properties = null)
        {
            if (_userSettings.Get<bool>(UserSettingsConstants.TelemetryOn))
            {
                Analytics.TrackEvent(eventName, properties);
            }
        }


        public void SuggestSound(string soundSuggestion)
        {
            if (string.IsNullOrWhiteSpace(soundSuggestion))
                return;

            soundSuggestion = soundSuggestion.Trim();

            Analytics.TrackEvent("soundSuggestion", new Dictionary<string, string>
            {
                { "value", soundSuggestion }
            });
        }
    }
}
