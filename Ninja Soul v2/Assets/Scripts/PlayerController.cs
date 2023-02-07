using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed, jumpForce;
    public Rigidbody2D body;
    public Transform groundCheck;
    [SerializeField]
    private bool isOnGround;
    public LayerMask ground;
    public Animator anim;
    public Animator ball_anim;

    [SerializeField]private bool canDoubleJump;


    //shooting
    public BulletController bullet;
    public Transform shotPoint;

    //bomb
    public Transform bombPoint;
    public GameObject bomb;
    
    
    
    public float dashSpeed, dashTime;
   [SerializeField]private float dashCounter;


    //after image variables
    public SpriteRenderer SpriteRenderer, afterImage;
    public float  timeBetweenAfterImages;
    private float afterImageCounter;
    public Color afterImageColor;
    public float coolDown;
    private float dashRecharge;

    //ball mode variables;
    public GameObject standing, ball;
    public float waitToball;
    private float ballCounter;

    private PlayerAbilityTracker abilities;

    public bool canMove;
   

    // Start is called before the first frame update
    void Start()
    {
        abilities = GetComponent<PlayerAbilityTracker>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Time.timeScale != 0)
        {
            if (dashRecharge > 0)
            {
                dashRecharge -= Time.deltaTime;
            }
            else
            {
                if (Input.GetButtonDown("Fire2") && standing.activeSelf && abilities.canDash)
                {
                    dashCounter = dashTime;
                    ShowAfterImage();
                    AudioManager.instance.PlaySFXAdjusted(7);
                }

            }



            if (dashCounter > 0)
            {
                dashCounter = dashCounter - Time.deltaTime;

                body.velocity = new Vector2(dashSpeed * transform.localScale.x, body.velocity.y);

                afterImageCounter -= Time.deltaTime;

                if (afterImageCounter <= 0)
                {
                    ShowAfterImage();
                }

                dashRecharge = coolDown;
            }
            else
            {
                dashCounter = 0;
                //moving sideways
                body.velocity = new Vector2(
                    Input.GetAxisRaw("Horizontal") * moveSpeed,
                    body.velocity.y);

                //checking if on ground
                isOnGround = Physics2D.OverlapCircle(groundCheck.position, .2f, ground);


                //flipping sprite
                if (body.velocity.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (body.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

            }



            //jumping
            if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump)))
            {
                if (isOnGround)
                {
                    canDoubleJump = true;
                    AudioManager.instance.PlaySfx(12);
                }
                else
                {
                    anim.SetTrigger("DoubleJump");
                    canDoubleJump = false;
                    AudioManager.instance.PlaySfx(9);
                }


                body.velocity = new Vector2(body.velocity.x, jumpForce);
            }


            //shooting
            if (Input.GetButtonDown("Fire1"))
            {
                if (standing.activeSelf)
                {
                    anim.SetTrigger("Shooting");
                    Instantiate(bullet, shotPoint.position,
                        shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0);
                    AudioManager.instance.PlaySfx(14);
                }
                else if (ball.activeSelf && abilities.canDropMine)
                {
                    Instantiate(bomb, bombPoint.position, bombPoint.rotation);
                    AudioManager.instance.PlaySfx(13);
                }
            }

            //ball mode
            if (!ball.activeSelf)
            {
                if (Input.GetAxisRaw("Vertical") < -.9f && abilities.canBecomeBall)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);
                        AudioManager.instance.PlaySfx(6);
                    }
                }
                else
                {
                    ballCounter = waitToball;
                }
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > .9f)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);
                        AudioManager.instance.PlaySfx(10);
                    }
                }
                else
                {
                    ballCounter = waitToball;
                }

            }
        }
        else
        {
            body.velocity = Vector2.zero;
        }

        if (standing.activeSelf)
        {
            anim.SetBool("onGround", isOnGround);
            anim.SetFloat("Speed", Mathf.Abs(body.velocity.x));
        }

        if (ball.activeSelf)
        {
            if(body.velocity.x == 0f)
            {
                ball_anim.speed = 0;
            }
            else if( Mathf.Abs(body.velocity.x) > 1)
            {
                ball_anim.speed = 1;

            }
        }

        
    }

    void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = SpriteRenderer.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        afterImageCounter = timeBetweenAfterImages;    
    }

}
