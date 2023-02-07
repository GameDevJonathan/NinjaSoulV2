using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManager theAm;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            AudioManager newAm = Instantiate(theAm);
            AudioManager.instance = newAm;
            DontDestroyOnLoad(newAm.gameObject);
        }
    }
}
