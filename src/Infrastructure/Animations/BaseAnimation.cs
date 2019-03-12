using System.Threading.Tasks;
using Xamarin.Forms;

namespace West.Extensions.Xamarin
{
    public abstract class BaseAnimation : BindableObject
    {
        public static readonly BindableProperty TargetProperty =
            BindableProperty.Create(
                nameof(Target),
                typeof(VisualElement),
                typeof(BaseAnimation),
                null,
                BindingMode.TwoWay);

        public VisualElement Target
        {
            get => (VisualElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        public static readonly BindableProperty DurationProperty =
            BindableProperty.Create(
                nameof(Duration),
                typeof(string),
                typeof(BaseAnimation),
                "1000",
                BindingMode.TwoWay);

        public string Duration
        {
            get => (string)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        protected abstract Task BeginAnimation();

        public async Task Begin()
        {
            await BeginAnimation();
        }
    }
}