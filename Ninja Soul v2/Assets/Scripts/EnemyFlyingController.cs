using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    public float rangeToStartChase;
    private bool isChasing;

    public float moveSpeed, turnSpeed;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing)
        {
            if(Vector3.Distance(transform.position, player.position) < rangeToStartChase)
            {
                isChasing = true;
            }
        }
        else
        {
            if (player.gameObject.activeSelf)
            {
                Vector3 direction = transform.position - player.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation,targetRot, turnSpeed * Time.deltaTime);

                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position,
                //    moveSpeed * Time.deltaTime);

                transform.position += -transform.right * moveSpeed * Time.deltaTime;
            }
        }
        
    }
}
