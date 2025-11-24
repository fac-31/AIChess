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

        CollectMoves(1, 0, possibleMoves);   // Up
        CollectMoves(-1, 0, possibleMoves);  // Down
        CollectMoves(0, 1, possibleMoves);   // Right
        CollectMoves(0, -1, possibleMoves);  // Left

        return possibleMoves;
    }
}