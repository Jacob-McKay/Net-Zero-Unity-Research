using UnityEngine;
using System.Collections;

public class WMG_Controller : MonoBehaviour {
	
	private float speed = 4f;
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && this.transform.localPosition.x > 0) {
			this.transform.localPosition = new Vector3(this.transform.localPosition.x - speed, this.transform.localPosition.y, this.transform.localPosition.z);
		}
		if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && this.transform.localPosition.x < 846) {
			this.transform.localPosition = new Vector3(this.transform.localPosition.x + speed, this.transform.localPosition.y, this.transform.localPosition.z);
		}
		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && this.transform.localPosition.y < 478) {
			this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + speed, this.transform.localPosition.z);
		}
		if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && this.transform.localPosition.y > 0) {
			this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - speed, this.transform.localPosition.z);
		}
	}
}
