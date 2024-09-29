using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGameController : MonoBehaviour
{
	PythonEngineHook testEngine;

    // Start is called before the first frame update
    void Start()
    {
        testEngine = new PythonEngineHook();
		testEngine.Init();

		StartCoroutine(GetNextMove());
    }

	IEnumerator GetNextMove()
	{
		yield return new WaitForSeconds(1);
		testEngine.GetNextMove();
		StartCoroutine(GetNextMove());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnApplicationQuit()
	{
		testEngine.Cleanup();
	}
}
