﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug=UnityEngine.Debug;

public class GameController : MonoBehaviour {
	//start vuforia camera
	//wait for detected target
	//summon arena, summon momon immediately
	//summon enemy. load enemy parameters
	//summon ui, with animation entry
	//for ui bar, set the value 100%. health value hidden.
	//set 

	[SerializeField] private Renderer arenaRenderer;
	[SerializeField] bool hideArenaOnTrackingLost;
	[SerializeField] Text healthText;
	[HideInInspector] public CharacterData SummonedCharacter;
	[HideInInspector] public CharacterData EnemyCharacter;
	private int healthVal;

	#region UI Buttons Handler
	public Button AtkButton;
	public Button MgcButton;
	public Button UltiButton;

	public Image MgcCoolDown;
	public Image UltCoolDown;
    #endregion

    #region BattleController
    public int roundTime = 99;
    private float lastTimeUpdate = 0;
    private bool battleStarted;
    private bool battleEnded;
    public BannerController banner;
    //Human
    public Fighter player1;
    //AI
    public Fighter player2;
    #endregion

    Stopwatch timerMagic;
	Stopwatch timerUlti;

	public bool HideArenaOnTrackingLost {
		get {
			return hideArenaOnTrackingLost;
		}
	}
			
	// Use this for initialization
	void Start () {
		//arenaCircle.SetActive (false);
		arenaRenderer.enabled = false;
		timerMagic = new Stopwatch ();
		timerUlti = new Stopwatch ();

        GameObject[] ToFindFighter = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject fighter in ToFindFighter)
        {
            if (fighter.GetComponent<Fighter>().player == Fighter.PlayerType.HUMAN && fighter.gameObject.activeInHierarchy == true)
            {
                player1 = fighter.GetComponent<Fighter>();
            }
            if (fighter.GetComponent<Fighter>().player == Fighter.PlayerType.AI)
            {
                player2 = fighter.GetComponent<Fighter>();
            }
        }
        if (player1 != null)
        {
            banner.showRoundFight();
        }

    }
	
	// Update is called once per frame
	void Update () {
        
        GameObject[] ToFindFighter = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject fighter in ToFindFighter)
        {
            // Debug.Log(fighter.name + "  for HUD");
            if (fighter.GetComponent<Fighter>().player == Fighter.PlayerType.HUMAN && fighter.activeInHierarchy == true)
            {
                player1 = fighter.GetComponent<Fighter>();
            }
        }
        if (player1 != null)
        {
            
            if (!battleStarted && !banner.isAnimating)
            {
                battleStarted = true;

                player1.enable = true;
                player2.enable = true;
            }
            if (battleStarted && player1.enable == false)
            {
                player1.enable = true;
            }
        }
        if (battleStarted && !battleEnded)
        {
            if (roundTime > 0 && Time.time - lastTimeUpdate > 1)
            {
                roundTime--;
                lastTimeUpdate = Time.time;
                if (roundTime == 0)
                {
                    expireTime();
                }
            }

            if (player1.healtPercent <= 0)
            {
                banner.showYouLose();
                battleEnded = true;

            }
            else if (player2.healtPercent <= 0)
            {
                banner.showYouWin();
                battleEnded = true;
            }
        }

    }

    private void expireTime()
    {
        if (player1.healtPercent > player2.healtPercent)
        {
            player2.health = 0;
        }
        else
        {
            player1.health = 0;
        }
    }

    public void BuildArena(bool val) {
		//arenaCircle.SetActive (val);
		arenaRenderer.enabled = val;
	}

	public void CharacterFound(CharacterData cd) {
		BuildArena (true);
		SummonedCharacter = cd;
		//SetHealth (cd.HP);

	}
	/*
	void SetHealth(int val) {
		healthVal = val;
		healthText.text = "HP: " + val.ToString ();
	}*/

	public void Register3AttackButtonListeners(Action atkBtnCallBack, Action mgcBtnCallBack, Action ultBtnCallBack) {
		AtkButton.onClick.AddListener (delegate() {
			atkBtnCallBack();
		});

		//add listener to fighter script + to start cooldown
		MgcButton.onClick.AddListener (delegate() {
			mgcBtnCallBack();
		});


		UltiButton.onClick.AddListener (delegate() {
			ultBtnCallBack();
		});
	}

	public void StartCoolDownMGC() {
		if (SummonedCharacter==null) {
			CharacterData.LoadCharacterData ("000");
			CharacterFound (CharacterData.LoadedCharData);
		}

		MgcButton.interactable = false;
		timerMagic.Start ();
		StartCoroutine ("CoolDownMGC");
	}


	public void StartCoolDownULT() {
		if (SummonedCharacter==null) {
			CharacterData.LoadCharacterData ("000");
			CharacterFound (CharacterData.LoadedCharData);
		}

		UltiButton.interactable = false;
		timerUlti.Start ();
		StartCoroutine ("CoolDownULT");
	}


	IEnumerator CoolDownMGC() {
		Debug.Log ("CD MG");
		float mgcCdTime = SummonedCharacter.CooldownMGC;

		do {
			MgcCoolDown.fillAmount = (float)timerMagic.ElapsedMilliseconds / 1000 / mgcCdTime;
			yield return new WaitForEndOfFrame();
		} while ((float)timerMagic.ElapsedMilliseconds/1000.0f <= mgcCdTime);

		//Debug.Log ((float)timerMagic.ElapsedMilliseconds/100);

		timerMagic.Reset ();
		MgcButton.interactable = true;
		yield break;
	}

	IEnumerator CoolDownULT() {
		Debug.Log ("CD UL");
		float ultCdTime = SummonedCharacter.CooldownULT;

		do {
			UltCoolDown.fillAmount = (float)timerUlti.ElapsedMilliseconds / 1000 / ultCdTime;
			yield return new WaitForEndOfFrame();
		} while ((float)timerUlti.ElapsedMilliseconds/1000.0f <= ultCdTime);

		//Debug.Log ((float)timerMagic.ElapsedMilliseconds/100);

		timerUlti.Reset ();
		UltiButton.interactable = true;
		yield break;
	}

}
