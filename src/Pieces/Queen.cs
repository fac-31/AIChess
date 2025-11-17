namespace AvaloniaChessApp.Pieces;

class Queen : Base
{
    public Queen(Position position, Team team) : base(position, team)
    {
        Name = "Queen";
        Icon = "♛";
    }
}