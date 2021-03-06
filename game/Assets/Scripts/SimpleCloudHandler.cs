﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler {
	private CloudRecoBehaviour mCloudRecoBehaviour;
	private bool mIsScanning = false;
	private string mTargetMetadata = "";

	// Use this for initialization
	void Start () {
		mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();

		if (mCloudRecoBehaviour) {
			mCloudRecoBehaviour.RegisterEventHandler(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		// Display current 'scanning' status
		GUI.Box (new Rect(100,100,200,50), mIsScanning ? "Scanning" : "Not scanning");
		// Display metadata of latest detected cloud-target
		GUI.Box (new Rect(100,200,200,50), "Metadata: " + mTargetMetadata);
		// If not scanning, show button
		// so that user can restart cloud scanning
		if (!mIsScanning) {
			if (GUI.Button(new Rect(100,300,200,50), "Restart Scanning")) {
				// Restart TargetFinder
				mCloudRecoBehaviour.CloudRecoEnabled = true;
			}
		}
	}

	#region ICloudRecoEventHandler Interface

	public void OnInitialized() {
		Debug.Log ("Cloud Reco initialized");
	}

	public void OnInitError(TargetFinder.InitState initError) {
		Debug.Log ("Cloud Reco init error " + initError.ToString());
	}

	public void OnUpdateError(TargetFinder.UpdateState updateError) {
		Debug.Log ("Cloud Reco update error " + updateError.ToString());
	}

	public void OnStateChanged(bool scanning) {
		mIsScanning = scanning;
		if (scanning)
		{
			// clear all known trackables
			//ImageTracker tracker = TrackerManager.Instance.GetTracker<ImageTracker>();
			//tracker.TargetFinder.ClearTrackables(false);
			Debug.Log("Object Found");
		}
	}

	// Here we handle a cloud target recognition event
	public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult) {
		// do something with the target metadata
		mTargetMetadata = targetSearchResult.MetaData;
		// stop the target finder (i.e. stop scanning the cloud)
		mCloudRecoBehaviour.CloudRecoEnabled = false;
	}

	#endregion
}
