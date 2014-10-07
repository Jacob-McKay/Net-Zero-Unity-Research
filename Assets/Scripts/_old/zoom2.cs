using UnityEngine;
using System.Collections;

public class zoom2 : MonoBehaviour {
	public Vector3 camPos;
	public Transform camTr;
	public float speed= 2.5f;
	
	void  Start () {
		camTr = Camera.main.transform;
		camPos = camTr.position;
	}
	
	void  LateUpdate () {	
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit) && hit.collider.tag == "MSOE") {
				GameObject[] blocks;
				blocks = GameObject.FindGameObjectsWithTag("MSOE");
				foreach(GameObject go in blocks) {
					if (go == hit.collider.gameObject) {
						camPos.x = go.transform.position.x;
						camPos.y = go.transform.position.y;
						camPos.z += 5.0f; // Zoom
						Debug.Log("camera move");
					}
					else {
						//go.SetActive(false);
						
					}
				}
			}
		}
		
		camTr.position = Vector3.Lerp(camTr.position, camPos, Time.deltaTime * speed);
	}
}