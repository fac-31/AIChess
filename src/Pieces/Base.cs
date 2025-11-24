using System.Collections.Generic;
using Avalonia.Controls;
using AvaloniaChessApp.Views;

namespace AvaloniaChessApp.Pieces;

public enum Team
{
    White,
    Black
}

public enum CanMove
{
    Yes,
    No,
    Capture
}

public class Position(int row, int column)
{
    public int Row { get; set; } = row;
    public int Column { get; set; } = column;
}

public abstract class Base(Position position, Team team)
{
    public string? Name { get; protected set; }
    public string? IconBlack { get; protected set; }
    public string? IconWhite { get; protected set; }
    public Position Position { get; set; } = position;
    public Team Team { get; } = team;

    public TextBlock TextBlock { get; set; } = null;

    public abstract List<Position> GetPossibleMoves();

    public void CollectMoves(int rowDirection, int colDirection, List<Position> possibleMoves)
    {
        for (int i = 1; i < App.BoardSize; i++)
        {
            int newRow = Position.Row + i * rowDirection;
            int newCol = Position.Column + i * colDirection;

            CanMove moveStatus = CanMoveTo(newRow, newCol);
            if (moveStatus != CanMove.No)
                possibleMoves.Add(new Position(newRow, newCol));

            if (moveStatus != CanMove.Yes)
                return;
        }
    }

    public CanMove CanMoveTo(int row, int column)
    {
        return CanMoveTo(new Position(row, column));
    }

    public CanMove CanMoveTo(Position pos)
    {
        if (pos.Row < 0 || pos.Row >= App.BoardSize || pos.Column < 0 || pos.Column >= App.BoardSize)
            return CanMove.No;

        if (MainWindow.viewModel.GetPieceAtPosition(pos) == null)
            return CanMove.Yes;
        else if (MainWindow.viewModel.GetPieceAtPosition(pos).Team != Team)
            return CanMove.Capture;
        else
            return CanMove.No;
    }

    public virtual void OnMove(Position oldPosition, Position newPosition) { }

    public string Icon()
    {
        return Team == Team.White ? IconWhite! : IconBlack!;
    }

    public override string ToString()
    {
        return $"{Team} {Name}";
    }
}