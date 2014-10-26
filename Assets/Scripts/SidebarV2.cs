using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SidebarV2 : MonoBehaviour
{

		float MyBoxLeft = Screen.width;
		float sidebarclosed = Screen.width;
		float sidebaropen = Screen.width - 400.0F;
		int SidebarInt = 0;
		public float smoothTime = 0.3F;
		private Vector3 velocity = Vector3.zero;
	public Transform splitfloor;

		void Start ()
		{
		//Transform splitfloor = this.transform;
		Vector3 newPOS = new Vector2 (splitfloor.position.x, splitfloor.position.y + 3000);
		splitfloor.position = newPOS;
		Debug.Log (splitfloor.position.x);
		Debug.Log (splitfloor.position.y);
		Debug.Log (splitfloor.position.y+3000);
		}

		void  OnMouseEnter ()
		{
			
		}

		void  Update ()
		{
		}
}