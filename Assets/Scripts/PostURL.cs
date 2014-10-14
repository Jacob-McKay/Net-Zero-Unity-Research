using UnityEngine;
using System.Collections;


public class PostURL : MonoBehaviour {
	
	void Start () {
		
		string url = "http://127.0.0.1/NetZero";
		

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