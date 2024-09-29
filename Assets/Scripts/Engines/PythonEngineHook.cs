using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Python.Runtime;
using UnityEditor.Scripting.Python;

public class PythonEngineHook
{
	string PythonScript = "PythonEngineTemplate";

    public void Init()
	{
		PythonRunner.EnsureInitialized();
		PythonEngine.Initialize();
	}

	public void GetNextMove()
	{
		using (Py.GIL())
		{
			dynamic sys = Py.Import("sys");
			sys.path.append(Application.dataPath + "/Scripts");
			dynamic PythonChessEngine = Py.Import(PythonScript);

			PyObject NextMoveMethod = PythonChessEngine.GetAttr("GetNextMove");

			PyList gameBoardStrings = new PyList();
			string[] gameBoard = ChessGame.GetBoardAsStrings();
			foreach (string square in gameBoard)
			{
				gameBoardStrings.Append(new PyString(square));
			}

			PyObject NextMove = NextMoveMethod.Invoke(gameBoardStrings);

			Debug.Log(NextMove.ToString());
		}
	}

	public void Cleanup()
	{
		PythonEngine.Shutdown();
	}
}
