using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TesiNexus.Nexus.Views
{
    public partial class DataBaseConfigView : Window
    {
        public DataBaseConfigView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
