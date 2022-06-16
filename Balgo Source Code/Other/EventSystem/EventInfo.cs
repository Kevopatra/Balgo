//BUILD STUFF EventInfo
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

//Not sure how this really goes...
[System.Serializable]
public abstract class EventInfo
{
	/*
	 * The base EventInfo,
	 * might have some generic text
	 * for doing Debug.Log?
	 */

	public string EventDescription;
}

public class PlaySFXEvent : EventInfo 
{
	public string ArrayName;
}
public class BGMEvent : EventInfo
{
	public float TransitionSpd = 0.2f;
	public AudioClip NewSong;

	public bool FightMode;
}