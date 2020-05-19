using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PiHatWPF.Commands;

namespace PiHatWPF.ViewModel
{
    class MainWindowViewModel:BaseViewModel
    {
        #region Properties
        public BaseViewModel _selectedViewModel = new ConfigViewModel();
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        #endregion

        public MainWindowViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
