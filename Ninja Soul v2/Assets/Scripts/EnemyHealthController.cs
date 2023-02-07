using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int health = 3;

    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            
            if (deathEffect)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            AudioManager.instance.PlaySFXAdjusted(4);
            Destroy(gameObject);
        }
       

    }
}
