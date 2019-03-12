using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace West.Extensions.Xamarin
{
    public class ColorAnimation : BaseAnimation
    {
        public static readonly BindableProperty ToColorProperty =
            BindableProperty.Create(
                nameof(ToColor),
                typeof(Color),
                typeof(ColorAnimation),
                Color.Default,
                BindingMode.TwoWay);

        public Color ToColor
        {
            get => (Color)GetValue(ToColorProperty);
            set => SetValue(ToColorProperty, value);
        }

        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            var fromColor = Target.BackgroundColor;

            return Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Target.ColorTo(fromColor, ToColor, c => Target.BackgroundColor = c, Convert.ToUInt32(Duration));
                });
            });
        }
    }
}
