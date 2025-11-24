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

        CollectMoves(1, 0, possibleMoves);   // Up
        CollectMoves(-1, 0, possibleMoves);  // Down
        CollectMoves(0, 1, possibleMoves);   // Right
        CollectMoves(0, -1, possibleMoves);  // Left
        CollectMoves(1, 1, possibleMoves);   // Top-right
        CollectMoves(1, -1, possibleMoves);  // Top-left
        CollectMoves(-1, 1, possibleMoves);  // Bottom-right
        CollectMoves(-1, -1, possibleMoves); // Bottom-left

        return possibleMoves;
    }
}