using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{

    private Animator anim;
    private BoxCollider2D box2D;
    StatBars getStats;
    [SerializeField]  PlayerMovement player;
    public bool P_Health;
    public bool P_Magic;
    [SerializeField] private AudioSource audioM;
    [SerializeField] private AudioClip use;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        box2D = GetComponent<BoxCollider2D>();
        getStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatBars>();
        audioM = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("Used", true);
        }
    }

    private void UsePotion()
    {
        audioM.clip = use;
        audioM.Play();
        if (P_Health)
        {
            PlayerMovement.str += 2;
        }
        else if (P_Magic)
        {
            PlayerMovement.mag += 2;
        }
        else
        {
            PlayerMovement.stam += 2;
        }
    }

    private void DestroyPotion()
    {
        ScoreScript.scoreValue += 1000;
        Destroy(gameObject);
    }
}
