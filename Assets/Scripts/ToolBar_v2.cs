using UnityEngine;
using System.Collections;

public class ToolBar_v2 : MonoBehaviour
{
		//Home button that resets level
		public void Home ()
		{
				Application.LoadLevel (Application.loadedLevelName);
		}

		public void resetView ()
		{
				Application.LoadLevel ("Campus_Model");
		}

		public void button3 ()
		{
				print ("You clicked the third button!");
		}
		//exit button that closes application
		public void exitButton ()
		{
				Application.Quit ();
		}
}
