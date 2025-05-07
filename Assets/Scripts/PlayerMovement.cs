using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float velocity = 5f;// Una variable publica se puede modificar desde el edigor de unity
    public float jumpVel = 10f;

    private Vector2 movement;
    private Rigidbody2D rb;

    public Animator animator;

    public LayerMask GroundLayer;
    private bool isGrounded;
    public Transform groundCheckPoint;
    public float radius;

    //private float coyoteTime = 0.2f;
    //private float coyoteTimeCounter;

    //private float jumpBufferTime = 0.2f;
    //private float jumpBufferCounter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position,radius,GroundLayer);

        movement.x = Input.GetAxisRaw("Horizontal");
        transform.Translate(movement * velocity * Time.deltaTime);

        if (movement.x < 0)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            //transform.localScale = new Vector3(1, 1, 1);
        }

        //animator.SetBool("onFloor", isGrounded);
        //animator.SetFloat("movement", movement.x);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        else
        {
            movement.x = 0f;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FixedUpdate()
    {
        
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVel);
        //animator.SetBool("onFloor", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colison");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheckPoint.position, radius);
    }

}
