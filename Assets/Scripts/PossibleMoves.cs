using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

	public static List<string> Rook(Piece pieceId, Player color)
	{
		List<string> moves = new();
		string square = ChessGame.GetPieceSquare(pieceId, color);

		moves = moves.Concat(StraightLine(square, -1, 0)).ToList();
		moves = moves.Concat(StraightLine(square, 1, 0)).ToList();
		moves = moves.Concat(StraightLine(square, 0, -1)).ToList();
		moves = moves.Concat(StraightLine(square, 0, 1)).ToList();

		return moves;
	}

	public static List<string> Bishop(Piece pieceId, Player color)
	{
		List<string> moves = new();
		string square = ChessGame.GetPieceSquare(pieceId, color);

		moves = moves.Concat(StraightLine(square, -1, -1)).ToList();
		moves = moves.Concat(StraightLine(square, -1, 1)).ToList();
		moves = moves.Concat(StraightLine(square, 1, -1)).ToList();
		moves = moves.Concat(StraightLine(square, 1, 1)).ToList();

		return moves;
	}

	public static List<string> Queen(Piece pieceId, Player color)
	{
		List<string> moves = new();
		string square = ChessGame.GetPieceSquare(pieceId, color);

		moves = moves.Concat(StraightLine(square, -1, 0)).ToList();
		moves = moves.Concat(StraightLine(square, 1, 0)).ToList();
		moves = moves.Concat(StraightLine(square, 0, -1)).ToList();
		moves = moves.Concat(StraightLine(square, 0, 1)).ToList();
		moves = moves.Concat(StraightLine(square, -1, -1)).ToList();
		moves = moves.Concat(StraightLine(square, -1, 1)).ToList();
		moves = moves.Concat(StraightLine(square, 1, -1)).ToList();
		moves = moves.Concat(StraightLine(square, 1, 1)).ToList();

		return moves;
	}

	public static List<string> Knight(Piece pieceId, Player color)
	{
		List<string> moves = new();
		string square = ChessGame.GetPieceSquare(pieceId, color);

		int col = ChessGame.ColumnStringToInt(square.Split("-")[0]);
		int row = int.Parse(square.Split("-")[1]);

		Func<int, int, bool> isValid = (col, row) => col >= 1 && row >= 1 && col <= 8 && row <= 8;

		Func<int, int, string> constructId = (col, row) => ChessGame.ColumnIntToString(col) + "-" + row;

		if (isValid(col + 2, row + 1)) moves.Add(constructId(col + 2, row + 1));
		if (isValid(col + 2, row - 1)) moves.Add(constructId(col + 2, row - 1));
		if (isValid(col - 2, row + 1)) moves.Add(constructId(col - 2, row + 1));
		if (isValid(col - 2, row - 1)) moves.Add(constructId(col - 2, row - 1));

		if (isValid(col + 1, row + 2)) moves.Add(constructId(col + 1, row + 2));
		if (isValid(col + 1, row - 2)) moves.Add(constructId(col + 1, row - 2));
		if (isValid(col - 1, row + 2)) moves.Add(constructId(col - 1, row + 2));
		if (isValid(col - 1, row - 2)) moves.Add(constructId(col - 1, row - 2));

		return moves;
	}

	private static List<string> StraightLine(string square, int xOffset, int yOffset)
	{
		List<string> moves = new();
		int col = ChessGame.ColumnStringToInt(square.Split("-")[0]);
		int row = int.Parse(square.Split("-")[1]);

		col += yOffset;
		row += xOffset;

		if (col < 1 || row < 1 || col > 8 || row > 8)
		{
			return moves;
		}

		string nextSquare = ChessGame.ColumnIntToString(col) + "-" + row;

		while (ChessGame.GetOccupied(nextSquare) == PieceID.NONE)
		{
			moves.Add(nextSquare);

			col += yOffset;
			row += xOffset;

			if (col < 1 || row < 1 || col > 8 || row > 8)
			{
				return moves;
			}

			nextSquare = ChessGame.ColumnIntToString(col) + "-" + row;
		}

		return moves;
	}
}
