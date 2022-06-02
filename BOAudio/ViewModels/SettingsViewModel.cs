using AmbientAndNotAmbientSounds.Constants;
using AmbientAndNotAmbientSounds.Services;
using Microsoft.Toolkit.Diagnostics;

namespace AmbientAndNotAmbientSounds.ViewModels
{
    public class SettingsViewModel  
    {
        private readonly IUserSettings _userSettings;
        private readonly IStoreNotificationRegister _notifications;
        private readonly IScreensaverService _screensaverService;
        private readonly ISystemInfoProvider _systemInfoProvider;
        private readonly string _theme;
        private bool _notificationsLoading;

        public SettingsViewModel(IUserSettings userSettings, 
                                 IStoreNotificationRegister notifications, 
                                 IScreensaverService screensaverService, 
                                 ISystemInfoProvider systemInfoProvider)
        {
            Guard.IsNotNull(userSettings, nameof(userSettings));
            Guard.IsNotNull(notifications, nameof(notifications));
            Guard.IsNotNull(screensaverService, nameof(screensaverService));
            Guard.IsNotNull(systemInfoProvider, nameof(systemInfoProvider));
            
            _userSettings = userSettings;
            _notifications = notifications;
            _screensaverService = screensaverService;
            _systemInfoProvider = systemInfoProvider;

            _theme = userSettings.Get<string>(UserSettingsConstants.Theme);
            InitializeTheme();

        }

        /// <summary>
        /// Settings  flag for telemetry.
        /// </summary>
        public bool TelemetryEnabled
        {
            get => _userSettings.Get<bool>(UserSettingsConstants.TelemetryOn);
            set => _userSettings.Set(UserSettingsConstants.TelemetryOn, value);
        }


        public bool ScreensaverEnabled
        {
            get => _userSettings.Get(UserSettingsConstants.EnableScreenSaver, _systemInfoProvider.IsTenFoot());
            set
            {
                _userSettings.Set(UserSettingsConstants.EnableScreenSaver, value);

                if (value)
                {
                    _screensaverService.StartTimer();
                }
                else
                {
                    _screensaverService.StopTimer();
                }
            }

        }

        /// <summary>
        /// Settings flag for dark  screensaver.
        /// </summary>
        public  bool DarkScreensaverEnabled
        {
            get => _userSettings.Get<bool>(UserSettingsConstants.DarkScreenSaver);
            set => _userSettings.Set(UserSettingsConstants.DarkScreenSaver, value);
        }


        /// <summary>
        /// Settings  flag for notifications.
        /// </summary>
        public bool NotificationsEnabled
        {
            get => _userSettings.Get<bool>(UserSettingsConstants.Notifications);
            set => SetNotifications(value);
        }

        /// <summary>
        /// Property for setting IsChecked  property of RadioButton  for default  app  theme.
        /// </summary>
        public bool DefaultRadioIsChecked { get; set; }

        /// <summary>
        /// Property  for setting IsChecked property of RadioButton  for default  app theme.
        /// </summary>
        public bool DarkRadioIsChecked { get; set; }



        /// <summary>
        ///  Property for setting IsChecked  property of RadioButton for light app  theme.
        /// </summary>
        public bool LightRadioIsChecked { get; set; }


        /// <summary>
        /// Event  handler  for RadioButton (Dark  theme) click  event.
        /// </summary>
        public void DarkThemeRadioClicked()
        {
            _userSettings.Set(UserSettingsConstants.Theme, "dark");
        }

        /// <summary>
        /// Event  handler  for RadioButton (default  theme) click event
        /// </summary>
        public void DefaultThemeRadioClicked()
        {
            _userSettings.Set(UserSettingsConstants.Theme, "default"); 
        }

        /// <summary>
        /// Event handler for RadioButton  (light theme)  click  event.
        /// </summary>
        public void LightThemeRadioClicked()
        {
            _userSettings.Set(UserSettingsConstants.Theme, "light");
        }

        private async void SetNotifications(bool  value)
        {
            if (value == NotificationsEnabled ||  _notificationsLoading)
            {
                return;
            }

            _notificationsLoading = true;
            if (value)
            {
                await _notifications.Register();
            }
            else
            {
                await _notifications.Unregister();
            }

            _userSettings.Set(UserSettingsConstants.Notifications,  value);
            _notificationsLoading = false;
        }

        private void InitializeTheme()
        {
            if (_theme != null)
            {
                switch (_theme)
                {
                    case "default":
                        DefaultRadioIsChecked = true;
                        break;
                    case "light":
                        LightRadioIsChecked = true;
                        break;
                    case "dark":
                        DarkRadioIsChecked = true;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
