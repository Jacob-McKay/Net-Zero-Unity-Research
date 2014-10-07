using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IWMG_Caching_Functions {
	void updateCacheAndFlagFloat(ref float cache, float val, ref bool flag);
	
	void updateCacheAndFlagInt(ref int cache, int val, ref bool flag);
	
	void updateCacheAndFlagString(ref string cache, string val, ref bool flag);
	
	void updateCacheAndFlagColor(ref Color cache, Color val, ref bool flag);
	
	void updateCacheAndFlagBool(ref bool cache, bool val, ref bool flag);
	
	void updateCacheAndFlagObject(ref Object cache, Object val, ref bool flag);
	
	void updateCacheAndFlagStringList(ref List<string> cache, List<string> val, ref bool flag);
	
	void SwapFloats(ref float val1, ref float val2);
	
	void SwapInts(ref int val1, ref int val2);
	
	void SwapBools(ref bool val1, ref bool val2);
}

public class WMG_Caching_Functions : IWMG_Caching_Functions {

	public void updateCacheAndFlagFloat(ref float cache, float val, ref bool flag) {
		if (cache != val) {
			cache = val;
			flag = true;
		}
	}
	
	public void updateCacheAndFlagInt(ref int cache, int val, ref bool flag) {
		if (cache != val) {
			cache = val;
			flag = true;
		}
	}
	
	public void updateCacheAndFlagString(ref string cache, string val, ref bool flag) {
		if (cache != val) {
			cache = val;
			flag = true;
		}
	}
	
	public void updateCacheAndFlagColor(ref Color cache, Color val, ref bool flag) {
		if (cache != val) {
			cache = val;
			flag = true;
		}
	}
	
	public void updateCacheAndFlagBool(ref bool cache, bool val, ref bool flag) {
		if (cache != val) {
			cache = val;
			flag = true;
		}
	}
	
	public void updateCacheAndFlagObject(ref Object cache, Object val, ref bool flag) {
		if (cache != val) {
			cache = val;
			flag = true;
		}
	}
	
	public void updateCacheAndFlagStringList(ref List<string> cache, List<string> val, ref bool flag) {
		if (cache.Count != val.Count) {
			cache = new List<string>(val);
			flag = true;
		}
		else {
			for (int i = 0; i < val.Count; i++) {
				if (val[i] != cache[i]) {
					cache = new List<string>(val);
					flag = true;
					break;
				}
			}
		}
	}
	
	public void SwapFloats(ref float val1, ref float val2) {
		float tmp = val1;
		val1 = val2;
		val2 = tmp;
	}
	
	public void SwapInts(ref int val1, ref int val2) {
		int tmp = val1;
		val1 = val2;
		val2 = tmp;
	}
	
	public void SwapBools(ref bool val1, ref bool val2) {
		bool tmp = val1;
		val1 = val2;
		val2 = tmp;
	}
}
