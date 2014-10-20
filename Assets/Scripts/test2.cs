using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour {

	void Start()
	{
		//test
	}
	void OnGUI()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("MSOE");
		foreach(GameObject g in go)
		{
			if(GUILayout.Button(g.name))
			{
				DoSomething(g.name);
			}
		}
	}
	void DoSomething(string buttonName)
	{
		switch(buttonName)
		{
		case "RWJ":
		Debug.Log("The 'Blue' button was pressed");
		break;
				
		case "Kern Center":
		Debug.Log("The 'Red' button was pressed");
		break;
		}
	}
}
