using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug=UnityEngine.Debug;

public class TimerObjectController : MonoBehaviour {
	//set what function to callback when timer finished
	//summon timer when needed and notify a callback function when finished

	//how to use
	//1. instantiate
	//2. set callback, set timerlength
	//3. start countdown
	//4. on finish, call callback
	//5. commit suicide

	Stopwatch timer;
	float timerLength = 0;
	Action callback;

	void Start () {
		timer = new Stopwatch ();
	}

	void Update () {
		if (!timer.IsRunning)
			return;
		Debug.Log (timer.ElapsedMilliseconds);
		if (timer.ElapsedMilliseconds > timerLength) {
			callback ();
			Destroy (gameObject);
		}
	}

	public void SetTimer(Action cb, float duration = 1.0f) {
		timer.Start ();
		callback = cb;
		timerLength = duration;
	}
}
