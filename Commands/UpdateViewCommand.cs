using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PiHatWPF.ViewModel;

namespace PiHatWPF.Commands
{
    class UpdateViewCommand : ICommand
    {
        private MainWindowViewModel viewModel;

        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (parameter)
            {
                case "Charts":
                    viewModel.SelectedViewModel = new ChartsViewModel();
                    break;
                case "Config":
                    viewModel.SelectedViewModel = new ConfigViewModel();
                    break;
                case "Led":
                    viewModel.SelectedViewModel = new LedViewModel();
                    break;
            }
            
        }
    }
}
