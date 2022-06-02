using System.Threading.Tasks;

namespace AmbientAndNotAmbientSounds.Services
{
    /// <summary>
    /// Interface for triggering a dialog.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Opens  a  settings  dialog.
        /// </summary>
        /// <returns></returns>
        Task OpenSettingsAsync();
    }
}
