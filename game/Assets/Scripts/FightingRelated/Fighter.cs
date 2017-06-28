﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {
    public enum PlayerType
    {
        HUMAN, AI
    };

    public static float MAX_HEALTH = 100f;

    public float health = MAX_HEALTH;
    public string fighterName;
    //FOR AI
    public Fighter oponent;
    public bool enable;

    //for AI only
    private float random;
    private float randomSetTime;

    public PlayerType player;
    public FighterStates currentState = FighterStates.IDLE;

    protected Animator animator;
    private Rigidbody myBody;
    private AudioSource audioPlayer;

    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        GameObject[] ToFindOponent = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject enemy in ToFindOponent)
        {
            if (enemy != gameObject)
            {
                oponent = enemy.GetComponent<Fighter>();
            }
        }
    }

    private float getRotationOpponent()
    {
        return Vector3.Angle(gameObject.transform.forward, oponent.transform.position - gameObject.transform.position);
    }
    private float getDistanceToOponent()
    {
        return Vector3.Distance(new Vector3(oponent.transform.position.x, oponent.transform.position.y, oponent.transform.position.z), (new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z)));
    }

    public void UpdateAiInput()
    {
        //turn
        if (getRotationOpponent() < 10)
        {
            animator.SetBool("LEFT", true);
        }
        else
        {
            animator.SetBool("LEFT", false);
        }
        if (getRotationOpponent() > 30)
        {
            animator.SetBool("RIGHT", true);
        }
        else
        {
            animator.SetBool("RIGHT", false);
        }

        //to move forward
        if (random < 0.8 || getDistanceToOponent()<7)
        {
            if (getDistanceToOponent() > 7)
            {
                animator.SetBool("WALK", true);
            }
            else
            {
                animator.SetBool("WALK", false);
            }
                        
        }
        if (Time.time - randomSetTime > 1)
        {
            random = Random.value;
            randomSetTime = Time.time;
        }

        if (random > 0.6 && getDistanceToOponent() < 6.5 && oponent.health > 0.01)
        {
            animator.SetTrigger("ATTACK");
        }
        else if (random < 0.2 && getDistanceToOponent() > 10 && getDistanceToOponent() < 15 && oponent.health > 40)
        {
            animator.SetTrigger("SPECIAL");
        }


    }

    public void UpdateHumanInput()
    {
        if (Input.GetAxis("Vertical") > 0.1)
        {
            animator.SetBool("WALK", true);
        }
        else
        {
            animator.SetBool("WALK", false);
        }

        if (Input.GetAxis("Vertical") < -0.1)
        {
            animator.SetBool("WALK_BACK", true);
        }
        else
        {
            animator.SetBool("WALK_BACK", false);
        }
        if (Input.GetAxis("Horizontal") < -0.1)
        {
            animator.SetBool("LEFT", true);
        }
        else
        {
            animator.SetBool("LEFT", false);
        }
        if (Input.GetAxis("Horizontal") > 0.1)
        {
            animator.SetBool("RIGHT", true);
        }
        else
        {
            animator.SetBool("RIGHT", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("ATTACK");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("SPECIAL");
        }
    }
    // Update is called once per frame
    void Update () {
        if (player == PlayerType.HUMAN)
        {
            UpdateHumanInput();
        }
        else
        {
            UpdateAiInput();
        }

        if (health <= 0 && currentState != FighterStates.DIED)
        {
            animator.SetTrigger("DIED");
        }

    }

    public Rigidbody body
    {
        get
        {
            return this.myBody;
        }
    }

    public virtual void hurt(float damage)
    {
        if (!invulnerable)
        {
            if (defending)
            {
                damage *= 0.2f;
            }
            if (health >= damage)
            {
                health -= damage;
            }
            else
            {
                health = 0;
            }

            if (health > 0)
            {
                animator.SetTrigger("GET_HIT");
            }
        }
    }
    public bool defending
    {
        get
        {
            return currentState == FighterStates.DEFEND
                || currentState == FighterStates.GET_HIT_DEFEND;
        }
    }
    public bool invulnerable
    {
        get
        {
            return currentState == FighterStates.GET_HIT
                || currentState == FighterStates.GET_HIT_DEFEND
                    || currentState == FighterStates.DIED;
        }
    }

    public bool attacking
    {
        get
        {
            return currentState == FighterStates.ATTACK;
        }
    }

    public float healtPercent
    {
        get
        {
            return health / MAX_HEALTH;
        }
    }
}
