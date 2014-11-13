using UnityEngine;
using System.Collections;


public class PostURL : MonoBehaviour {
	
	void Start () {
		
		string url = "http://155.92.205.140/NZE/";
		

		WWW www = new WWW(url);
		
		StartCoroutine(WaitForRequest(www));
	}
	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
			
			// check for errors
			if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}    
}