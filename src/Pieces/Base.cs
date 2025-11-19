using System.Collections.Generic;
using Avalonia.Controls;

namespace AvaloniaChessApp.Pieces;

public enum Team
{
    White,
    Black
}

public class Position(int row, int column)
{
    public int Row { get; set; } = row;
    public int Column { get; set; } = column;
}

public abstract class Base(Position position, Team team)
{
    public string? Name { get; protected set; }
    public string? Icon { get; protected set; }
    public Position Position { get; set; } = position;
    public Team Team { get; } = team;

    public TextBlock TextBlock { get; set; } = null;

    public abstract List<Position> GetPossibleMoves();

    public override string ToString()
    {
        return $"{Team} {Name}";
    }
}