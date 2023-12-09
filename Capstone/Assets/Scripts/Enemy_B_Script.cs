using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_B_Script : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private GameObject Self;
    [SerializeField] private GameObject Player;
    [SerializeField] PlayerMovement PlayerVar;
    [SerializeField] private LayerMask PlayerMask;
    [SerializeField] private AudioSource audioM;
    [SerializeField] private AudioClip run, attack, hit, death;


    [SerializeField] public float attackRecovery = 1;
    [SerializeField] private float recoveryTimer = 10;
    private float moveSpeed;
    public float enemyRange = 1;
    public bool canSeePlayer = false;
    public bool facingRight = true;
    [SerializeField] private float attkSpeed;
    [SerializeField] private float defaultSpeed;
    //[SerializeField] private bool attk = false;
    public float unitHealth;
    public float unitDamage;
    public float unitScore = 100;

    [SerializeField] private Transform vision;
    [SerializeField] private Transform SlamPosition;
    public float slamRange = 1f;
    public float slamHeight = .75f;
    public float enemyDir = 1f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioM = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * moveSpeed;
        recoveryTimer += Time.deltaTime;

        if (seesPlayer(enemyRange))
        {
            
            if (recoveryTimer >= attackRecovery)
            {
                recoveryTimer = 0;
                anim.SetBool("Attk",true);
                moveSpeed = attkSpeed;
            } 
        }
        else
        {
            moveSpeed = defaultSpeed;
        }
        

        if(unitHealth <= 0)
        {
            LayerMask.NameToLayer("Default");
            moveSpeed = 0;
            
            anim.SetBool("Dying", true);
        }

    }

    public bool seesPlayer(float distance)
    {
        float castDist;

        if (facingRight)
        {
            castDist = -distance;
        }
        else
        {
            castDist = distance;
        }

        Vector2 endPos = vision.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(vision.position, endPos, PlayerMask);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            { 
                canSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
            }
            Debug.DrawLine(vision.position, hit.point, Color.red);
        }
        else
        {
            canSeePlayer = false;
            recoveryTimer = 3;
            Debug.DrawLine(vision.position, endPos, Color.white);
        }
        return canSeePlayer;
    }


    public void AttackE()
    {
        audioM.clip = attack;
        audioM.Play();
        Collider2D[] player = Physics2D.OverlapBoxAll(SlamPosition.position, new Vector2(slamRange, slamHeight), 0f, PlayerMask);
        foreach (Collider2D playerObject in player)
        {
            Debug.Log("Hit enemy");
            playerObject.GetComponent<PlayerMovement>().hitPlayer(unitDamage);
        }
    }

    public void stopAttack()
    {
        anim.SetBool("Attk", false);

    }

    public void flipDetection()
    {
        facingRight = !facingRight;
        
    }

    public void DamageUnit(float damage)
    {
        audioM.clip = hit;
        audioM.Play();
        stopAttack();
        recoveryTimer = 0;
        anim.SetTrigger("Damaged");
        unitHealth -= damage;
    }


    private void destroyUnit()
    {
        ScoreScript.scoreValue += (int) unitScore;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(SlamPosition.position, new Vector3(slamRange, slamHeight));
    }

}
