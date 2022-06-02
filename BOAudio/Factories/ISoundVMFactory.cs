using AmbientAndNotAmbientSounds.Models;
using AmbientAndNotAmbientSounds.ViewModels;



namespace AmbientAndNotAmbientSounds.Factories
{
    public interface ISoundVMFactory
    {
        /// <summary> 
        /// Creates  a new online sound viewmodel.
        /// </summary>
        /// <param name="s">The sound to associate  with the  viewmodel. </param>
        /// <returns>An online sound viewmodel.</returns>
        OnlineSoundViewModel GetOnlineSoundVm(Sound s);


        /// <summary>
        /// Creates new sound viewmodel.
        /// </summary>
        /// <param name="s">The sound to associate with the viewmodel.</param>
        /// <param name="index">The index of the sound in the list.</param>
        /// <returns>A sound viewmodel.</returns>
        SoundViewModel GetSoundVm(Sound s, int  index);


    }
}
