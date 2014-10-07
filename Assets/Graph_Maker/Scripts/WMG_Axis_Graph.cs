using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class WMG_Axis_Graph : WMG_Graph_Manager {
	
	public enum graphTypes {line, bar_side, bar_stacked, bar_stacked_percent};
	public graphTypes graphType;
	public enum orientationTypes {vertical, horizontal};
	public orientationTypes orientationType;
	public enum axesTypes {MANUAL, CENTER, AUTO_ORIGIN, I, II, III, IV, I_II, III_IV, II_III, I_IV};
	public axesTypes axesType;
	public List<GameObject> lineSeries;
	
	public List<Object> pointPrefabs;
	public List<Object> linkPrefabs;
	public Object barPrefab;
	public Object seriesPrefab;
	
	public float yAxisMaxValue;
	public float yAxisMinValue;
	public int yAxisNumTicks;
	public float yAxisLength;
	
	public float xAxisMaxValue;
	public float xAxisMinValue;
	public int xAxisNumTicks;
	public float xAxisLength;
	
	public bool[] yMinMaxAutoGrow = new bool[2];
	public bool[] yMinMaxAutoShrink = new bool[2];
	public bool[] xMinMaxAutoGrow = new bool[2];
	public bool[] xMinMaxAutoShrink = new bool[2];
	public float autoShrinkAtPercent = 0.6f;
	public float autoGrowAndShrinkByPercent = 0.2f;
	
	public float barWidth;
	public int axisWidth;
	
	public bool hideXTicks;
	public bool hideYTicks;
	public bool hideXLabels;
	public bool hideYLabels;
	public List<string> yAxisLabels;
	public List<string> xAxisLabels;
	
	public bool SetYLabelsUsingMaxMin;
	public int numDecimalsYAxisLabels;
	public bool SetXLabelsUsingMaxMin;
	public int numDecimalsXAxisLabels;
	
	public bool AutoCenterYAxisLabels;
	public bool AutoCenterXAxisLabels;
	public float yAxisLabelSpacingY;
	public float yAxisLabelSpacingX;
	public float xAxisLabelSpacingY;
	public float xAxisLabelSpacingX;
	
	public float yAxisLabelSize;
	public float xAxisLabelSize;
	
	public bool yAxisTicksRight;
	public bool xAxisTicksAbove;
	
	public bool[] xAxisArrows = new bool[2];
	public float xAxisLinePadding;
	public int xAxisYTick;
	public bool hideYTick;
	
	public bool[] yAxisArrows = new bool[2];
	public float yAxisLinePadding;
	public int yAxisXTick;
	public bool hideXTick;
	
	public enum legendTypes {None, Horizontal, Vertical};
	public legendTypes legendType;
	public enum legendPositions {Bottom, Right, Top};
	public legendPositions legendPosition;
	public bool hideLegendLabels;
	
	public float legendEntrySpacingX;
	public float legendEntrySpacingY;
	public float legendParentOffsetX;
	public float legendParentOffsetY;
	public float backgroundPaddingLeft;
	public float backgroundPaddingRight;
	public float backgroundPaddingTop;
	public float backgroundPaddingBottom;
	
	public GameObject graphBackground;
	public GameObject legendParent;
	public GameObject horizontalGridLines;
	public GameObject yAxisTicks;
	public GameObject verticalGridLines;
	public GameObject xAxisTicks;
	public GameObject yAxisLine;
	public GameObject xAxisLine;
	public GameObject xAxisArrowR;
	public GameObject xAxisArrowL;
	public GameObject yAxisArrowU;
	public GameObject yAxisArrowD;
	public GameObject yAxis;
	public GameObject xAxis;
	public GameObject seriesParent;
	
	private List<float> totalPointValues = new List<float>();
	private float yGridLineLength;
	private float xGridLineLength;
	private float xAxisLinePaddingTot;
	private float yAxisLinePaddingTot;
	
	// Cache
	private orientationTypes cachedOrientationType;
	private axesTypes cachedAxesType;
	private graphTypes cachedGraphType;
	private float cachedYAxisMaxValue;
	private float cachedYAxisMinValue;
	private float cachedYAxisLength;
	private int cachedYAxisNumTicks;
	private float cachedXAxisMaxValue;
	private float cachedXAxisMinValue;
	private float cachedXAxisLength;
	private int cachedXAxisNumTicks;
	private bool[] cachedYMinMaxAutoGrow = new bool[2];
	private bool[] cachedYMinMaxAutoShrink = new bool[2];
	private bool[] cachedXMinMaxAutoGrow = new bool[2];
	private bool[] cachedXMinMaxAutoShrink = new bool[2];
	private float cachedAutoShrinkAtPercent;
	private float cachedAutoGrowAndShrinkByPercent;
	private float cachedBarWidth;
	private int cachedAxisWidth;
	
	private bool cachedHideXTicks;
	private bool cachedHideYTicks;
	private bool cachedHideXLabels;
	private bool cachedHideYLabels;
	private bool cachedSetYLabelsUsingMaxMin;
	private int cachedNumDecimalsYAxisLabels;
	private bool cachedSetXLabelsUsingMaxMin;
	private int cachedNumDecimalsXAxisLabels;
	private float cachedYAxisLabelSize;
	private float cachedXAxisLabelSize;
	private bool[] cachedXAxisArrows = new bool[2];
	private bool[] cachedYAxisArrows = new bool[2];
	private float cachedXAxisLinePadding;
	private float cachedYAxisLinePadding;
	private bool cachedHideYTick;
	private bool cachedHideXTick;
	private List<string> cachedYAxisLabels = new List<string>();
	private List<string> cachedXAxisLabels = new List<string>();
	
	private bool cachedAutoCenterYAxisLabels;
	private bool cachedAutoCenterXAxisLabels;
	private float cachedYAxisLabelSpacingY;
	private float cachedYAxisLabelSpacingX;
	private float cachedXAxisLabelSpacingY;
	private float cachedXAxisLabelSpacingX;
	
	private bool cachedYAxisTicksRight;
	private bool cachedXAxisTicksAbove;
	private int cachedXAxisYTick;
	private int cachedYAxisXTick;
	
	private legendTypes cachedLegendType;
	private legendPositions cachedLegendPosition;
	private bool cachedHideLegendLabels;
	private float cachedLegendEntrySpacingX;
	private float cachedLegendEntrySpacingY;
	private float cachedLegendParentOffsetX;
	private float cachedLegendParentOffsetY;
	private float cachedBackgroundPaddingLeft;
	private float cachedBackgroundPaddingRight;
	private float cachedBackgroundPaddingTop;
	private float cachedBackgroundPaddingBottom;
	
	private int cachedNumYAxisLabels;
	private int cachedNumXAxisLabels;
	// Flags
	private bool aSeriesPointsChanged;
	private bool orientationTypeChanged;
	private bool axesTypeChanged;
	private bool graphTypeChanged;
	private bool yAxisMaxValueChanged;
	private bool yAxisMinValueChanged;
	private bool yAxisLengthChanged;
	private bool yAxisNumTicksChanged;
	private bool xAxisMaxValueChanged;
	private bool xAxisMinValueChanged;
	private bool xAxisLengthChanged;
	private bool xAxisNumTicksChanged;
	private bool autoGrowShrinkChanged;
	private bool barWidthChanged;
	private bool axisWidthChanged;
	
	private bool hideXTicksChanged;
	private bool hideYTicksChanged;
	private bool hideXLabelsChanged;
	private bool hideYLabelsChanged;
	private bool SetYLabelsUsingMaxMinChanged;
	private bool numDecimalsYAxisLabelsChanged;
	private bool SetXLabelsUsingMaxMinChanged;
	private bool numDecimalsXAxisLabelsChanged;
	private bool yAxisLabelSizeChanged;
	private bool xAxisLabelSizeChanged;
	private bool xAxisArrowsChanged;
	private bool yAxisArrowsChanged;
	private bool xAxisLinePaddingChanged;
	private bool yAxisLinePaddingChanged;
	private bool hideYTickChanged;
	private bool hideXTickChanged;
	private bool yAxisLabelsChanged;
	private bool xAxisLabelsChanged;
	
	private bool AutoCenterYAxisLabelsChanged;
	private bool AutoCenterXAxisLabelsChanged;
	private bool yAxisLabelSpacingYChanged;
	private bool yAxisLabelSpacingXChanged;
	private bool xAxisLabelSpacingYChanged;
	private bool xAxisLabelSpacingXChanged;
	
	private bool yAxisTicksRightChanged;
	private bool xAxisTicksAboveChanged;
	private bool xAxisYTickChanged;
	private bool yAxisXTickChanged;
	
	private bool legendChanged;
	private bool backgroundPaddingChanged;
	
	private bool numYAxisLabelsChanged;
	private bool numXAxisLabelsChanged;
	
	void Start() {
		checkCache(); // Set all cache variables to the current values
		setCacheFlags(true); // Set all cache change flags to true to update everything on start
		orientationTypeChanged = false; // Except orientation, since this swaps the orientation
	}
	
	void LateUpdate () {
		checkCache(); // Check current vs cached values for all graph and series variables
		refreshGraph(); // Only does stuff if cache changes
		refreshSeries(); // Only does stuff if cache changes
		setCacheFlags(false); // Set all cache change flags to false
	}
	
	public void refreshGraph() {
		// Swap values based on horizontal vs vertical
		UpdateOrientation();
		
		// Auto update Axes Min Max values based on grow and shrink booleans
		UpdateAxesMinMaxValues();
		
		// Update axes quadrant and related boolean variables such as which arrows appear
		UpdateAxesType();
		
		// Calculate grid spacing and update grid visuals
		UpdateGrids();
		
		// Update axes visuals such as position, length, width, and arrows
		UpdateAxes();
		
		// Update position and text of axes labels which might be based off max / min values or percentages for stacked percentage bar
		UpdateAxesLabels();
		
		// Update Line Series Parents
		UpdateSeriesParentPositions();
		UpdateLineSeriesLegends();
		
		// Update background sprite
		UpdateBackground();
	}
	
	public void refreshSeries() {
		
		if (yAxisMaxValue <= yAxisMinValue || xAxisMaxValue <= xAxisMinValue) return;
		
		for (int j = 0; j < lineSeries.Count; j++) {
			if (!activeInHierarchy(lineSeries[j])) continue;
			WMG_Series theSeries = lineSeries[j].GetComponent<WMG_Series>();
			
			List<GameObject> prevPoints = null;
			if (j > 0 && (graphType == graphTypes.bar_stacked || graphType == graphTypes.bar_stacked_percent) && activeInHierarchy(lineSeries[j-1])) {
				WMG_Series prevSeries = lineSeries[j-1].GetComponent<WMG_Series>();
				prevPoints = prevSeries.getPoints();
			}
			
			theSeries.UpdatePrefabType();
			theSeries.UpdateTotalValues();
			theSeries.CreateOrDeleteSpritesBasedOnPointValues();
			theSeries.UpdateLineColor();
			theSeries.UpdatePointColor();
			theSeries.UpdateLineScale();
			theSeries.UpdatePointWidthHeight();
			theSeries.UpdateHideLines();
			theSeries.UpdateHidePoints();
			theSeries.UpdateSeriesName();
			theSeries.UpdateLegendEntry();
			theSeries.UpdateLinePadding();
			theSeries.UpdateSprites( prevPoints);
		}
	}
	
	void checkCache() {
		if (cachedOrientationType != orientationType) {
			cachedOrientationType = orientationType;
			orientationTypeChanged = true;
		}
		if (cachedAxesType != axesType) {
			cachedAxesType = axesType;
			axesTypeChanged = true;
		}
		if (cachedGraphType != graphType) {
			cachedGraphType = graphType;
			graphTypeChanged = true;
		}
		updateCacheAndFlagFloat(ref cachedYAxisMaxValue, yAxisMaxValue, ref yAxisMaxValueChanged);
		updateCacheAndFlagFloat(ref cachedYAxisMinValue, yAxisMinValue, ref yAxisMinValueChanged);
		updateCacheAndFlagFloat(ref cachedYAxisLength, yAxisLength, ref yAxisLengthChanged);
		updateCacheAndFlagInt(ref cachedYAxisNumTicks, yAxisNumTicks, ref yAxisNumTicksChanged);
		updateCacheAndFlagFloat(ref cachedXAxisMaxValue, xAxisMaxValue, ref xAxisMaxValueChanged);
		updateCacheAndFlagFloat(ref cachedXAxisMinValue, xAxisMinValue, ref xAxisMinValueChanged);
		updateCacheAndFlagFloat(ref cachedXAxisLength, xAxisLength, ref xAxisLengthChanged);
		updateCacheAndFlagInt(ref cachedXAxisNumTicks, xAxisNumTicks, ref xAxisNumTicksChanged);
		
		updateCacheAndFlagBool(ref cachedYMinMaxAutoGrow[0], yMinMaxAutoGrow[0], ref autoGrowShrinkChanged);
		updateCacheAndFlagBool(ref cachedYMinMaxAutoGrow[1], yMinMaxAutoGrow[1], ref autoGrowShrinkChanged);
		updateCacheAndFlagBool(ref cachedYMinMaxAutoShrink[0], yMinMaxAutoShrink[0], ref autoGrowShrinkChanged);
		updateCacheAndFlagBool(ref cachedYMinMaxAutoShrink[1], yMinMaxAutoShrink[1], ref autoGrowShrinkChanged);
		updateCacheAndFlagBool(ref cachedXMinMaxAutoGrow[0], xMinMaxAutoGrow[0], ref autoGrowShrinkChanged);
		updateCacheAndFlagBool(ref cachedXMinMaxAutoGrow[1], xMinMaxAutoGrow[1], ref autoGrowShrinkChanged);
		updateCacheAndFlagBool(ref cachedXMinMaxAutoShrink[0], xMinMaxAutoShrink[0], ref autoGrowShrinkChanged);
		updateCacheAndFlagBool(ref cachedXMinMaxAutoShrink[1], xMinMaxAutoShrink[1], ref autoGrowShrinkChanged);
		updateCacheAndFlagFloat(ref cachedAutoShrinkAtPercent, autoShrinkAtPercent, ref autoGrowShrinkChanged);
		updateCacheAndFlagFloat(ref cachedAutoGrowAndShrinkByPercent, autoGrowAndShrinkByPercent, ref autoGrowShrinkChanged);
		
		updateCacheAndFlagFloat(ref cachedBarWidth, barWidth, ref barWidthChanged);
		updateCacheAndFlagInt(ref cachedAxisWidth, axisWidth, ref axisWidthChanged);
		
		updateCacheAndFlagBool(ref cachedHideXTicks, hideXTicks, ref hideXTicksChanged);
		updateCacheAndFlagBool(ref cachedHideYTicks, hideYTicks, ref hideYTicksChanged);
		updateCacheAndFlagBool(ref cachedHideXLabels, hideXLabels, ref hideXLabelsChanged);
		updateCacheAndFlagBool(ref cachedHideYLabels, hideYLabels, ref hideYLabelsChanged);
		updateCacheAndFlagBool(ref cachedSetYLabelsUsingMaxMin, SetYLabelsUsingMaxMin, ref SetYLabelsUsingMaxMinChanged);
		updateCacheAndFlagInt(ref cachedNumDecimalsYAxisLabels, numDecimalsYAxisLabels, ref numDecimalsYAxisLabelsChanged);
		updateCacheAndFlagBool(ref cachedSetXLabelsUsingMaxMin, SetXLabelsUsingMaxMin, ref SetXLabelsUsingMaxMinChanged);
		updateCacheAndFlagInt(ref cachedNumDecimalsXAxisLabels, numDecimalsXAxisLabels, ref numDecimalsXAxisLabelsChanged);
		updateCacheAndFlagFloat(ref cachedYAxisLabelSize, yAxisLabelSize, ref yAxisLabelSizeChanged);
		updateCacheAndFlagFloat(ref cachedXAxisLabelSize, xAxisLabelSize, ref xAxisLabelSizeChanged);
		updateCacheAndFlagBool(ref cachedXAxisArrows[0], xAxisArrows[0], ref xAxisArrowsChanged);
		updateCacheAndFlagBool(ref cachedXAxisArrows[1], xAxisArrows[1], ref xAxisArrowsChanged);
		updateCacheAndFlagBool(ref cachedYAxisArrows[0], yAxisArrows[0], ref yAxisArrowsChanged);
		updateCacheAndFlagBool(ref cachedYAxisArrows[1], yAxisArrows[1], ref yAxisArrowsChanged);
		updateCacheAndFlagFloat(ref cachedXAxisLinePadding, xAxisLinePadding, ref xAxisLinePaddingChanged);
		updateCacheAndFlagFloat(ref cachedYAxisLinePadding, yAxisLinePadding, ref yAxisLinePaddingChanged);
		updateCacheAndFlagBool(ref cachedHideYTick, hideYTick, ref hideYTickChanged);
		updateCacheAndFlagBool(ref cachedHideXTick, hideXTick, ref hideXTickChanged);
		updateCacheAndFlagStringList(ref cachedYAxisLabels, yAxisLabels, ref yAxisLabelsChanged);
		updateCacheAndFlagStringList(ref cachedXAxisLabels, xAxisLabels, ref xAxisLabelsChanged);
		
		updateCacheAndFlagBool(ref cachedAutoCenterYAxisLabels, AutoCenterYAxisLabels, ref AutoCenterYAxisLabelsChanged);
		updateCacheAndFlagBool(ref cachedAutoCenterXAxisLabels, AutoCenterXAxisLabels, ref AutoCenterXAxisLabelsChanged);
		updateCacheAndFlagFloat(ref cachedYAxisLabelSpacingY, yAxisLabelSpacingY, ref yAxisLabelSpacingYChanged);
		updateCacheAndFlagFloat(ref cachedYAxisLabelSpacingX, yAxisLabelSpacingX, ref yAxisLabelSpacingXChanged);
		updateCacheAndFlagFloat(ref cachedXAxisLabelSpacingY, xAxisLabelSpacingY, ref xAxisLabelSpacingYChanged);
		updateCacheAndFlagFloat(ref cachedXAxisLabelSpacingX, xAxisLabelSpacingX, ref xAxisLabelSpacingXChanged);
		
		updateCacheAndFlagBool(ref cachedYAxisTicksRight, yAxisTicksRight, ref yAxisTicksRightChanged);
		updateCacheAndFlagBool(ref cachedXAxisTicksAbove, xAxisTicksAbove, ref xAxisTicksAboveChanged);
		updateCacheAndFlagInt(ref cachedXAxisYTick, xAxisYTick, ref xAxisYTickChanged);
		updateCacheAndFlagInt(ref cachedYAxisXTick, yAxisXTick, ref yAxisXTickChanged);
		
		if (cachedLegendType != legendType) {
			cachedLegendType = legendType;
			legendChanged = true;
		}
		if (cachedLegendPosition != legendPosition) {
			cachedLegendPosition = legendPosition;
			legendChanged = true;
		}
		updateCacheAndFlagBool(ref cachedHideLegendLabels, hideLegendLabels, ref legendChanged);
		updateCacheAndFlagFloat(ref cachedLegendEntrySpacingX, legendEntrySpacingX, ref legendChanged);
		updateCacheAndFlagFloat(ref cachedLegendEntrySpacingY, legendEntrySpacingY, ref legendChanged);
		updateCacheAndFlagFloat(ref cachedLegendParentOffsetX, legendParentOffsetX, ref legendChanged);
		updateCacheAndFlagFloat(ref cachedLegendParentOffsetY, legendParentOffsetY, ref legendChanged);
		updateCacheAndFlagFloat(ref cachedBackgroundPaddingLeft, backgroundPaddingLeft, ref backgroundPaddingChanged);
		updateCacheAndFlagFloat(ref cachedBackgroundPaddingRight, backgroundPaddingRight, ref backgroundPaddingChanged);
		updateCacheAndFlagFloat(ref cachedBackgroundPaddingTop, backgroundPaddingTop, ref backgroundPaddingChanged);
		updateCacheAndFlagFloat(ref cachedBackgroundPaddingBottom, backgroundPaddingBottom, ref backgroundPaddingChanged);
		
		updateCacheAndFlagInt(ref cachedNumYAxisLabels, yAxisTicks.GetComponent<WMG_Grid>().gridNumNodesY, ref numYAxisLabelsChanged);
		updateCacheAndFlagInt(ref cachedNumXAxisLabels, xAxisTicks.GetComponent<WMG_Grid>().gridNumNodesX, ref numXAxisLabelsChanged);
		
		for (int j = 0; j < lineSeries.Count; j++) {
			if (!activeInHierarchy(lineSeries[j])) continue;
			WMG_Series theSeries = lineSeries[j].GetComponent<WMG_Series>();
			
			theSeries.checkCache();
			
			if (yAxisMaxValueChanged || yAxisMinValueChanged || yAxisLengthChanged ||
				xAxisMaxValueChanged || xAxisMinValueChanged || xAxisLengthChanged || barWidthChanged) {
				theSeries.setPointValuesChanged(true);
			}
			
			if (theSeries.getPointValuesChanged()) aSeriesPointsChanged = true;
		}
	}
	
	void setCacheFlags(bool val) {
		aSeriesPointsChanged = val;
		orientationTypeChanged = val;
		axesTypeChanged = val;
		graphTypeChanged = val;
		yAxisMaxValueChanged = val;
		yAxisMinValueChanged = val;
		yAxisLengthChanged = val;
		yAxisNumTicksChanged = val;
		xAxisMaxValueChanged = val;
		xAxisMinValueChanged = val;
		xAxisLengthChanged = val;
		xAxisNumTicksChanged = val;
		autoGrowShrinkChanged = val;
		barWidthChanged = val;
		axisWidthChanged = val;
		
		hideXTicksChanged = val;
		hideYTicksChanged = val;
		hideXLabelsChanged = val;
		hideYLabelsChanged = val;
		SetYLabelsUsingMaxMinChanged = val;
		numDecimalsYAxisLabelsChanged = val;
		SetXLabelsUsingMaxMinChanged = val;
		numDecimalsXAxisLabelsChanged = val;
		yAxisLabelSizeChanged = val;
		xAxisLabelSizeChanged = val;
		xAxisArrowsChanged = val;
		yAxisArrowsChanged = val;
		xAxisLinePaddingChanged = val;
		yAxisLinePaddingChanged = val;
		hideYTickChanged = val;
		hideXTickChanged = val;
		yAxisLabelsChanged = val;
		xAxisLabelsChanged = val;
		AutoCenterYAxisLabelsChanged = val;
		AutoCenterXAxisLabelsChanged = val;
		yAxisLabelSpacingYChanged = val;
		yAxisLabelSpacingXChanged = val;
		xAxisLabelSpacingYChanged = val;
		xAxisLabelSpacingXChanged = val;
		
		yAxisTicksRightChanged = val;
		xAxisTicksAboveChanged = val;
		xAxisYTickChanged = val;
		yAxisXTickChanged = val;
		
		legendChanged = val;
		backgroundPaddingChanged = val;
		
		numYAxisLabelsChanged = val;
		numXAxisLabelsChanged = val;
		
		for (int j = 0; j < lineSeries.Count; j++) {
			if (!activeInHierarchy(lineSeries[j])) continue;
			WMG_Series theSeries = lineSeries[j].GetComponent<WMG_Series>();
			
			theSeries.setCacheFlags(val);
		}
	}
	
	void UpdateOrientation() {
		if (orientationTypeChanged) {
		
			SwapFloats(ref xAxisMaxValue, ref yAxisMaxValue);
			SwapFloats(ref xAxisMinValue, ref yAxisMinValue);
			SwapInts(ref xAxisNumTicks, ref yAxisNumTicks);
			SwapInts(ref numDecimalsXAxisLabels, ref numDecimalsYAxisLabels);
			SwapBools(ref xMinMaxAutoGrow[0], ref yMinMaxAutoGrow[0]);
			SwapBools(ref xMinMaxAutoGrow[1], ref yMinMaxAutoGrow[1]);
			SwapBools(ref xMinMaxAutoShrink[0], ref yMinMaxAutoShrink[0]);
			SwapBools(ref xMinMaxAutoShrink[1], ref yMinMaxAutoShrink[1]);
			SwapBools(ref SetXLabelsUsingMaxMin, ref SetYLabelsUsingMaxMin);
			SwapBools(ref AutoCenterXAxisLabels, ref AutoCenterYAxisLabels);
			SwapFloats(ref yAxisLabelSpacingY, ref xAxisLabelSpacingX);
			
			List<string> tmp = new List<string>(xAxisLabels);
			xAxisLabels = yAxisLabels;
			yAxisLabels = tmp;
		}
	}
	
	void UpdateAxesType() {
		if (axesTypeChanged || yAxisNumTicksChanged || xAxisNumTicksChanged) {
			if (axesType == axesTypes.CENTER) {
				setAxesQuadrant1_2_3_4();
			}
			else if (axesType == axesTypes.I) {
				setAxesQuadrant1();
			}
			else if (axesType == axesTypes.II) {
				setAxesQuadrant2();
			}
			else if (axesType == axesTypes.III) {
				setAxesQuadrant3();
			}
			else if (axesType == axesTypes.IV) {
				setAxesQuadrant4();
			}
			else if (axesType == axesTypes.I_II) {
				setAxesQuadrant1_2();
			}
			else if (axesType == axesTypes.III_IV) {
				setAxesQuadrant3_4();
			}
			else if (axesType == axesTypes.II_III) {
				setAxesQuadrant2_3();
			}
			else if (axesType == axesTypes.I_IV) {
				setAxesQuadrant1_4();
			}
		}
		// Automatically position axes to the tick nearest 0,0 
		if (axesType == axesTypes.AUTO_ORIGIN) {
			if (xAxisMinValueChanged || xAxisMaxValueChanged || xAxisNumTicksChanged || xAxisYTickChanged ||
				yAxisMinValueChanged || yAxisMaxValueChanged || yAxisNumTicksChanged || yAxisXTickChanged) {
				if (xAxisMinValue >= 0) {
					yAxisXTick = 0;
				}
				else if (xAxisMaxValue <= 0) {
					yAxisXTick = xAxisNumTicks - 1;
				}
				else {
					yAxisXTick = Mathf.RoundToInt(-xAxisMinValue / (xAxisMaxValue - xAxisMinValue) * (xAxisNumTicks - 1));
				}
				if (yAxisXTick == 0 || yAxisXTick == xAxisNumTicks - 1) {
					hideYTick = false;
					if (yAxisXTick == 0) {
						yAxisTicksRight = false;
						xAxisArrows[0] = true;
		       			xAxisArrows[1] = false;
					}
					else {
						yAxisTicksRight = true;
						xAxisArrows[0] = false;
		       			xAxisArrows[1] = true;
					}
				}
				else {
					hideYTick = true;
					yAxisTicksRight = false;
					xAxisArrows[0] = true;
	       			xAxisArrows[1] = true;
				}
	
				if (yAxisMinValue >= 0) {
					xAxisYTick = 0;
				}
				else if (yAxisMaxValue <= 0) {
					xAxisYTick = yAxisNumTicks - 1;
				}
				else {
					xAxisYTick = Mathf.RoundToInt(-yAxisMinValue / (yAxisMaxValue - yAxisMinValue) * (yAxisNumTicks - 1));
				}
				if (xAxisYTick == 0 || xAxisYTick == yAxisNumTicks - 1) {
					hideXTick = false;
					if (xAxisYTick == 0) {
						xAxisTicksAbove = false;
						yAxisArrows[0] = true;
		       			yAxisArrows[1] = false;
					}
					else {
						xAxisTicksAbove = true;
						yAxisArrows[0] = false;
				        yAxisArrows[1] = true;
					}
				}
				else {
					hideXTick = true;
					xAxisTicksAbove = false;
					yAxisArrows[0] = true;
			        yAxisArrows[1] = true;
				}
			}
		}
	}
	
	void UpdateAxesMinMaxValues() {
		if (autoGrowShrinkChanged || orientationTypeChanged || graphTypeChanged || aSeriesPointsChanged ||
			yAxisMaxValueChanged || yAxisMinValueChanged || xAxisMaxValueChanged || xAxisMinValueChanged) {
			
			if (!xMinMaxAutoGrow[0] && !xMinMaxAutoGrow[1] && !xMinMaxAutoShrink[0] && !xMinMaxAutoShrink[1] &&
				!yMinMaxAutoGrow[0] && !yMinMaxAutoGrow[1] && !yMinMaxAutoShrink[0] && !yMinMaxAutoShrink[1]) return;
			float minX = Mathf.Infinity;
			float maxX = Mathf.NegativeInfinity;
			float minY = Mathf.Infinity;
			float maxY = Mathf.NegativeInfinity;
			for (int j = 0; j < lineSeries.Count; j++) {
				if (!activeInHierarchy(lineSeries[j])) continue;
				WMG_Series theSeries = lineSeries[j].GetComponent<WMG_Series>();
				// Find the current max and min point value data
				if (orientationType == orientationTypes.vertical) {
					for (int i = 0; i < theSeries.pointValues.Count; i++) {
						if (theSeries.pointValues[i].x < minX) minX = theSeries.pointValues[i].x;
						if (theSeries.pointValues[i].y < minY) minY = theSeries.pointValues[i].y;
						if (theSeries.pointValues[i].x > maxX) maxX = theSeries.pointValues[i].x;
						if (theSeries.pointValues[i].y > maxY) maxY = theSeries.pointValues[i].y;
						if (graphType == graphTypes.bar_stacked) {
							if (totalPointValues[i] + yAxisMinValue > maxY) maxY = totalPointValues[i] + yAxisMinValue;
						}
					}
				}
				else {
					for (int i = 0; i < theSeries.pointValues.Count; i++) {
						if (theSeries.pointValues[i].y < minX) minX = theSeries.pointValues[i].y;
						if (theSeries.pointValues[i].x < minY) minY = theSeries.pointValues[i].x;
						if (theSeries.pointValues[i].y > maxX) maxX = theSeries.pointValues[i].y;
						if (theSeries.pointValues[i].x > maxY) maxY = theSeries.pointValues[i].x;
						if (graphType == graphTypes.bar_stacked) {
							if (totalPointValues[i] + xAxisMinValue > maxX) maxX = totalPointValues[i] + xAxisMinValue;
						}
					}
				}
			}
			// If point data outside axis max / min then grow, if the point data significantly (percentage of total axis length variable) less than axis min / max then srhink 
			// y-axis
			if (yMinMaxAutoGrow[0] || yMinMaxAutoGrow[1] || yMinMaxAutoShrink[0] || yMinMaxAutoShrink[1]) {
				if (minY == maxY || minY == Mathf.Infinity || maxY == Mathf.NegativeInfinity) return;
				float origMax = yAxisMaxValue;
				float origMin = yAxisMinValue;
				// grow - max
				if (yMinMaxAutoGrow[1] && maxY > origMax) {
					AutoSetAxisMinMax(true, maxY, minY, true, true, origMin, origMax);
				}
				// grow - min
				if (yMinMaxAutoGrow[0] && minY < origMin) {
					AutoSetAxisMinMax(true, minY, maxY, false, true, origMin, origMax);
				}
				// shrink - max
				if (yMinMaxAutoShrink[1] && autoShrinkAtPercent > (maxY - origMin) / (origMax - origMin) ) {
					AutoSetAxisMinMax(true, maxY, minY, true, false, origMin, origMax);
				}
				// shrink - min
				if (yMinMaxAutoShrink[0] && autoShrinkAtPercent > (origMax - minY) / (origMax - origMin) ) {
					AutoSetAxisMinMax(true, minY, maxY, false, false, origMin, origMax);
				}
			}
			// x-axis
			if (xMinMaxAutoGrow[0] || xMinMaxAutoGrow[1] || xMinMaxAutoShrink[0] || xMinMaxAutoShrink[1]) {
				if (minX == maxX || minX == Mathf.Infinity || maxX == Mathf.NegativeInfinity) return;
				float origMax = xAxisMaxValue;
				float origMin = xAxisMinValue;
				// grow - max
				if (xMinMaxAutoGrow[1] && maxX > origMax) {
					AutoSetAxisMinMax(false, maxX, minX, true, true, origMin, origMax);
				}
				// grow - min
				if (xMinMaxAutoGrow[0] && minX < origMin) {
					AutoSetAxisMinMax(false, minX, maxX, false, true, origMin, origMax);
				}
				// shrink - max
				if (xMinMaxAutoShrink[1] && autoShrinkAtPercent > (maxX - origMin) / (origMax - origMin) ) {
					AutoSetAxisMinMax(false, maxX, minX, true, false, origMin, origMax);
				}
				// shrink - min
				if (xMinMaxAutoShrink[0] && autoShrinkAtPercent > (origMax - minX) / (origMax - origMin) ) {
					AutoSetAxisMinMax(false, minX, maxX, false, false, origMin, origMax);
				}
			}
		}
	}
	
	void UpdateGrids() {
		if (yAxisNumTicksChanged || xAxisNumTicksChanged || yAxisLengthChanged || xAxisLengthChanged || AutoCenterYAxisLabelsChanged || AutoCenterXAxisLabelsChanged) {
			// Ticks cant go below 1
			if (yAxisNumTicks <= 1) {
				yAxisNumTicks = 1;
				yGridLineLength = 0;
				if (AutoCenterYAxisLabels) {
					yAxisLabelSpacingY = 0;
				}
			}
			else {
				yGridLineLength = yAxisLength / (yAxisNumTicks-1);
				if (AutoCenterYAxisLabels) {
					yAxisLabelSpacingY = yAxisLength / (yAxisNumTicks - 1) / 2;
				}
			}
			if (xAxisNumTicks <= 1) {
				xAxisNumTicks = 1;
				xGridLineLength = 0;
				if (AutoCenterXAxisLabels) {
					xAxisLabelSpacingX = 0;
				}
			}
			else {
				xGridLineLength = xAxisLength / (xAxisNumTicks-1);
				if (AutoCenterXAxisLabels) {
					xAxisLabelSpacingX = xAxisLength / (xAxisNumTicks - 1) / 2;
				}
			}
			
			// Update horizontal grid lines
			WMG_Grid hGridLines = horizontalGridLines.GetComponent<WMG_Grid>();
			hGridLines.gridNumNodesY = yAxisNumTicks;
			hGridLines.gridLinkLengthY = yGridLineLength;
			hGridLines.gridLinkLengthX = xAxisLength;
			
			// Update vertical grid lines
			WMG_Grid vGridLines = verticalGridLines.GetComponent<WMG_Grid>();
			vGridLines.gridNumNodesX = xAxisNumTicks;
			vGridLines.gridLinkLengthX = xGridLineLength;
			vGridLines.gridLinkLengthY = yAxisLength;
		}
	}
	
	void UpdateAxes() {
		// y-axis
		if (axisWidthChanged || yAxisNumTicksChanged || xAxisNumTicksChanged || yAxisLengthChanged || xAxisLengthChanged ||
			yAxisTicksRightChanged || yAxisXTickChanged || yAxisLinePaddingChanged || yAxisArrowsChanged || numYAxisLabelsChanged)
		{
			SetAxisVisuals(	true, yAxisLine, ref yAxisLinePaddingTot, yAxisLinePadding, yAxisArrows, yAxisLength, yAxisNumTicks, xAxisNumTicks, 
							yAxisXTick, yAxisTicks.GetComponent<WMG_Grid>(), yGridLineLength, yAxisArrowU, yAxisArrowD);
		}
		// x-axis
		if (axisWidthChanged || yAxisNumTicksChanged || xAxisNumTicksChanged || yAxisLengthChanged || xAxisLengthChanged ||
			xAxisTicksAboveChanged || xAxisYTickChanged || xAxisLinePaddingChanged || xAxisArrowsChanged || numXAxisLabelsChanged)
		{
			SetAxisVisuals(	false, xAxisLine, ref xAxisLinePaddingTot, xAxisLinePadding, xAxisArrows, xAxisLength, xAxisNumTicks, yAxisNumTicks, 
							xAxisYTick, xAxisTicks.GetComponent<WMG_Grid>(), xGridLineLength, xAxisArrowR, xAxisArrowL);
		}
	}
	
	void UpdateAxesLabels() {
		// y-axis
		if (orientationTypeChanged || graphTypeChanged || yAxisTicksRightChanged || yAxisLabelSpacingYChanged || yAxisLabelSpacingXChanged ||
			yAxisNumTicksChanged || hideYTickChanged || xAxisYTickChanged || hideYTicksChanged || hideYLabelsChanged || yAxisLabelSizeChanged || numYAxisLabelsChanged ||
			SetYLabelsUsingMaxMinChanged || yAxisMaxValueChanged || yAxisMinValueChanged || numDecimalsYAxisLabelsChanged || yAxisLabelsChanged)
		{
			SetAxisLabels(	true, getYAxisLabels(), ref yAxisLabels, yAxisNumTicks, hideYTick, xAxisYTick, hideYTicks, hideYLabels, yAxisLabelSize, 
							SetYLabelsUsingMaxMin, yAxisMaxValue, yAxisMinValue, numDecimalsYAxisLabels);
		}
		// x-axis
		if (orientationTypeChanged || graphTypeChanged || xAxisTicksAboveChanged || xAxisLabelSpacingYChanged || xAxisLabelSpacingXChanged ||
			xAxisNumTicksChanged || hideXTickChanged || yAxisXTickChanged || hideXTicksChanged || hideXLabelsChanged || xAxisLabelSizeChanged || numXAxisLabelsChanged ||
			SetXLabelsUsingMaxMinChanged || xAxisMaxValueChanged || xAxisMinValueChanged || numDecimalsXAxisLabelsChanged || xAxisLabelsChanged)
		{
			SetAxisLabels(	false, getXAxisLabels(), ref xAxisLabels, xAxisNumTicks, hideXTick, yAxisXTick, hideXTicks, hideXLabels, xAxisLabelSize, 
							SetXLabelsUsingMaxMin, xAxisMaxValue, xAxisMinValue, numDecimalsXAxisLabels);
		}
	}
	
	void SetAxisVisuals(bool isY, GameObject AxisLine, ref float AxisLinePaddingTot, float AxisLinePadding, bool[] AxisArrows, float axisLength,
						int numTicks, int otherAxisNumTicks, int otherAxisTick, WMG_Grid gridLines, float gridLineLength, GameObject topRightArrow, GameObject bottomLeftArrow) {
		
		AxisLinePaddingTot = 2 * AxisLinePadding;
		float axisRepos = 0;
		if (!AxisArrows[0]) AxisLinePaddingTot -= AxisLinePadding;
		else axisRepos += AxisLinePadding / 2f;
		if (!AxisArrows[1]) AxisLinePaddingTot -= AxisLinePadding;
		else axisRepos -= AxisLinePadding / 2f;
		
		// Used for repositioning the axis line based on the tick number and total ticks
		float percentAxis = 0;
		if (otherAxisNumTicks == 1) percentAxis = 1;
		else percentAxis = otherAxisTick / (otherAxisNumTicks - 1f);
		
		if (isY) {
			changeSpriteWidth(AxisLine, axisWidth);
			changeSpriteHeight(AxisLine, Mathf.RoundToInt(axisLength + AxisLinePaddingTot));
			
			changeSpritePositionTo(yAxisLine, new Vector3(0, axisRepos + axisLength/2, 0));
			
			// Reposition axis line and grid ticks based on the opposite axis tick number and number ticks
			changeSpritePositionToX(yAxis, percentAxis * xAxisLength);
			
			if (!yAxisTicksRight) {
				changeSpritePositionToX(yAxisTicks, percentAxis * xAxisLength - gridLines.gridLinkLengthX - axisWidth / 2);
			}
			else {
				changeSpritePositionToX(yAxisTicks, percentAxis * xAxisLength + axisWidth / 2);
			}
			
			// Update grid lines
			gridLines.gridNumNodesY = numTicks;
			gridLines.gridLinkLengthY = gridLineLength;
			
			// Since the ticks are a grid implementation, there are 2 nodes and 1 link for every tick, this disables one of the nodes for each tick
			gridLines.setActiveColumn(false,1);
		}
		else {
			changeSpriteWidth(AxisLine, Mathf.RoundToInt(axisLength + AxisLinePaddingTot));
			changeSpriteHeight(AxisLine, axisWidth);
			
			changeSpritePositionTo(xAxisLine, new Vector3(axisRepos + axisLength/2, 0, 0));
			
			// Reposition axis line and grid ticks based on the opposite axis tick number and number ticks
			changeSpritePositionToY(xAxis, percentAxis * yAxisLength);
			
			if (!xAxisTicksAbove) {
				changeSpritePositionToY(xAxisTicks, percentAxis * yAxisLength - gridLines.gridLinkLengthY - axisWidth / 2);
			}
			else {
				changeSpritePositionToY(xAxisTicks, percentAxis * yAxisLength + axisWidth / 2);
			}
			
			// Update grid lines
			gridLines.gridNumNodesX = numTicks;
			gridLines.gridLinkLengthX = gridLineLength;
			
			// Since the ticks are a grid implementation, there are 2 nodes and 1 link for every tick, this disables one of the nodes for each tick
			gridLines.setActiveRow(false,1);
		}
		
		// Update Arrows
		SetActiveAnchoredSprite(topRightArrow,AxisArrows[0]);
		SetActiveAnchoredSprite(bottomLeftArrow,AxisArrows[1]);
	}
	
	void SetAxisLabels(	bool isY, List<WMG_Node> TickNodes, ref List<string> AxisLabels, int numTicks, bool hideTick, 
						int axisTick, bool hideTicks, bool hideLabels, float labelSize, bool setUsingMaxMin, float axisMax, float axisMin, int numDecimals) {
		
		// Create or delete labels based on numTicks
		for (int i = 0; i < numTicks; i++) {
			if (AxisLabels.Count <= i) {
				AxisLabels.Add("");
			}
		}
		for (int i = AxisLabels.Count - 1; i >= 0; i--) {
			if (i >= numTicks) {
				AxisLabels.RemoveAt(i);
			}
		}
		
		if (TickNodes == null) return;

		for (int i = 0; i < AxisLabels.Count; i++) {
			if (i >= TickNodes.Count) break;
			// Hide label that is the same as the axis
			if (hideTick && i == axisTick) SetActive(TickNodes[axisTick].gameObject,false);
			else SetActive(TickNodes[i].gameObject,!hideLabels);
			// Hide tick that is the same as the axis
			if (hideTick && i == axisTick) SetActive(TickNodes[axisTick].links[0],false);
			else SetActive(TickNodes[i].links[0],!hideTicks);
			// Position the ticks
			if (isY) {
				if (!yAxisTicksRight) {
					changeSpritePivot(TickNodes[i].objectToLabel, WMG_Graph_Manager.WMGpivotTypes.Right);
					changeSpritePositionTo(TickNodes[i].objectToLabel, new Vector3(-yAxisLabelSpacingX, yAxisLabelSpacingY, 0));
					
				}
				else {
					changeSpritePivot(TickNodes[i].objectToLabel, WMG_Graph_Manager.WMGpivotTypes.Left);
					changeSpritePositionTo(TickNodes[i].objectToLabel, new Vector3(yAxisLabelSpacingX + yAxisTicks.GetComponent<WMG_Grid>().gridLinkLengthX, yAxisLabelSpacingY, 0));
					
				}
			}
			else {
				if (!xAxisTicksAbove) {
					changeSpritePivot(TickNodes[i].objectToLabel, WMG_Graph_Manager.WMGpivotTypes.Center);
					changeSpritePositionTo(TickNodes[i].objectToLabel, new Vector3(xAxisLabelSpacingX, -xAxisLabelSpacingY, 0));
					
				}
				else {
					changeSpritePivot(TickNodes[i].objectToLabel, WMG_Graph_Manager.WMGpivotTypes.Center);
					changeSpritePositionTo(TickNodes[i].objectToLabel, new Vector3(xAxisLabelSpacingX, xAxisLabelSpacingY, 0));
					
				}
			}
			// Set the labels
			TickNodes[i].objectToLabel.transform.localScale = new Vector3(labelSize, labelSize, 1);
			if (setUsingMaxMin) {
				float num = axisMin + i * (axisMax - axisMin) / (TickNodes.Count-1);
				if (i == 0) num = axisMin;
				
				if (graphType == graphTypes.bar_stacked_percent && ((isY && orientationType == orientationTypes.vertical) 
																	|| (!isY && orientationType == orientationTypes.horizontal))) {
					num = i / (TickNodes.Count-1f) * 100f;
				}
				float numberToMult = Mathf.Pow(10f, numDecimals);
				AxisLabels[i] = (Mathf.Round(num*numberToMult)/numberToMult).ToString();
				if (graphType == graphTypes.bar_stacked_percent && ((isY && orientationType == orientationTypes.vertical) 
																	|| (!isY && orientationType == orientationTypes.horizontal))) {
					AxisLabels[i] += "%";
				}
			}
			changeLabelText(TickNodes[i].objectToLabel, AxisLabels[i]);
		}
	}
	
	void UpdateSeriesParentPositions () {
		if (graphTypeChanged || orientationTypeChanged || axisWidthChanged || barWidthChanged) {
			for (int j = 0; j < lineSeries.Count; j++) {
				if (!activeInHierarchy(lineSeries[j])) continue;
				
				if (graphType != graphTypes.line) {
					if (orientationType == orientationTypes.vertical) {
						changeSpritePositionTo(lineSeries[j], new Vector3(axisWidth/2f, axisWidth/2f, 0));
					}
					else {
						changeSpritePositionTo(lineSeries[j], new Vector3(axisWidth/2f, axisWidth/2f + barWidth, 0));
					}
				}
				else {
					changeSpritePositionTo(lineSeries[j], new Vector3(0, 0, 0));
				}
				
				// Update spacing between series
				if (graphType == graphTypes.bar_side) {
					if (j > 0) {
						if (orientationType == orientationTypes.vertical) {
							changeSpritePositionRelativeToObjByX(lineSeries[j], lineSeries[j-1], barWidth);
						}
						else {
							changeSpritePositionRelativeToObjByY(lineSeries[j], lineSeries[j-1], barWidth);
						}
					}
				}
				else {
					if (j > 0) {
						if (orientationType == orientationTypes.vertical) {
							changeSpritePositionRelativeToObjByX(lineSeries[j], lineSeries[0], 0);
						}
						else {
							changeSpritePositionRelativeToObjByY(lineSeries[j], lineSeries[0], 0);
						}
					}
				}
			}
		}
	}
	
	void UpdateLineSeriesLegends() {
		if (legendChanged || xAxisLengthChanged || yAxisLengthChanged) {
			if (legendPosition == legendPositions.Bottom) {
				changeSpritePositionTo(legendParent, new Vector3(xAxisLength / 2 + legendParentOffsetX, -legendParentOffsetY, 0));
			}
			else if (legendPosition == legendPositions.Top) {
				changeSpritePositionTo(legendParent, new Vector3(xAxisLength / 2 + legendParentOffsetX, yAxisLength + legendParentOffsetY, 0));
			}
			else if (legendPosition == legendPositions.Right) {
				changeSpritePositionTo(legendParent, new Vector3(xAxisLength + legendParentOffsetX, yAxisLength / 2 - legendParentOffsetY, 0));
			}
			
			for (int j = 0; j < lineSeries.Count; j++) {
				if (!activeInHierarchy(lineSeries[j])) continue;
				WMG_Series theSeries = lineSeries[j].GetComponent<WMG_Series>();
				
				GameObject seriesLegend = theSeries.getLegendParent();
				
				SetActive(seriesLegend.GetComponent<WMG_Node>().objectToLabel, !hideLegendLabels);
				
				if (legendType != legendTypes.None && !activeInHierarchy(seriesLegend)) SetActive(seriesLegend,true);
				if (legendType == legendTypes.None && activeInHierarchy(seriesLegend)) SetActive(seriesLegend,false);
				
				if (legendType == legendTypes.Horizontal) {
					changeSpritePositionTo(seriesLegend, new Vector3(j * legendEntrySpacingX, 0, 0));
				}
				else if (legendType == legendTypes.Vertical) {
					changeSpritePositionTo(seriesLegend, new Vector3(0, -j * legendEntrySpacingY, 0));
				}
			}
		}
	}
	
	void UpdateBackground() {
		if (backgroundPaddingChanged || xAxisLengthChanged || yAxisLengthChanged) {
			changeSpriteWidth(graphBackground, Mathf.RoundToInt(backgroundPaddingLeft + backgroundPaddingRight + xAxisLength));
			changeSpriteHeight(graphBackground, Mathf.RoundToInt(backgroundPaddingTop + backgroundPaddingBottom + yAxisLength));
			changeSpritePositionToX(graphBackground, -backgroundPaddingLeft);
			changeSpritePositionToY(graphBackground, -backgroundPaddingBottom);
		}
	}
	
	// Helper function for update min max, ensures the new values have sensible level of precision
	void AutoSetAxisMinMax(bool isY, float val, float val2, bool max, bool grow, float aMin, float aMax) {
		int numTicks = 0;
		if (isY) numTicks = yAxisNumTicks-1;
		else numTicks = xAxisNumTicks-1;
		
		float changeAmt = 1 + autoGrowAndShrinkByPercent;
		
		// Find tentative new max / min value
		float temp = 0;
		if (max) {
			if (grow) temp = changeAmt * (val - aMin) / (numTicks);
			else temp = changeAmt * (val - val2) / (numTicks);
		}
		else {
			if (grow) temp = changeAmt * (aMax - val) / (numTicks);
			else temp = changeAmt * (val2 - val) / (numTicks);
		}
		
		if (temp == 0 || aMax <= aMin) return;
		
		// Determine level of precision of tentative new value
		float temp2 = temp;
		int pow = 0;
		
		if (Mathf.Abs(temp2) > 1) {
			while (Mathf.Abs(temp2) > 10) {
				pow++;
				temp2 /= 10f;
			}
		}
		else {
			while (Mathf.Abs(temp2) < 0.1f) {
				pow--;
				temp2 *= 10f;
			}
		}
		
		// Update tentative to sensible level of precision
		float temp3 = Mathf.Pow( 10f, pow-1);
		temp2 = temp - (temp % temp3) + temp3;
		
		float newVal = 0;
		if (max) {
			if (grow) newVal = (numTicks) * temp2 + aMin;
			else newVal = (numTicks) * temp2 + val2;
		}
		else {
			if (grow) newVal = aMax - (numTicks) * temp2;
			else newVal = val2 - (numTicks) * temp2;
		}
		
		// Set the min / max value to the newly calculated value
		if (max) {
			if (isY) yAxisMaxValue = newVal;
			else xAxisMaxValue = newVal;
		}
		else {
			if (isY) yAxisMinValue = newVal;
			else xAxisMinValue = newVal;
		}
	}
	
	public List<float> TotalPointValues {
		get { return totalPointValues; }
	}
	
	public List<WMG_Node> getXAxisLabels() {
		WMG_Grid xTicks = xAxisTicks.GetComponent<WMG_Grid>();
		return xTicks.getRow(0);
	}
	
	public List<GameObject> getXAxisTicks() {
		List<WMG_Node> theLabels = getXAxisLabels();
		if (theLabels != null) {
			List<GameObject> theLinks = new List<GameObject>();
			foreach (WMG_Node aNode in theLabels) {
				theLinks.Add(aNode.links[0]);
			}
			return theLinks;
		}
		return null;
	}
	
	public List<WMG_Node> getYAxisLabels() {
		WMG_Grid yTicks = yAxisTicks.GetComponent<WMG_Grid>();
		return yTicks.getColumn(0);
	}
	
	public List<GameObject> getYAxisTicks() {
		List<WMG_Node> theLabels = getYAxisLabels();
		if (theLabels != null) {
			List<GameObject> theLinks = new List<GameObject>();
			foreach (WMG_Node aNode in theLabels) {
				theLinks.Add(aNode.links[0]);
			}
			return theLinks;
		}
		return null;
	}
	
	public void changeAllLinePivots(WMGpivotTypes newPivot) {
		for (int j = 0; j < lineSeries.Count; j++) {
			if (!activeInHierarchy(lineSeries[j])) continue;
			WMG_Series aSeries = lineSeries[j].GetComponent<WMG_Series>();
			List<GameObject> lines = aSeries.getLines();
			for (int i = 0; i < lines.Count; i++) {
				changeSpritePivot(lines[i], newPivot);
				WMG_Link aLink = lines[i].GetComponent<WMG_Link>();
				aLink.Reposition();
			}
		}
	}
	
	public List<Vector3> getSeriesScaleVectors(bool useLineWidthForX, float x, float y) {
		List<Vector3> results = new List<Vector3>();
		for (int j = 0; j < lineSeries.Count; j++) {
			if (!activeInHierarchy(lineSeries[j])) continue;
			WMG_Series aSeries = lineSeries[j].GetComponent<WMG_Series>();
			if (useLineWidthForX) {
				results.Add(new Vector3(aSeries.lineScale, y, 1));
			}
			else {
				results.Add(new Vector3(x, y, 1));
			}
		}
		return results;
	}
	
	public void setAxesQuadrant1_2_3_4() {
		xAxisArrows[0] = true;
		xAxisArrows[1] = true;
		yAxisArrows[0] = true;
		yAxisArrows[1] = true;
		hideYTick = true;
		hideXTick = true;
		xAxisYTick = yAxisNumTicks / 2;
		yAxisXTick = xAxisNumTicks / 2;
		yAxisTicksRight = false;
		xAxisTicksAbove = false;
	}
	
	public void setAxesQuadrant1() {
		xAxisArrows[0] = true;
		xAxisArrows[1] = false;
		yAxisArrows[0] = true;
		yAxisArrows[1] = false;
		hideYTick = false;
		hideXTick = false;
		xAxisYTick = 0;
		yAxisXTick = 0;
		yAxisTicksRight = false;
		xAxisTicksAbove = false;
	}
	
	public void setAxesQuadrant2() {
		xAxisArrows[0] = false;
		xAxisArrows[1] = true;
		yAxisArrows[0] = true;
		yAxisArrows[1] = false;
		hideYTick = false;
		hideXTick = false;
		xAxisYTick = 0;
		yAxisXTick = xAxisNumTicks - 1;
		yAxisTicksRight = true;
		xAxisTicksAbove = false;
	}
	
	public void setAxesQuadrant3() {
		xAxisArrows[0] = false;
		xAxisArrows[1] = true;
		yAxisArrows[0] = false;
		yAxisArrows[1] = true;
		hideYTick = false;
		hideXTick = false;
		xAxisYTick = yAxisNumTicks - 1;
		yAxisXTick = xAxisNumTicks - 1;
		yAxisTicksRight = true;
		xAxisTicksAbove = true;
	}
	
	public void setAxesQuadrant4() {
		xAxisArrows[0] = true;
		xAxisArrows[1] = false;
		yAxisArrows[0] = false;
		yAxisArrows[1] = true;
		hideYTick = false;
		hideXTick = false;
		xAxisYTick = yAxisNumTicks - 1;
		yAxisXTick = 0;
		yAxisTicksRight = false;
		xAxisTicksAbove = true;
	}
	
	public void setAxesQuadrant1_2() {
		xAxisArrows[0] = true;
		xAxisArrows[1] = true;
		yAxisArrows[0] = true;
		yAxisArrows[1] = false;
		hideYTick = true;
		hideXTick = false;
		xAxisYTick = 0;
		yAxisXTick = xAxisNumTicks / 2;
		yAxisTicksRight = false;
		xAxisTicksAbove = false;
	}
	
	public void setAxesQuadrant3_4() {
		xAxisArrows[0] = true;
		xAxisArrows[1] = true;
		yAxisArrows[0] = false;
		yAxisArrows[1] = true;
		hideYTick = true;
		hideXTick = false;
		xAxisYTick = yAxisNumTicks - 1;
		yAxisXTick = xAxisNumTicks / 2;
		yAxisTicksRight = false;
		xAxisTicksAbove = true;
	}
	
	public void setAxesQuadrant2_3() {
		xAxisArrows[0] = false;
		xAxisArrows[1] = true;
		yAxisArrows[0] = true;
		yAxisArrows[1] = true;
		hideYTick = false;
		hideXTick = true;
		xAxisYTick = yAxisNumTicks / 2;
		yAxisXTick = xAxisNumTicks - 1;
		yAxisTicksRight = true;
		xAxisTicksAbove = false;
	}
	
	public void setAxesQuadrant1_4() {
		xAxisArrows[0] = true;
		xAxisArrows[1] = false;
		yAxisArrows[0] = true;
		yAxisArrows[1] = true;
		hideYTick = false;
		hideXTick = true;
		xAxisYTick = yAxisNumTicks / 2;
		yAxisXTick = 0;
		yAxisTicksRight = false;
		xAxisTicksAbove = false;
	}
	
	// Animate all the points in all the series simultaneously
	public void animScaleAllAtOnce(bool isPoint, float duration, float delay, EaseType anEaseType, List<Vector3> before, List<Vector3> after) {
		for (int j = 0; j < lineSeries.Count; j++) {
			if (!activeInHierarchy(lineSeries[j])) continue;
			WMG_Series aSeries = lineSeries[j].GetComponent<WMG_Series>();
			List<GameObject> objects;
			if (isPoint) objects = aSeries.getPoints();
			else objects = aSeries.getLines();
			for (int i = 0; i < objects.Count; i++) {
				Transform aComponent = objects[i].transform;
				aComponent.localScale = before[j];
				HOTween.To(aComponent, duration, new TweenParms()
		            .Prop("localScale", after[j], false)
		            .Ease(anEaseType)
					.Delay(delay)
		        );
			}
		}
	}
	
	// Animate all the points in a single series simultaneously, and then proceed to the next series
	public void animScaleBySeries(bool isPoint, float duration, float delay, EaseType anEaseType, List<Vector3> before, List<Vector3> after) {
		Sequence sequence = new Sequence();
		float individualDuration = duration / lineSeries.Count;
		float individualDelay = delay / lineSeries.Count;
		for (int j = 0; j < lineSeries.Count; j++) {
			if (!activeInHierarchy(lineSeries[j])) continue;
			WMG_Series aSeries = lineSeries[j].GetComponent<WMG_Series>();
			List<GameObject> objects;
			if (isPoint) objects = aSeries.getPoints();
			else objects = aSeries.getLines();
			float insertTimeLoc = j * individualDuration + (j+1) * individualDelay;
			for (int i = 0; i < objects.Count; i++) {
				Transform aComponent = objects[i].transform;
				aComponent.localScale = before[j];
				
		        sequence.Insert(insertTimeLoc, HOTween.To(aComponent, individualDuration + individualDelay, new TweenParms()
					.Prop("localScale", after[j], false)
					.Ease(anEaseType)
					.Delay(individualDelay)
				));
			}
		}
	    sequence.Play();
	}
	
	// Animate the points across multiple series simultaneously, and then proceed to the next points.
	public void animScaleOneByOne(bool isPoint, float duration, float delay, EaseType anEaseType, List<Vector3> before, List<Vector3> after, int loopDir) {
		for (int j = 0; j < lineSeries.Count; j++) {
			if (!activeInHierarchy(lineSeries[j])) continue;
			Sequence sequence = new Sequence();
			WMG_Series aSeries = lineSeries[j].GetComponent<WMG_Series>();
			List<GameObject> objects;
			if (isPoint) objects = aSeries.getPoints();
			else objects = aSeries.getLines();
			float individualDuration = duration / objects.Count;
			float individualDelay = delay / objects.Count;
			if (loopDir == 0) {
				// Loop from left to right
				for (int i = 0; i < objects.Count; i++) {
					Transform aComponent = objects[i].transform;
					aComponent.localScale = before[j];
					
			        sequence.Append(HOTween.To(aComponent, individualDuration + individualDelay, new TweenParms()
						.Prop("localScale", after[j], false)
						.Ease(anEaseType)
						.Delay(individualDelay)
					));
				}
			}
			else if (loopDir == 1) {
				// Loop from right to left
				for (int i = objects.Count-1; i >= 0; i--) {
					Transform aComponent = objects[i].transform;
					aComponent.localScale = before[j];
					
			        sequence.Append(HOTween.To(aComponent, individualDuration + individualDelay, new TweenParms()
						.Prop("localScale", after[j], false)
						.Ease(anEaseType)
						.Delay(individualDelay)
					));
				}
			}
			else if (loopDir == 2) {
				// Loop from the center point to the edges, alternating sides.
				int max = objects.Count - 1;
				int i = max / 2;
				int dir = -1;
				int difVal = 0;
				bool reachedMin = false;
				bool reachedMax = false;
				while (true) {
					
					if (reachedMin && reachedMax) break;
					
					if (i >= 0 && i <= max) {
						Transform aComponent = objects[i].transform;
						aComponent.localScale = before[j];
						
				        sequence.Append(HOTween.To(aComponent, individualDuration + individualDelay, new TweenParms()
							.Prop("localScale", after[j], false)
							.Ease(anEaseType)
							.Delay(individualDelay)
						));
					}
					
					difVal++;
					dir *= -1;
					i = i + (dir * difVal);
					
					if (i < 0) reachedMin = true;
					if (i > max) reachedMax = true;
					
				}
			}
	        sequence.Play();
		}
	}
	
	public WMG_Series addSeries() {
		GameObject curObj = Instantiate(seriesPrefab) as GameObject;
		changeSpriteParent(curObj, seriesParent);
		curObj.transform.localScale = Vector3.one;
		WMG_Series theSeries = curObj.GetComponent<WMG_Series>();
		theSeries.theGraph = this;
		theSeries.checkCache();
		theSeries.setCacheFlags(true);
		legendChanged = true;
		lineSeries.Add(curObj);
		curObj.name = "Series" + lineSeries.Count;
		barWidthChanged = true;
		return curObj.GetComponent<WMG_Series>();
	}
	
	public void deleteSeries() {
		GameObject lastSeries = lineSeries[lineSeries.Count-1];
		lineSeries.Remove(lastSeries);
		WMG_Series theSeries = lastSeries.GetComponent<WMG_Series>();
		theSeries.deleteAllNodesFromGraphManager();
		Destroy(theSeries.getLegendParent());
		Destroy(lastSeries);
		barWidthChanged = true;
	}
}
