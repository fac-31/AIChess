using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using AvaloniaChessApp.ViewModels;
using AvaloniaChessApp.Pieces;

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
                Base piece = viewModel.GetPieceAtPosition(row, col);

                // Draw square background
                var brush = (row + col) % 2 == 0
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
                if (piece != null)
                {
                    var text = new TextBlock
                    {
                        Text = piece.Icon,
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
