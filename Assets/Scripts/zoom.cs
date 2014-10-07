// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class zoom : MonoBehaviour {
	
	
	Vector3 camPos;
	Transform camTr;
	float speed= 2.5f;
	
	void  Start (){
		camTr = Camera.main.transform;
		camPos = camTr.position;
	}
	
	void  Update (){
		
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit) && hit.collider.tag == "MSOE") {
				//GameObject[] msoebuildings;
				GameObject[] blocks= GameObject.FindGameObjectsWithTag("MSOE");
				foreach(GameObject go in blocks) {
					if (go == hit.collider.gameObject) {
						camPos.x = go.transform.position.x;
						camPos.y = go.transform.position.y;
						camPos.z += 5.0f; // Zoom
					}
					else {
						go.SetActive(false);
						
					}
				}
			}
		}

//		if (Input.GetMouseButtonDown(0))
//		{
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit2;
//			if (Physics.Raycast(ray, out hit2) && hit2.collider.tag == "MSOE")
//			{
//				GameObject[] msoebuildings;
//				msoebuildings = GameObject.FindGameObjectsWithTag("MSOE");
//				foreach (GameObject go in msoebuildings)
//				{
//					if (go == hit2.collider.gameObject)
//					{
//						target = go.transform;
//					}
//				}
//			}
//		}
		
		camTr.position = Vector3.Lerp(camTr.position, camPos, Time.deltaTime * speed);
	}
}