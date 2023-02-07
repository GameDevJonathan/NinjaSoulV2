using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public AudioSource mainMenuMusic, levelMusic, bossMusic;
    public AudioSource[] sfx;
    // Start is called before the first frame update
    public void PlayMenuMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        
        mainMenuMusic.Play();
    }
    public void PlayLevelMusic()
    {
        if (!levelMusic.isPlaying)
        {
            mainMenuMusic.Stop();
            bossMusic.Stop();

            levelMusic.Play();

        }
        
        
    }
    public void PlayBossMusic()
    {
        levelMusic.Stop();
        
        bossMusic.Play();

    }

    public void PlaySfx(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void PlaySFXAdjusted(int sfxToAdjust)
    {
        sfx[sfxToAdjust].pitch = Random.Range(.8f, 1.2f);
        PlaySfx(sfxToAdjust);
    }
}
