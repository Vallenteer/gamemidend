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
    //public FighterStates currentState = FighterStates.IDLE;

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
        if (Input.GetAxis("Horizontal") > 0.1)
        {
            animator.SetBool("WALK", true);
        }
        else
        {
            animator.SetBool("WALK", false);
        }

        if (Input.GetAxis("Horizontal") < -0.1)
        {
            animator.SetBool("WALK_BACK", true);
        }
        else
        {
            animator.SetBool("WALK_BACK", false);
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
        UpdateHumanInput();
	}

    public Rigidbody body
    {
        get
        {
            return this.myBody;
        }
    }
}
