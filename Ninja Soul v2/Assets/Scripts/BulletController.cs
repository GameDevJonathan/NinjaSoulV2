using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float bulletSpeed;
    public Rigidbody2D body;

    public Vector2 moveDir;
    public GameObject impactEFX;
    public int damageAmount = 1;
        // Update is called once per frame
    void Update()
    {
        body.velocity = moveDir * bulletSpeed;
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().TakeDamage(damageAmount);
            
        }

        if (other.tag == "Boss")
        {
           BossHealthController.instance.TakeDamage(damageAmount);

        }


        if (impactEFX)
        {
            Instantiate(impactEFX, transform.position, Quaternion.identity);
        }
        
        AudioManager.instance.PlaySFXAdjusted(3);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
