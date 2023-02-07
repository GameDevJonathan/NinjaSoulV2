using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;

    public int threshold1, threshold2;

    public float activeTime, fadeOutTime, inactiveTime;
    private float activeCounter, fadeCounter, inactiveCounter;

    public Transform[] spawnPoints;
    private Transform targetPoint;
    public float moveSpeed;
    public Animator anim;

    public Transform theBoss;

    public float timeBetweenShoots1, timeBetweenShoots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;
    public GameObject winObjects;
    private bool battleEnded;

    public string bossRef;
    // Start is called before the first frame update
    void Start()
    {
        theCam = FindObjectOfType<CameraController>();
        theCam.canFollow = false;
        theCam.enabled = false;
        activeCounter = activeTime;

        shotCounter = timeBetweenShoots1;
        AudioManager.instance.PlayBossMusic();
        
    }

    // Update is called once per frame
    void Update()
    {
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position,
            camPosition.position, camSpeed * Time.deltaTime);

        if (!battleEnded)
        {
            if (BossHealthController.instance.currentHealth > threshold1)
            {
                if (activeCounter > 0)
                {
                    activeCounter -= Time.deltaTime;
                    if (activeCounter <= 0)
                    {
                        fadeCounter = fadeOutTime;
                        anim.SetTrigger("vanish");

                    }
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShoots1;

                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    }
                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        theBoss.gameObject.SetActive(false);
                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        theBoss.gameObject.SetActive(true);
                        activeCounter = activeTime;
                    }

                    shotCounter = timeBetweenShoots1;
                }
            }
            else
            {
                if (targetPoint == null)
                {
                    targetPoint = theBoss;
                    fadeCounter = fadeOutTime;
                    anim.SetTrigger("vanish");
                }
                else
                {
                    if (Vector3.Distance(theBoss.position, targetPoint.position) > .02f)
                    {
                        theBoss.position = Vector3.MoveTowards(theBoss.position, targetPoint.position, moveSpeed * Time.deltaTime);


                        if (Vector3.Distance(theBoss.position, targetPoint.position) <= .02f)
                        {
                            fadeCounter = fadeOutTime;
                            anim.SetTrigger("vanish");

                        }

                        shotCounter -= Time.deltaTime;

                        if (shotCounter <= 0)
                        {
                            if (BossHealthController.instance.currentHealth > threshold2)
                            {
                                shotCounter = timeBetweenShoots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShoots2;
                            }


                            Instantiate(bullet, shotPoint.position, Quaternion.identity);
                        }
                    }
                    else if (fadeCounter > 0)
                    {
                        fadeCounter -= Time.deltaTime;
                        if (fadeCounter <= 0)
                        {
                            theBoss.gameObject.SetActive(false);
                            inactiveCounter = inactiveTime;
                        }
                    }
                    else if (inactiveCounter > 0)
                    {
                        inactiveCounter -= Time.deltaTime;
                        if (inactiveCounter <= 0)
                        {
                            theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                            while (targetPoint.position == theBoss.position)
                            {
                                targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            }

                            theBoss.gameObject.SetActive(true);

                            if (BossHealthController.instance.currentHealth > threshold2)
                            {
                                shotCounter = timeBetweenShoots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShoots2;
                            }

                        }
                    }

                }
            }
        }
        else
        {
            fadeCounter -= Time.deltaTime;

            if(fadeCounter < 0)
            {
                if (winObjects != null)
                {
                    winObjects.SetActive(true);
                    winObjects.transform.SetParent(null);
                }

                theCam.enabled = true;
                
                //theCam.CalculateOffset(PlayerHealthController.instance.transform.position.x, PlayerHealthController.instance.transform.position.y);
                gameObject.SetActive(false);
                AudioManager.instance.PlayLevelMusic();
                PlayerPrefs.SetInt(bossRef, 1);
            }
        }

        



    }

    public void EndBossBattle()
    {
        battleEnded = true;
        fadeCounter = fadeOutTime;
        anim.SetTrigger("vanish");
        theBoss.GetComponent<Collider2D>().enabled = false;

        BossBullet[] bullets = FindObjectsOfType<BossBullet>();
        if(bullets.Length > 0)
        {
            foreach(BossBullet bb in bullets)
            {
                Destroy(bb.gameObject);
            }
        }
        
    }
}
