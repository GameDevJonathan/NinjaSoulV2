using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;
    public GameObject deathEffect;
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

    private Vector3 respawnPoint;
    public float waitToRespawn;

    private GameObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.gameObject;

        respawnPoint = thePlayer.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpawn(Vector3 newPoistion)
    {
        respawnPoint = newPoistion;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnTime());
    }

    IEnumerator RespawnTime()
    {
        if(deathEffect != null)
        {
            Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        }
        thePlayer.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        thePlayer.transform.position = respawnPoint;
        thePlayer.SetActive(true);

        PlayerHealthController.instance.FillHealth();
    }
}
