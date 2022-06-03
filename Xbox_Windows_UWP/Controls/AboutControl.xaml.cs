using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI.Xaml.Controls;



namespace Xbox_Windows_UWP.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            this.InitializeComponent();
        }

        private string Version
        {
            get
            {
                var version = SystemInformation.Instance.ApplicationVersion;
                return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            }
        }

    }
}
