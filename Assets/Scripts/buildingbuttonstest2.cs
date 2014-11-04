using UnityEngine;
using System.Collections;

public class buildingbuttonstest2 : MonoBehaviour
{

		Transform target;
		public retainInfoForNewScene script;

		void Start ()
		{
				
		}

		void  OnMouseDown ()
		{
				msoebuildingLabel ();
		}

		void OnMouseExit ()
		{
				//destroy UI on exit after 2 seconds or so
				//DestroyObject (this);
		}

		public void msoebuildingLabel ()
		{
				//label all MSOE buildings with bouncing object
				GameObject[] go = GameObject.FindGameObjectsWithTag ("MSOE");
				foreach (GameObject g in go) {
						GameObject testPrefab = Instantiate (Resources.Load ("Canvas-Building Tag", typeof(GameObject))) as GameObject;
						testPrefab.transform.position = new Vector3 (g.transform.position.x, g.transform.position.y + 80, g.transform.position.z);
			//set the building label text to equal the building name selected
			//testPrefab.GetComponentInChildren<TextMesh>().text = this.gameObject.name;
				}
		}

		public void buildingViewer ()
		{
				retainInfoForNewScene.selectedBuilding2 = this.gameObject.name;
				//Debug.Log (this.gameObject);
				//Debug.Log (script.selectedBuilding2);
				Application.LoadLevel ("Building_Viewer_test");
		}
}