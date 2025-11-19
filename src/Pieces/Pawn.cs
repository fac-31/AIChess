namespace AvaloniaChessApp.Pieces;

using System.Collections.Generic;

class Pawn : Base
{
    public Pawn(Position position, Team team) : base(position, team)
    {
        Name = "Pawn";
        IconBlack = "♟";
        IconWhite = "♙";
    }

    public override List<Position> GetPossibleMoves()
    {
        List<Position> possibleMoves = new List<Position>();
        int direction = Team == Team.White ? -1 : 1;

        // Move forward
        int newRow = Position.Row + direction;
        if (newRow >= 0 && newRow < App.BoardSize)
        {
            possibleMoves.Add(new Position(newRow, Position.Column));
        }

        return possibleMoves;
    }
}