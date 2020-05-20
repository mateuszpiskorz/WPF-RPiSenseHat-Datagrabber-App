using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace PiHatWPF.ViewModel
{
    class LedViewModel:BaseViewModel
    {
        private Dictionary<Tuple<int, int>, Rectangle> ledMatrix = new Dictionary<Tuple<int, int>, Rectangle>();
        private List<Rectangle> selectedLeds = new List<Rectangle>();
        private readonly int[] InitialColors = { 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440,
                                                 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440,
                                                 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440,
                                                 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440,
                                                 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440,
                                                 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440,
                                                 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440,
                                                 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440, 5263440};

        public Grid ViewLedMatrix { get; set; }

        public LedViewModel()
        {
            this.LedMatrixInit();
        }

        private void LedMatrixInit()
        {
            BrushConverter bc = new BrushConverter();
            int[] ledColors = InitialColors; 
            Grid grid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            for (int i = 0; i < 8; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                for (int j = 0; j < 8; j++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    Rectangle rect = new Rectangle()
                    {
                        Fill = new SolidColorBrush(Color.FromRgb((byte)((ledColors[i * 8 + j] >> 16) & 0xFF), (byte)((ledColors[i * 8 + j] >> 8) & 0xFF), (byte)((ledColors[i * 8 + j] >> 0) & 0xFF))),
                        Width = 30,
                        Height = 30,
                        Margin = new Thickness(5, 5, 5, 5),
                      
                    };
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    grid.Children.Add(rect);
                    ledMatrix.Add(Tuple.Create(i, j), rect);
                }
            }

            ViewLedMatrix = grid;
        }
    }
}
