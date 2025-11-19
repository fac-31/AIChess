namespace AvaloniaChessApp.Pieces;

using System.Collections.Generic;
using AvaloniaChessApp;

class Rook : Base
{
    public Rook(Position position, Team team) : base(position, team)
    {
        Name = "Rook";
        IconBlack = "♜";
        IconWhite = "♖";
    }

    public override List<Position> GetPossibleMoves()
    {
        List<Position> possibleMoves = new List<Position>();

        // Horizontal and vertical moves
        for (int i = 0; i < App.BoardSize; i++)
        {
            if (i != Position.Column)
                possibleMoves.Add(new Position(Position.Row, i)); // Horizontal moves
            if (i != Position.Row)
                possibleMoves.Add(new Position(i, Position.Column)); // Vertical moves
        }

        return possibleMoves;
    }
}