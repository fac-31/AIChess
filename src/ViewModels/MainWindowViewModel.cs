using System.Collections.ObjectModel;
using ReactiveUI;
using AvaloniaChessApp.Pieces;

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

    private Base? GetInitialPiece(int row, int col)
    {
        // Setup chess pieces in their starting positions
        return (row, col) switch
        {
            // Black pieces (top)
            (0, 0) or (0, 7) => new Rook(Team.Black),    // Black Rook
            (0, 1) or (0, 6) => new Knight(Team.Black),  // Black Knight
            (0, 2) or (0, 5) => new Bishop(Team.Black),  // Black Bishop
            (0, 3) => new Queen(Team.Black),             // Black Queen
            (0, 4) => new King(Team.Black),              // Black King
            (1, _) => new Pawn(Team.Black),              // Black Pawn

            (7, 0) or (7, 7) => new Rook(Team.White),    // Rook
            (7, 1) or (7, 6) => new Knight(Team.White),  // Knight
            (7, 2) or (7, 5) => new Bishop(Team.White),  // Bishop
            (7, 3) => new Queen(Team.White),             // Queen
            (7, 4) => new King(Team.White),              // King
            (6, _) => new Pawn(Team.White),              // Pawn

            _ => null                       // Empty square
        };
    }
}

public class ChessSquareViewModel
{
    public int Row { get; set; }
    public int Column { get; set; }
    public string SquareColor { get; set; } = "White";
    public Base? Piece { get; set; } = null;
}
