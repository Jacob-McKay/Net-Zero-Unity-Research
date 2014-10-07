using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class WMG_Data_Gen : MonoBehaviour {
	
	public WMG_Axis_Graph theGraph;
	public List<int> numPoints;
	public List<float> a;
	public List<float> b;
	public List<float> c;
	public bool refreshEveryFrame;
	public GameObject UIParentGraphSettings;
	public GameObject UIParentSeriesSettings;
	public GameObject UIParentAnimationSettings;
	public GameObject toolTipPanel;
	public GameObject toolTipLabel;
	public GameObject hexagon;
	public GameObject dataGenTypesDrop;
	public GameObject graphTypeDrop;
	public GameObject gridDrop;
	public GameObject axesDrop;
	public GameObject seriesDrop;
	public GameObject markerTypesDrop;
	public GameObject lineTypesDrop;
	public GameObject seriesSelectionDrop;
	public GameObject showLabelsDrop;
	public GameObject showTicksDrop;
	public GameObject vertHorizDrop;
	public GameObject animationType;
	public GameObject animationEaseType;
	public GameObject sliderNumPoints;
	public GameObject sliderNumPointsLabel;
	public GameObject sliderA;
	public GameObject sliderALabel;
	public GameObject sliderB;
	public GameObject sliderBLabel;
	public GameObject sliderC;
	public GameObject sliderCLabel;
	public GameObject sliderLinePadding;
	public GameObject sliderLinePaddingLabel;
	public GameObject sliderMarkerSize;
	public GameObject sliderMarkerSizeLabel;
	public GameObject sliderLineWidth;
	public GameObject sliderLineWidthLabel;
	public GameObject sliderXMax;
	public GameObject sliderXMaxLabel;
	public GameObject sliderXMin;
	public GameObject sliderXMinLabel;
	public GameObject sliderYMax;
	public GameObject sliderYMaxLabel;
	public GameObject sliderYMin;
	public GameObject sliderYMinLabel;
	public GameObject sliderXTicks;
	public GameObject sliderXTicksLabel;
	public GameObject sliderYTicks;
	public GameObject sliderYTicksLabel;
	public GameObject sliderNumDecimals;
	public GameObject sliderNumDecimalsLabel;
	public GameObject sliderFontSize;
	public GameObject sliderFontSizeLabel;
	public GameObject sliderAxisWidth;
	public GameObject sliderAxisWidthLabel;
	public GameObject sliderBarWidth;
	public GameObject sliderBarWidthLabel;
	public GameObject fpsLabel;
	public GameObject buttonGraphLabel;
	public GameObject buttonSeriesLabel;
	public GameObject buttonAnimationsLabel;
	public List<GameObject> checkBoxes = new List<GameObject>();
	public GameObject checkBoxAutoSpace;
	
	// Animation
	public float animationDuration;
	public GameObject sliderAnimationDuration;
	public GameObject sliderAnimationDurationLabel;
	private EaseType easeType = EaseType.Linear;
	private bool isAnimating = false;
	
	private WMG_Series theSeries;
	private List<int> type = new List<int>();
	private List<int> cachedNumPoints = new List<int>();
	private int currentSeries;
	private bool graphSettingsSelected;
	private bool seriesSettingsSelected;
	private bool animationSettingsSelected;
	private bool loadingPrevious;
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	private bool runningRealTimeEx;
	
	// FPS counter
	private float updateInterval = 0.5f;
	private float accum   = 0; // FPS accumulated over the interval
	private int frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	
	// Graph size depending on whether graph / series / animation settings clicked
	// Daikon
//	private float axisNoneClicked = 530.5f;
//	private float backNoneClicked = 80;
//	private float axisAnimClicked = 500.5f;
//	private float backAnimClicked = 110;
//	private float axisSeriesClicked = 430.5f;
//	private float backSeriesClicked = 180;
//	private float axisGraphClicked = 410.5f;
//	private float backGraphClicked = 200;
//	private string realTimeCompName = "dfSprite";
//	private string realTimePropName = "RelativePosition";
	
	// NGUI
	private float axisNoneClicked = 545;
	private float backNoneClicked = 70;
	private float axisAnimClicked = 510;
	private float backAnimClicked = 105;
	private float axisSeriesClicked = 445;
	private float backSeriesClicked = 170;
	private float axisGraphClicked = 425f;
	private float backGraphClicked = 190;
	private string realTimeCompName = "Transform";
	private string realTimePropName = "localPosition";
	
	void Start() {
		theSeries = theGraph.lineSeries[currentSeries].GetComponent<WMG_Series>();
		theGraph.axesType = WMG_Axis_Graph.axesTypes.I;
		type.Add(1);
		type.Add(6);
		cachedNumPoints.Add(7);
		cachedNumPoints.Add(7);
		OnSliderChangeNumPoints();
		OnSliderChangeA();
		OnSliderChangeB();
		OnSliderChangeC();
		OnSliderChangeLinePadding();
		OnSliderChangeMarkerSize();
		OnSliderChangeLineWidth();
		OnSliderChangeNumDecimals();
		OnSliderChangeBarWidth();
		OnSliderChangeAxisWidth();
		OnSliderChangeFontSize();
		OnSliderChangeAnimationDuration();
		WMG_Series tmpSeries = theGraph.lineSeries[1].GetComponent<WMG_Series>();
		tmpSeries.pointValues = theGraph.GenRandomY(numPoints[1],theGraph.xAxisMinValue,theGraph.xAxisMaxValue,theGraph.yAxisMinValue,theGraph.yAxisMaxValue);
		tmpSeries.seriesName = "Random Y";
		theGraph.setToggle(checkBoxAutoSpace, false);
		OnAnimationSettingsClick();
		theGraph.WMG_MouseEnter += MyMouseEnter;
//		theGraph.WMG_MouseLeave += MyMouseLeave;
		theGraph.WMG_MouseEnter_Leg += MyMouseEnter_Leg;
//		theGraph.WMG_MouseLeave_Leg += MyMouseLeave_Leg;
		theGraph.WMG_Link_MouseEnter_Leg += MyMouseEnter_Leg_Link;
//		theGraph.WMG_Link_MouseLeave_Leg += MyMouseLeave_Leg_Link;
	}
	
	// Update is called once per frame
	void Update () {
		
		// FPS counter
		timeleft -= Time.deltaTime;
    	accum += Time.timeScale/Time.deltaTime;
    	++frames;
	    if( timeleft <= 0.0 ) {
			float fps = accum/frames;
			string format = System.String.Format("{0:F1} FPS",fps);
			theGraph.changeLabelText(fpsLabel, format);
	        timeleft = updateInterval;
	        accum = 0.0F;
	        frames = 0;
	    }
		
		if (theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical) {
			minX = theGraph.xAxisMinValue;
			maxX = theGraph.xAxisMaxValue;
			minY = theGraph.yAxisMinValue;
			maxY = theGraph.yAxisMaxValue;
		}
		else {
			minX = theGraph.yAxisMinValue;
			maxX = theGraph.yAxisMaxValue;
			minY = theGraph.xAxisMinValue;
			maxY = theGraph.xAxisMaxValue;
		}
		
		if (refreshEveryFrame && !graphSettingsSelected) {
			if (type[currentSeries] == 0) theSeries.pointValues = theGraph.GenLinear(numPoints[currentSeries],minX,maxX,a[currentSeries],b[currentSeries]);
			if (type[currentSeries] == 1) theSeries.pointValues = theGraph.GenQuadratic(numPoints[currentSeries],minX,maxX,a[currentSeries],b[currentSeries],c[currentSeries]);
			if (type[currentSeries] == 2) theSeries.pointValues = theGraph.GenExponential(numPoints[currentSeries],minX,maxX,a[currentSeries],b[currentSeries],c[currentSeries]);
			if (type[currentSeries] == 3) theSeries.pointValues = theGraph.GenLogarithmic(numPoints[currentSeries],minX,maxX,a[currentSeries],b[currentSeries],c[currentSeries]);
			if (type[currentSeries] == 4) theSeries.pointValues = theGraph.GenCircular(numPoints[currentSeries],a[currentSeries],b[currentSeries],c[currentSeries]);
			if (type[currentSeries] == 5) theSeries.pointValues = theGraph.GenRandomXY(numPoints[currentSeries],minX,maxX,minY,maxY);
			if (type[currentSeries] == 6) theSeries.pointValues = theGraph.GenRandomY(numPoints[currentSeries],minX,maxX,minY,maxY);
		}
		if (cachedNumPoints[currentSeries] != numPoints[currentSeries]) {
			cachedNumPoints[currentSeries] = numPoints[currentSeries];
			if (type[currentSeries] == 5) theSeries.pointValues = theGraph.GenRandomXY(numPoints[currentSeries],minX,maxX,minY,maxY);
			if (type[currentSeries] == 6) theSeries.pointValues = theGraph.GenRandomY(numPoints[currentSeries],minX,maxX,minY,maxY);
		}
		
		OnSliderChangeXMax();
		OnSliderChangeXMin();
		OnSliderChangeYMax();
		OnSliderChangeYMin();
		UpdateCheckboxStates();
		OnSliderChangeXTicks();
		OnSliderChangeYTicks();
		UpdateLineWidthLabel();
		OnSliderChangeBarWidth();
		
		if( theGraph.getControlVisibility(toolTipPanel) ) {
			setTooltipPosition( Input.mousePosition );
		}
	}
	
	public void OnSelectionChange() {
		string dropVal = theGraph.getDropdownSelection(dataGenTypesDrop);
		if (dropVal == "Real-Time Ex.") {
			if (runningRealTimeEx) return;
			runningRealTimeEx = true;
			theGraph.xAxisMinValue = 0;
			theGraph.xAxisMaxValue = 10;
			theGraph.SetActive(hexagon, true);
			theGraph.changeSpritePositionTo(hexagon, new Vector3(411, 282, 0));
			refreshEveryFrame = false;
			
			if (theGraph.lineSeries.Count >= 1) {
//				type[0] = -1;
				theGraph.yAxisMinValue = 350;
				theGraph.yAxisMaxValue = 450;
				WMG_Series tmpSeries1 = theGraph.lineSeries[0].GetComponent<WMG_Series>();
				tmpSeries1.connectFirstToLast = false;
				tmpSeries1.StartRealTimeUpdate();
			}
			if (theGraph.lineSeries.Count >= 2) {
//				type[1] = -1;
				WMG_Series tmpSeries2 = theGraph.lineSeries[1].GetComponent<WMG_Series>();
				tmpSeries2.connectFirstToLast = false;
				// This is because you could delete and then add series 2 but not series 1.
				tmpSeries2.realTimeObject = hexagon;
				tmpSeries2.realTimeComponentName = realTimeCompName;
				tmpSeries2.realTimeFieldName = "y";
				tmpSeries2.realTimePropertyName = realTimePropName;
				tmpSeries2.StartRealTimeUpdate();
			}
			if (theGraph.lineSeries.Count >= 3) {
				for (int j = 2; j < theGraph.lineSeries.Count; j++) {
					if (!theGraph.activeInHierarchy(theGraph.lineSeries[j])) continue;
					theGraph.SetActive(theGraph.lineSeries[j], false);
				}
			}
			setEquationText(currentSeries);
			
			theGraph.SetActive(sliderNumPoints, false);
			theGraph.SetActive(sliderA, false);
			theGraph.SetActive(sliderB, false);
			theGraph.SetActive(sliderC, false);
		}
		else {
			
			if (runningRealTimeEx) {
				runningRealTimeEx = false;
				theGraph.xAxisMinValue = -10;
				theGraph.xAxisMaxValue = 10;
				theGraph.yAxisMinValue = -100;
				theGraph.yAxisMaxValue = 100;
				
				theGraph.SetActive(hexagon, false);
				if (theGraph.lineSeries.Count >= 1) {
					numPoints[0] = 7;
					WMG_Series tmpSeries1 = theGraph.lineSeries[0].GetComponent<WMG_Series>();
					tmpSeries1.StopRealTimeUpdate();
					tmpSeries1.pointValues = theGraph.GenRandomY(numPoints[0],theGraph.xAxisMinValue,theGraph.xAxisMaxValue,theGraph.yAxisMinValue,theGraph.yAxisMaxValue);
					tmpSeries1.seriesName = "Random Y";
				}
				if (theGraph.lineSeries.Count >= 2) {
					numPoints[1] = 7;
					WMG_Series tmpSeries2 = theGraph.lineSeries[1].GetComponent<WMG_Series>();
					tmpSeries2.StopRealTimeUpdate();
					tmpSeries2.pointValues = theGraph.GenRandomY(numPoints[1],theGraph.xAxisMinValue,theGraph.xAxisMaxValue,theGraph.yAxisMinValue,theGraph.yAxisMaxValue);
					tmpSeries2.seriesName = "Random Y";
				}
				if (theGraph.lineSeries.Count >= 3) {
					for (int j = 2; j < theGraph.lineSeries.Count; j++) {
						theGraph.SetActive(theGraph.lineSeries[j], true);
					}
				}
				
			}
			
			
			
			theGraph.SetActive(sliderNumPoints, true);
			theGraph.SetActive(sliderA, true);
			theGraph.SetActive(sliderB, true);
			theGraph.SetActive(sliderC, true);
			if (dropVal == "Linear") {
				type[currentSeries] = 0;
				if (!loadingPrevious) {
					a[currentSeries] = 5;
					b[currentSeries] = 0;
					c[currentSeries] = -25;
					OnSliderChangeA();
					OnSliderChangeB();
					OnSliderChangeC();
				}
				theGraph.SetActive(sliderC, false);
				theSeries.connectFirstToLast = false;
				refreshEveryFrame = true;
			}
			else if (dropVal == "Quadratic") {
				type[currentSeries] = 1;
				if (!loadingPrevious) {
					a[currentSeries] = 1;
					b[currentSeries] = 0;
					c[currentSeries] = -25;
					OnSliderChangeA();
					OnSliderChangeB();
					OnSliderChangeC();
				}
				theSeries.connectFirstToLast = false;
				refreshEveryFrame = true;
			}
			else if (dropVal == "Exponential") {
				type[currentSeries] = 2;
				if (!loadingPrevious) {
					a[currentSeries] = 0.5f;
					b[currentSeries] = 2;
					c[currentSeries] = -25;
					OnSliderChangeA();
					OnSliderChangeB();
					OnSliderChangeC();
				}
				theSeries.connectFirstToLast = false;
				refreshEveryFrame = true;
			}
			else if (dropVal == "Logarithmic") {
				type[currentSeries] = 3;
				if (!loadingPrevious) {
					a[currentSeries] = 20;
					b[currentSeries] = 2;
					c[currentSeries] = -25;
					OnSliderChangeA();
					OnSliderChangeB();
					OnSliderChangeC();
				}
				theSeries.connectFirstToLast = false;
				refreshEveryFrame = true;
			}
			else if (dropVal == "Circular") {
				type[currentSeries] = 4;
				if (!loadingPrevious) {
					a[currentSeries] = 0;
					b[currentSeries] = 0;
					c[currentSeries] = 5;
					OnSliderChangeA();
					OnSliderChangeB();
					OnSliderChangeC();
				}
				theSeries.connectFirstToLast = true;
				refreshEveryFrame = true;
			}
			else if (dropVal == "Random-XY") {
				type[currentSeries] = 5;
				setEquationText(currentSeries);
				theGraph.SetActive(sliderA, false);
				theGraph.SetActive(sliderB, false);
				theGraph.SetActive(sliderC, false);
				theSeries.connectFirstToLast = false;
				refreshEveryFrame = false;
				if (!loadingPrevious) theSeries.pointValues = theGraph.GenRandomXY(numPoints[currentSeries],theGraph.xAxisMinValue,theGraph.xAxisMaxValue,theGraph.yAxisMinValue,theGraph.yAxisMaxValue);
			}
			else if (dropVal == "Random-Y") {
				type[currentSeries] = 6;
				setEquationText(currentSeries);
				theGraph.SetActive(sliderA, false);
				theGraph.SetActive(sliderB, false);
				theGraph.SetActive(sliderC, false);
				theSeries.connectFirstToLast = false;
				refreshEveryFrame = false;
				if (!loadingPrevious) theSeries.pointValues = theGraph.GenRandomY(numPoints[currentSeries],theGraph.xAxisMinValue,theGraph.xAxisMaxValue,theGraph.yAxisMinValue,theGraph.yAxisMaxValue);
			}
		}
	}
	
	public void OnSelectionChange2() {
		string dropVal = theGraph.getDropdownSelection(seriesDrop);
		if (dropVal == "Lines & Points") {
			theSeries.hideLines = false;
			theSeries.hidePoints = false;
		}
		else if (dropVal == "Lines") {
			theSeries.hideLines = false;
			theSeries.hidePoints = true;
		}
		else if (dropVal == "Points") {
			theSeries.hideLines = true;
			theSeries.hidePoints = false;
		}
	}
	
	public void OnSelectionChange3() {
		string dropVal = theGraph.getDropdownSelection(gridDrop);
		if (dropVal == "No Grids") {
			theGraph.SetActive(theGraph.verticalGridLines, false);
			theGraph.SetActive(theGraph.horizontalGridLines, false);
		}
		else if (dropVal == "Grid-XY") {
			theGraph.SetActive(theGraph.verticalGridLines, true);
			theGraph.SetActive(theGraph.horizontalGridLines, true);
		}
		else if (dropVal == "Grid-X") {
			theGraph.SetActive(theGraph.verticalGridLines, false);
			theGraph.SetActive(theGraph.horizontalGridLines, true);
		}
		else if (dropVal == "Grid-Y") {
			theGraph.SetActive(theGraph.verticalGridLines, true);
			theGraph.SetActive(theGraph.horizontalGridLines, false);
		}
	}
	
	public void OnSelectionChange4() {
		string dropVal = theGraph.getDropdownSelection(axesDrop);
		if (dropVal == "Axes: I - IV") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.CENTER;
		}
		else if (dropVal == "Axes: I") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.I;
		}
		else if (dropVal == "Axes: II") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.II;
		}
		else if (dropVal == "Axes: III") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.III;
		}
		else if (dropVal == "Axes: IV") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.IV;
		}
		else if (dropVal == "Axes: I & II") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.I_II;
		}
		else if (dropVal == "Axes: III & IV") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.III_IV;
		}
		else if (dropVal == "Axes: II & III") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.II_III;
		}
		else if (dropVal == "Axes: I & IV") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.I_IV;
		}
		else if (dropVal == "Axes: Auto") {
			theGraph.axesType = WMG_Axis_Graph.axesTypes.AUTO_ORIGIN;
		}
	}
	
	public void OnSelectionChange5() {
		string dropVal = theGraph.getDropdownSelection(markerTypesDrop);
		if (dropVal == "Circle") {
			theSeries.pointPrefab = 0;
		}
		else if (dropVal == "Square") {
			theSeries.pointPrefab = 1;
		}
		else if (dropVal == "Triangle") {
			theSeries.pointPrefab = 2;
		}
	}
	
	public void OnSelectionChange6() {
		string dropVal = theGraph.getDropdownSelection(lineTypesDrop);
		if (dropVal == "Line: Normal") {
			theSeries.linkPrefab = 0;
		}
		else if (dropVal == "Line: Dotted") {
			theSeries.linkPrefab = 1;
		}
	}
	
	public void OnSelectionChange7() {
		string dropVal = theGraph.getDropdownSelection(showLabelsDrop);
		if (dropVal == "No Labels") {
			theGraph.hideXLabels = true;
			theGraph.hideYLabels = true;
		}
		else if (dropVal == "Labels-XY") {
			theGraph.hideXLabels = false;
			theGraph.hideYLabels = false;
		}
		else if (dropVal == "Labels-X") {
			theGraph.hideXLabels = false;
			theGraph.hideYLabels = true;
		}
		else if (dropVal == "Labels-Y") {
			theGraph.hideXLabels = true;
			theGraph.hideYLabels = false;
		}
	}
	
	public void OnSelectionChange12() {
		string dropVal = theGraph.getDropdownSelection(showTicksDrop);
		if (dropVal == "No Ticks") {
			theGraph.hideXTicks = true;
			theGraph.hideYTicks = true;
		}
		else if (dropVal == "Ticks-XY") {
			theGraph.hideXTicks = false;
			theGraph.hideYTicks = false;
		}
		else if (dropVal == "Ticks-X") {
			theGraph.hideXTicks = false;
			theGraph.hideYTicks = true;
		}
		else if (dropVal == "Ticks-Y") {
			theGraph.hideXTicks = true;
			theGraph.hideYTicks = false;
		}
	}
	
	public void OnSelectionChange8() {
		string dropVal = theGraph.getDropdownSelection(graphTypeDrop);
		if (dropVal == "Line") {
			theGraph.graphType = WMG_Axis_Graph.graphTypes.line;
		}
		else if (dropVal == "Bar - Side") {
			theGraph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		}
		else if (dropVal == "Bar - Stacked") {
			theGraph.graphType = WMG_Axis_Graph.graphTypes.bar_stacked;
		}
		else if (dropVal == "Bar - Percent") {
			theGraph.graphType = WMG_Axis_Graph.graphTypes.bar_stacked_percent;
		}
	}
	
	public void OnSelectionChange9() {
		string dropVal = theGraph.getDropdownSelection(vertHorizDrop);
		if (dropVal == "Vertical") {
			theGraph.orientationType = WMG_Axis_Graph.orientationTypes.vertical;
		}
		else if (dropVal == "Horizontal") {
			theGraph.orientationType = WMG_Axis_Graph.orientationTypes.horizontal;
		}
	}
	
	public void OnSelectionChange10() {
		string dropVal = theGraph.getDropdownSelection(seriesSelectionDrop);
		int seriesNum = -1;
		int.TryParse(dropVal.Substring(7), out seriesNum);
		if (currentSeries != seriesNum - 1) {
			loadSeriesData(seriesNum - 1);
		}
	}
	
	private void setAutoSpaceX(bool val) {
		theGraph.setToggle(checkBoxAutoSpace, val);
		for (int j = 0; j < theGraph.lineSeries.Count; j++) {
			if (!theGraph.activeInHierarchy(theGraph.lineSeries[j])) continue;
			WMG_Series aSeries = theGraph.lineSeries[j].GetComponent<WMG_Series>();
			aSeries.AutoUpdateXDistBetween = val;
			aSeries.UseXDistBetweenToSpace = val;
		}
	}
	
	public void OnCheckboxChange0() {
		theGraph.xMinMaxAutoGrow[1] = theGraph.getToggle(checkBoxes[0]);
	}
	
	public void OnCheckboxChange1() {
		theGraph.xMinMaxAutoGrow[0] = theGraph.getToggle(checkBoxes[1]);
	}
	
	public void OnCheckboxChange2() {
		theGraph.yMinMaxAutoGrow[1] = theGraph.getToggle(checkBoxes[2]);
	}
	
	public void OnCheckboxChange3() {
		theGraph.yMinMaxAutoGrow[0] = theGraph.getToggle(checkBoxes[3]);
	}
	
	public void OnCheckboxChange4() {;
		theGraph.xMinMaxAutoShrink[1] = theGraph.getToggle(checkBoxes[4]);
	}
	
	public void OnCheckboxChange5() {
		theGraph.xMinMaxAutoShrink[0] = theGraph.getToggle(checkBoxes[5]);
	}
	
	public void OnCheckboxChange6() {
		theGraph.yMinMaxAutoShrink[1] = theGraph.getToggle(checkBoxes[6]);
	}
	
	public void OnCheckboxChange7() {
		theGraph.yMinMaxAutoShrink[0] = theGraph.getToggle(checkBoxes[7]);
	}
	
	public void OnCheckboxChangeAutoSpace() {
		for (int j = 0; j < theGraph.lineSeries.Count; j++) {
			if (!theGraph.activeInHierarchy(theGraph.lineSeries[j])) continue;
			WMG_Series aSeries = theGraph.lineSeries[j].GetComponent<WMG_Series>();
			aSeries.AutoUpdateXDistBetween = theGraph.getToggle(checkBoxAutoSpace);
			aSeries.UseXDistBetweenToSpace = theGraph.getToggle(checkBoxAutoSpace);
		}
	}
	
	public void UpdateCheckboxStates() {
		if (!graphSettingsSelected) return;
		theGraph.setToggle(checkBoxes[0], theGraph.xMinMaxAutoGrow[1]);
		theGraph.setToggle(checkBoxes[1], theGraph.xMinMaxAutoGrow[0]);
		theGraph.setToggle(checkBoxes[2], theGraph.yMinMaxAutoGrow[1]);
		theGraph.setToggle(checkBoxes[3], theGraph.yMinMaxAutoGrow[0]);
		theGraph.setToggle(checkBoxes[4], theGraph.xMinMaxAutoShrink[1]);
		theGraph.setToggle(checkBoxes[5], theGraph.xMinMaxAutoShrink[0]);
		theGraph.setToggle(checkBoxes[6], theGraph.yMinMaxAutoShrink[1]);
		theGraph.setToggle(checkBoxes[7], theGraph.yMinMaxAutoShrink[0]);
		
		bool allSeriesAutoSpace = true;
		for (int j = 0; j < theGraph.lineSeries.Count; j++) {
			if (!theGraph.activeInHierarchy(theGraph.lineSeries[j])) continue;
			WMG_Series aSeries = theGraph.lineSeries[j].GetComponent<WMG_Series>();
			if (!aSeries.AutoUpdateXDistBetween) allSeriesAutoSpace = false;
		}
		theGraph.setToggle(checkBoxAutoSpace, allSeriesAutoSpace);
	}
	
	public void unfocusAll() {
		theGraph.unfocusControl(dataGenTypesDrop);
		theGraph.unfocusControl(graphTypeDrop);
		theGraph.unfocusControl(gridDrop);
		theGraph.unfocusControl(axesDrop);
		theGraph.unfocusControl(seriesDrop);
		theGraph.unfocusControl(markerTypesDrop);
		theGraph.unfocusControl(lineTypesDrop);
		theGraph.unfocusControl(seriesSelectionDrop);
		theGraph.unfocusControl(showLabelsDrop);
		theGraph.unfocusControl(vertHorizDrop);
		theGraph.unfocusControl(animationType);
		theGraph.unfocusControl(animationEaseType);
		theGraph.unfocusControl(showTicksDrop);
	}
	
	
	public void OnGraphSettingsClick() {
		if (!graphSettingsSelected) {
			theGraph.setButtonColor(Color.green, buttonGraphLabel);
			theGraph.setButtonColor(Color.white, buttonSeriesLabel);
			theGraph.setButtonColor(Color.white, buttonAnimationsLabel);
			theGraph.SetActive(UIParentGraphSettings, true);
			theGraph.SetActive(UIParentSeriesSettings, false);
			theGraph.SetActive(UIParentAnimationSettings, false);
			theGraph.yAxisLength = axisGraphClicked;
			theGraph.backgroundPaddingTop = backGraphClicked;
			graphSettingsSelected = true;
		}
		else {
			theGraph.setButtonColor(Color.white, buttonGraphLabel);
			theGraph.setButtonColor(Color.white, buttonSeriesLabel);
			theGraph.setButtonColor(Color.white, buttonAnimationsLabel);
			theGraph.SetActive(UIParentGraphSettings, false);
			theGraph.SetActive(UIParentSeriesSettings, false);
			theGraph.SetActive(UIParentAnimationSettings, false);
			theGraph.yAxisLength = axisNoneClicked;
			theGraph.backgroundPaddingTop = backNoneClicked;
			graphSettingsSelected = false;
		}
		seriesSettingsSelected = false;
		animationSettingsSelected = false;
	}
	
	public void OnSeriesSettingsClick() {
		if (!seriesSettingsSelected) {
			theGraph.setButtonColor(Color.white, buttonGraphLabel);
			theGraph.setButtonColor(Color.green, buttonSeriesLabel);
			theGraph.setButtonColor(Color.white, buttonAnimationsLabel);
			theGraph.SetActive(UIParentGraphSettings, false);
			theGraph.SetActive(UIParentSeriesSettings, true);
			theGraph.SetActive(UIParentAnimationSettings, false);
			theGraph.yAxisLength = axisSeriesClicked;
			theGraph.backgroundPaddingTop = backSeriesClicked;
			seriesSettingsSelected = true;
		}
		else {
			theGraph.setButtonColor(Color.white, buttonGraphLabel);
			theGraph.setButtonColor(Color.white, buttonSeriesLabel);
			theGraph.setButtonColor(Color.white, buttonAnimationsLabel);
			theGraph.SetActive(UIParentGraphSettings, false);
			theGraph.SetActive(UIParentSeriesSettings, false);
			theGraph.SetActive(UIParentAnimationSettings, false);
			theGraph.yAxisLength = axisNoneClicked;
			theGraph.backgroundPaddingTop = backNoneClicked;
			seriesSettingsSelected = false;
		}
		graphSettingsSelected = false;
		animationSettingsSelected = false;
	}
	
	public void OnAnimationSettingsClick() {
		if (!animationSettingsSelected) {
			theGraph.setButtonColor(Color.white, buttonGraphLabel);
			theGraph.setButtonColor(Color.white, buttonSeriesLabel);
			theGraph.setButtonColor(Color.green, buttonAnimationsLabel);
			theGraph.SetActive(UIParentGraphSettings, false);
			theGraph.SetActive(UIParentSeriesSettings, false);
			theGraph.SetActive(UIParentAnimationSettings, true);
			theGraph.yAxisLength = axisAnimClicked;
			theGraph.backgroundPaddingTop = backAnimClicked;
			animationSettingsSelected = true;
		}
		else {
			theGraph.setButtonColor(Color.white, buttonGraphLabel);
			theGraph.setButtonColor(Color.white, buttonSeriesLabel);
			theGraph.setButtonColor(Color.white, buttonAnimationsLabel);
			theGraph.SetActive(UIParentGraphSettings, false);
			theGraph.SetActive(UIParentSeriesSettings, false);
			theGraph.SetActive(UIParentAnimationSettings, false);
			theGraph.yAxisLength = axisNoneClicked;
			theGraph.backgroundPaddingTop = backNoneClicked;
			animationSettingsSelected = false;
		}
		graphSettingsSelected = false;
		seriesSettingsSelected = false;
	}
	
	public void OnSliderChangeNumPoints() {
		int theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderNumPoints)*100) - 50;
		theGraph.setSliderVal(sliderNumPoints, 0.5f);
		numPoints[currentSeries] += Mathf.RoundToInt(theNum/30);
		if (numPoints[currentSeries] < 2) numPoints[currentSeries] = 2;
		if (numPoints[currentSeries] > 1000) numPoints[currentSeries] = 1000;
		theGraph.changeLabelText(sliderNumPointsLabel, numPoints[currentSeries].ToString());
	}
	
	public void OnSliderChangeA() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderA)*100)/2f - 25;
		theGraph.setSliderVal(sliderA, 0.5f);
		a[currentSeries] += theNum/30f;
		a[currentSeries] = Mathf.RoundToInt(a[currentSeries]*10)/10f;
		if (a[currentSeries] < -100) a[currentSeries] = -100;
		if (a[currentSeries] > 100) a[currentSeries] = 100;
		theGraph.changeLabelText(sliderALabel, a[currentSeries].ToString());
		setEquationText(currentSeries);
	}
	
	public void OnSliderChangeB() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderB)*100)/2f - 25;
		theGraph.setSliderVal(sliderB, 0.5f);
		b[currentSeries] += theNum/30f;
		b[currentSeries] = Mathf.RoundToInt(b[currentSeries]*10)/10f;
		if (b[currentSeries] < -100) b[currentSeries] = -100;
		if (b[currentSeries] > 100) b[currentSeries] = 100;
		theGraph.changeLabelText(sliderBLabel, b[currentSeries].ToString());
		setEquationText(currentSeries);
	}
	
	public void OnSliderChangeC() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderC)*100)/2f - 25;
		theGraph.setSliderVal(sliderC, 0.5f);
		c[currentSeries] += theNum/30f;
		c[currentSeries] = Mathf.RoundToInt(c[currentSeries]*10)/10f;
		if (c[currentSeries] < -100) c[currentSeries] = -100;
		if (c[currentSeries] > 100) c[currentSeries] = 100;
		theGraph.changeLabelText(sliderCLabel, c[currentSeries].ToString());
		setEquationText(currentSeries);
	}
	
	private void setEquationText(int seriesNum) {
		string seriesName = "";
		if (type[seriesNum] == 0) {
			seriesName = "Y = " + a[seriesNum].ToString() + "X";
			if (b[seriesNum] < 0) seriesName += " - " + Mathf.Abs(b[seriesNum]).ToString();
			if (b[seriesNum] > 0) seriesName += " + " + Mathf.Abs(b[seriesNum]).ToString();
		}
		else if (type[seriesNum] == 1) {
			seriesName = "Y = " + a[seriesNum].ToString() + "X^2";
			if (b[seriesNum] < 0) seriesName += " - " + Mathf.Abs(b[seriesNum]).ToString() + "X";
			if (b[seriesNum] > 0) seriesName += " + " + Mathf.Abs(b[seriesNum]).ToString() + "X";
			if (c[seriesNum] < 0) seriesName += " - " + Mathf.Abs(c[seriesNum]).ToString();
			if (c[seriesNum] > 0) seriesName += " + " + Mathf.Abs(c[seriesNum]).ToString();
		}
		else if (type[seriesNum] == 2) {
			seriesName = "Y = " + a[seriesNum].ToString() + "(" + b[seriesNum].ToString() + ")^X";
			if (c[seriesNum] < 0) seriesName += " - " + Mathf.Abs(c[seriesNum]).ToString();
			if (c[seriesNum] > 0) seriesName += " + " + Mathf.Abs(c[seriesNum]).ToString();
		}
		else if (type[seriesNum] == 3) {
			seriesName = "Y = " + a[seriesNum].ToString() + "Log(base" + b[seriesNum].ToString() + ") X";
			if (c[seriesNum] < 0) seriesName += " - " + Mathf.Abs(c[seriesNum]).ToString();
			if (c[seriesNum] > 0) seriesName += " + " + Mathf.Abs(c[seriesNum]).ToString();
		}
		else if (type[seriesNum] == 4) {
			seriesName = "(X - " + a[seriesNum].ToString() + ")^2 + (Y - " + b[seriesNum].ToString() + ")^2 = (" + c[seriesNum].ToString() + ")^2";
		}
		else if (type[seriesNum] == 5) {
			seriesName = "Random XY";
		}
		else if (type[seriesNum] == 6) {
			seriesName = "Random Y";
		}
		
		WMG_Series tmpSeries = theGraph.lineSeries[seriesNum].GetComponent<WMG_Series>();
		tmpSeries.seriesName = seriesName;
		
		if (runningRealTimeEx) {
			if (theGraph.lineSeries.Count >= 1) {
				WMG_Series tmpSeries1 = theGraph.lineSeries[0].GetComponent<WMG_Series>();
				tmpSeries1.seriesName = "Hexagon X";
			}
			if (theGraph.lineSeries.Count >= 2) {
				WMG_Series tmpSeries2 = theGraph.lineSeries[1].GetComponent<WMG_Series>();
				tmpSeries2.seriesName = "Hexagon Y";
			}
		}
	}
	
	public void OnSliderChangeLinePadding() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderLinePadding)*10) - 5;
		theGraph.setSliderVal(sliderLinePadding, 0.5f);
		float tmp = theSeries.linePadding;
		tmp += theNum/10f;
		tmp = Mathf.RoundToInt(tmp*10)/10f;
		if (tmp < -10) tmp = -10;
		if (tmp > 10) tmp = 10;
		theSeries.linePadding = tmp;
		theGraph.changeLabelText(sliderLinePaddingLabel, tmp.ToString());
	}
	
	public void OnSliderChangeMarkerSize() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderMarkerSize)*20) - 10;
		theGraph.setSliderVal(sliderMarkerSize, 0.5f);
		float tmp = theSeries.pointWidthHeight;
		tmp += theNum/20f;
		tmp = Mathf.RoundToInt(tmp*10)/10f;
		if (tmp < 0) tmp = 0;
		if (tmp > 20) tmp = 20;
		theSeries.pointWidthHeight = tmp;
		theGraph.changeLabelText(sliderMarkerSizeLabel, tmp.ToString());
	}
	
	public void OnSliderChangeLineWidth() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderLineWidth)*10) - 5;
		theGraph.setSliderVal(sliderLineWidth, 0.5f);
		float tmp = theSeries.lineScale;
		tmp += theNum/10f;
		tmp = Mathf.RoundToInt(tmp*10)/10f;
		if (tmp < 0) tmp = 0;
		if (tmp > 10) tmp = 10;
		theSeries.lineScale = tmp;
		theGraph.changeLabelText(sliderLineWidthLabel, tmp.ToString());
	}
	
	public void OnSliderChangeAnimationDuration() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderAnimationDuration)*10) - 5;
		theGraph.setSliderVal(sliderAnimationDuration, 0.5f);
		float tmp = animationDuration;
		tmp += theNum/10f;
		tmp = Mathf.RoundToInt(tmp*10)/10f;
		if (tmp < 0.1f) tmp = 0.1f;
		if (tmp > 10) tmp = 10;
		animationDuration = tmp;
		theGraph.changeLabelText(sliderAnimationDurationLabel, tmp.ToString());
	}
	
	private void loadSeriesData(int seriesNum) {
		currentSeries = seriesNum;
		theSeries = theGraph.lineSeries[currentSeries].GetComponent<WMG_Series>();
		loadingPrevious = true;
		
		if (type[currentSeries] == 0) {
			theGraph.setDropdownSelection(dataGenTypesDrop, "Linear");
		}
		else if (type[currentSeries] == 1) {
			theGraph.setDropdownSelection(dataGenTypesDrop, "Quadratic");
		}
		else if (type[currentSeries] == 2) {
			theGraph.setDropdownSelection(dataGenTypesDrop, "Exponential");
		}
		else if (type[currentSeries] == 3) {
			theGraph.setDropdownSelection(dataGenTypesDrop, "Logarithmic");
		}
		else if (type[currentSeries] == 4) {
			theGraph.setDropdownSelection(dataGenTypesDrop, "Circular");
		}
		else if (type[currentSeries] == 5) {
			theGraph.setDropdownSelection(dataGenTypesDrop, "Random-XY");
		}
		else if (type[currentSeries] == 6) {
			theGraph.setDropdownSelection(dataGenTypesDrop, "Random-Y");
		}
		else if (runningRealTimeEx) {
			theGraph.setDropdownSelection(dataGenTypesDrop, "Real-Time Ex.");
		}
		
		OnSelectionChange();
		
		loadingPrevious = false;
		
		OnSliderChangeNumPoints();
		OnSliderChangeA();
		OnSliderChangeB();
		OnSliderChangeC();
		OnSliderChangeMarkerSize();
		OnSliderChangeLineWidth();
		
		if (!theSeries.hideLines && !theSeries.hidePoints) {
			theGraph.setDropdownSelection(seriesDrop, "Lines & Points");
		}
		else if (!theSeries.hideLines && theSeries.hidePoints) {
			theGraph.setDropdownSelection(seriesDrop, "Lines");
		}
		else if (theSeries.hideLines && !theSeries.hidePoints) {
			theGraph.setDropdownSelection(seriesDrop, "Points");
		}
		
		if (theSeries.pointPrefab == 0) {
			theGraph.setDropdownSelection(markerTypesDrop, "Circle");
		}
		else if (theSeries.pointPrefab == 1) {
			theGraph.setDropdownSelection(markerTypesDrop, "Square");
		}
		else if (theSeries.pointPrefab == 2) {
			theGraph.setDropdownSelection(markerTypesDrop, "Triangle");
		}
		
		if (theSeries.linkPrefab == 0) {
			theGraph.setDropdownSelection(lineTypesDrop, "Line: Normal");
		}
		else if (theSeries.linkPrefab == 1) {
			theGraph.setDropdownSelection(lineTypesDrop, "Line: Dotted");
		}
	}
	
	public void OnSliderChangeXMax() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderXMax)*100) - 50;
		theGraph.setSliderVal(sliderXMax, 0.5f);
		float tmp = theGraph.xAxisMaxValue;
		tmp += Mathf.RoundToInt(theNum/30);
		if (tmp <= theGraph.xAxisMinValue) tmp = theGraph.xAxisMinValue+1;
		theGraph.xAxisMaxValue = tmp;
		tmp = Mathf.RoundToInt(tmp*10)/10f;
		theGraph.changeLabelText(sliderXMaxLabel, tmp.ToString());
	}
	
	public void OnSliderChangeXMin() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderXMin)*100) - 50;
		theGraph.setSliderVal(sliderXMin, 0.5f);
		float tmp = theGraph.xAxisMinValue;
		tmp += Mathf.RoundToInt(theNum/30);
		if (tmp >= theGraph.xAxisMaxValue) tmp = theGraph.xAxisMaxValue-1;
		theGraph.xAxisMinValue = tmp;
		tmp = Mathf.RoundToInt(tmp*10)/10f;
		theGraph.changeLabelText(sliderXMinLabel, tmp.ToString());
	}
	
	public void OnSliderChangeYMax() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderYMax)*100) - 50;
		theGraph.setSliderVal(sliderYMax, 0.5f);
		float tmp = theGraph.yAxisMaxValue;
		tmp += Mathf.RoundToInt(theNum/30);
		if (tmp <= theGraph.yAxisMinValue) tmp = theGraph.yAxisMinValue+1;
		theGraph.yAxisMaxValue = tmp;
		tmp = Mathf.RoundToInt(tmp*10)/10f;
		theGraph.changeLabelText(sliderYMaxLabel, tmp.ToString());
	}
	
	public void OnSliderChangeYMin() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderYMin)*100) - 50;
		theGraph.setSliderVal(sliderYMin, 0.5f);
		float tmp = theGraph.yAxisMinValue;
		tmp += Mathf.RoundToInt(theNum/30);
		if (tmp >= theGraph.yAxisMaxValue) tmp = theGraph.yAxisMaxValue-1;
		theGraph.yAxisMinValue = tmp;
		tmp = Mathf.RoundToInt(tmp*10)/10f;
		theGraph.changeLabelText(sliderYMinLabel, tmp.ToString());
	}
	
	public void OnSliderChangeXTicks() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderXTicks)*10) - 5;
		theGraph.setSliderVal(sliderXTicks, 0.5f);
		int tmp = theGraph.xAxisNumTicks;
		tmp += Mathf.RoundToInt(theNum/2);
		if (tmp < 2) tmp = 2;
		if (tmp > 30) tmp = 30;
		theGraph.xAxisNumTicks = tmp;
		theGraph.changeLabelText(sliderXTicksLabel, tmp.ToString());
	}
	
	public void OnSliderChangeYTicks() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderYTicks)*10) - 5;
		theGraph.setSliderVal(sliderYTicks, 0.5f);
		int tmp = theGraph.yAxisNumTicks;
		tmp += Mathf.RoundToInt(theNum/2);
		if (tmp < 2) tmp = 2;
		if (tmp > 30) tmp = 30;
		theGraph.yAxisNumTicks = tmp;
		theGraph.changeLabelText(sliderYTicksLabel, tmp.ToString());
	}
	
	public void OnSliderChangeNumDecimals() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderNumDecimals)*10) - 5;
		theGraph.setSliderVal(sliderNumDecimals, 0.5f);
		int tmp = theGraph.numDecimalsXAxisLabels;
		tmp += Mathf.RoundToInt(theNum/2);
		if (tmp < 0) tmp = 0;
		if (tmp > 5) tmp = 5;
		theGraph.numDecimalsXAxisLabels = tmp;
		theGraph.numDecimalsYAxisLabels = tmp;
		theGraph.changeLabelText(sliderNumDecimalsLabel, tmp.ToString());
	}
	
	public void OnSliderChangeFontSize() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderFontSize)*2) - 1;
		theGraph.setSliderVal(sliderFontSize, 0.5f);
		float tmp = theGraph.xAxisLabelSize;
		tmp += theNum/100f;
		tmp = Mathf.RoundToInt(tmp*100)/100f;
		if (tmp < 0) tmp = 0;
		if (tmp > 1) tmp = 1;
		theGraph.xAxisLabelSize = tmp;
		theGraph.yAxisLabelSize = tmp;
		theGraph.changeLabelText(sliderFontSizeLabel, tmp.ToString());
	}
	
	public void OnSliderChangeAxisWidth() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderAxisWidth)*10) - 5;
		theGraph.setSliderVal(sliderAxisWidth, 0.5f);
		int tmp = theGraph.axisWidth;
		tmp += Mathf.RoundToInt(theNum/2);
		if (tmp < 2) tmp = 2;
		if (tmp > 6) tmp = 6;
		theGraph.axisWidth = tmp;
		theGraph.changeLabelText(sliderAxisWidthLabel, tmp.ToString());
	}
	
	public void OnSliderChangeBarWidth() {
		float theNum = Mathf.RoundToInt(theGraph.getSliderVal(sliderBarWidth)*10) - 5;
		theGraph.setSliderVal(sliderBarWidth, 0.5f);
		int tmp = Mathf.RoundToInt(theGraph.barWidth);
		tmp += Mathf.RoundToInt(theNum/2);
		if (tmp < 2) tmp = 2;
		if (tmp > 100) tmp = 100;
		theGraph.barWidth = tmp;
		theGraph.changeLabelText(sliderBarWidthLabel, tmp.ToString());
	}
	
	private void UpdateLineWidthLabel() {
		theGraph.changeLabelText(sliderLineWidthLabel, theSeries.lineScale.ToString());
	}
	
	public void OnSelectionChange11() {
		string dropVal = theGraph.getDropdownSelection(animationType);
		
		bool newAutoSpaceX = false;
		
		if (dropVal == "Line: All - Center") {
			animationDuration = 1;
		}
		else if (dropVal == "Line: All - Left") {
			animationDuration = 1;
		}
		else if (dropVal == "Line: All - Right") {
			animationDuration = 1;
		}
		else if (dropVal == "Line: Series - Center") {
			animationDuration = 2;
		}
		else if (dropVal == "Line: Series - Left") {
			animationDuration = 2;
		}
		else if (dropVal == "Line: Series - Right") {
			animationDuration = 2;
		}
		else if (dropVal == "Line: Point - Center") {
			animationDuration = 4;
		}
		else if (dropVal == "Line: Point - Left") {
			animationDuration = 4;
		}
		else if (dropVal == "Line: Point - Right") {
			animationDuration = 4;
		}
		else if (dropVal == "Bar: All") {
			animationDuration = 1;
			newAutoSpaceX = true;
		}
		else if (dropVal == "Bar: Series") {
			animationDuration = 2;
			newAutoSpaceX = true;
		}
		else if (dropVal == "Bar: Point") {
			animationDuration = 4;
			newAutoSpaceX = true;
		}
		
		if (newAutoSpaceX) {
			if (theGraph.graphType == WMG_Axis_Graph.graphTypes.line) {
				theGraph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
				theGraph.setDropdownIndex(graphTypeDrop, 1);
			}
		}
		else {
			theGraph.graphType = WMG_Axis_Graph.graphTypes.line;
			theGraph.setDropdownIndex(graphTypeDrop, 0);
		}
		
		setAutoSpaceX(newAutoSpaceX);
		OnSliderChangeAnimationDuration();
	}
	
	public void OnAnimationPlayClick() {
		
		if (!isAnimating) {
			
			isAnimating = true;
			
			// Set animation parameters: duration, and ease type
			setEaseTypeFromDropdown();
			
			// Get before and after scale vectors for each series. Sometimes we need to use series data (line widths).
			List<Vector3> beforeScaleLine = theGraph.getSeriesScaleVectors(true, -1, 0);
			List<Vector3> afterScaleLine = theGraph.getSeriesScaleVectors(true, -1, 1);
			List<Vector3> beforeScalePoint = theGraph.getSeriesScaleVectors(false, 0, 0);
			List<Vector3> afterScalePoint = theGraph.getSeriesScaleVectors(false, 1, 1);
			List<Vector3> beforeScaleBar;
			if (theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical) beforeScaleBar = theGraph.getSeriesScaleVectors(false, 1, 0);
			else beforeScaleBar = theGraph.getSeriesScaleVectors(false, 0, 1);
			
			// Play various example animations using API based on animation dropdown
			string dropVal = theGraph.getDropdownSelection(animationType);
			if (dropVal == "Line: All - Center") {
				theGraph.changeAllLinePivots(WMG_Graph_Manager.WMGpivotTypes.Center);
				theGraph.animScaleAllAtOnce(false, animationDuration, 0, easeType, beforeScaleLine, afterScaleLine);
				theGraph.animScaleAllAtOnce(true, 0, animationDuration, easeType, beforeScalePoint, afterScalePoint);
			}
			else if (dropVal == "Line: All - Left") {
				theGraph.changeAllLinePivots(WMG_Graph_Manager.WMGpivotTypes.Top);
				theGraph.animScaleAllAtOnce(false, animationDuration, 0, easeType, beforeScaleLine, afterScaleLine);
				theGraph.animScaleAllAtOnce(true, 0, animationDuration, easeType, beforeScalePoint, afterScalePoint);
			}
			else if (dropVal == "Line: All - Right") {
				theGraph.changeAllLinePivots(WMG_Graph_Manager.WMGpivotTypes.Bottom);
				theGraph.animScaleAllAtOnce(false, animationDuration, 0, easeType, beforeScaleLine, afterScaleLine);
				theGraph.animScaleAllAtOnce(true, 0, animationDuration, easeType, beforeScalePoint, afterScalePoint);
			}
			else if (dropVal == "Line: Series - Center") {
				theGraph.changeAllLinePivots(WMG_Graph_Manager.WMGpivotTypes.Center);
				theGraph.animScaleBySeries(false, animationDuration, 0, easeType, beforeScaleLine, afterScaleLine);
				theGraph.animScaleBySeries(true, 0, animationDuration, easeType, beforeScalePoint, afterScalePoint);
			}
			else if (dropVal == "Line: Series - Left") {
				theGraph.changeAllLinePivots(WMG_Graph_Manager.WMGpivotTypes.Top);
				theGraph.animScaleBySeries(false, animationDuration, 0, easeType, beforeScaleLine, afterScaleLine);
				theGraph.animScaleBySeries(true, 0, animationDuration, easeType, beforeScalePoint, afterScalePoint);
			}
			else if (dropVal == "Line: Series - Right") {
				theGraph.changeAllLinePivots(WMG_Graph_Manager.WMGpivotTypes.Bottom);
				theGraph.animScaleBySeries(false, animationDuration, 0, easeType, beforeScaleLine, afterScaleLine);
				theGraph.animScaleBySeries(true, 0, animationDuration, easeType, beforeScalePoint, afterScalePoint);
			}
			else if (dropVal == "Line: Point - Center") {
				theGraph.changeAllLinePivots(WMG_Graph_Manager.WMGpivotTypes.Center);
				theGraph.animScaleOneByOne(false, animationDuration, 0, easeType, beforeScaleLine, afterScaleLine, 2);
				theGraph.animScaleOneByOne(true, 0, animationDuration, easeType, beforeScalePoint, afterScalePoint, 2);
			}
			else if (dropVal == "Line: Point - Left") {
				theGraph.changeAllLinePivots(WMG_Graph_Manager.WMGpivotTypes.Top);
				theGraph.animScaleOneByOne(false, animationDuration, 0, easeType, beforeScaleLine, afterScaleLine, 0);
				theGraph.animScaleOneByOne(true, 0, animationDuration, easeType, beforeScalePoint, afterScalePoint, 0);
			}
			else if (dropVal == "Line: Point - Right") {
				theGraph.changeAllLinePivots(WMG_Graph_Manager.WMGpivotTypes.Bottom);
				theGraph.animScaleOneByOne(false, animationDuration, 0, easeType, beforeScaleLine, afterScaleLine, 1);
				theGraph.animScaleOneByOne(true, 0, animationDuration, easeType, beforeScalePoint, afterScalePoint, 1);
			}
			else if (dropVal == "Bar: All") {
				theGraph.animScaleAllAtOnce(true, animationDuration, 0, easeType, beforeScaleBar, afterScalePoint);
			}
			else if (dropVal == "Bar: Series") {
				theGraph.animScaleBySeries(true, animationDuration, 0, easeType, beforeScaleBar, afterScalePoint);
			}
			else if (dropVal == "Bar: Point") {
				theGraph.animScaleOneByOne(true, animationDuration, 0, easeType, beforeScaleBar, afterScalePoint, 0);
			}
			
			HOTween.To(this.transform, 0, new TweenParms()
		            .Prop("localScale", Vector3.one, false)
					.Delay(animationDuration)
					.OnComplete(setIsAnimating)
		        );
			
		}
		
	}
	
	void setIsAnimating() {
		isAnimating = false;
	}
	
	void setEaseTypeFromDropdown() {
		string dropVal = theGraph.getDropdownSelection(animationEaseType);
		// There are more, but these are different enough for the example
		if (dropVal == "Linear") {
			easeType = EaseType.Linear;
		}
		else if (dropVal == "In Quad") {
			easeType = EaseType.EaseInQuad;
		}
		else if (dropVal == "Out Quad") {
			easeType = EaseType.EaseOutQuad;
		}
		else if (dropVal == "In Elastic") {
			easeType = EaseType.EaseInElastic;
		}
		else if (dropVal == "Out Elastic") {
			easeType = EaseType.EaseOutElastic;
		}
		else if (dropVal == "In Bounce") {
			easeType = EaseType.EaseInBounce;
		}
		else if (dropVal == "Out Bounce") {
			easeType = EaseType.EaseOutBounce;
		}
	}
	
	public void OnAddSeriesClick() {
		if (theGraph.lineSeries.Count == 10) return;
		WMG_Series addSeries = theGraph.addSeries();
		addSeries.pointValues = theGraph.GenRandomY(numPoints[currentSeries],minX,maxX,minY,maxY);
		addSeries.lineColor = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),1);
		addSeries.pointColor = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),1);
		addSeries.linePadding = 0.5f;
		addSeries.AutoUpdateXDistBetween = theGraph.getToggle(checkBoxAutoSpace);
		addSeries.UseXDistBetweenToSpace = theGraph.getToggle(checkBoxAutoSpace);
		theGraph.addDropdownItem(seriesSelectionDrop, "Series " + theGraph.lineSeries.Count);
		numPoints.Add(7);
		a.Add(0);
		b.Add(0);
		c.Add(0);
		cachedNumPoints.Add(7);
		type.Add(6);
		setEquationText(theGraph.lineSeries.Count-1);
		theGraph.setDropdownIndex(seriesSelectionDrop, theGraph.lineSeries.Count - 1);
		theGraph.setDropdownSelection(seriesSelectionDrop, "Series " + theGraph.lineSeries.Count);
		centerLegendEntries();
	}
	
	public void OnDeleteSeriesClick() {
		if (theGraph.lineSeries.Count == 1) return;
		theGraph.deleteSeries();
		theGraph.deleteDropdownItem(seriesSelectionDrop);
		numPoints.RemoveAt(theGraph.lineSeries.Count);
		a.RemoveAt(theGraph.lineSeries.Count);
		b.RemoveAt(theGraph.lineSeries.Count);
		c.RemoveAt(theGraph.lineSeries.Count);
		cachedNumPoints.RemoveAt(theGraph.lineSeries.Count);
		type.RemoveAt(theGraph.lineSeries.Count);
		theGraph.setDropdownIndex(seriesSelectionDrop, theGraph.lineSeries.Count - 1);
		theGraph.setDropdownSelection(seriesSelectionDrop, "Series " + theGraph.lineSeries.Count);
		centerLegendEntries();
	}
	
	private void centerLegendEntries() {
		float totalWidth = (theGraph.lineSeries.Count-1) * theGraph.legendEntrySpacingX;
		float legendOffset = 0;
		WMG_Series firstSeries = theGraph.lineSeries[0].GetComponent<WMG_Series>();
		WMG_Node cObj = firstSeries.getLegendParent().GetComponent<WMG_Node>();
		legendOffset = theGraph.getSpriteWidth(cObj.objectToLabel) + theGraph.getSpritePositionX(cObj.objectToLabel);
		legendOffset = firstSeries.legendEntryLinkSpacing - 0.5f * legendOffset;
		if (theGraph.hideLegendLabels) legendOffset = 0;
		theGraph.legendParentOffsetX = -0.5f * (totalWidth) + legendOffset;
	}
	
	private void setTooltipPosition( Vector2 position ) {
		toolTipPanel.GetComponent<UIWidget>().transform.localPosition = new Vector3(position.x - 70, position.y - 35, 0);
	}
	
	public void MyMouseEnter(WMG_Series aSeries, WMG_Node aNode, bool state) {
		if (state) {
			Vector2 nodeData = aSeries.getNodeValue(aNode);
			int numDecimals = 2;
			float numberToMult = Mathf.Pow(10f, numDecimals);
			string nodeX = (Mathf.Round(nodeData.x*numberToMult)/numberToMult).ToString();
			string nodeY = (Mathf.Round(nodeData.y*numberToMult)/numberToMult).ToString();
			int seriesNum = -1;
			int.TryParse(aSeries.name.Substring(6), out seriesNum);
			
			// Set the text
			theGraph.changeLabelText(toolTipLabel, "Series " + seriesNum + ": (" + nodeX + ", " + nodeY + ")");
			
			// Display the base panel
			theGraph.showControl(toolTipPanel);
			theGraph.bringSpriteToFront(toolTipPanel);
			
			Vector3 newVec = new Vector3(2,2,1);
			if (theGraph.graphType != WMG_Axis_Graph.graphTypes.line) {
				if (theGraph.orientationType == WMG_Axis_Graph.orientationTypes.vertical) {
					newVec = new Vector3(1,1.1f,1);
				}
				else {
					newVec = new Vector3(1.1f,1,1);
				}
			}
			
			HOTween.To(aNode.transform, 0.5f, new TweenParms()
	            .Prop("localScale", newVec, false)
	            .Ease(EaseType.EaseOutElastic)
	        );
		}
		else {
			theGraph.hideControl(toolTipPanel);
			theGraph.sendSpriteToBack(toolTipPanel);
			
			HOTween.To(aNode.transform, 0.5f, new TweenParms()
	            .Prop("localScale", new Vector3(1,1,1), false)
	            .Ease(EaseType.EaseOutElastic)
	        );
		}
	}
	
	public void MyMouseEnter_Leg(WMG_Series aSeries, WMG_Node aNode, bool state) {
		if (state) {
			int seriesNum = -1;
			int.TryParse(aSeries.name.Substring(6), out seriesNum);
			// Set the text
			theGraph.changeLabelText(toolTipLabel, "Series " + seriesNum + ": " + aSeries.seriesName);
			
			// Display the base panel
			theGraph.showControl(toolTipPanel);
			theGraph.bringSpriteToFront(toolTipPanel);
			
			HOTween.To(aNode.transform, 0.5f, new TweenParms()
	            .Prop("localScale", new Vector3(2,2,1), false)
	            .Ease(EaseType.EaseOutElastic)
	        );
		}
		else {
			theGraph.hideControl(toolTipPanel);
			theGraph.sendSpriteToBack(toolTipPanel);
			
			HOTween.To(aNode.transform, 0.5f, new TweenParms()
	            .Prop("localScale", new Vector3(1,1,1), false)
	            .Ease(EaseType.EaseOutElastic)
	        );
		}
	}
	
	public void MyMouseEnter_Leg_Link(WMG_Series aSeries, WMG_Link aLink, bool state) {
		if (state) {
			if (!aSeries.hidePoints) return;
			int seriesNum = -1;
			int.TryParse(aSeries.name.Substring(6), out seriesNum);
			// Set the text
			theGraph.changeLabelText(toolTipLabel, "Series " + seriesNum + ": " + aSeries.seriesName);
			
			// Display the base panel
			theGraph.showControl(toolTipPanel);
			theGraph.bringSpriteToFront(toolTipPanel);
			
			HOTween.To(aLink.transform, 0.5f, new TweenParms()
	            .Prop("localScale", new Vector3(2,1.05f,1), false)
	            .Ease(EaseType.EaseOutElastic)
	        );
		}
		else {
			if (!aSeries.hidePoints) return;
			theGraph.hideControl(toolTipPanel);
			theGraph.sendSpriteToBack(toolTipPanel);
			
			HOTween.To(aLink.transform, 0.5f, new TweenParms()
	            .Prop("localScale", new Vector3(1,1,1), false)
	            .Ease(EaseType.EaseOutElastic)
	        );
		}
	}
	
}
