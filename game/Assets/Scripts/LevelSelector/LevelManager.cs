using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    [System.Serializable]
    public class Level
    {
        public string LevelText; //text untuk angka di stage
        public int UnLocked; //0 untuk locked
        public bool IsInteractable; //bisa diteken apa engga

        //public Button.ButtonClickedEvent OnClickEvent; //ngerecord pas di klik
    }

    public List<Level> LevelList;
    public GameObject levelButton;
    public Transform Spacer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FillList()
    {
        foreach (var level in LevelList)
        {
            GameObject newbutton = Instantiate(levelButton) as GameObject; //load semua data dari list
            LevelButtonScript button = newbutton.GetComponent<LevelButtonScript>();
            button.LevelText.text = level.LevelText;

            //Level_001, Level_002
            //string newValue = button.LevelText.text.PadLeft(3,'0');
            if (PlayerPrefs.GetInt(button.LevelText.text) == 1)
            {
                level.UnLocked = 1;
                level.IsInteractable = true;
            }

            //Debug.Log(newValue);

            button.unlocked = level.UnLocked;
            button.GetComponent<Button>().interactable = level.IsInteractable;
            button.GetComponent<Button>().onClick.AddListener(() => loadLevels(button.LevelText.text));

            if (PlayerPrefs.GetInt(button.LevelText.text + "_score") == 1)
            {
                //button.Star1.SetActive(true);
            }
            else if (PlayerPrefs.GetInt(button.LevelText.text + "_score") == 2)
            {
                //button.Star1.SetActive(true);
                //button.Star2.SetActive(true);
            }
            else if (PlayerPrefs.GetInt(button.LevelText.text + "_score") == 3)
            {
                //button.Star1.SetActive(true);
                //button.Star2.SetActive(true);
                //button.Star3.SetActive(true);
            }



            newbutton.transform.SetParent(Spacer);
        }
        SaveAll();
    }

    void SaveAll()
    {
        GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");

        foreach (GameObject buttons in allButtons)
        {
            LevelButtonScript button = buttons.GetComponent<LevelButtonScript>();
            //string newValue = button.LevelText.text.PadLeft(3, '0');
            PlayerPrefs.SetInt(button.LevelText.text, button.unlocked);
        }
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    void loadLevels(string value)
    {
        SceneManager.LoadScene(value);
    }
}
