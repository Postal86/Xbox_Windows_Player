using Windows.UI.Xaml;

namespace Xbox_Windows_UWP.StateTriggers
{
    /// <summary>
    /// State  trigger for whether or not
    /// the current device is in ten  foot mode.
    /// </summary>
    public class TenFootTrigger : StateTriggerBase
    {
        public TenFootTrigger()
        {
            SetActive(App.IsTenFoot);
        }
    }
}
