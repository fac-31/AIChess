namespace AvaloniaChessApp.Pieces;

using System.Collections.Generic;

public class King : Base
{
    public King(Position position, Team team) : base(position, team)
    {
        Name = "King";
        IconBlack = "♚";
        IconWhite = "♔";
    }

    public override List<Position> GetPossibleMoves()
    {
        List<Position> possibleMoves = new List<Position>();

        // One square in any direction
        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (int colOffset = -1; colOffset <= 1; colOffset++)
            {
                if (rowOffset == 0 && colOffset == 0)
                    continue; // Skip the current position

                int newRow = Position.Row + rowOffset;
                int newCol = Position.Column + colOffset;

                if (newRow >= 0 && newRow < App.BoardSize && newCol >= 0 && newCol < App.BoardSize)
                {
                    possibleMoves.Add(new Position(newRow, newCol));
                }
            }
        }

        return possibleMoves;
    }
}