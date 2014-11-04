using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {
	
	//refrence for the pause menu panel in the hierarchy
	public GameObject pauseMenuPanel;
	//animator reference
	private Animator anim;
	//
	private int sidebar = 0;

	// Use this for initialization
	void Start () {
		//get the animator component
		anim = pauseMenuPanel.GetComponent<Animator>();
		//disable it on start to stop it from playing the default animation
		anim.enabled = false;
	}

	public void sidebarButton(){
		if (sidebar == 0) {
						sidebarSlideIn ();
				} else {
						sidebarSlideOut ();
				}
	}
	
	//function to slide the sidebar into the scene
	public void sidebarSlideIn(){
		//enable the animator component
		anim.enabled = true;
		//play the Slidein animation
		anim.Play("SlideIn");
		sidebar = 1;
	}
	//function to slide the sidebar off the screen
	public void sidebarSlideOut(){
		//play the SlideOut animation
		anim.Play("SlideOut");
		sidebar = 0;
	}
	
}