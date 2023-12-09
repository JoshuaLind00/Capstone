using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] PlayerMovement Player;
    [SerializeField] BoxCollider2D box2D;
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] Enemy_B_Script unitBVar;
    //[SerializeField] Enemy_F_Script unitFVar;
    private Animator animate;
    StatBars getStats;
    public float p_damage;
    public float base_damage;
    public float proj_speed = 15f;

    [SerializeField] private LayerMask hitMask;
    public float hitRange = 1f;



    void Start()
    {
        rb.velocity = -transform.right * proj_speed;
        Animator animate = GetComponent<Animator>();
        BoxCollider2D box2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        p_damage = base_damage * (1 + PlayerMovement.mag * .55f);
    }

    //Checks to see if projectile collides with anything other than the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            if  (collision.gameObject.layer == 8 || collision.gameObject.layer == 7)
            {
                rb.velocity = Vector3.zero;
                box2D.enabled = false;

                if (collision.gameObject.tag == "EnemyB")
                {
                    collision.gameObject.GetComponent<Enemy_B_Script>().DamageUnit(p_damage);
                }
                else if (collision.gameObject.tag == "EnemyF")
                {
                    collision.gameObject.GetComponent<Enemy_F_Script>().DamageUnit(p_damage);
                }
                else if (collision.gameObject.tag == "Goal")
                {
                    collision.gameObject.GetComponent<GoalScript>().DamageGoal(p_damage);
                }
                //Damages Enemies based on the damage value and the magic stat
            }
        }

    }

    //Function called by the "Destroy_Orb" animation to destroy the object after it plays
    private void ProjectileDestroy()
    {
        Destroy(gameObject);
        //Debug.Log("Projectile Destroyed. It dealt " + p_damage + " damage!");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }


}