using UnityEngine;
using System.Collections;
using Vuforia;

/// <summary>
/// track one image target and ask the controller to summon the recognised character.
/// </summary>
public class TrackableEventHandler : MonoBehaviour, ITrackableEventHandler 
{
	#region PRIVATE_MEMBER_VARIABLES

	[SerializeField] bool kinemateObjectOnStart;
	[SerializeField] string characterID;
	//[SerializeField] GameObject characterPrefab;
	private TrackableBehaviour mTrackableBehaviour;
	private GameController gameController;
	private Rigidbody rb;

	#endregion // PRIVATE_MEMBER_VARIABLES



	#region UNTIY_MONOBEHAVIOUR_METHODS

	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		//
		gameController = Component.FindObjectOfType<GameController> ();
		rb = GetComponentInChildren<Rigidbody> ();
		if(rb && kinemateObjectOnStart)
			rb.isKinematic = kinemateObjectOnStart;

		// Disable child
		transform.GetChild(0).gameObject.SetActive(false);

		CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}

	#endregion // UNTIY_MONOBEHAVIOUR_METHODS



	#region PUBLIC_METHODS

	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}

	#endregion // PUBLIC_METHODS



	#region PRIVATE_METHODS


	private void OnTrackingFound()
	{	
		GameObject fighter = transform.GetChild (0).gameObject;

		if (gameController.SummonedCharacter == null) {
			CharacterData.LoadCharacterData (characterID);
			gameController.OnCharacterFound (CharacterData.LoadedCharData);
			fighter.GetComponent<Fighter> ().LinkFighter2Button ();
		}

		//rb.isKinematic = false;
		//Debug.Log ("ontrackingfound");
		/*
		Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);


		// Enable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = true;
		}

		// Enable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = true;
		}
		*/
			
		// Enable child
		if (gameController.SummonedCharacter.charID.Equals (characterID)) {
			
			fighter.SetActive (true);
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
		}

	}


	private void OnTrackingLost()
	{
		//rbComponent.isKinematic = true;
		if(gameController && gameController.HideArenaOnTrackingLost) gameController.BuildArena (false);

		if(rb)
			rb.isKinematic = true;

		//FIXME: instead of matiin ini
		/*
		Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

		// Disable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = false;
		}

		// Disable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = false;
		}
		*/
		GameObject fighter = transform.GetChild(0).gameObject;
		fighter.SetActive(false);


		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
	}

	#endregion // PRIVATE_METHODS
}
