namespace AvaloniaChessApp.Pieces;

class Pawn : Base
{
    public Pawn(Team team) : base(team)
    {
        Name = "Pawn";
        Icon = "♟";
    }
}