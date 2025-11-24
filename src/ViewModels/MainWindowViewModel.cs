using System.Collections.ObjectModel;
using ReactiveUI;
using AvaloniaChessApp.Pieces;
using AvaloniaChessApp;
using AvaloniaChessApp.Views;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Agent;
using System.Threading.Tasks;
using System;
using DotNetEnv;

namespace AvaloniaChessApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private MainWindow _window;
    private ChessAgent _agent;
    private ObservableCollection<Base> _pieces;
    private string _status;
    private Team _currentTurn;

    public MainWindowViewModel(MainWindow window)
    {
        _window = window;
        _pieces = new ObservableCollection<Base>();
        _status = "White's Turn";
        _currentTurn = Team.White;

        _agent = new ChessAgent(this, Env.GetString("OPENAI_KEY"), Env.GetString("OPENAI_MODEL"));

        InitializeBoard();
    }

    public ObservableCollection<Base> Pieces
    {
        get => _pieces;
        set => this.RaiseAndSetIfChanged(ref _pieces, value);
    }

    public Base GetPieceAtPosition(Position pos)
    {
        return GetPieceAtPosition(pos.Row, pos.Column);
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

    public void MovePiece(Base piece, Position newPosition)
    {
        // Delete whatever piece is at the clicked position
        Base targetPiece = GetPieceAtPosition(newPosition);
        if (targetPiece != null)
        {
            var canvas = _window.FindControl<Canvas>("ChessBoard");
            canvas.Children.Remove(targetPiece.TextBlock);
            RemovePiece(targetPiece);
        }

        Rectangle oldRect = _window.GetRectangleAtPosition(piece.Position);
        Rectangle newRect = _window.GetRectangleAtPosition(newPosition);

        // Move piece
        oldRect.Tag = piece.Position;
        newRect.Tag = piece;
        piece.Position = newPosition;

        // Update turn
        CurrentTurn = CurrentTurn == Team.White ? Team.Black : Team.White;
        Status = CurrentTurn == Team.White ? "White's Turn" : "Black's Turn";

        _window.ClearSelectedSquare();
        _window.DrawIcon(piece);
        _window.ResetSquareColors();

        if (CurrentTurn == Team.Black)
        {
            Task.Run(async () =>
            {
                try
                {
                    ResponseMove move = await _agent.DoNextMoveAsync();
                    if (move != null)
                    {
                        Base agentPiece = GetPieceAtPosition(move.OldPosition);
                        if (agentPiece != null)
                        {
                            // Move the piece on the UI thread
                            await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                            {
                                MovePiece(agentPiece, move.NewPosition);
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    Console.WriteLine("Error during agent move:");
                    Console.WriteLine(ex.Message);
                }
            });
        }
    }

    public void RemovePiece(Base piece)
    {
        _pieces.Remove(piece);
    }
}
