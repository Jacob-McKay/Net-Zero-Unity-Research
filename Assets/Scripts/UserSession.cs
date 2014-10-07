using UnityEngine;
using System.Collections;

public class UserSession : MonoBehaviour {
	// User login:
	public string userLogin;
	
	void Awake() {
		// Do not destroy this game object:
		DontDestroyOnLoad(this);
	} 
}
