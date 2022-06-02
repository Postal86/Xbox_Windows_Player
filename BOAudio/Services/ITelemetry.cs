using System.Collections.Generic;


namespace AmbientAndNotAmbientSounds.Services
{
    public interface ITelemetry
    {

        /// <summary>
        /// Tracks the  given event and its  properties.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="properties"></param>
        void TrackEvent(string eventName, IDictionary<string, string>? properties  = null);


        /// <summary>
        /// Uses  telemetry  infra to track
        /// a sound  suggestion from  the user.
        /// </summary>
        /// <param name="soundSuggestion">The user-suggested  sound.</param>
        void SuggestSound(string  soundSuggestion);

    }
}
