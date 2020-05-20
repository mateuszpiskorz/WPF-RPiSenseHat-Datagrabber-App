using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PiHatWPF.ViewModel
{
    class LedViewModel:BaseViewModel
    {
        private Dictionary<Tuple<int, int>, Rectangle> ledMatrix = new Dictionary<Tuple<int, int>, Rectangle>();
        private List<Rectangle> selectedLeds = new List<Rectangle>();
        public Grid ViewLedMatrix { get; set; }

        public LedViewModel()
        {

        }

        private void LedMatrixInit()
        {
            BrushConverter bc = new BrushConverter();
            int[] initialColors = { 65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535,
                                    65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535,
                                    65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535,
                                    65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535,
                                    65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535,
                                    65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535,
                                    65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535,
                                    65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535};
        }
    }
}
