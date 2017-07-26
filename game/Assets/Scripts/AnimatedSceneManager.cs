using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSceneManager : MonoBehaviour {

    SoundManager soundManager;
    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        soundManager.BgmFight2();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
