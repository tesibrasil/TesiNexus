using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TesiNexus.Synchronizer.Views
{
    public partial class ServerGroupView : Window
    {
        public ServerGroupView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            this.Get<Control>("ServerGroupScreen").PointerPressed += (i, e) =>
            {
                PlatformImpl?.BeginMoveDrag(e);
            };
        }
    }
}
