using System.Threading.Tasks;

namespace AmbientAndNotAmbientSounds.Services
{
    /// <summary>
    /// Interface for registering or unregistering
    /// for  store-based pushed notifications.
    /// </summary>
    public  interface IStoreNotificationRegister
    {
        /// <summary>
        /// Registers for store-based notifications.
        /// </summary>
        
        Task Register();

        /// <summary>
        /// Unregisters for store-based  notifications.
        /// </summary>
        
        Task Unregister();

        /// <summary>
        /// Tracks when the app is  activated due
        /// to the  notification.
        /// </summary>
        /// <param name="launchArgs">The launch  arguments of the  activation.</param>
        /// <returns>Returns the launch arguments  with  tracking IDs stripped out.</returns>
        string TrackLaunch(string launchArgs);
    }
}
