using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_ClickToChangeText : MonoBehaviour {

    [SerializeField] GameObject ImageBoxLeft;
    [SerializeField] GameObject ImageBoxRight;
    [SerializeField] Text[] textList;
    [SerializeField] AudioClip[] DubText;
    AudioSource AudioDub;
    // First Text
    int activatedObj = 0;
    [Tooltip("False=Left, True=Right.")]
    [SerializeField] bool[] PositionImage; // false = Left, true = Right
    [SerializeField] Sprite[] Images;

    [SerializeField] int NextScene;


    // Use this for initialization
    void Start () {
        AudioDub = GetComponent<AudioSource>();
        activatedObj = 0;
        foreach (Text list in textList)
        {
            list.gameObject.SetActive(false);
        }


        if (textList[activatedObj] != null)
        {
            

            if (PositionImage[activatedObj] == true)
            {
                ImageBoxRight.transform.GetComponent<Image>().sprite = Images[activatedObj];
            }
            else
            {
                ImageBoxLeft.transform.GetComponent<Image>().sprite = Images[activatedObj];
            }
            //Make sure the picture is transparant if there is no picture
            if (ImageBoxRight.transform.GetComponent<Image>().sprite == null)
            {
                ImageBoxRight.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                ImageBoxRight.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            if (ImageBoxLeft.transform.GetComponent<Image>().sprite == null)
            {
                ImageBoxLeft.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                ImageBoxLeft.transform.GetComponent<Image>().color = Color.white;
            }


            AudioDub.Stop();
            AudioDub.clip = DubText[activatedObj];
            AudioDub.Play();
            textList[activatedObj].gameObject.SetActive(true);
        }

    }

    public void NextSceneMethod(int nextScene)
    {
        SceneManager.LoadScene(nextScene);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            textList[activatedObj].gameObject.SetActive(false);
            activatedObj++;

            if (textList.Length > activatedObj)
            {
                AudioDub.Stop();
                AudioDub.clip = DubText[activatedObj];
                AudioDub.Play();

                textList[activatedObj].gameObject.SetActive(true);
                
                if (PositionImage[activatedObj] == true)
                {
                    ImageBoxRight.transform.GetComponent<Image>().sprite = Images[activatedObj];
                }
                else
                {
                    ImageBoxLeft.transform.GetComponent<Image>().sprite = Images[activatedObj];
                }

                //Make sure the picture is transparant if there is no picture
                if (ImageBoxRight.transform.GetComponent<Image>().sprite == null)
                {
                    ImageBoxRight.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
                else
                {
                    ImageBoxRight.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                if (ImageBoxLeft.transform.GetComponent<Image>().sprite == null)
                {
                    ImageBoxLeft.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
                else
                {
                    ImageBoxLeft.transform.GetComponent<Image>().color = Color.white;
                }
            }
            else
            {
                SceneManager.LoadScene(NextScene);
                //Debug.Log ("Text abis");
            }

        }
    }
}
