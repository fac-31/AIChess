using System.Collections.ObjectModel;
using ReactiveUI;

namespace AvaloniaChessApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ObservableCollection<ChessSquareViewModel> _squares;
    private string _status;

    public MainWindowViewModel()
    {
        _squares = new ObservableCollection<ChessSquareViewModel>();
        _status = "Game Ready";

        InitializeBoard();
    }

    public ObservableCollection<ChessSquareViewModel> Squares
    {
        get => _squares;
        set => this.RaiseAndSetIfChanged(ref _squares, value);
    }

    public string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }

    private void InitializeBoard()
    {
        // Create 8x8 chess board
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                var isLightSquare = (row + col) % 2 == 0;
                var squareColor = isLightSquare ? "LightGray" : "DarkGray";
                var piece = GetInitialPiece(row, col);

                _squares.Add(new ChessSquareViewModel
                {
                    Row = row,
                    Column = col,
                    SquareColor = squareColor,
                    Piece = piece
                });
            }
        }
    }

    private string GetInitialPiece(int row, int col)
    {
        // Setup chess pieces in their starting positions
        return (row, col) switch
        {
            // Black pieces (top)
            (0, 0) or (0, 7) => "♜",      // Black Rook
            (0, 1) or (0, 6) => "♞",      // Black Knight
            (0, 2) or (0, 5) => "♝",      // Black Bishop
            (0, 3) => "♛",                // Black Queen
            (0, 4) => "♚",                // Black King
            (1, _) => "♟",                // Black Pawn

            // White pieces (bottom)
            (7, 0) or (7, 7) => "♖",      // White Rook
            (7, 1) or (7, 6) => "♘",      // White Knight
            (7, 2) or (7, 5) => "♗",      // White Bishop
            (7, 3) => "♕",                // White Queen
            (7, 4) => "♔",                // White King
            (6, _) => "♙",                // White Pawn

            _ => ""                       // Empty square
        };
    }
}

public class ChessSquareViewModel
{
    public int Row { get; set; }
    public int Column { get; set; }
    public string SquareColor { get; set; } = "White";
    public string Piece { get; set; } = "";
}
