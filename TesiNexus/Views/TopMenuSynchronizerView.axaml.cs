using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TesiNexus.Views
{
    public partial class TopMenuSynchronizerView : UserControl
    {
        public TopMenuSynchronizerView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
