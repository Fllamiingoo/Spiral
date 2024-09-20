using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Spiral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Zavolá metodu pro vykreslení spirály
            DrawSquareSpiralRecursive(spiralCanvas, 200, 200, 100, 10);

            // Odkomentujte následující řádek, pokud chcete použít nerekurzivní verzi
            // DrawSquareSpiralNonRecursive(spiralCanvas, 200, 200, 100, 10);
        }

        // Nerekurzivní generování spirály
        public void DrawSquareSpiralNonRecursive(Canvas canvas, double startX, double startY, double lineLength, double gapWidth)
        {
            double x = startX;
            double y = startY;
            double currentLineLength = lineLength;

            int direction = 0; // 0: doprava, 1: dolů, 2: doleva, 3: nahoru
            for (int i = 0; i < 50; i++)
            {
                // Vykreslí čáru
                Line line = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };

                line.X1 = x;
                line.Y1 = y;

                switch (direction)
                {
                    case 0: // Doprava
                        x += currentLineLength;
                        break;
                    case 1: // Dolů
                        y += currentLineLength;
                        break;
                    case 2: // Doleva
                        x -= currentLineLength;
                        break;
                    case 3: // Nahoru
                        y -= currentLineLength;
                        break;
                }

                line.X2 = x;
                line.Y2 = y;
                canvas.Children.Add(line);

                // Změní směr a mírně sníží délku pro efekt spirály
                direction = (direction + 1) % 4;
                currentLineLength -= gapWidth;
                if (currentLineLength <= 0)
                {
                    break; // Ukončí smyčku, pokud je délka čáry záporná nebo nulová
                }
            }
        }

        // Rekurzivní generování spirály
        public void DrawSquareSpiralRecursive(Canvas canvas, double startX, double startY, double lineLength, double gapWidth, int direction = 0)
        {
            if (lineLength <= 0) return; // Ukončí se, pokud je délka čáry 0 nebo méně

            // Vykreslí čáru
            Line line = new Line
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            line.X1 = startX;
            line.Y1 = startY;

            double endX = startX;
            double endY = startY;

            switch (direction)
            {
                case 0: // Doprava
                    endX += lineLength;
                    break;
                case 1: // Dolů
                    endY += lineLength;
                    break;
                case 2: // Doleva
                    endX -= lineLength;
                    break;
                case 3: // Nahoru
                    endY -= lineLength;
                    break;
            }

            line.X2 = endX;
            line.Y2 = endY;
            canvas.Children.Add(line);

            // Rekurze
            DrawSquareSpiralRecursive(canvas, endX, endY, lineLength - gapWidth, gapWidth, (direction + 1) % 4);
        }
    }
}
