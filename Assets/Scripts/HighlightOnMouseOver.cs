using UnityEngine;
using System.Collections;

public class HighlightOnMouseOver : MonoBehaviour{
	public Transform target;
	private Color startcolor;

	void OnMouseEnter() {
		startcolor = renderer.material.color;
		renderer.material.color = Color.red;
	}

	void OnMouseExit() {
		//renderer.material.color = Color.blue;
		renderer.material.color = startcolor;
	}

	//void OnMouseEnter() {
		//do something
	//}
}