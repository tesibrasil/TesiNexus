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
using MessageBox.Avalonia;

namespace TesiNexus.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            ShowSynchronizerCommand = ReactiveCommand.Create(ShowSynchronizer);
            Synchronizer = new SynchronizerViewModel();
            CurrentView = Synchronizer;
        }

        public ICommand ShowSynchronizerCommand { get; }
        public SynchronizerViewModel Synchronizer { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { this.RaiseAndSetIfChanged(ref _currentView, value); }
        }


        private void ShowSynchronizer()
        {
            var msg = MessageBoxManager.GetMessageBoxStandardWindow("title", "Working");

            msg.Show();
        }


    }


}


