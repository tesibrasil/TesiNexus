using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TesiNexus.Synchronizer.Views;
using TesiNexus.ViewModels;

namespace TesiNexus.Synchronizer.ViewModels
{
    public class SynchronizerViewModel : ViewModelBase
    {

        public SynchronizerViewModel()
        {
            ShowServerGroup = ReactiveCommand.CreateFromTask(async () => { CallServerGroup(); });
        }

        #region Properties


        #endregion

        #region Commands
        public ICommand ShowServerGroup { get; set; }

        #endregion

        #region Methods
        public void CallServerGroup()
        {
            
            var window = new ServerGroupView();
            window.Show();
        }
        #endregion

    }
}
