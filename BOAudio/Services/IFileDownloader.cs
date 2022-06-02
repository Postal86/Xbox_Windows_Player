using System.Threading.Tasks;

namespace AmbientAndNotAmbientSounds.Services
{

    /// <summary>
    /// Interface for downloading and saving sounds.
    /// </summary>
    public  interface IFileDownloader
    {
        /// <summary>
        ///  Downloads  sound  and saves  it to
        ///  a local  directory
        /// </summary>
        /// <param name="url"></param>
        /// <param name="nameWithExt"></param>
        /// <returns></returns>
        Task<string> SoundDownloadAndSaveAsync(string? url, string  nameWithExt);


        /// <summary>
        /// Downloads  image  and saves it to
        /// a local directory.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<string> ImageDownloadAndSaveAsync(string? url, string  name);
    }
}
