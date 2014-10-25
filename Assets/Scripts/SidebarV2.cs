using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SidebarV2 : MonoBehaviour {

	float MyBoxLeft= Screen.width;
	float sidebarclosed= Screen.width;
	float sidebaropen= Screen.width - 400.0F;
	int SidebarInt = 0;

	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;

	void  OnMouseEnter (){
			StartCoroutine(AnimateBox());
	}

	void  Update (){
	}

	//sidebar animation
	public IEnumerator  AnimateBox (){

		if (SidebarInt == 0) {			
			while (MyBoxLeft > sidebaropen) {
				MyBoxLeft-=20.0F;
				SidebarInt = 1;
				yield return 0;
			}			
		} 
		else if (SidebarInt == 1) {	
			while (MyBoxLeft < sidebarclosed) {
				MyBoxLeft+=20.0f;
				SidebarInt = 0;
				yield return 0;
			}
		}
	}
}