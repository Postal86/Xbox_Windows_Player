namespace AmbientAndNotAmbientSounds.Models
{
    public class Sound
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string? Id { get; set; }

        ///<summary>
        /// Path to image  file.
        ///</summary>
        public string? ImagePath { get; set; }

        /// <summary>
        /// Name of sound.
        /// </summary>
        public string? Name { get; set; }


        /// <summary>
        /// Path of Sound  file.
        /// </summary>
        public string? FilePath { get; set; }

        /// <summary>
        /// Extension of sound file.
        /// </summary>
        public string? FileExtension { get; set; }

        /// <summary> 
        /// Copyright  or attributes  text.
        /// </summary>
        public string? Attribution { get; set; }


        /// <summary> 
        /// The  list of image  paths to be  used 
        /// for  the screensaver.
        /// </summary>
        public string[]? ScreensaverImagePaths { get; set; } 

    }
}
