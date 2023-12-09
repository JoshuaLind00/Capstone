using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D box2D;
    private CapsuleCollider2D capsule2D;
    [SerializeField] private AudioSource audioM;
    [SerializeField] private AudioClip run, jump, attack, range, hit, death;
    StatBars getStats;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private Transform ThrustPosition;
    [SerializeField] private Transform SwingPosition;
    [SerializeField] private Transform FloatPosition;
    [SerializeField] private Transform FirePosition;
    [SerializeField] private GameObject Projectile;
    [SerializeField] Enemy_B_Script unitBVar;
    /*public float score = ScoreScript.scoreValue;
    public static int finalScore;
    public int finalCheck = finalScore;*/
    public bool timerActive = true;
    public float currentTime;
    public float finalTime;

    public float thrustRange = 1f;
    public float thrustHeight = .75f;
    public float swingRange = 1f;
    public float floatRange = 1f;


    [SerializeField] private float movement;
    public float moveSpeed;
    [SerializeField] private float defaultSpeed;
    public float jumpForce;
    public float currentHealth, currentMagic, currentStamina;
    public bool isGrounded;
    public static float str;
    public static float mag;
    public static float stam;
    //private bool attkL = false;
    //private bool attkH = false;
    //private bool attkM = false;
    public float jumpingReq = 60;
    public float castingReq = 30;
    [SerializeField] private bool testing;
    [SerializeField] private bool testingMelee;
    public float lightA;
    public float heavyA;
    [SerializeField] private float attkRecoveryL = 1;
    [SerializeField] private float attkRecoveryH = 1;
    [SerializeField] private float recoveryTimer = 10;
    [SerializeField] private float hitRecovery;
    [SerializeField] private float hitTimer = 10;
    [SerializeField] private float walkTimer = 10;
    [SerializeField] private float attkL_damage = 10;
    [SerializeField] private float attkH_damage, attkM_damage = 35;
    public bool draining = false;
    public bool winGame = false;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box2D = GetComponent<BoxCollider2D>();
        capsule2D = GetComponent<CapsuleCollider2D>();
        audioM = GetComponent<AudioSource>();
        getStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatBars>();
        if (!testing)
        {
            str = StatManager.StrengthStat;
            mag = StatManager.MagicStat;
            stam = StatManager.StaminaStat;
        }
        else
        {
            str = 10;
            mag = 15;
            stam = 5;
        }


    }

    void Update()
    {
        //Updates bars and damage values based on preset stats
        currentHealth = getStats.getHealth();
        currentMagic = getStats.getMagic();
        currentStamina = getStats.getStamina();
        attkL_damage = lightA * (1 + str * .2f);
        attkH_damage = heavyA * (1 + str * .3f);
        attkM_damage = heavyA * (1 + mag * .3f);

        recoveryTimer += Time.deltaTime;
        hitTimer += Time.deltaTime;
        walkTimer += Time.deltaTime;
        isGrounded = Grounded();


        //Actions a player can do while their health is above 0
        if (currentHealth > 0)
        {
            //Player movement
            movement = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
            

            //Flip character movement
            if (!Mathf.Approximately(0, movement))
            {
                transform.rotation = movement > 0 ? Quaternion.Euler(0f, -180f, 0f) : Quaternion.identity;
            }

            //Enablees players to jump provided that they have stamina and release the SpaceBar
            if (Input.GetKeyUp(KeyCode.Space) && currentStamina >= jumpingReq && rb.velocity.y <= 0)
            {
                Jump();
            }

            //Enables the player to activate 3 different attacks with the W and S keys, each with different properties
            if (recoveryTimer >= attkRecoveryL)
            {
                if (Input.GetKeyUp(KeyCode.W) && isGrounded)
                {

                    recoveryTimer = 0;
                    audioM.clip = attack;
                    anim.SetBool("LightAttk", true);
                }
            }
            if (recoveryTimer >= attkRecoveryH)
            { 
                if (Input.GetKeyUp(KeyCode.S) && isGrounded)
                {
                    if (currentMagic >= castingReq)
                    {

                        recoveryTimer = 0;
                        audioM.clip = attack;
                        anim.SetBool("MagicAttk", true);
                        getStats.consumeMagic(castingReq);

                    }
                    else
                    {

                        recoveryTimer = 0;
                        audioM.clip = attack;
                        anim.SetBool("HeavyAttk", true);
                        getStats.consumeMagic(castingReq);
                    }
                }
            }
            if (winGame)
            {
                anim.SetBool("Win", true);
            }

            if (timerActive == true)
            {
                currentTime = currentTime + Time.deltaTime;
                finalTime = currentTime;
            }

        }
        else
        {
            //Sets the values for the dying animation to play
            LayerMask.NameToLayer("Default");
            StopTimer();
            finalTime = currentTime;
            anim.SetTrigger("Death");
            anim.SetBool("Dying", true);
        }


        //Testing damage to player and damage animations as well as changing canvas assets;
        if (testing)
        {
            if (Input.GetKeyUp(KeyCode.O))
            {
                hitPlayer(25f);
                str += 2;
                mag += 3;
                stam += 5;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                transform.position = new Vector2(25, 333);
            }
        }

        anim.SetBool("Grounded", Grounded());
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetFloat("xVelocity", rb.velocity.x);

        
    }

    private void LateUpdate()
    {
        if (movement == 0f)
        {
            moveSpeed = 0;
        }
        else if(movement != 0)
        {
            moveSpeed = defaultSpeed;
        }
        if (movement != 0 && isGrounded && walkTimer >= .25f)
        {
            if (currentHealth > 0)
            {
                walkTimer = 0;
                audioM.clip = run;
                audioM.Play();
            }
            
        }else if (currentHealth <= 0 && walkTimer >= 1.5)
        {
            walkTimer = 0;
            audioM.clip = death;
            audioM.Play();
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            SceneManager.LoadScene("OpeningScene");
        }
    }


    private void Jump()
    {
        audioM.clip = jump;
        audioM.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        getStats.consumeStamina(jumpingReq);
        //Debug.Log("Jump Button Pressed. Remaining Stamina = " + getStats.getStamina());

    }


    private bool Grounded()
    {
        //Determines if the bottom of the ColliderBox2D is touching an object with the mask "Ground"
        return Physics2D.BoxCast(box2D.bounds.center, box2D.bounds.size, 0f, Vector2.down, .1f, groundMask);
    }


 
    public void stopAttack()
    {
        anim.SetBool("LightAttk", false);
        anim.SetBool("MagicAttk", false);
        anim.SetBool("HeavyAttk", false);
    }


    public void AttackM()
    {
        audioM.clip = attack;
        audioM.Play();
        Collider2D[] enemy = Physics2D.OverlapCircleAll(SwingPosition.position, swingRange, EnemyMask);
        foreach (Collider2D enemyObject in enemy)
        {
            
            if (enemyObject != null && enemyObject.tag == "EnemyB")
            {
                if(currentMagic >= castingReq)
                {
                    //Debug.Log("Hit enemy for " + attkM_damage + " damage");
                    enemyObject.GetComponent<Enemy_B_Script>().DamageUnit(attkM_damage);
                } else
                {
                    //Debug.Log("Hit enemy for " + attkH_damage + " damage");
                    enemyObject.GetComponent<Enemy_B_Script>().DamageUnit(attkH_damage);
                }
            }
            else if (enemyObject != null && enemyObject.tag == "Goal")
            {
                if (currentMagic >= castingReq)
                {
                    enemyObject.GetComponent<GoalScript>().DamageGoal(attkM_damage);
                } else
                {
                    enemyObject.GetComponent<GoalScript>().DamageGoal(attkH_damage);
                }
            }
        }
    }

    public void AttackL()
    {
        audioM.clip = attack;
        audioM.Play();
        Collider2D[] enemy = Physics2D.OverlapBoxAll(ThrustPosition.position, new Vector2(thrustRange,thrustHeight), 0f, EnemyMask);
        foreach (Collider2D enemyObject in enemy)
        {
            
            if (enemyObject != null && enemyObject.tag == "EnemyB")
            {
                //Debug.Log("Hit enemy for " + attkL_damage + " damage");
                enemyObject.GetComponent<Enemy_B_Script>().DamageUnit(attkL_damage);
            }
            else if (enemyObject != null && enemyObject.tag == "Goal")
            {
                enemyObject.GetComponent<GoalScript>().DamageGoal(attkL_damage);
            }
        }
    }


    public void FireProjectile()
    {
        if (!testingMelee)
        {
            audioM.clip = range;
            audioM.Play();
            Instantiate(Projectile, FirePosition.position, FirePosition.rotation);
        }

    }

    public void hitPlayer(float damage)
    {
        if(currentHealth > 0 && hitTimer >= hitRecovery) {
            hitTimer = 0;
            recoveryTimer = 0;
            rb.velocity = Vector2.zero;
            audioM.clip = hit;
            audioM.Play();
            if (isGrounded)
            {
                anim.SetTrigger("DamageB");
            }
            else if (!isGrounded)
            {
                anim.SetTrigger("DamageF");
            }
            getStats.consumeHealth(damage);
            stopAttack();
        }
    }

    public void drainPlayer(float damage)
    {
        if(currentHealth >= getStats.maxH * .15f)
        {
            getStats.consumeHealth(damage);
        }

    }

    public void WinGame()
    {
        if(finalTime <= 360)
        {
            ScoreScript.scoreValue *= 4;
        }
        else if (finalTime <= 390)
        {
            ScoreScript.scoreValue *= 3;
        }
        else if (finalTime <= 450)
        {
            ScoreScript.scoreValue *= 2;
        }
        else
        {
            ScoreScript.scoreValue *= 1;
        }
        ScoreScript.scoreValue *= 10;
        StatManager.StrengthStat = 0;
        StatManager.MagicStat = 0;
        StatManager.StaminaStat = 0;
        SceneManager.LoadScene("VictoryScene");
    }


    public void DeathSceneChange()
    {
        ScoreScript.scoreValue = 0;
        StatManager.StrengthStat = 0;
        StatManager.MagicStat = 0;
        StatManager.StaminaStat = 0;
        SceneManager.LoadScene("LoseScene");
    }


    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //Gizmos.DrawWireCube(box2D.bounds.center, box2D.bounds.size);
        //Gizmos.DrawWireCube(capsule2D.bounds.center, capsule2D.bounds.size);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(SwingPosition.position, swingRange);
        Gizmos.DrawWireCube(ThrustPosition.position, new Vector3(thrustRange,thrustHeight));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(FloatPosition.position, new Vector3(floatRange, floatRange));
        
    }


}
