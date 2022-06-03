using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace Xbox_Windows_UWP.Animations
{
    /// <summary>
    /// Class  with reusable animations for sound items.
    /// </summary>
    public static class SoundItemAnimations
    {
        /// <summary>
        /// Scales the item back to normal with animations.
        /// </summary>
        /// <param name="sender"> A <see cref="UIElement"/>.</param>
        
        public static void ItemScaleUp(UIElement  sender, float  scale)
        {
            var visual  = ElementCompositionPreview.GetElementVisual(sender);
            visual.Scale = new Vector3(1);
        }

        /// <summary>
        /// Scales  the item  back to normal  with animations.
        /// </summary>
        /// <param name="sender">A <see cref="UIElement"/>.</param>
        public  static void ItemScaleNormal(UIElement  sender)
        {
            var visual = ElementCompositionPreview.GetElementVisual(sender);
            visual.Scale = new Vector3(1);
        }
    }
}
