using System;
using UnityEngine;

public class Readme1 : ScriptableObject {
	public Texture2D icon;
	public string title;
	public Section[] sections;
	public bool loadedLayout;
	
	public class Section {
		public string heading, text, linkText, url;
	}
}
