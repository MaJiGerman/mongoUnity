using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class master : MonoBehaviour
{
	string logSavePath = "Assets/logs.txt";
	private bool restart = true;

	void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
		if(restart)
		{
			StreamWriter writer2 = new StreamWriter(logSavePath, false);
			writer2.WriteLine("LIMPIEZA");
			writer2.Close();
			restart = false;
		}
		StreamWriter writer = new StreamWriter(logSavePath, true);
		writer.WriteLine(logString);
		writer.Close();
    }
}
