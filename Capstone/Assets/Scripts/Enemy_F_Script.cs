using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_F_Script : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 offset;
    [SerializeField] PlayerMovement PlayerVar;

    [SerializeField] private AudioSource audioM;
    [SerializeField] private AudioClip drain, hit, death;
    //[SerializeField] private Transform hitable;
    //[SerializeField] private LayerMask HitMask;
    private bool canSeePlayer = false;
    private float drainCounter = 10;
    //public float hitableRange = 1;
    public float enemyRadius;
    public float moveSpeed;
    private float defaultSpeed;
    private float maxDistance;
    public float unitHealth;
    public float unitDamage;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioM = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        drainCounter += Time.deltaTime;
        if (canSeePlayer)
        {
            PursuePlayer();
            if (Vector2.Distance(transform.position, Target.position) <= enemyRadius)
            {
                
                PlayerVar.draining = true;
                PlayerVar.drainPlayer(unitDamage);
                if (drainCounter >= 4)
                {
                    drainCounter = 0;
                    audioM.clip = drain;
                    audioM.Play();
                }
            }
            else
            {
                PlayerVar.draining = false;
                audioM.Pause();
            }

        }
        

        if (transform.position.x > Player.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (unitHealth <= 0)
        {
            audioM.clip = death;
            audioM.Play();
            //anim.SetTrigger("Death");
            LayerMask.NameToLayer("Default");
            moveSpeed = 0;
            anim.SetBool("Dying", true);
        }

    }

    private void PursuePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position - offset, moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canSeePlayer = true;
        }
    }


    public void DamageUnit(float damage)
    {
        audioM.clip = hit;
        audioM.Play();
        anim.SetTrigger("Damaged");
        unitHealth -= damage;
    }

    private void destroyUnit()
    {
        PlayerVar.draining = false;
        ScoreScript.scoreValue += 50;
        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);

    }

}
