namespace AvaloniaChessApp.Pieces;

using System.Collections.Generic;
using AvaloniaChessApp;

class Knight : Base
{
    public Knight(Position position, Team team) : base(position, team)
    {
        Name = "Knight";
        IconBlack = "♞";
        IconWhite = "♘";
    }

    public override List<Position> GetPossibleMoves()
    {
        List<Position> possibleMoves = new List<Position>();
        int[,] moveOffsets = new int[,]
        {
            { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
            { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
        };

        for (int i = 0; i < moveOffsets.GetLength(0); i++)
        {
            int newRow = Position.Row + moveOffsets[i, 0];
            int newCol = Position.Column + moveOffsets[i, 1];

            if (newRow >= 0 && newRow < App.BoardSize && newCol >= 0 && newCol < App.BoardSize)
            {
                possibleMoves.Add(new Position(newRow, newCol));
            }
        }

        return possibleMoves;
    }
}