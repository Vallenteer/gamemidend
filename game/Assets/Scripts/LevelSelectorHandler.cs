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
		if (!StaticVars.StoryLv1HasPlayed) {
			StaticVars.StoryLv1HasPlayed = true;
			SceneManager.LoadScene ("AnimatedScene");
		} else {
			SceneManager.LoadScene (3);
		}
	}
}
