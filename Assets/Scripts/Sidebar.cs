using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sidebar : MonoBehaviour {

	float MyBoxLeft= Screen.width;
	float sidebarclosed= Screen.width;
	float sidebaropen= Screen.width - 400.0F;
	int SidebarInt = 0;

	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;

	public Texture icon;
	public GUISkin Main;
	public GUIText targetGuiText;

	void  OnGUI (){ 
		GUI.skin = Main; 
		if (GUI.Button(new Rect(MyBoxLeft - 25 , Screen.height / 2 - 50 , 25 , 50), "<")){
			buildingbuttonstest test = new buildingbuttonstest();
			test.msoebuildingObject();
			StartCoroutine(AnimateBox());
		}
		GUI.BeginGroup (new Rect (MyBoxLeft, 50.0F, 400, Screen.height - 50));
		GUI.Box (new Rect (0,0,400,Screen.height - 50), "Building List");
		//Rect tBoxRect= new Rect(MyBoxLeft, 30.0F, 200, Screen.height - 50);
		GameObject[] go = GameObject.FindGameObjectsWithTag("MSOE");
		foreach(GameObject g in go)
		{
			if(GUILayout.Button(g.name))
			{
				DoSomething(g.name);
				Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, g.transform.position, ref velocity, 0.5f);
			}
		}
		//GUI.Box(tBoxRect, icon);
		//GUI.TextArea(new Rect(0,100,400,300), "Hello World\nI've got 2 lines...", 200);
		GUI.EndGroup ();
	}

	void  Update (){
	}

	//building list button actions
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

	//sidebar animation
	public IEnumerator  AnimateBox (){

		if (SidebarInt == 0) {			
			while (MyBoxLeft > sidebaropen) {
				MyBoxLeft-=20.0F;
				SidebarInt = 1;
				yield return 0;
			}			
		} 
		else if (SidebarInt == 1) {	
			while (MyBoxLeft < sidebarclosed) {
				MyBoxLeft+=20.0f;
				SidebarInt = 0;
				yield return 0;
			}
		}
	}
}