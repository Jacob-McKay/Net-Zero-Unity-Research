using UnityEngine;
using System.Collections;

public class ZoomWithOffCenterObject : MonoBehaviour {
	
	public GameObject goTrack;
	private Vector3 v3ZoomFactor = new Vector3(0.0f, 0.0f, 0.05f);
	
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow))
			ZoomCamera (true);
		if (Input.GetKey (KeyCode.DownArrow))
			ZoomCamera (false);
	}
	
	private void ZoomCamera(bool bIn) {
		Vector3 v3OrgPos = Camera.main.WorldToViewportPoint(goTrack.transform.position);
		
		if (bIn)
			transform.Translate (v3ZoomFactor);
		else
			transform.Translate (-v3ZoomFactor);
		
		v3OrgPos.z = Mathf.Abs (Camera.main.transform.position.z - goTrack.transform.position.z);
		v3OrgPos = Camera.main.ViewportToWorldPoint(v3OrgPos);
		transform.Translate (goTrack.transform.position - v3OrgPos);
	}
}