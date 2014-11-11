using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SidebarV2 : MonoBehaviour
{
		public Transform sidePanel;
		public float changeTargetSmoothTime = 5f;
		public Transform target;
		public MainNavigation nav;

		void Start ()
		{
		}

		public void startAnimateBox ()
		{
				Debug.Log ("start position x= " + sidePanel.position.x + "y= " + sidePanel.position.y + "z= " + sidePanel.position.z);
				Vector3 source = new Vector3 (Screen.width - 1000, sidePanel.position.y, sidePanel.position.z);
				Vector3 target = new Vector3 (600, sidePanel.position.y, sidePanel.position.z);
				StartCoroutine (MoveObject (source, target, 1));
		}

		//sidebar animation
		IEnumerator MoveObject (Vector3 source, Vector3 target, float overTime)
		{
				float startTime = Time.time;
				while (Time.time < startTime + overTime) {
						transform.position = Vector3.Lerp (source, target, (Time.time - startTime) / overTime);
						yield return null;
				}
				//transform.position = target;
		}

		public void changetarget ()
		{
				string building = this.gameObject.name;
				
		GameObject buildingobject2;
		buildingobject2 = GameObject.Find ("RWJ");
		target = buildingobject2.transform;
		Debug.Log (target);


				GameObject buildingobject;
				buildingobject = GameObject.Find ("Tower");
				Vector3 buildingVector = new Vector3 (buildingobject.transform.position.x, buildingobject.transform.position.y, buildingobject.transform.position.z);
				//building = GameObject.Find (building.ToString);

				nav = new MainNavigation ();
				nav.setCameraTargetFromSidebar (target, buildingVector);
		}
}