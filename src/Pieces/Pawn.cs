namespace AvaloniaChessApp.Pieces;

class Pawn : Base
{
    public Pawn(Position position, Team team) : base(position, team)
    {
        Name = "Pawn";
        Icon = "♟";
    }
}