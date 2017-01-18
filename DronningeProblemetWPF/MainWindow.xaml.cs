using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using DronningeProblemet;
using DronningeProblemet.Enums;

namespace DronningeProblemetWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public readonly int FieldDimension = 50;
        public ResultSet Results;
        public List<ChessBoard> Boards;
        public int CurrentIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
            DrawBoard();
            Results = new ResultSet(0, new HashSet<ChessBoard>());
            Boards = new List<ChessBoard>();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Results = Program.Algorithm(x =>
            {
                lb.Items.Add(x);
            });
            lb.Items.Add("Calculations Ended");
            lb.Items.Add("Max Number of Queens: " + Results.Queens);
            lb.Items.Add("Number of Solutions: " + Results.Solutions.Count);
            Boards = Results.Solutions.ToList();
            DrawNext.IsEnabled = true;
            NextClick();
        }
        private void DrawBoard()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    PlaceField(x, y, ((x + y) % 2) == 0 ? Colors.Wheat : Colors.Black);
                }
            }
        }
        private void PlaceQueen(int x, int y)
        {
            var ellip = new Ellipse
            {
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.White),
                Width = FieldDimension * 0.6,
                Height = FieldDimension * 0.6
            };

            Canvas.SetLeft(ellip, (x * FieldDimension) + (FieldDimension * 0.2));
            Canvas.SetTop(ellip, (y * FieldDimension) + (FieldDimension * 0.2));
            can.Children.Add(ellip);
        }
        private void PlaceField(int x, int y, Color b)
        {
            var rect = new Rectangle
            {
                Stroke = new SolidColorBrush(b),
                Fill = new SolidColorBrush(b),
                Width = FieldDimension,
                Height = FieldDimension
            };
            Canvas.SetLeft(rect, x * FieldDimension);
            Canvas.SetTop(rect, y * FieldDimension);
            can.Children.Add(rect);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NextClick();
        }

        private void NextClick()
        {
            can.Children.Clear();
            DrawBoard();
            index.Content = CurrentIndex;
            ChessBoard b = Boards.ElementAt(CurrentIndex);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (b.Board[x, y].Piece.Type == PieceType.Queen)
                        PlaceQueen(x, y);
                }
            }
            if (CurrentIndex == Boards.Count - 1)
                CurrentIndex = 0;
            else
                CurrentIndex++;
        }
    }
}
