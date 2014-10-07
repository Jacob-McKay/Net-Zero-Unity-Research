using UnityEngine;
using System.Collections;

public class buildingbuttonstest : MonoBehaviour {

	Transform target;
	public Texture button1;
	public Texture button2;
	public Texture button3;
	public Texture button4;

	private Color startcolor;
	//public string ObjectName = "Object";
	bool  isHighlighted = false;

	void Start(){
		//msoebuildingObject();
	}

	void  OnMouseEnter (){
		isHighlighted = true;
		startcolor = renderer.material.color;
		renderer.material.color = Color.red;
	}
	
//	void  OnMouseExit (){
//		isHighlighted = false;
//		renderer.material.color = startcolor;
//	}

	void  OnGUI (){
		//menu buttons that float above selected building
		Vector3 screenPos= Camera.main.WorldToScreenPoint (transform.position);
		if (isHighlighted) {
			GUI.BeginGroup (new Rect (screenPos.x - 112 ,Screen.height-Camera.main.WorldToScreenPoint (renderer.bounds.max).y - 100 ,500,500));
			GUI.Box(new Rect(0, 0, 200, 70), "");
			GUI.TextArea (new Rect(0,0,200f,20f),this.gameObject.name);
			GUI.Button ( new Rect(0,20,50,50), button1);
			GUI.Button ( new Rect(50,20,50,50), button2);
			GUI.Button ( new Rect(100,20,50,50), button3);
			if(GUI.Button ( new Rect(150,20,50,50), button4))
				Closemenu();
			GUI.EndGroup ();
		}
	}

	void Closemenu(){
		isHighlighted = false;
		renderer.material.color = startcolor;
	}

	public void msoebuildingObject(){
		//label all MSOE buildings with bouncing object
		GameObject[] go = GameObject.FindGameObjectsWithTag("MSOE");
		foreach(GameObject g in go)
		{
			GameObject testPrefab = Instantiate(Resources.Load("buildingbutton", typeof(GameObject))) as GameObject;
			testPrefab.transform.position = new Vector3(g.transform.position.x,g.transform.position.y + 2, g.transform.position.z);
//			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//			cube.transform.position = new Vector3(g.transform.position.x, 2, g.transform.position.z);
//			cube.transform.localScale = new Vector3(.5f,.5f,.5f);
//			cube.transform.gameObject.tag = "Respawn";
		}
	}

	void Update(){
		//label all MSOE buildings with bouncing object
		GameObject[] go = GameObject.FindGameObjectsWithTag("Respawn");
		foreach(GameObject g in go)
		{
			//billboard rotation. world y axis constrained
			Vector3 v3T = transform.position + Camera.main.transform.rotation * Vector3.back;
			v3T.y = transform.position.y;
			g.transform.LookAt(v3T, Vector3.up);

			//animation effect
			//iTween.MoveBy(g, iTween.Hash("y", .5, "easeType", "easeInOutSine", "loopType", "pingPong", 0, 1));
		}
	}


//	void DoSomething(string buttonName)
//	{
//		Debug.Log("hello");
////		switch(buttonName)
////		{
////		case "RWJ":
////			Debug.Log("The 'Blue' button was pressed");
////			break;
////			
////		case "Kern Center":
////			Debug.Log("The 'Red' button was pressed");
////			break;
////		}
//	}
}