using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    //[HideInInspector]
    public int currentHealth; 
    public int maxHealth;

    public float invincibilityLenght;
    private float invCounter;
    public float flashLength;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
        currentHealth = maxHealth;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(invCounter > 0)
        {
            invCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                foreach(SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLength;
            }

            if(invCounter <= 0)
            {
                foreach(SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0;
            }
            
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if(invCounter <= 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);

                RespawnController.instance.Respawn();
                AudioManager.instance.PlaySfx(8);

            }
            else
            {
                invCounter = invincibilityLenght;

                AudioManager.instance.PlaySfx(11);
            }

            UiController.instance.UpdateHealth(currentHealth, maxHealth);

        }
        
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
        UiController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealthUp(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UiController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
