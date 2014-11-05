using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Splash());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator Splash() {
				yield return new WaitForSeconds (3);
				Application.LoadLevel ("Campus_Model");
		}

}
