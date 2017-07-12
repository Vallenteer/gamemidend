using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	//start vuforia camera
	//wait for detected target
	//summon arena, summon momon immediately
	//summon enemy. load enemy parameters
	//summon ui, with animation entry
	//for ui bar, set the value 100%. health value hidden.
	//set 

	[SerializeField] private Renderer arenaRenderer;
	[SerializeField] bool hideArenaOnTrackingLost;
	[SerializeField] Text healthText;
	[HideInInspector] public CharacterData SummonedCharacter;
	[HideInInspector] public CharacterData EnemyCharacter;
	private int healthVal;

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

	public void CharacterFound(CharacterData cd) {
		BuildArena (true);
		SummonedCharacter = cd;
		SetHealth (cd.HP);
	}

	void SetHealth(int val) {
		healthVal = val;
		healthText.text = "HP: " + val.ToString ();
	}

}
