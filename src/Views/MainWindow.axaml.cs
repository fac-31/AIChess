using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using AvaloniaChessApp.ViewModels;
using AvaloniaChessApp.Pieces;
using AvaloniaChessApp;
using System;
using System.Collections.Generic;

namespace AvaloniaChessApp.Views;

public partial class MainWindow : Window
{
    private const int SquareSize = 50;
    private List<Rectangle> squares = new List<Rectangle>();
    private Rectangle? selectedSquare;

    public MainWindow()
    {
        InitializeComponent();
        var viewModel = new MainWindowViewModel();
        DataContext = viewModel;
        DrawChessBoard(viewModel);
    }

    private Position? GetPositionFromRectangle(Rectangle rect)
    {
        if (rect.Tag is Position pos)
        {
            return pos;
        }
        else if (rect.Tag is Base piece)
        {
            return piece.Position;
        }
        return null;
    }

    private Rectangle GetRectangleAtPosition(Position position)
    {
        foreach (var rect in squares)
        {
            Position pos = GetPositionFromRectangle(rect);
            if (pos != null && pos.Row == position.Row && pos.Column == position.Column)
                return rect;
        }
        return null!;
    }

    private void DrawChessBoard(MainWindowViewModel viewModel)
    {
        var canvas = this.FindControl<Canvas>("ChessBoard");
        if (canvas == null) return;

        for (int row = 0; row < App.BoardSize; row++)
        {
            for (int col = 0; col < App.BoardSize; col++)
            {
                Base piece = viewModel.GetPieceAtPosition(row, col);

                // Draw square background
                var rect = new Rectangle
                {
                    Fill = null,
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 1,
                    Width = SquareSize,
                    Height = SquareSize,
                    Tag = piece != null ? piece : new Position(row, col)  // Store position for click handling
                };

                squares.Add(rect);

                // Add event handlers for hover and click
                rect.PointerEntered += (s, e) => OnSquareHoverEnter(rect);
                rect.PointerExited += (s, e) => OnSquareHoverExit(rect);
                rect.PointerPressed += (s, e) => OnSquareClick(rect);

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
                        Foreground = new SolidColorBrush(Colors.Black),
                        IsHitTestVisible = false // Prevent hover/click
                    };

                    Canvas.SetLeft(text, col * SquareSize + 5);
                    Canvas.SetTop(text, row * SquareSize + 5);
                    canvas.Children.Add(text);
                }
            }
        }

        ResetSquareColors();
    }

    private void ResetSquareColors()
    {
        foreach (var rect in squares)
        {
            var pos = GetPositionFromRectangle(rect);
            bool isLightSquare = (pos.Row + pos.Column) % 2 == 0;
            rect.Fill = isLightSquare
                ? new SolidColorBrush(Colors.LightGray)
                : new SolidColorBrush(Colors.DarkGray);
        }
    }

    private void OnSquareHoverEnter(Rectangle rect)
    {
        // Only show hover effect if not selected
        if (rect != selectedSquare)
        {
            rect.Fill = new SolidColorBrush(Colors.Yellow);
        }
    }

    private void OnSquareHoverExit(Rectangle rect)
    {
        // Restore original color on hover exit
        ResetSquareColors();
    }

    private void OnSquareClick(Rectangle rect)
    {
        // Deselect previous square
        ResetSquareColors();

        // Select new square
        selectedSquare = rect;
        rect.Fill = new SolidColorBrush(Colors.Green);

        Position position = null;
        if (rect.Tag is Position pos)
        {
            position = pos;
        }
        else if (rect.Tag is Base piece)
        {
            position = piece.Position;
            List<Position> moves = piece.GetPossibleMoves();
            foreach (var move in moves)
            {
                Rectangle moveRect = GetRectangleAtPosition(move);
                if (moveRect != null)
                {
                    moveRect.Fill = new SolidColorBrush(Colors.LightGreen);
                }
            }
        }
    }
}
