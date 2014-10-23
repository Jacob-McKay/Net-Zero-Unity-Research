using UnityEngine;
using System.Collections;

public class TestButtonClick : MonoBehaviour {
	//set target
	public Transform target;

	//private Vector3 newPosition;

	void awake ()
	{
		//get initial building floors from children objects and put into an array??
	}

	public void upButton () {
		Debug.Log ("up button clicked");

		//transform.position = newPosition;

		}
	public void downButton () {
		Debug.Log ("down button clicked");
	}


	//public void downButton (string text) {
	//			Debug.Log (text);
	//	}
}
