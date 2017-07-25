using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorHandler : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		
	}

	public void OnLevel1Button() {
		SceneManager.LoadScene ("VuforiaTestGameplay");
	}
}
