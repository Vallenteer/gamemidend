using System;
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
	[SerializeField] GameObject UICanvasGO;

	[HideInInspector] public CharacterData SummonedCharacter;
	[HideInInspector] public CharacterData EnemyCharacter;
	private int healthVal;

    SoundManager soundManager;

	#region UI Buttons Handler
	[Header("UI BUTTONS")]
	public Button AtkButton;
	public Button MgcButton;
	public Button UltiButton;

	public Image MgcCoolDown;
	public Image UltCoolDown;
    #endregion

	[Header("BATTLE RESULT")]
	public GameObject BattleResUI;
	public Text EarnedExpText;
	public Text NameText;
	public Text LevelText;
	public Text ExpText;

    #region BattleController
    public int roundTime = 99;
    private float lastTimeUpdate = 0;
    private bool battleStarted;
    private bool battleEnded;
    private bool FirstBanner;
    public BannerController banner;
    //Human
	[HideInInspector] public Fighter player1;
    //AI
	[HideInInspector] public Fighter player2;
    #endregion

    Stopwatch timerMagic;
	Stopwatch timerUlti;

	public bool HideArenaOnTrackingLost {
		get {
			return hideArenaOnTrackingLost;
		}
	}
    private void Awake()
    {
       soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // Use this for initialization
    void Start () {
		//arenaCircle.SetActive (false);
		arenaRenderer.enabled = false;
		timerMagic = new Stopwatch ();
		timerUlti = new Stopwatch ();
		UICanvasGO.SetActive (false);
        battleStarted = false;
        FirstBanner = false;
		BattleResUI.SetActive (false);
        
        //if (player1 != null && player1.gameObject.activeInHierarchy==true)
        //{
        //    banner.showRoundFight();
        //}

    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(battleStarted);
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
            
            if (battleStarted==true && player1.enable == false)
            {
               // Debug.Log("1 Kelvin");
                player1.enable = true;
            }
            else if (battleStarted == false && banner.isAnimating==false)
            {
                //Debug.Log("2 Kelvin");
                battleStarted = true;

                player1.enable = true;
                player2.enable = true;
            }
            else if (battleStarted==false && player1.gameObject.activeInHierarchy == true  && FirstBanner==false)
            {
                //TODO: add show button OnTrackableFound. find a better optimisation if possible
                //Debug.Log("3 Kelvin");
                UICanvasGO.SetActive(true);
                FirstBanner = true;
                banner.showRoundFight();
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
					//TODO: call battle result
                }
            }

            if (player1.healtPercent <= 0)
            {
                banner.showYouLose();
                battleEnded = true;
				//TODO: call battle result
            }
            else if (player2.healtPercent <= 0)
            {
                soundManager.BgmWin();
                banner.showYouWin();
                battleEnded = true;
				//TODO: call battle result
				ShowBattleResultUI(true);
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

	/// to be called from TrackableEventHandler during trackable found/lost
	public void SetPlayerControlInteractable(bool val) {
		if (SummonedCharacter != null) {
			AtkButton.interactable = val;
			MgcButton.interactable = val;
			UltiButton.interactable = val;
			MgcCoolDown.enabled = val;
			UltCoolDown.enabled = val;
		}

		//TODO: KELVIN, tolong make sure dpad nya juga bisa set un/interactable
	}


	public void OnCharacterFound(CharacterData cd) {
		BuildArena (true);
		SummonedCharacter = cd;

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
		if (SummonedCharacter != null) {
			//CharacterData.LoadCharacterData ("000");
			//OnCharacterFound (CharacterData.LoadedCharData);
			MgcButton.interactable = false;
			timerMagic.Start ();
			StartCoroutine ("CoolDownMGC");

		}
	}


	public void StartCoolDownULT() {
		if (SummonedCharacter != null) {
			//CharacterData.LoadCharacterData ("000");
			//OnCharacterFound (CharacterData.LoadedCharData);
			UltiButton.interactable = false;
			timerUlti.Start ();
			StartCoroutine ("CoolDownULT");
		}
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

	void ShowBattleResultUI(bool wins) {
		Debug.Log ("ShowBattleResultUI");
		BattleResUI.SetActive (true);
		NameText.text = SummonedCharacter.Namae;
		LevelText.text = SummonedCharacter.Level.ToString ();

		if(wins) {
			int result;
			int.TryParse (EarnedExpText.text, out result);
			ExpText.text = result + "/" + SummonedCharacter.TargetExp;
			SummonedCharacter.CurExp += result;

		}
	}
}
