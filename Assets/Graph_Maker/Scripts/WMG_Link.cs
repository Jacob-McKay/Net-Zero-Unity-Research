using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WMG_Link : WMG_GUI_Functions {
	public int id;	// Each link has a unique id
	// Node reference
	public GameObject fromNode;
	public GameObject toNode;
	// References to children of this link that could be interesting to use in scripts
	public GameObject objectToScale;
	public GameObject objectToColor;
	public GameObject objectToLabel;
	public bool weightIsLength;	// Updates the link weight based on its length
	public bool updateLabelWithLength; // Updates the objectToLabel with the link length
	public bool isSelected = false;	// Used in the editor when the link is selected
	public bool wasSelected = false; // Used in the editor for drag select operations
	public float weight;	// A link's weight, used in find shortest path weighted algorithms
	
	public void Setup(GameObject fromNode, GameObject toNode, int linkId) {
		// Setup references and give a default name of the link based on node IDs
		this.fromNode = fromNode;
		this.toNode = toNode;
		SetId(linkId);
		WMG_Node fromN = fromNode.GetComponent<WMG_Node>();
		WMG_Node toN = toNode.GetComponent<WMG_Node>();
		this.name = "WMG_Link_" + fromN.id + "_" + toN.id;
		Reposition();	// Update position and scale based on connected nodes
	}
	
	public void Reposition() {
		float posXdif = getSpritePositionX(toNode) - getSpritePositionX(fromNode);
		float posYdif = getSpritePositionY(toNode) - getSpritePositionY(fromNode);
		
		float angle = Mathf.Atan2(posYdif,posXdif)*Mathf.Rad2Deg + 90;
		
		WMG_Node fromN = fromNode.GetComponent<WMG_Node>();
		WMG_Node toN = toNode.GetComponent<WMG_Node>();
		
		SetNodeAngles(angle,fromN,toN);	// Set angles in node references, so they don't need to be calculated in various places
		
		float radiuses = fromN.radius + toN.radius;
		float length = Mathf.Sqrt(Mathf.Pow(posYdif,2) + Mathf.Pow(posXdif,2)) - radiuses;
		if (length < 0) length = 0;
		
		if (weightIsLength) weight = length;
		if (updateLabelWithLength) {
			if (objectToLabel != null) {
				changeLabelText(objectToLabel, Mathf.Round(length).ToString());
				objectToLabel.transform.localEulerAngles = new Vector3 (0,0,360-angle);
			}
		}
		
		// NGUI
		this.transform.localPosition = new Vector3 (getSpriteFactorY2(this.gameObject) * posXdif + fromNode.transform.localPosition.x, 
													getSpriteFactorY2(this.gameObject) * posYdif + fromNode.transform.localPosition.y, 
													this.transform.localPosition.z);
		
		// Daikon
//		changeSpritePositionRelativeToObjBy(this.gameObject, fromNode, 
//											new Vector3(getSpriteFactorY(this.gameObject) * posXdif + 
//														-getSpriteOffsetX(this.gameObject) +
//														Mathf.Cos(Mathf.Deg2Rad * angle) * 0.5f * getSpriteWidth(this.gameObject) +
//														Mathf.Cos(Mathf.Deg2Rad * angle) * (getSpriteFactorX(this.gameObject) - 1) * getSpriteWidth(this.gameObject) +
//														getSpriteOffsetX(fromNode), 
//														getSpriteFactorY(this.gameObject) * posYdif +
//														getSpriteOffsetY(this.gameObject) +
//														-Mathf.Sin(Mathf.Deg2Rad * angle) * 0.5f * getSpriteWidth(this.gameObject) +
//														Mathf.Sin(Mathf.Deg2Rad * angle) * getSpriteFactorX(this.gameObject) * getSpriteWidth(this.gameObject) +
//														-getSpriteOffsetY(fromNode), 1));
		
		changeSpriteHeight(objectToScale, Mathf.RoundToInt(length));
		this.transform.localEulerAngles = new Vector3 (0,0,angle);
		
	}
	
	public void SetId(int linkId) {
		this.id = linkId;
	}
	
	void SetNodeAngles(float angle, WMG_Node fromN, WMG_Node toN) {
		for (int i = 0; i < fromN.numLinks; i++) {
			WMG_Link fromNlink = fromN.links[i].GetComponent<WMG_Link>();
			if (fromNlink.id == this.id) {
				fromN.linkAngles[i] = angle - 90;
			}
		}
		for (int i = 0; i < toN.numLinks; i++) {
			WMG_Link toNlink = toN.links[i].GetComponent<WMG_Link>();
			if (toNlink.id == this.id) {
				toN.linkAngles[i] = angle + 90;
			}
		}
	}
}
