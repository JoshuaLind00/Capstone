using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    [SerializeField] PlayerMovement victoryScream;
    private Animator anim;
    //private float counter = 0f;
    public float goalHealth = 2000;
    public float currentHealth;
    [SerializeField] private float timer = 0;
    //[SerializeField] private float hitable = 5;
    [SerializeField] private AudioSource audioM;
    [SerializeField] private AudioClip hit, death;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = goalHealth;
        audioM = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

    }


    public void HitEffect()
    {
        if (currentHealth > (goalHealth / 2))
        {
            audioM.clip = hit;
            audioM.Play();
            anim.SetBool("Stage2", false);
        }
        else if(currentHealth <= (goalHealth/2) && currentHealth > 0)
        {
            audioM.clip = hit;
            audioM.Play();
            anim.SetBool("Stage2", true);
        }
        else
        {
            audioM.clip = hit;
            audioM.Play();
            anim.SetBool("Stage3", true);

        }
    }

    public void DamageGoal(float damage)
    {
        
        anim.SetTrigger("Hit");
        currentHealth -= damage;
        HitEffect();


    }

    public void StopDamage()
    {
        anim.ResetTrigger("Hit");
    }

    public void victoryInitiation()
    {
        audioM.clip = death;
        audioM.Play();
        Debug.Log("Congragulation!");
        ScoreScript.scoreValue += 500;
        victoryScream.winGame = true;
        Destroy(gameObject,5);
    }


}
