using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;
    public BoxCollider2D boundsBox;
    public bool canFollow;
    public float halfHeight, halfWidth;

    public Transform cameraRoot;
    public float distanceX, distanceY;
    public float moveSpeed;

    // Start is called before the first frame update

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
        canFollow = true;
        //AudioManager.instance.PlayLevelMusic();

    }

    // Update is called once per frame
    void Update()
    {
       

        if (player != null && canFollow)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth),
                Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight),
                transform.position.z);
        }
        else if (player != null && !canFollow)
        {
            distanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
            distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);
           
            
            Vector3 newPos = transform.position;
            
            if(distanceX > 0.05f)
            {
                newPos.x = Mathf.Lerp(transform.position.x,
                    Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth), 0.01f);
                newPos.z = transform.position.z;

            }
            
            if (distanceY > 0.05f)
            {
                newPos.y = Mathf.Lerp(transform.position.y,
                     Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight), 0.01f);
            }
            
            transform.position = Vector3.MoveTowards(transform.position,
                    newPos, 3f);

            if (distanceX < 0.05f && distanceY < 0.05f)
            {
                transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth),
                Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight),
                transform.position.z);
                canFollow = true;
            }

        }
        else
        {
            player = FindObjectOfType<PlayerController>();
        }

    }
}
