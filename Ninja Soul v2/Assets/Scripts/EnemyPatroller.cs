using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint;
    public float moveSpeed, waitAtPoint;
    private float waitCounter;

    public float jumpForce;
    public Rigidbody2D body;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
        foreach(Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            if(transform.position.x < patrolPoints[currentPoint].position.x)
            {
                body.velocity = new Vector2(moveSpeed, body.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                body.velocity = new Vector2(-moveSpeed, body.velocity.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (transform.position.y < patrolPoints[currentPoint].position.y - .5f && body.velocity.y < .1f)
            {
                body.velocity = new Vector2(body.velocity.x, jumpForce);
            }
        }
        else
        {
            body.velocity = Vector2.zero;
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoint;

                currentPoint++;

                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }
        anim.SetFloat("speed", Mathf.Abs(body.velocity.x));

    }
   }
