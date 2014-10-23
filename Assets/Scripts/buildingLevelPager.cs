using UnityEngine;
using System.Collections;

public class buildingLevelPager : MonoBehaviour {
	public Component[] hingeJoints;

	public GameObject building;
	public GameObject upButton;
	public GameObject downButton;
	// Use this for initialization
	void Start () {
		Debug.Log("Child Objects: " + CountChildren(transform));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int CountChildren(Transform a)
	{
		int childCount = 0;
		foreach (Transform b in a)
         {
			Debug.Log("Child: "+b);
			childCount ++;
			childCount += CountChildren(b);
		}
         return childCount;
	}


	void Example() {
		hingeJoints = GetComponentsInChildren<HingeJoint>();
		foreach (HingeJoint joint in hingeJoints) {
			joint.useSpring = false;
		}
	}
}
