using ReactiveUI;
using System;
using System.Reactive;
using System.Windows.Input;
using System.Windows;
using TesiNexus.Views;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;
using Avalonia.Controls;
using System.Threading.Tasks;
using Avalonia.Input;

namespace TesiNexus.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand CloseCommand { get; }
        public ReactiveCommand<object, Unit> EditCommand { get; set; }
        public MainWindowViewModel()
        {
            CloseCommand = ReactiveCommand.Create(CloseApp);
            EditCommand = ReactiveCommand.CreateFromTask<object, Unit>(EditCommandExecuted);
        }

        private void ShowMessage()
        {
            var msg = MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxStandardWindow("title", "Lorem ipsum ");
            
            msg.Show();
        }
        public void CloseApp()
        {
            var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;

            lifetime.Shutdown();
        }

        private async Task<Unit> EditCommandExecuted(object p)
        {
            var msg = MessageBox.Avalonia.MessageBoxManager
            .GetMessageBoxStandardWindow("Teste", "Working...");
            
            
            msg.Show();

            return Unit.Default;
        }
        


    }


}


