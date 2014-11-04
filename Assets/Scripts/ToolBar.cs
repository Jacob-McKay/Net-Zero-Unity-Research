using UnityEngine;
using System.Collections;

public class ToolBar : MonoBehaviour {
	public GUISkin Main;
	public Texture btnTexture;

	//toolbar button that is initally set active
	public int toolbarInt = 0;
	//define button text for toolbar
	public string[] toolbarStrings = new string[] {"TOOLBAR1", "TOOLBAR2", "TOOLBAR3", "TOOLBAR4"};
	void OnGUI() {
		//assign custom GUISKIN
		GUI.skin = Main;
		//MSOE LOGO
		//GUI.DrawTexture(new Rect(0, 0, 150, 179),btnTexture, ScaleMode.StretchToFill, true);
		//BUILDING LIST SIDEBAR BUTTON
		//if (GUI.Button(new Rect(Screen.width - 25 , Screen.height / 2 - 50 , 25 , 50), "<"))
			//GUI.DrawTexture(new Rect(Screen.width / 2 , Screen.height - 30, Screen.width / 2 , Screen.height - 30 ),btnTexture, ScaleMode.StretchToFill, true);
		//Main navigation toolbar
		toolbarInt = GUI.Toolbar(new Rect(150, 0, Screen.width - 150 , 50), toolbarInt, toolbarStrings);
		if(GUI.changed){
			//Home button that resets level
			if(toolbarInt == 0){
				Application.LoadLevel (Application.loadedLevelName);
			}
			if(toolbarInt == 1){
				Application.LoadLevel ("Campus_Model");
			}
			if(toolbarInt == 2){
				print ("You clicked the third button!");
			}
			//exit button that closes application
			if(toolbarInt == 3){
				Application.Quit ();
			}
		}
	}
}