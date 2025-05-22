using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement attributes")]
    public float velocity = 5f; //Velocidad del jugador
    private Vector2 movement; //Vector para saber la direccion del movimiento del jugador
    private Rigidbody2D rb; //Rigidbody
    [SerializeField] private Collider2D standingCollider;
    [SerializeField] private Collider2D crouchingCollder;

    [Header("Animator")]
    public Animator animator; //Animator

    [Header("Grounded Info")]
    public LayerMask GroundLayer; //Detectar objetos con cierta layer puesta
    public Transform groundCheckPoint; //Punto donde se dibuja la esfera para checar si el jugador esta en el suelo
    public float radius; //Radio de la esfera mencionada

    [Header("Jump attributes")]
    public float jumpVel = 10f; //Velocidad del salto
    private bool isJumping;
    private float coyoteTime = 0.1f; //Tiempo que queremos que dure el coyote time
    private float coyoteTimeCounter; //Contador con el que checamos el coyote time

    [Header("Dash attributes")]
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    [HideInInspector] public bool canDash = true;
    [HideInInspector] public bool isDashing;

    [Header("Ledge info")]
    [SerializeField] private Vector2 offset1;
    [SerializeField] private Vector2 offset2;
    [HideInInspector] public bool ledgeDetected;
    private Vector2 climbBegunPosition;
    private Vector2 climbOverPosition;
    private bool canGrabLedge = true;
    private bool canClimb;

    private Sounds sounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sounds = GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) 
        {
            return;
        }

        rb.WakeUp();
        movement.x = Input.GetAxisRaw("Horizontal"); // recibir input de derecha o izquierda

        
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        animator.SetBool("onFloor", isGrounded());
        animator.SetFloat("movement", movement.x);
        

        if (isGrounded()) //Coyote time related stuff
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f && !isJumping) //jump action
        {
            Jump();
            coyoteTimeCounter = 0f;
            StartCoroutine(JumpCooldown());
        }

        if(Input.GetKeyDown(KeyCode.L) && isGrounded() && canDash)
        {
            StartCoroutine(Dash());
        }

        CheckForLedge();
        if (canClimb)
        {
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                sounds.edgeGrab();
                Invoke("LedgeClimbOver", 0.5f);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canClimb = false;
                Jump();
                coyoteTimeCounter = 0f;
                StartCoroutine(JumpCooldown());
                canGrabLedge = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        transform.Translate(movement * velocity * Time.deltaTime); //mover el jugador de derecha a izquierda
    }

    private bool isGrounded() // funcion para checar si el jugador esta en el piso
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, radius, GroundLayer);
    }

    private void Jump() // funcion para saltaar
    {
        sounds.jump();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVel);
    }

    private IEnumerator JumpCooldown() //cooldown para el salto
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private IEnumerator Dash()
    {
        sounds.dash();
        canDash = false;
        isDashing = true;
        animator.SetBool("dashing", true);
        standingCollider.enabled = false;
        crouchingCollder.enabled = true;
        rb.linearDamping = 10;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        animator.SetBool("dashing", false);
        standingCollider.enabled = true;
        crouchingCollder.enabled = false;
        rb.linearDamping = 0;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    private void CheckForLedge()
    {
        if(ledgeDetected && canGrabLedge)
        {
            sounds.edgeGrab();
            canGrabLedge = false;
            Vector2 ledgePosition = GetComponentInChildren<LerdgeCheck>().transform.position;

            if (movement.x < 0)
            {
                climbBegunPosition = ledgePosition + new Vector2 (-offset1.x , offset1.y);
                climbOverPosition = ledgePosition + new Vector2(-offset2.x, offset2.y);
            }
            else if (movement.x > 0)
            {
                climbBegunPosition = ledgePosition + offset1;
                climbOverPosition = ledgePosition + offset2;
            }

            canClimb = true;
        }

        if (canClimb) 
        {
            transform.position = climbBegunPosition;
            //Invoke("LedgeClimbOver", 0.5f);
        }
    }

    private void LedgeClimbOver()
    {
        canClimb = false;
        transform.position = climbOverPosition;
        canGrabLedge = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) //cosilla puesta pa ver si esta colisionado el jugador XD
    {
        Debug.Log("Colison");
    }

    private void OnDrawGizmos() // gizmo para checar el circulo que se usa en la funcion isGrounded
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheckPoint.position, radius);
    }

}
