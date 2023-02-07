using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    public Animator anim;

    public float distanceToOpen;

    private PlayerController player;

    public Transform exitPoint;
    public float movePlayerSpeed;

    private bool playerExit;
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        }
        else
        {
            anim.SetBool("doorOpen", false);
        }

        if (playerExit)
        {
            player.transform.position = Vector3.MoveTowards(
                player.transform.position, exitPoint.transform.position, movePlayerSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!playerExit)
            {
                player.canMove = false;
                StartCoroutine(UseDoor());
            }

        }
        
    }

    IEnumerator UseDoor()
    {
        playerExit = true;
        player.anim.enabled = false;
        UiController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(1.5f);
        RespawnController.instance.SetSpawn(exitPoint.position);
        player.canMove = true;
        player.anim.enabled = true;
        UiController.instance.StartFadeFromBlack();


        PlayerPrefs.SetString("ContinueLevel", levelToLoad);
        PlayerPrefs.SetFloat("PosX",exitPoint.position.x);
        PlayerPrefs.SetFloat("PosY",exitPoint.position.y);
        PlayerPrefs.SetFloat("PosZ",exitPoint.position.z);
        
        
        
        SceneManager.LoadScene(levelToLoad);
    }
}
