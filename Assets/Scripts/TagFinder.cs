// xeophin.net/code

// 

// (c) 2010 Kaspar Manz

// code@xeophin.net

// 

// All rights reserved.

// 

using System;

using UnityEngine;



[AddComponentMenu("Helper Scripts/List Objects By Tag")]



/// <summary>

/// This is a simple little script that lists all objects with a

/// certain tag, in order to find wrongly tagged objects.

/// </summary>

public class TagFinder : MonoBehaviour {
	
	
	
	/// <summary>
	
	/// A list of tags to look for.
	
	/// </summary>
	
	public string[] tagsToFind = { "EditorOnly" };
	
	
	
	/// <summary>
	
	/// At the start of the game, this script lists all objects
	
	/// with the defined tags.
	
	/// </summary>
	
	void Start () {
		
		foreach (string tag in tagsToFind) {
			
			print ("Objects tagged with '" + tag + "':");
			
			
			
			try {
				
				GameObject[] objects = GameObject.FindGameObjectsWithTag (tag);
				
				if (objects.Length != 0) {
					
					foreach (GameObject item in objects) {
						
						print ("  " + item.name + " on layer " + item.layer);
						
					}
					
				} else {
					
					print ("  There are no objects with the tag '" + tag + "'.");
					
					
					
				}
				
			} catch (UnityException ex) {
				
				print ("  The tag '" + tag + "' has not been found. Exception Message: " + ex.Message);
				
			}
			
			
			
		}
		
		
		
	}
	
}