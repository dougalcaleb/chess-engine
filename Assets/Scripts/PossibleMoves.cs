using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PossibleMoves
{
	private static List<string> columns = new List<string> {"a", "b", "c", "d", "e", "f", "g", "h"};

	public static List<string> Pawn(Piece pieceId, Player color)
	{
		List<string> validMoves = new List<string>();

		string col = ChessGame.GetColumn(pieceId, color);
		int row = ChessGame.GetRow(pieceId, color);

		if (ChessGame.IsInDefaultPosition(pieceId, color)) {

			if (color == Player.White) {
				if (ChessGame.GetOccupied(col + "-" + (row + 1)) == PieceID.NONE) {
					validMoves.Add(col + "-" + (row + 1));
					if (ChessGame.GetOccupied(col + "-" + (row + 2)) == PieceID.NONE) {
						validMoves.Add(col + "-" + (row + 2));
					}
				}
			} else {
				if (ChessGame.GetOccupied(col + "-" + (row - 1)) == PieceID.NONE) {
					validMoves.Add(col + "-" + (row - 1));
					if (ChessGame.GetOccupied(col + "-" + (row - 2)) == PieceID.NONE) {
						validMoves.Add(col + "-" + (row - 2));
					}
				}
			}
		} else {
			if (color == Player.White && ChessGame.GetOccupied(col + "-" + (row + 1)) == PieceID.NONE) {
				validMoves.Add(col + "-" + (row + 1));
			} else if (color == Player.Black && ChessGame.GetOccupied(col + "-" + (row - 1)) == PieceID.NONE) {
				validMoves.Add(col + "-" + (row - 1));
			}
		}
		
		return validMoves;
	}
}
