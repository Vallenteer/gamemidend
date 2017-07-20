using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {
    public enum PlayerType
    {
        HUMAN, AI
    };
    //for Human 
    private LeftJoystick leftJoystick;
    float leftJoystickInputX;
    float leftJoystickInputY;

    public static float MAX_HEALTH = 100f;

    public float health = MAX_HEALTH;
    public string fighterName;
    //FOR AI
    public Fighter oponent;
    public bool enable;

    //for AI only
    [Header("FOR AI")]
    private float random;
    private float randomSetTime;
    private float sizeModel;
    public float distanceEnemy = 7f;
    public float distanceForUlti = 9f;
    [Tooltip("Batas bawah")]
    public float distanceForSpecialDownLimit = 10f;
    [Tooltip("Batas atas")]
    public float distanceForSpecialUpperLimit = 15f;
    [Tooltip("Batas Rotatsi Kanan")]
    public float rotationRightLimit =20f;
    [Tooltip("Batas Rotasi Kiri")]
    public float rotationLeftLimit = 10f;

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
        sizeModel = transform.localScale.x;
        leftJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<LeftJoystick>();
       // Debug.Log(leftJoystick);
        GameObject[] ToFindOponent = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject enemy in ToFindOponent)
        {
            if (enemy != gameObject && enemy.activeInHierarchy==true)
            {
                oponent = enemy.GetComponent<Fighter>();
                //Debug.Log(oponent.name);
            }
        }

		/// FOR TESTING ONLY
		/// activate fighter instantly OnStart instead of Vuforia OnTrackFound
		//ActivateFighter ();
    }

    private float getRotationOpponent()
    {
        return Vector3.Angle(gameObject.transform.forward, oponent.transform.position - gameObject.transform.position);
    }
    private float getDistanceToOponent()
    {
        return Vector3.Distance(new Vector3(oponent.transform.position.x, oponent.transform.position.y, oponent.transform.position.z), (new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z)))/sizeModel;
    }

    public void UpdateAiInput()
    {
        //turn
        if (getRotationOpponent() < rotationLeftLimit)
        {
            animator.SetBool("LEFT", true);
        }
        else
        {
            animator.SetBool("LEFT", false);
        }
        if (getRotationOpponent() > rotationRightLimit)
        {
            animator.SetBool("RIGHT", true);
        }
        else
        {
            animator.SetBool("RIGHT", false);
        }

        //to move forward
        if (random < 0.8 || getDistanceToOponent()<distanceEnemy)
        {
            if (getDistanceToOponent() > distanceEnemy)
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

        if (random > 0.6 && getDistanceToOponent() < distanceEnemy && oponent.health > 0.01)
        {
            animator.SetTrigger("ATTACK");
        }
        else if (random < 0.2 && getDistanceToOponent() > distanceForSpecialDownLimit && getDistanceToOponent() < distanceForSpecialUpperLimit && oponent.health > 40)
        {
            animator.SetTrigger("SPECIAL");
        }
        else if (random < 0.5 && random >0.3 && getDistanceToOponent() > distanceForUlti && oponent.health > 50)
        {
            animator.SetTrigger("ULTIMATE");
        }


    }

    public void UpdateHumanInput()
    {
        leftJoystickInputX = leftJoystick.GetInputDirection().x;
        leftJoystickInputY = leftJoystick.GetInputDirection().y;

        if (Input.GetAxis("Vertical") > 0.1| leftJoystickInputY > 0.1)
        {
            animator.SetBool("WALK", true);
        }
        else
        {
            animator.SetBool("WALK", false);
        }

        if (Input.GetAxis("Vertical") < -0.1||leftJoystickInputY<-0.1)
        {
            animator.SetBool("WALK_BACK", true);
        }
        else
        {
            animator.SetBool("WALK_BACK", false);
        }
        if (Input.GetAxis("Horizontal") < -0.1|| leftJoystickInputX<-0.5)
        {
            animator.SetBool("LEFT", true);
        }
        else
        {
            animator.SetBool("LEFT", false);
        }
        if (Input.GetAxis("Horizontal") > 0.1||leftJoystickInputX>0.5)
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

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("ULTIMATE");
        }
    }

	public void OnAttackButton() {
        if (player == PlayerType.HUMAN)
        { animator.SetTrigger("ATTACK"); }
	}

	public void OnMagicButton() {

        if (player == PlayerType.HUMAN)
        { animator.SetTrigger("SPECIAL"); }
	}
    public void OnUltimateButton() {
        if (player == PlayerType.HUMAN)
        { animator.SetTrigger("ULTIMATE"); }
    }


    // Update is called once per frame
    void Update () {
        if (player == PlayerType.HUMAN)
        {
            UpdateHumanInput();
        }
        else
        {
            if (oponent == null ||oponent.gameObject.activeInHierarchy==false)
            {
                GameObject[] ToFindOponent = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject enemy in ToFindOponent)
                {
                    if (enemy != gameObject && enemy.activeInHierarchy == true)
                    {
                        oponent = enemy.GetComponent<Fighter>();
                        Debug.Log(oponent.name);
                    }
                }
            }
            else
            {
                UpdateAiInput();
            }
            
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

	public void ActivateFighter()
	{
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().Register3AttackButtonListeners (
			OnAttackButton, OnMagicButton, OnUltimateButton);
	}
    
}
