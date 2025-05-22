using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMovement : MonoBehaviour
{
    public float speed; //Velocidad de las plataformas
    public int startingPoint; //Punto inicial para la paltaforma
    public Transform[] points;
    private bool moving;
    private bool attacking;
    private int randomNumber;
    private int currentPoint;
    private int nextPoint;
    public GameObject wheelAttack;
    public Animator animator;
    private int i; //incrementador
    private EnemySounds sounds;

    private void Start()
    {
        transform.position = points[startingPoint].position; //Teleporta el objeto al punto inicial
        currentPoint = startingPoint;
        sounds = GetComponent<EnemySounds>();
    }

    void Update()
    {
        if (moving == false)
        {
            randomNumber = Random.Range(startingPoint, points.Length);
            nextPoint = randomNumber;
            moving = true;
        }
        
        if (attacking == false)
        {
            Vector2 movement = transform.position - points[randomNumber].position;
            if (movement.x < 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            if (movement.x > 0)
                transform.localScale = new Vector3(1f, 1f, 1f);
            transform.position = Vector2.MoveTowards(transform.position, points[randomNumber].position, speed * Time.deltaTime); //Mueve el onjeto hacia uno de los puntos donde va a pasar
        }

        if (Vector2.Distance(transform.position, points[randomNumber].position) < 0.1f) //compara la distancia entre el objeto y uno de los puntos donde va a pasar
        {
            transform.position = points[randomNumber].position;
            moving = false;
            if (nextPoint == 0 || nextPoint == 1)
            {
                StartCoroutine(Attacking(1f));
            }
            else if (nextPoint == 2)
            {
                animator.SetTrigger("Throw");
                sounds.throwWheel();
                GameObject instanceObject = Instantiate(wheelAttack);
                instanceObject.GetComponent<Wheel>().isSpawned = true;
                StartCoroutine (Attacking(3f));
                
            }
        }
    }

    public void StopEnemy()
    {
        speed = 0;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private IEnumerator Attacking(float timeAttack)
    {
        attacking = true;
        StartCoroutine(DashSound(timeAttack));
        yield return new WaitForSeconds(timeAttack);
        attacking = false;
    }

    private IEnumerator DashSound(float timeAttack)
    {
        yield return new WaitForSeconds(timeAttack);
        sounds.dash();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var healthComponent = collision.GetComponent<HealthController>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(21);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //animator.SetTrigger("Attack");
        }
    }
}
