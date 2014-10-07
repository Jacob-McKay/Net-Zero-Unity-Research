using UnityEngine;
using System.Collections;

// Contains GUI system dependent functions

public class WMG_GUI_Functions : MonoBehaviour {
	
	public enum WMGpivotTypes {Bottom, BottomLeft, BottomRight, Center, Left, Right, Top, TopLeft, TopRight};
	
	public void SetActive(GameObject obj, bool state) {
		#if (UNITY_3_5 || UNITY_3_4 || UNITY_3_3 || UNITY_3_2 || UNITY_3_1 || UNITY_3_0)
			obj.SetActiveRecursively(state);
		#else
		    obj.SetActive(state);
		#endif
	}
	
	public bool activeInHierarchy(GameObject obj) {
		#if (UNITY_3_5 || UNITY_3_4 || UNITY_3_3 || UNITY_3_2 || UNITY_3_1 || UNITY_3_0)
			return obj.active;
		#else
			return obj.activeInHierarchy;
		#endif
	}
	
	public void SetActiveAnchoredSprite(GameObject obj, bool state) {
		SetActive(obj, state);
	}
	
	public void changeSpriteColor(GameObject obj, Color aColor) {
		UISprite theSprite = obj.GetComponent<UISprite>();
		theSprite.color = aColor;
	}
	
	public void changeSpriteWidth(GameObject obj, int aWidth) {
//		obj.transform.localScale = new Vector3(aWidth, obj.transform.localScale.y, obj.transform.localScale.z);
		UIWidget theSprite = obj.GetComponent<UIWidget>();
		theSprite.width = aWidth;
		NGUITools.UpdateWidgetCollider(obj);
	}
	
	public void changeSpriteHeight(GameObject obj, int aHeight) {
//		obj.transform.localScale = new Vector3(obj.transform.localScale.x, aHeight, obj.transform.localScale.z);
		UIWidget theSprite = obj.GetComponent<UIWidget>();
		theSprite.height = aHeight;
		NGUITools.UpdateWidgetCollider(obj);
	}
	
	public float getSpriteWidth(GameObject obj) {
//		return obj.transform.localScale.x;
		UIWidget theSprite = obj.GetComponent<UIWidget>();
		return theSprite.width;
	}
	
	public float getSpriteHeight(GameObject obj) {
//		return obj.transform.localScale.y;
		UIWidget theSprite = obj.GetComponent<UIWidget>();
		return theSprite.height;
	}
	
	public void changeLabelText(GameObject obj, string aText) {
		UILabel theLabel = obj.GetComponent<UILabel>();
		theLabel.text = aText;
	}
	
	public float getSpritePositionX(GameObject obj) {
		return obj.transform.localPosition.x;
	}
	
	public float getSpritePositionY(GameObject obj) {
		return obj.transform.localPosition.y;
	}
	
	public float getSpriteOffsetX(GameObject obj) {
		return 0;
	}
	
	public float getSpriteFactorX(GameObject obj) {
		return 0;
	}
	
	public float getSpriteOffsetY(GameObject obj) {
		return 0;
	}
	
	public float getSpriteFactorY(GameObject obj) {
		return 0;
	}
	
	public float getSpriteFactorY2(GameObject obj) {
		float factor = 0;
		UISprite theSprite = obj.GetComponent<UISprite>();
		if (theSprite.pivot == UIWidget.Pivot.Bottom) {
			factor = 1;
		}
		else if (theSprite.pivot == UIWidget.Pivot.BottomLeft) {
			factor = 1;
		}
		else if (theSprite.pivot == UIWidget.Pivot.BottomRight) {
			factor = 1;
		}
		else if (theSprite.pivot == UIWidget.Pivot.Center) {
			factor = 0.5f;
		}
		else if (theSprite.pivot == UIWidget.Pivot.Left) {
			factor = 0.5f;
		}
		else if (theSprite.pivot == UIWidget.Pivot.Right) {
			factor = 0.5f;
		}
		else if (theSprite.pivot == UIWidget.Pivot.Top) {
			factor = 0;
		}
		else if (theSprite.pivot == UIWidget.Pivot.TopLeft) {
			factor = 0;
		}
		else if (theSprite.pivot == UIWidget.Pivot.TopRight) {
			factor = 0;
		}
		return factor;
	}
	
	public void changeSpritePositionTo(GameObject obj, Vector3 newPos) {
		obj.transform.localPosition = new Vector3(newPos.x, newPos.y, newPos.z);
	}
	
	public void changeSpritePositionToX(GameObject obj, float newPos) {
		Vector3 thePos = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(newPos, thePos.y, thePos.z);
	}
	
	public void changeSpritePositionToY(GameObject obj, float newPos) {
		Vector3 thePos = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(thePos.x, newPos, thePos.z);
	}
	
	public void changeSpritePositionRelativeToObjBy(GameObject obj, GameObject relObj, Vector3 changeAmt) {
		Vector3 thePos = relObj.transform.localPosition;
		obj.transform.localPosition = new Vector3(thePos.x + changeAmt.x, thePos.y + changeAmt.y, thePos.z + changeAmt.z);
	}
	
	public void changeSpritePositionRelativeToObjByX(GameObject obj, GameObject relObj, float changeAmt) {
		Vector3 thePos = relObj.transform.localPosition;
		Vector3 curPos = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(thePos.x + changeAmt, curPos.y, curPos.z);
	}
	
	public void changeSpritePositionRelativeToObjByY(GameObject obj, GameObject relObj, float changeAmt) {
		Vector3 thePos = relObj.transform.localPosition;
		Vector3 curPos = obj.transform.localPosition;
		obj.transform.localPosition = new Vector3(curPos.x, thePos.y + changeAmt, curPos.z);
	}
	
	public void changeSpritePivot(GameObject obj, WMGpivotTypes theType) {
		UIWidget theSprite = obj.GetComponent<UIWidget>();
		if (theType == WMGpivotTypes.Bottom) {
			theSprite.pivot = UIWidget.Pivot.Bottom;
		}
		else if (theType == WMGpivotTypes.BottomLeft) {
			theSprite.pivot = UIWidget.Pivot.BottomLeft;
		}
		else if (theType == WMGpivotTypes.BottomRight) {
			theSprite.pivot = UIWidget.Pivot.BottomRight;
		}
		else if (theType == WMGpivotTypes.Center) {
			theSprite.pivot = UIWidget.Pivot.Center;
		}
		else if (theType == WMGpivotTypes.Left) {
			theSprite.pivot = UIWidget.Pivot.Left;
		}
		else if (theType == WMGpivotTypes.Right) {
			theSprite.pivot = UIWidget.Pivot.Right;
		}
		else if (theType == WMGpivotTypes.Top) {
			theSprite.pivot = UIWidget.Pivot.Top;
		}
		else if (theType == WMGpivotTypes.TopLeft) {
			theSprite.pivot = UIWidget.Pivot.TopLeft;
		}
		else if (theType == WMGpivotTypes.TopRight) {
			theSprite.pivot = UIWidget.Pivot.TopRight;
		}
	}
	
	public void changeSpriteParent(GameObject child, GameObject parent) {
		child.transform.parent = parent.transform;
		child.transform.localPosition = Vector3.zero;
	}
	
	public void bringSpriteToFront(GameObject obj) {
		// Only needed in Daikon
	}
	
	public void sendSpriteToBack(GameObject obj) {
		// Only needed in Daikon
	}
	
	public string getDropdownSelection(GameObject obj) {
		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		return dropdown.value;
	}
	
	public void setDropdownSelection(GameObject obj, string newval) {
		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		dropdown.value = newval;
	}
	
	public void addDropdownItem(GameObject obj, string item) {
		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		dropdown.items.Add(item);
	}
	
	public void deleteDropdownItem(GameObject obj) {
		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		dropdown.items.RemoveAt(dropdown.items.Count-1);
	}
	
	public void setDropdownIndex(GameObject obj, int index) {
		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		dropdown.value = dropdown.items[index];
	}
	
	public void setButtonColor(Color aColor, GameObject obj) {
		UILabel aButton = obj.GetComponent<UILabel>();
		aButton.color = aColor;
	}
	
	public bool getToggle(GameObject obj) {
		UIToggle theTog = obj.GetComponent<UIToggle>();
		return theTog.value;
	}
	
	public void setToggle(GameObject obj, bool state) {
		UIToggle theTog = obj.GetComponent<UIToggle>();
		theTog.value = state;
	}
	
	public float getSliderVal(GameObject obj) {
		UISlider theSlider = obj.GetComponent<UISlider>();
		return theSlider.value;
	}
	
	public void setSliderVal(GameObject obj, float val) {
		UISlider theSlider = obj.GetComponent<UISlider>();
		theSlider.value = val;
	}
	
	public void showControl(GameObject obj) {
		SetActive(obj, true);
	}
	
	public void hideControl(GameObject obj) {
		SetActive(obj, false);
	}
	
	public bool getControlVisibility(GameObject obj) {
		return activeInHierarchy(obj);
	}
	
	public void unfocusControl(GameObject obj) {
		// Only needed in Daikon
	}
	
	public bool isDaikon() {
		// Sometimes this may be needed, usually hacky quick fix workaround
		return false;
	}
}
