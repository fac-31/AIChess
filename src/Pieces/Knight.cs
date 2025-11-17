namespace AvaloniaChessApp.Pieces;

class Knight : Base
{
    public Knight(Position position, Team team) : base(position, team)
    {
        Name = "Knight";
        Icon = "♞";
    }
}