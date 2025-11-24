namespace AvaloniaChessApp.Pieces;

using System.Collections.Generic;
using AvaloniaChessApp.Views;

class Bishop : Base
{
    public Bishop(Position position, Team team) : base(position, team)
    {
        Name = "Bishop";
        IconBlack = "♝";
        IconWhite = "♗";
    }

    public override List<Position> GetPossibleMoves()
    {
        List<Position> possibleMoves = new List<Position>();

        // Diagonal moves
        CollectMoves(1, 1, possibleMoves);   // Top-right
        CollectMoves(1, -1, possibleMoves);  // Top-left
        CollectMoves(-1, 1, possibleMoves);  // Bottom-right
        CollectMoves(-1, -1, possibleMoves); // Bottom-left

        return possibleMoves;
    }
}