using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject arenaCircle;

	public GameObject ArenaCircle {
		get {
			return arenaCircle;
		}
	}
		
	// Use this for initialization
	void Start () {
		arenaCircle.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BuildArena() {
		arenaCircle.SetActive (true);
	}
}
