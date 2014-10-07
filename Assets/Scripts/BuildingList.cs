using UnityEngine;
using System.Collections;

public class BuildingList : MonoBehaviour {
	void start(){
		GameObject[] buildinglist;
		buildinglist = GameObject.FindGameObjectsWithTag("MSOE");

		foreach (Object x in buildinglist){
			print (x.name);
		}

}
}
