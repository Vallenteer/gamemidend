using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
	public GameObject TitleUI;
	public GameObject LevelSelectorUI;

	// Use this for initialization
	void Start () {
		TitleUI.SetActive (true);
		LevelSelectorUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void play()
    {
        //SceneManager.LoadScene("VuvoriaTestGameplay");
		TitleUI.SetActive(false);
		LevelSelectorUI.SetActive (true);
    }
    public void quit()
    {
        Application.Quit();
    }
}
