using UnityEngine;
using System.Collections;

public class retainInfoForNewScene : MonoBehaviour
{
	//i have no idea what i am doing with this... but it works so whatever
		public static string selectedBuilding2;

		void Awake ()
		{
				// Do not destroy this game object:
				DontDestroyOnLoad (this);
		}
}
