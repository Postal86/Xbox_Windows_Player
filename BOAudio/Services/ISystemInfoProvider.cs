

namespace AmbientAndNotAmbientSounds.Services
{
    /// <summary>
    /// Interface  fot getting system information
    /// for  the current session.
    /// </summary>
    public interface ISystemInfoProvider
    {
        /// <summary>
        /// Retrieves  the  culture name
        /// in en-US  format.
        /// </summary>
        /// <returns></returns>
        string GetCulture();


        /// <summary>
        /// Returns true is  the  current
        /// device is Xbox or other  device
        /// optimized for a 10-foot viewing
        /// distance.
        /// </summary>
        /// <returns></returns>
        bool IsTenFoot();
    }
}
