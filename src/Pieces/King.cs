namespace AvaloniaChessApp.Pieces;

class King : Base
{
    public King(Position position, Team team) : base(position, team)
    {
        Name = "King";
        Icon = "♚";
    }
}