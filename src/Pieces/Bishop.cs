namespace AvaloniaChessApp.Pieces;

class Bishop : Base
{
    public Bishop(Position position, Team team) : base(position, team)
    {
        Name = "Bishop";
        Icon = "♝";
    }
}