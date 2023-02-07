using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump, unlockDash, unlockBall, unlockMine;
    public GameObject pickupEFX;

    public string unlockMessage;
    public TMP_Text unlockText;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();

            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;
                PlayerPrefs.SetInt("DoubleJumpUnlocked", 1);
            }
            
            if (unlockDash)
            {
                player.canDash = true;
                PlayerPrefs.SetInt("DashUnlocked", 1);

            }

            if (unlockBall)
            {
                player.canBecomeBall = true;
                PlayerPrefs.SetInt("BallUnlocked", 1);
            }

            if (unlockMine)
            {
                player.canDropMine = true;
                PlayerPrefs.SetInt("MineUnlocked", 1);
            }
                   

            Instantiate(pickupEFX, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;
            
            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            AudioManager.instance.PlaySfx(5);

            Destroy(unlockText.transform.parent.gameObject, 5f);
            Destroy(gameObject);
        }
    }
}
