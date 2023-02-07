using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider bossHealthSlider;

    public int currentHealth = 30;
    public BossBattle theBoss;
    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            theBoss.EndBossBattle();
            AudioManager.instance.PlaySfx(0);
        }
        else
        {
            AudioManager.instance.PlaySfx(1);
        }

        bossHealthSlider.value = currentHealth;
    }
}
