namespace AvaloniaChessApp.Pieces;

using System.Collections.Generic;
using AvaloniaChessApp.Views;

class Pawn : Base
{
    private bool hasMoved = false;
    private bool enPassantEligible = false;

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
        if (CanMoveTo(newRow, Position.Column) != CanMove.No)
        {
            possibleMoves.Add(new Position(newRow, Position.Column));
        }

        for (int colOffset = -1; colOffset <= 1; colOffset += 2)
        {
            int newCol = Position.Column + colOffset;
            CanMove moveStatus = CanMoveTo(newRow, newCol);
            if (moveStatus == CanMove.Capture)
            {
                possibleMoves.Add(new Position(newRow, newCol));
            }
            // En passant
            if (moveStatus == CanMove.Yes)
            {
                Position adjacentPos = new Position(Position.Row, newCol);
                Base adjacentPiece = MainWindow.viewModel.GetPieceAtPosition(adjacentPos);
                if (adjacentPiece is Pawn adjacentPawn &&
                    adjacentPawn.Team != this.Team &&
                    adjacentPawn.enPassantEligible)
                {
                    possibleMoves.Add(new Position(newRow, newCol));
                }
            }
        }

        if (!hasMoved && CanMoveTo(newRow, Position.Column) == CanMove.Yes)
        {
            int twoStepRow = Position.Row + 2 * direction;
            if (CanMoveTo(twoStepRow, Position.Column) == CanMove.Yes)
            {
                possibleMoves.Add(new Position(twoStepRow, Position.Column));
            }
        }

        return possibleMoves;
    }

    public override void OnMove(Position oldPosition, Position newPosition)
    {
        hasMoved = true;

        // Check for en passant eligibility
        if (System.Math.Abs(newPosition.Row - oldPosition.Row) == 2)
        {
            enPassantEligible = true;
        }
        else
        {
            enPassantEligible = false;
        }
    }
}