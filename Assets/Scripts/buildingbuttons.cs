using UnityEngine;
using System.Collections;

public class buildingbuttons : MonoBehaviour
{
		public Transform target;
		private Color startcolor;
		//public string ObjectName = "Object";
		bool  isHighlighted = false;


		public string displayText;
		private float sliderValue = 1.0f;
		private float maxSliderValue = 10.0f;

	public GUIStyle customButton;
	
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

		void Update ()
		{
				if (Input.GetMouseButtonDown (0))
						Debug.Log ("Pressed left click.");
		
				if (Input.GetMouseButtonDown (1))
						Debug.Log ("Pressed right click.");
		
				if (Input.GetMouseButtonDown (2))
						Debug.Log ("Pressed middle click.");

		}
		
		void  OnGUI ()
		{
				Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
		
				if (isHighlighted) {
						//GUI.BeginGroup (new Rect (screenPos.x,Screen.height-Camera.main.WorldToScreenPoint (renderer.bounds.max).y,500,500));
						if (GUI.Button (new Rect (screenPos.x, Screen.height - Camera.main.WorldToScreenPoint (renderer.bounds.max).y, 100, 30), this.gameObject.name)) {
								Application.LoadLevel ("MSOE_Apartments"); 
						}
						//GUI.EndGroup ();
				}

				GUILayout.Button ("I am not inside an Area");
				GUILayout.BeginArea (new Rect (Screen.width / 2, Screen.height / 2, 300, 300));
		GUILayout.Button (displayText);
				GUILayout.EndArea ();

				// Wrap everything in the designated GUI Area
				GUILayout.BeginArea (new Rect (0, 0, 200, 60));
		
				// Begin the singular Horizontal Group
				GUILayout.BeginHorizontal ();
		
				// Place a Button normally
				if (GUILayout.RepeatButton ("Increase max\nSlider Value")) {
						maxSliderValue += 3.0f * Time.deltaTime;
				}
		
				// Arrange two more Controls vertically beside the Button
				GUILayout.BeginVertical ();
				GUILayout.Box ("Slider Value: " + Mathf.Round (sliderValue));
				sliderValue = GUILayout.HorizontalSlider (sliderValue, 0.0f, maxSliderValue);
		
				// End the Groups and Area
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.EndArea ();

		customButton  = GUI.skin.GetStyle ("button");
	}
}