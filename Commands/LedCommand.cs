﻿using PiHatWPF.ViewModel;
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
        private int _x, _y;

        public event EventHandler CanExecuteChanged;

        public LedCommand(LedViewModel viewModel, int x, int y)
        {
            _viewmodel = viewModel;
            _x = x;
            _y = y;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            _viewmodel.LedClickedFunction(_x, _y);
        }
    }
}
