using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    public Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;
    private string run_ANIMATION = "run";

    private bool isGrounded;
    private string GROUND_TAG = "Ground";
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
        float speed = Mathf.Abs(movementX);
        anim.SetFloat("speed",speed); 


    }

    private void FixedUpdate()
    {
        PlayerJump();  
    }

    void PlayerMoveKeyboard()
    {

        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;
    }

    void AnimatePlayer() {

        // we are going to the right side
        if (movementX > 0)
        {
            anim.SetBool("run", true);
            sr.flipX = false;
        }
        else if (movementX < 0)
        {
        // we are going to the left side
            anim.SetBool("run", true);
            sr.flipX = true;
        }     
        else
        {
            anim.SetBool("run", false);
        }   

    }

    void PlayerJump(){

        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger("jumpTrigger");
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag(GROUND_TAG)) {
            isGrounded = true;
            anim.SetBool("jump", false);
        }
    }

} // class