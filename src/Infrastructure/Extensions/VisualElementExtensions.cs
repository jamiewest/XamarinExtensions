using System;
using System.Threading.Tasks;

namespace Xamarin.Forms
{
    public static class VisualElementExtensions
    {
        public static Task<bool> ColorTo(
            this VisualElement self,
            Color fromColor,
            Color toColor,
            Action<Color> callback,
            uint length = 250,
            Easing easing = null)
        {
            Color Transform(double t)
            {
                return Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
                    fromColor.G + t * (toColor.G - fromColor.G), fromColor.B + t * (toColor.B - fromColor.B),
                    fromColor.A + t * (toColor.A - fromColor.A));
            }

            return ColorAnimation(self, "ColorTo", Transform, callback, length, easing);
        }

        private static Task<bool> ColorAnimation(
            IAnimatable element,
            string name,
            Func<double, Color> transform,
            Action<Color> callback,
            uint length,
            Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }
    }
}