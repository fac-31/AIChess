using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using AvaloniaChessApp.ViewModels;

namespace AvaloniaChessApp.Views;

public partial class MainWindow : Window
{
    private const int SquareSize = 50;
    private const int BoardSize = 8;

    public MainWindow()
    {
        InitializeComponent();
        var viewModel = new MainWindowViewModel();
        DataContext = viewModel;
        DrawChessBoard(viewModel);
    }

    private void DrawChessBoard(MainWindowViewModel viewModel)
    {
        var canvas = this.FindControl<Canvas>("ChessBoard");
        if (canvas == null) return;

        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                var square = viewModel.Squares[row * BoardSize + col];

                // Draw square background
                var brush = square.SquareColor == "LightGray"
                    ? new SolidColorBrush(Colors.LightGray)
                    : new SolidColorBrush(Colors.DarkGray);

                var rect = new Rectangle
                {
                    Fill = brush,
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 1,
                    Width = SquareSize,
                    Height = SquareSize
                };

                Canvas.SetLeft(rect, col * SquareSize);
                Canvas.SetTop(rect, row * SquareSize);
                canvas.Children.Add(rect);

                // Draw piece if exists
                if (square.Piece != null)
                {
                    var text = new TextBlock
                    {
                        Text = square.Piece.Icon,
                        FontSize = 40,
                        TextAlignment = Avalonia.Media.TextAlignment.Center,
                        Foreground = new SolidColorBrush(Colors.Black)
                    };

                    Canvas.SetLeft(text, col * SquareSize + 5);
                    Canvas.SetTop(text, row * SquareSize + 5);
                    canvas.Children.Add(text);
                }
            }
        }
    }
}
