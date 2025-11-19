namespace AvaloniaChessApp.Pieces;

using System.Collections.Generic;
using AvaloniaChessApp;

class Queen : Base
{
    public Queen(Position position, Team team) : base(position, team)
    {
        Name = "Queen";
        IconBlack = "♛";
        IconWhite = "♕";
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

        // Diagonal moves
        for (int i = -App.BoardSize; i < App.BoardSize; i++)
        {
            if (i != 0)
            {
                int newRow1 = Position.Row + i;
                int newCol1 = Position.Column + i;
                if (newRow1 >= 0 && newRow1 < App.BoardSize && newCol1 >= 0 && newCol1 < App.BoardSize)
                    possibleMoves.Add(new Position(newRow1, newCol1));

                int newRow2 = Position.Row + i;
                int newCol2 = Position.Column - i;
                if (newRow2 >= 0 && newRow2 < App.BoardSize && newCol2 >= 0 && newCol2 < App.BoardSize)
                    possibleMoves.Add(new Position(newRow2, newCol2));
            }
        }

        return possibleMoves;
    }
}