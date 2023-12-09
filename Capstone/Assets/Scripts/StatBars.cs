using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StatBars : MonoBehaviour
{
    public static StatBars getStat;
    //private Animator anim;
    PlayerMovement move;
    public Transform tf;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Gradient magicGradient;
    [SerializeField] private Gradient staminaGradient;
    public float baseAmount = 100;
    public float maxH, maxM, maxS;
    public Image healthBar;
    public Image boarderH;
    public float healthAmount;
    public Image magicBar;
    public Image boarderM;
    public float magicAmount;
    public Image staminaBar;
    public Image boarderS;
    public float staminaAmount;
    public float regen = 5;



    private void Start()
    {
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        tf = GetComponent<Transform>();

        //Sets the amount of available at start for each bar
        healthAmount = baseAmount * (1 + PlayerMovement.str * 0.1f);
        magicAmount = baseAmount * (1 + PlayerMovement.mag * 0.1f);
        staminaAmount = baseAmount * (1 + PlayerMovement.stam * 0.055f);
    }
    // Update is called once per frame
    void Update()
    {
        
        //Updates the maximum amount for each bar
        maxH = baseAmount * (1 + PlayerMovement.str * 0.1f);
        maxM = baseAmount * (1 + PlayerMovement.mag * 0.1f);
        maxS = baseAmount * (1 + PlayerMovement.stam * 0.055f);
        
        //Regeneration amount per update if player's health doen't fall below 0
        if(move.currentHealth > 0)
        {
            if (move.draining == false)
            {
                healthRegen((regen * (1 + PlayerMovement.str * 0.0005f)));
            }
            magicRegen((regen * (1 + PlayerMovement.mag * 0.005f)));


            //Changes stamina regen depending on if player is on the ground or not
            if (move.isGrounded)
            {
                staminaRegen((regen * (10 + PlayerMovement.stam * 0.5f)));
            }
            else
            {
                staminaRegen((regen * (1 + PlayerMovement.stam * 0.05f)));
            }
        }
        
        

        /*if (healthAmount <= 0)
        {
            anim.SetFloat("health", healthAmount);
        }*/
        if (magicAmount <= 0)
        {
            magicAmount = 0;
        }
        if (staminaAmount <= 0)
        {
            staminaAmount = 0;
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            consumeHealth(30);
            consumeMagic(30);
            consumeStamina(75);
        }

        getHealth();
        getMagic();
        getStamina();

        healthBar.transform.localScale = new Vector2(1+(PlayerMovement.str*.05f),1);
        magicBar.transform.localScale = new Vector2(1 + (PlayerMovement.mag * .05f), 1);
        staminaBar.transform.localScale = new Vector2(1 + (PlayerMovement.stam * .05f), 1);
        boarderH.transform.localScale = new Vector2(1 + (PlayerMovement.str * .05f), 1);
        boarderM.transform.localScale = new Vector2(1 + (PlayerMovement.mag * .05f), 1);
        boarderS.transform.localScale = new Vector2(1 + (PlayerMovement.stam * .05f), 1);

    }

    public void consumeHealth(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / maxH;
    }

    public void healthRegen(float regenAmount)
    {
        healthAmount += regenAmount * Time.deltaTime;
        healthAmount = Mathf.Clamp(healthAmount, -2, maxH);
        healthBar.fillAmount = healthAmount / maxH;
    }

    public void consumeMagic (float damage)
    {
        magicAmount -= damage;
        magicBar.fillAmount = magicAmount / maxM;
    }

    public void magicRegen(float regenAmount)
    {
        magicAmount += regenAmount * Time.deltaTime;
        magicAmount = Mathf.Clamp(magicAmount, 0, maxM);
        magicBar.fillAmount = magicAmount / maxM;
    }

    public void consumeStamina(float damage)
    {
        staminaAmount -= damage;
        staminaBar.fillAmount = staminaAmount / maxS;
    }

    public void staminaRegen(float regenAmount)
    {
        staminaAmount += regenAmount * Time.deltaTime;
        staminaAmount = Mathf.Clamp(staminaAmount, 0, maxS);
        staminaBar.fillAmount = staminaAmount / maxS;
    }

    private void GradientBarAmount()
    {
        healthBar.color = healthGradient.Evaluate((healthAmount/maxH));
        magicBar.color = magicGradient.Evaluate((magicAmount/ maxM));
        staminaBar.color = staminaGradient.Evaluate((staminaAmount / maxS));
    }


    //Return values for each bar
    public float getHealth()
    {
        GradientBarAmount();
        return healthAmount;

    }
    public float getMagic()
    {
        GradientBarAmount();
        return magicAmount;
    }
    public float getStamina()
    {
        GradientBarAmount();
        return staminaAmount;
    }

}
