using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthAmount;

    public GameObject pickUpEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.HealthUp(healthAmount);
            

            if(pickUpEffect != null)
            {
                Instantiate(pickUpEffect, transform.position, transform.rotation);
            }
        }
        AudioManager.instance.PlaySfx(5);
        Destroy(gameObject);
    }

}
