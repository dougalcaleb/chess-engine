using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Note: "Piece", "PieceID", and "Player" refer to enums in Enums.cs

public class EngineTemplate
{
	public Player team;

    public EngineTemplate(Player teamColor)
	{
		team = teamColor; // teamColor will either be Player.White or Player.Black
	}

	// Called by the main engine for each turn
	public (Piece, string) GetNextMove()
	{
		// Return value is a tuple of the Piece enum and a string
		// Piece is the piece ID (color not included) (e.g. Piece.king, Piece.bishop_left, Piece.rook_right)
		// Second string is the grid square to move to, dash-delimited. Col-row. Either letter-digit or digit-digit. Origin at white side, left rook. (e.g. "a-3", "4-5")

		return (Piece.NONE, "");
	}

	// Can include any methods necessary for your implementation in the class body.

	// Can query for the following game data from the ChessGame class:

	// ChessGame.GetOccupied(string squareId)							Returns the PieceID of any occupying piece, if any, otherwise PieceID.NONE. Includes color.
	// ChessGame.GetOccupiedSplit(string squareId)						Returns a tuple (Piece, Player) containing the piece and the team it belongs to. Returns (Piece.NONE, Player.NONE) if unoccupied.

	// ChessGame.IsAlive(PieceID pieceID)								Returns a boolean indicating whether the piece is still in the game.
	// ChessGame.IsAlive(Piece pieceID, Player teamColor)				Alternative parameters. Returns a boolean indicating whether the piece is still in the game.

	// ChessGame.GetPossibleMoves(PieceID pieceID)						Returns a List<string> of squares that the given piece can move to.
	// ChessGame.GetPossibleMoves(Piece pieceID, Player teamColor)		Alternative parameters. Returns a List<string> of squares that the given piece can move to.

	// ChessGame.GetPieceSquare(PieceID pieceID)						Returns the square ID where the piece is as letter-digit (e.g. "b-6")
	// ChessGame.GetPieceSquare(Piece pieceID, Player teamColor)		Alternative parameters. Returns the square ID where the piece is as letter-digit (e.g. "b-6")

	// ChessGame.GetColor(PieceID pieceID)								Returns the Player value of the given piece. (e.g. Player.White, Player.Black)

	// ChessGame.GetPieceID(Piece pieceID, Player teamColor)			Returns the PieceID of the given piece. (e.g. PieceID.king_white, PieceID.pawn_3_black)
	// ChessGame.GetPieceAndPlayer(PieceID pieceID)						Returns a tuple (Piece, Player) containing the piece and the team it belongs to. (e.g. (Piece.king, Player.White))

	// ChessGame.IsInDefaultPosition(PieceID pieceID)					Returns a boolean indicating whether the piece is in its default position.
	// ChessGame.IsInDefaultPosition(Piece pieceID, Player teamColor)	Alternative parameters. Returns a boolean indicating whether the piece is in its default position.

	// ChessGame.GetColumn(PieceID pieceID)								Returns the column letter (string) of the piece's square.
	// ChessGame.GetColumn(Piece pieceID, Player teamColor)				Alternative parameters. Returns the column letter (string) of the piece's square.

	// ChessGame.GetRow(PieceID pieceID)								Returns the row number (int) of the piece's square.
	// ChessGame.GetRow(Piece pieceID, Player teamColor)				Alternative parameters. Returns the row number (int) of the piece's square.

	// ChessGame.GetRelativeColumn(string column, int offset)			Returns the column letter offset by the given amount. (e.g. "a", 2 => "c")
}	
