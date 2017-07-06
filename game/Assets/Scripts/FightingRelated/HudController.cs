using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    private Fighter player1;
    private Fighter player2;

    public Text player1Tag;
    public Text player2Tag;

    public Scrollbar leftBar;
    public Scrollbar rightBar;

    public Text timerText;

    //public BattleController battle;


    // Use this for initialization
    void Start()
    {
        GameObject[] ToFindFighter = GameObject.FindGameObjectsWithTag("Player");
       // Debug.Log(ToFindFighter.GetUpperBound(0));
        foreach (GameObject fighter in ToFindFighter)
        {
           Debug.Log(fighter.name + "  for HUD");
            if (fighter.GetComponent<Fighter>().player == Fighter.PlayerType.HUMAN)
            {
                player1 = fighter.GetComponent<Fighter>();
            }
            if(fighter.GetComponent<Fighter>().player == Fighter.PlayerType.AI)
            {
                player2 = fighter.GetComponent<Fighter>();
            }
        }
        player1Tag.text = player1.fighterName;
        player2Tag.text = player2.fighterName;
    }

    // Update is called once per frame
    void Update()
    {
        //timerText.text = battle.roundTime.ToString();

        if (leftBar.size > player1.healtPercent)
        {
            leftBar.size -= 0.01f;
        }
        if (rightBar.size > player2.healtPercent)
        {
            rightBar.size -= 0.01f;
        }
    }
}
