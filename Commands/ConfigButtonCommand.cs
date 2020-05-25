using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PiHatWPF.Commands
{
    class ConfigButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action handler;
        public ConfigButtonCommand(Action handler)
        {
            this.handler = handler;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            handler();
        }
    }
}
