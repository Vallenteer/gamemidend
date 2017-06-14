using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField] private Renderer arenaRenderer;
	[SerializeField] bool hideArenaOnTrackingLost;

	public bool HideArenaOnTrackingLost {
		get {
			return hideArenaOnTrackingLost;
		}
	}
			
	// Use this for initialization
	void Start () {
		//arenaCircle.SetActive (false);
		arenaRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BuildArena(bool val) {
		//arenaCircle.SetActive (val);
		arenaRenderer.enabled = val;
	}

}
