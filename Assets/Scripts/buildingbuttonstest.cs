using UnityEngine;
using System.Collections;

public class buildingbuttonstest : MonoBehaviour
{

		Transform target;
		public Texture button1;
		public Texture button2;
		public Texture button3;
		public Texture button4;
		private Color startcolor;
		//public string ObjectName = "Object";
		bool  isHighlighted = false;
		public retainInfoForNewScene script;

		void Start ()
		{
				//msoebuildingObject();
		}

		void  OnMouseEnter ()
		{
				isHighlighted = true;
				startcolor = renderer.material.color;
				renderer.material.color = Color.red;
		}

		void OnMouseExit ()
		{
				renderer.material.color = startcolor;
		StartCoroutine (wait ());
				//isHighlighted = false;
		}

		void  OnGUI ()
		{
				//menu buttons that float above selected building
				Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
				if (isHighlighted) {
						GUI.BeginGroup (new Rect (screenPos.x - 112, Screen.height - Camera.main.WorldToScreenPoint (renderer.bounds.max).y - 100, 500, 500));
						GUI.Box (new Rect (0, 0, 200, 70), "");
						GUI.TextArea (new Rect (0, 0, 200f, 20f), this.gameObject.name);
						if (GUI.Button (new Rect (0, 20, 50, 50), button1)) {
								//script otherScript = GetComponent<script>();
								retainInfoForNewScene.selectedBuilding2 = this.gameObject.name;
								//Debug.Log (this.gameObject);
								//Debug.Log (script.selectedBuilding2);
								Application.LoadLevel ("Building_Viewer_test");
						}
						GUI.Button (new Rect (50, 20, 50, 50), button2);
						GUI.Button (new Rect (100, 20, 50, 50), button3);
						if (GUI.Button (new Rect (150, 20, 50, 50), button4))
								Closemenu ();
						GUI.EndGroup ();
				}
		}

		void Closemenu ()
		{
				isHighlighted = false;
				renderer.material.color = startcolor;
		}

		public void msoebuildingObject ()
		{
				//label all MSOE buildings with bouncing object
				GameObject[] go = GameObject.FindGameObjectsWithTag ("MSOE");
				foreach (GameObject g in go) {
						GameObject testPrefab = Instantiate (Resources.Load ("buildingbutton", typeof(GameObject))) as GameObject;
						testPrefab.transform.position = new Vector3 (g.transform.position.x, g.transform.position.y + 2, g.transform.position.z);
				}
		}

		void Update ()
		{
				//label all MSOE buildings with bouncing object
				GameObject[] go = GameObject.FindGameObjectsWithTag ("Respawn");
				foreach (GameObject g in go) {
						//billboard rotation. world y axis constrained
						Vector3 v3T = transform.position + Camera.main.transform.rotation * Vector3.back;
						v3T.y = transform.position.y;
						g.transform.LookAt (v3T, Vector3.up);

						//animation effect
						//iTween.MoveBy(g, iTween.Hash("y", .5, "easeType", "easeInOutSine", "loopType", "pingPong", 0, 1));
				}
		}
	IEnumerator wait() {
		yield return new WaitForSeconds (3);
		isHighlighted = false;
	}
}