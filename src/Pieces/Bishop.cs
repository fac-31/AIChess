namespace AvaloniaChessApp.Pieces;

using System.Collections.Generic;

class Bishop : Base
{
    public Bishop(Position position, Team team) : base(position, team)
    {
        Name = "Bishop";
        Icon = "♝";
    }

    public override List<Position> GetPossibleMoves()
    {
        List<Position> possibleMoves = new List<Position>();

        // Diagonal moves
        for (int i = 1; i < App.BoardSize; i++)
        {
            // Top-right diagonal
            if (Position.Row + i < App.BoardSize && Position.Column + i < App.BoardSize)
                possibleMoves.Add(new Position(Position.Row + i, Position.Column + i));
            // Top-left diagonal
            if (Position.Row + i < App.BoardSize && Position.Column - i >= 0)
                possibleMoves.Add(new Position(Position.Row + i, Position.Column - i));
            // Bottom-right diagonal
            if (Position.Row - i >= 0 && Position.Column + i < App.BoardSize)
                possibleMoves.Add(new Position(Position.Row - i, Position.Column + i));
            // Bottom-left diagonal
            if (Position.Row - i >= 0 && Position.Column - i >= 0)
                possibleMoves.Add(new Position(Position.Row - i, Position.Column - i));
        }

        return possibleMoves;
    }
}