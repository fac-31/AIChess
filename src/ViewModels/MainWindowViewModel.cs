using System.Collections.ObjectModel;
using ReactiveUI;
using AvaloniaChessApp.Pieces;
using AvaloniaChessApp;

namespace AvaloniaChessApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ObservableCollection<Base> _pieces;
    private string _status;
    private Team _currentTurn;

    public MainWindowViewModel()
    {
        _pieces = new ObservableCollection<Base>();
        _status = "White's Turn";
        _currentTurn = Team.White;

        InitializeBoard();
    }

    public ObservableCollection<Base> Pieces
    {
        get => _pieces;
        set => this.RaiseAndSetIfChanged(ref _pieces, value);
    }

    public Base GetPieceAtPosition(int row, int column)
    {
        foreach (Base piece in _pieces)
        {
            if (piece.Position.Row == row && piece.Position.Column == column)
                return piece;
        }
        return null;
    }

    public string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }

    public Team CurrentTurn
    {
        get => _currentTurn;
        set => this.RaiseAndSetIfChanged(ref _currentTurn, value);
    }

    private void InitializeBoard()
    {
        InitializeTeam(Team.Black, pawnRow: 1, majorRow: 0);
        InitializeTeam(Team.White, pawnRow: 6, majorRow: 7);
    }

    private void InitializeTeam(Team team, int pawnRow, int majorRow)
    {
        _pieces.Add(new Rook(new Position(majorRow, 0), team));
        _pieces.Add(new Knight(new Position(majorRow, 1), team));
        _pieces.Add(new Bishop(new Position(majorRow, 2), team));
        _pieces.Add(new Queen(new Position(majorRow, 3), team));
        _pieces.Add(new King(new Position(majorRow, 4), team));
        _pieces.Add(new Bishop(new Position(majorRow, 5), team));
        _pieces.Add(new Knight(new Position(majorRow, 6), team));
        _pieces.Add(new Rook(new Position(majorRow, 7), team));

        for (int col = 0; col < App.BoardSize; col++)
            _pieces.Add(new Pawn(new Position(pawnRow, col), team));
    }

    public void RemovePiece(Base piece)
    {
        _pieces.Remove(piece);
    }
}
