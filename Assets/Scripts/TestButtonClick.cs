﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;//added this for lists

public class TestButtonClick : MonoBehaviour
{
		
		public List<GameObject> buildingFloors;
		public GameObject selectedBuilding;
		private int currentFloor; //have a private variable that stores an INT for what is the current floor being viewed?
		private string newbuilding;
		public maxCamera cameraTarget;
		public float smooth = 2f;

		void Start ()
		{
				//getting selected building name from previous scene
				newbuilding = retainInfoForNewScene.selectedBuilding2;
				Debug.Log (newbuilding);
				//load the building from prefab and set selectedBuilding to loaded building
				GameObject go = (GameObject)Instantiate (Resources.Load (newbuilding));
				selectedBuilding = go;

				//shitty titty iterate over all of them for now :(
				//hopefully can find a solution that just gives all children in one call
				foreach (Transform child in selectedBuilding.transform) {
						//Debug.Log ("Test Tower is my daddy, my name is: " + child.gameObject.name);
						buildingFloors.Add (child.gameObject);
				}
				Debug.Log ("There are " + buildingFloors.Count + " floors in this selected building");

				//split building in half as starting point
				//OMG IT WORKS!
				Debug.Log (buildingFloors.Count / 2);
				currentFloor = buildingFloors.Count / 2;
				Debug.Log (currentFloor);
				for (int i = currentFloor; i < buildingFloors.Count; i++) {
						Transform splitfloor = buildingFloors [i].transform;
						Vector3 newPOS = new Vector3 (splitfloor.position.x, splitfloor.position.y + 300, splitfloor.position.z);
						splitfloor.position = newPOS;
				}

				/*
		Debug.Log("child count of selectedBuilding: " + selectedBuilding.transform.childCount);
			foreach (Transform buildingTransform in buildingFloors) {
			//output each building's postion
			Vector3 buildingPOS = buildingTransform.position;
			Debug.Log ("building: " is at X: " + buildingPOS.x + ", Y: " + buildingPOS.y);
		}*/
		}

		public void upButton ()
		{

				Transform topfloor = buildingFloors [currentFloor - 1].transform;
				Vector3 newPOS = new Vector3 (topfloor.position.x, topfloor.position.y + 300, topfloor.position.z);
				//WHY DOESNT THIS WORK? DO I NEED TO START A COROUTINE SINCE IT ISNT IN UPDATE?
				//
				//StartCoroutine (animateBuildingFloor (topfloor, newPOS, 5f));
				topfloor.position = newPOS;
				currentFloor = currentFloor - 1;
				Debug.Log (currentFloor);
				//StopCoroutine ("animateBuildingFloor");
				Debug.Log ("current floor2 = " + currentFloor);
		}

		public void downButton ()
		{
				Transform topfloor = buildingFloors [currentFloor].transform;
				Vector3 newPOS = new Vector3 (topfloor.position.x, topfloor.position.y - 300, topfloor.position.z);
				//StartCoroutine (animateBuildingFloor (topfloor, newPOS, 5f));
				topfloor.position = newPOS;
				currentFloor = currentFloor + 1;
				Debug.Log (currentFloor);
		}
		//coroutine to animate floors
		IEnumerator animateBuildingFloor (Transform topfloor2, Vector3 newPOS2, float overTime)
		{
				float startTime = Time.time;
				while (Time.time < startTime + overTime) {
						//Debug.Log ("pos2 = " + newPOS2);
						//Debug.Log ("topfloor2 = " + topfloor2.position);
						topfloor2.position = Vector3.Lerp (topfloor2.position, newPOS2, Time.deltaTime * smooth);
						yield return null;
				}
				topfloor2.position = newPOS2;
		}
}
