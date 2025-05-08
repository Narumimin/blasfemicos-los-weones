using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float velocity = 5f; //Velocidad del jugador
    public float jumpVel = 10f; //Velocidad del salto
    private bool isJumping;

    private Vector2 movement; //Vector para saber la direccion del movimiento del jugador
    private Rigidbody2D rb; //Rigidbody

    public Animator animator; //Animator

    public LayerMask GroundLayer; //Detectar objetos con cierta layer puesta

    public Transform groundCheckPoint; //Punto donde se dibuja la esfera para checar si el jugador esta en el suelo
    public float radius; //Radio de la esfera mencionada

    private float coyoteTime = 0.1f; //Tiempo que queremos que dure el coyote time
    private float coyoteTimeCounter; //Contador con el que checamos el coyote time

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // recibir input de derecha o izquierda

        /*
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
        */

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
        /*else
        {
            movement.x = 0f;
        }*/
    }

    private void FixedUpdate()
    {
        transform.Translate(movement * velocity * Time.deltaTime); //mover el jugador de derecha a izquierda
    }

    private bool isGrounded() // funcion para checar si el jugador esta en el piso
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, radius, GroundLayer);
    }

    private void Jump() // funcion para saltaar
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVel);
        //animator.SetBool("onFloor", false);
    }

    private IEnumerator JumpCooldown() //cooldown para el salto
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
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
