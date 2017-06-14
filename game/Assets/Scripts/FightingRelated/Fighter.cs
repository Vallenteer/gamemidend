using System.Collections;
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
    public Fighter oponent;
    public bool enable;

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
        //audioPlayer = GetComponent<AudioSource>();
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
        //else
        //{
        //   // UpdateAiInput();
        //}

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
