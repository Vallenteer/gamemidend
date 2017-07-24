using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


    [SerializeField] AudioClip[] SoundClip;
    //0 = Menu
    //1 = Story
    //2 = Fight1
    //3 = Fight2
    //4 = Win
    //5 = Lose
    public static AudioSource AudioBGM;

    void Awake()
    {
        AudioBGM = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        //in Menu
        AudioBGM.clip = SoundClip[0];
        AudioBGM.Play();
    }

    
	
	// Update is called once per frame
	void Update () {
		
	}
    public void BgmMenu()
    {
        AudioBGM.Stop();
        AudioBGM.clip = SoundClip[0];
        AudioBGM.Play();
    }
    public void BgmStory()
    {
        AudioBGM.Stop();
        AudioBGM.clip = SoundClip[1];
        AudioBGM.Play();
    }
    public void BgmFight1()
    {
        AudioBGM.Stop();
        AudioBGM.clip = SoundClip[2];
        AudioBGM.Play();
    }
    public void BgmFight2()
    {
        AudioBGM.Stop();
        AudioBGM.clip = SoundClip[3];
        AudioBGM.Play();
    }
    public void BgmWin()
    {
        AudioBGM.Stop();
        AudioBGM.clip = SoundClip[4];
        AudioBGM.Play();
    }
    public void BgmLose()
    {
        AudioBGM.Stop();
        AudioBGM.clip = SoundClip[5];
        AudioBGM.Play();
    }
}
