namespace AvaloniaChessApp.Pieces;

public enum Team
{
    White,
    Black
}

public abstract class Base(Team team)
{
    public string? Name { get; protected set; }
    public string? Icon { get; protected set; }
    public Team Team { get; } = team;

    public override string ToString()
    {
        return $"{Team} {Name}";
    }
}