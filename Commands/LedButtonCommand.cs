using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PiHatWPF.Commands
{
    class LedButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _handler;

        public LedButtonCommand(Action handler)
        {
            _handler = handler;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _handler();
        }
    }
}
