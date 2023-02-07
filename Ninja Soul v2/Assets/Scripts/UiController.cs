using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public static UiController instance;

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
    }

    public Slider healthSlider;
    public Image fadeScreen;
    public float fadeSpeed = 2f;
    private bool fadeIn, fadeOut;
    public string mainMenuScene;

    public GameObject pauseScene;
    // Start is called before the first frame update
    void Start()
    {
        //UiController.instance.UpdateHealth(PlayerHealthController.instance.currentHealth,PlayerHealthController.instance.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

        if (fadeIn)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.b,
                fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if(fadeScreen.color.a == 1f)
            {
                fadeIn = false;
            }
        }
        else if(fadeOut)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.b,
                fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadeOut = false;
            }

        }
        
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void StartFadeToBlack()
    {
        fadeIn = true;
        fadeOut = false;
    }

    public void StartFadeFromBlack()
    {
        fadeOut = true;
        fadeIn = false;
    }

    public void PauseUnpause()
    {
        if (!pauseScene.activeSelf)
        {
            pauseScene.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseScene.SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void GoToMainMenu()
    {
        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        instance = null;
        Destroy(gameObject);
        Time.timeScale = 1;
        
        SceneManager.LoadScene(mainMenuScene);
    }
}
