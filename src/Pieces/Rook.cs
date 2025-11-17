namespace AvaloniaChessApp.Pieces;

class Rook : Base
{
    public Rook(Position position, Team team) : base(position, team)
    {
        Name = "Rook";
        Icon = "♜";
    }
}