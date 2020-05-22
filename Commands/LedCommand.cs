using PiHatWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PiHatWPF.Commands
{
    class LedCommand : ICommand
    {
        private LedViewModel _viewmodel;
        private Rectangle _rect;

        public event EventHandler CanExecuteChanged;

        public LedCommand(LedViewModel viewModel, Rectangle rect)
        {
            _viewmodel = viewModel;
            _rect = rect;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            _viewmodel.LedClickedFunction(_rect);
        }
    }
}
