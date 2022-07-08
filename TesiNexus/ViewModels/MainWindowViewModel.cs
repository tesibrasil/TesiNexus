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


    #region Hamburger Menu Properties
        
        private bool _showMenu;

        public bool ShowMenu
        {
            get { return _showMenu; }
            set
            {
                this.RaiseAndSetIfChanged(ref _showMenu, value);
                ActionMenu();

            }
        }

        private int _widthMenu;

        public int WidthMenu
        {
            get { return _widthMenu; }
            set
            {
                this.RaiseAndSetIfChanged(ref _widthMenu, value);

            }
        }

        private Thickness _margin;

        public Thickness Margin
        {
            get { return _margin; }
            set
            {
                this.RaiseAndSetIfChanged(ref _margin, value);
            }
        }


        public ReactiveCommand<object, Unit> MouseOverCommand { get; set; }
        public ReactiveCommand<object, Unit> MouseLeaveCommand { get; set; }

        #endregion


        public MainWindowViewModel()
        {
            CloseCommand = ReactiveCommand.Create(CloseApp);
            EditCommand = ReactiveCommand.CreateFromTask<object, Unit>(EditCommandExecuted);

            #region Hamburger Menu Initialize

            ShowMenu = false;
            WidthMenu = 200;
            Margin = new Thickness(235, 10, 10, 10);
            MouseOverCommand = ReactiveCommand.CreateFromTask<object, Unit>(MouseOverCommandExecuted);
            MouseLeaveCommand = ReactiveCommand.CreateFromTask<object, Unit>(MouseLeaveCommandExecuted); 

            #endregion
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

      #region HamburgerMenuMethods
        public void ActionMenu()
        {
            if (ShowMenu)
            {
                WidthMenu = 50;
                Margin = new Thickness(65, 10, 10, 10);
            }
            else
            {
                WidthMenu = 200;
                Margin = new Thickness(235, 10, 10, 10);
            }
        }

        private async Task<Unit> MouseOverCommandExecuted(object p)
        {
            if (ShowMenu)
            {
                WidthMenu = 200;
            }
            return Unit.Default;
        }

        private async Task<Unit> MouseLeaveCommandExecuted(object p)
        {
            if (ShowMenu)
            {
                WidthMenu = 50;
            }
            return Unit.Default;
        } 

      #endregion

    }


}


