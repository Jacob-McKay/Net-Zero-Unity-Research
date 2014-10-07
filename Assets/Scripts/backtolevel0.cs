// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class backtolevel0 : MonoBehaviour {
	
	
	public string ObjectName = "Object";
	private bool  isHighlighted = false;
	
	void  OnMouseEnter (){
		isHighlighted = true;
	}
	
	void  OnMouseExit (){
		isHighlighted = false;
	}
	
	void  OnGUI (){
		Vector3 screenPos= Camera.main.WorldToScreenPoint (transform.position);
		
		if (isHighlighted) {
			if (GUI.Button ( new Rect(screenPos.x-100,Screen.height-Camera.main.WorldToScreenPoint (renderer.bounds.max).y,100,30), ObjectName)) {
				Application.LoadLevel ("Existing"); 
				print ("You clicked me!");
			}
		}
	}
	
}