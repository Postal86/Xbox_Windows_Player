namespace AmbientAndNotAmbientSounds.Services
{
    public interface INavigator
    {
        /// <summary>
        /// The  frame that can navigate. This must be set before
        /// any method is  called.
        /// </summary>
        object Frame { get; set; }


        /// <summary>
        /// Navigates to the  screensaver.
        /// </summary>
        void ToScreensaver();
    }
}
