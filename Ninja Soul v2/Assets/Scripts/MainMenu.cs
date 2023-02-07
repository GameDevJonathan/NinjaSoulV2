using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject continueButton;

    public PlayerAbilityTracker player;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueButton.SetActive(true);
        }


        AudioManager.instance.PlayMenuMusic();
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(newGameScene);

    }

    public void Continue()
    {
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"),
            PlayerPrefs.GetFloat("PosY"),
            PlayerPrefs.GetFloat("PosZ"));


        if (PlayerPrefs.HasKey("DoubleJumpUnlocked"))
        {
            if(PlayerPrefs.GetInt("DoubleJumpUnlocked") == 1)
            {
                player.canDoubleJump = true;
            }
        }

        if (PlayerPrefs.HasKey("DashUnlocked"))
        {
            if (PlayerPrefs.GetInt("DashUnlocked") == 1)
            {
                player.canDash = true;
            }
        }

        if (PlayerPrefs.HasKey("BallUnlocked"))
        {
            if (PlayerPrefs.GetInt("BallUnlocked") == 1)
            {
                player.canBecomeBall = true;
            }
        }

        if (PlayerPrefs.HasKey("MineUnlocked"))
        {
            if (PlayerPrefs.GetInt("MineUnlocked") == 1)
            {
                player.canDropMine = true;
            }
        }


        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));

    }
    
    
    public void QuitGame()
    {
        Application.Quit();

    }
}
