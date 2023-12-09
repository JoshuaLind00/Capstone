using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    //Controls how enemies react upon collision with EnemyLimit
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyB")
        {
            collision.transform.Rotate(0f, 180f, 0f);
            collision.GetComponent<Enemy_B_Script>().flipDetection();
        }
    }
}
