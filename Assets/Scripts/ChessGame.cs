using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public static class ChessGame
{
	public static HashSet<PieceID> livingPieces = new HashSet<PieceID>();
	public static PieceID[,] board = defaultBoard.Clone() as PieceID[,];

	private static PieceID[,] defaultBoard = new PieceID[8, 8]
	{
		{PieceID.rook_right_white, PieceID.knight_right_white, PieceID.bishop_right_white, PieceID.king_white, PieceID.queen_white, PieceID.bishop_left_white, PieceID.knight_left_white, PieceID.rook_left_white},
		{PieceID.pawn_8_white, PieceID.pawn_7_white, PieceID.pawn_6_white, PieceID.pawn_5_white, PieceID.pawn_4_white, PieceID.pawn_3_white, PieceID.pawn_2_white, PieceID.pawn_1_white},
		{PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE},
		{PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE},
		{PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE},
		{PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE, PieceID.NONE},
		{PieceID.pawn_1_black, PieceID.pawn_2_black, PieceID.pawn_3_black, PieceID.pawn_4_black, PieceID.pawn_5_black, PieceID.pawn_6_black, PieceID.pawn_7_black, PieceID.pawn_8_black},
		{PieceID.rook_left_black, PieceID.knight_left_black, PieceID.bishop_left_black, PieceID.king_black, PieceID.queen_black, PieceID.bishop_right_black, PieceID.knight_right_black, PieceID.rook_right_black}
	};
	private static Regex lettersRegex = new Regex("^[a-h]$");
	private static List<string> columns = new List<string> {"a", "b", "c", "d", "e", "f", "g", "h"};

	static ChessGame()
	{
		foreach (PieceID pieceId in Enum.GetValues(typeof(PieceID)))
		{
			if (pieceId != PieceID.NONE)
			{
				livingPieces.Add(pieceId);
			}
		}
	}


    public static PieceID GetOccupied(string squareId)
	{
		int col;
		if (lettersRegex.IsMatch(squareId[0].ToString())) {
			col = squareId[0] - 'a';
		} else {
			col = squareId[0] - '1';
		}
		int row = squareId[2] - '1';
		return board[row, col];
	}

	public static (Piece, Player) GetOccupiedSplit(string squareId)
	{
		PieceID pieceId = GetOccupied(squareId);
		return GetPieceAndPlayer(pieceId);
	}

	public static bool IsAlive(PieceID pieceId)
	{
		return _isAlive(pieceId);
	}

	public static bool IsAlive(Piece pieceId, Player teamColor)
	{
		return _isAlive(GetPieceID(pieceId, teamColor));
	}

	private static bool _isAlive(PieceID pieceId)
	{
		if (pieceId == PieceID.NONE)
		{
			Debug.LogError("Invalid pieceId: " + pieceId.ToString());
			return false;
		}
		return livingPieces.Contains(pieceId);
	}

	public static Player GetColor(PieceID pieceId)
	{
		string[] split = pieceId.ToString().Split('_');
		Player player;
		bool success = Enum.TryParse(split[1], out player);
		if (!success)
		{
			Debug.LogError("Invalid 'player' value': " + split[1]);
			player = Player.NONE;
		}
		return player;
	}

	public static List<string> GetPossibleMoves(PieceID pieceId)
	{
		var (piece, player) = GetPieceAndPlayer(pieceId);
		return _getPossibleMoves(piece, player);
	}

	public static List<string> GetPossibleMoves(Piece pieceId, Player teamColor)
	{
		return _getPossibleMoves(pieceId, teamColor);
	}

	private static List<string> _getPossibleMoves(Piece pieceId, Player teamColor)
	{


		return new List<string>();
	}

	public static string GetPieceSquare(PieceID pieceId, bool onDefaultBoard = false)
	{
		return _getPieceSquare(pieceId, onDefaultBoard);
	}

	public static string GetPieceSquare(Piece pieceId, Player teamColor, bool onDefaultBoard = false)
	{
		return _getPieceSquare(GetPieceID(pieceId, teamColor), onDefaultBoard);
	}

	private static string _getPieceSquare(PieceID pieceId, bool onDefaultBoard)
	{
		PieceID[,] lookAt = onDefaultBoard ? defaultBoard : board;
		for (int row = 0; row < 8; row++)
		{
			for (int col = 0; col < 8; col++)
			{
				if (lookAt[row, col] == pieceId)
				{
					return columns[8 - col] + "-" + row;
				}
			}
		}
		Debug.LogError("Piece not found: " + pieceId.ToString());
		return "";
	}

	public static bool IsInDefaultPosition(PieceID pieceId)
	{
		return _isInDefaultPosition(pieceId);
	}

	public static bool IsInDefaultPosition(Piece pieceId, Player teamColor)
	{
		return _isInDefaultPosition(GetPieceID(pieceId, teamColor));
	}

	private static bool _isInDefaultPosition(PieceID pieceId)
	{
		for (int row = 0; row < 8; row++)
		{
			for (int col = 0; col < 8; col++)
			{
				if (defaultBoard[row, col] == pieceId)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static string GetColumn(PieceID pieceId)
	{
		return _getColumn(pieceId);
	}

	public static string GetColumn(Piece pieceId, Player teamColor)
	{
		return _getColumn(GetPieceID(pieceId, teamColor));
	}

	private static string _getColumn(PieceID pieceId)
	{
		string square = GetPieceSquare(pieceId);
		return square[0].ToString();
	}

	public static int GetRow(PieceID pieceId)
	{
		return _getRow(pieceId);
	}

	public static int GetRow(Piece pieceId, Player teamColor)
	{
		return _getRow(GetPieceID(pieceId, teamColor));
	}

	private static int _getRow(PieceID pieceId)
	{
		string square = GetPieceSquare(pieceId);
		return int.Parse(square[2].ToString());
	}

	public static string GetRelativeColumn(string column, int offset)
	{
		int col = columns.IndexOf(column);
		col += offset;
		if (col < 0 || col > 7)
		{
			Debug.LogError("Invalid column: " + column + " offset: " + offset);
			return "";
		}
		return columns[col];
	}

	public static PieceID GetPieceID(Piece piece, Player teamColor)
	{
		string id = piece.ToString() + "_"+ teamColor.ToString();
		PieceID value;
		bool success = Enum.TryParse(id, out value);
		if (!success)
		{
			Debug.LogError("Invalid parameters: " + id);
			value = PieceID.NONE;
		}
		return value;
	}

	public static (Piece, Player) GetPieceAndPlayer(PieceID pieceId)
	{
		string[] split = pieceId.ToString().Split('_');
		Piece piece;
		bool success = Enum.TryParse(split[0], out piece);
		if (!success)
		{
			Debug.LogError("Invalid 'piece' value: " + split[0]);
			piece = Piece.NONE;
		}
		Player player;
		success = Enum.TryParse(split[1], out player);
		if (!success)
		{
			Debug.LogError("Invalid 'player' value': " + split[1]);
			player = Player.NONE;
		}
		return (piece, player);
	}


}
