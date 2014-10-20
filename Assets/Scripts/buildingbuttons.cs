using UnityEngine;
using System.Collections;

public class buildingbuttons : MonoBehaviour
{
		public Transform target;
		private Color startcolor;
		//public string ObjectName = "Object";
		bool  isHighlighted = false;
		bool  wasClickityClicked = false;
	
		void  OnMouseEnter ()
		{
				isHighlighted = true;
				startcolor = renderer.material.color;
				renderer.material.color = Color.red;
				//Debug.Log ("Aw Snap, " + this.target + " was hoverty hovered!!");
		}
	
		void  OnMouseExit ()
		{
				isHighlighted = false;
				renderer.material.color = startcolor;
		}
	

		void OnMouseDown ()
		{
				wasClickityClicked = true;
		}

		void OnMouseUp ()
		{
				wasClickityClicked = false;
		}
	
		void  OnGUI ()
		{
				Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
		
				if (isHighlighted) {
						//GUI.BeginGroup (new Rect (screenPos.x,Screen.height-Camera.main.WorldToScreenPoint (renderer.bounds.max).y,500,500));
						if (GUI.Button (new Rect (screenPos.x, Screen.height - Camera.main.WorldToScreenPoint (renderer.bounds.max).y, 100, 30), this.gameObject.name)) {
								Application.LoadLevel ("Building_Viewer"); 
						}
						//GUI.EndGroup ();
				}
				if (wasClickityClicked) {
						Debug.Log ("Aw Snap, " + this.target + " was clickity clicked!!");
				}
		}
	
}