using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Wheel : MonoBehaviour
{
    public float speed = 2f;
    public GameObject Player;
    public bool isGrounded = false;
    public bool isSpawned = false;
    public LayerMask targetLayerMask;
    private Vector3 direction;
    private Vector3 movement;
    private Vector3 originalPosition;
    public GameObject spawnPoint;
    public Animator animator;

    private void Start()
    {
        transform.position = new Vector2(spawnPoint.transform.position.x, spawnPoint.transform.position.y);
        originalPosition = transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawned == true)
        {
            originalPosition = transform.position;
            //AudioSource.PlayClipAtPoint(attackClip, transform.position, 1f);
            direction = Player.transform.position - transform.position;
            movement = direction.normalized;;
            if (movement.x < 0)
                transform.localScale = new Vector3(1f, 1f, 1f);
            if (movement.x > 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            StartCoroutine(EnemyCooldown());

        }

        if (isGrounded == false)
        {
            transform.position = transform.position + movement * Time.deltaTime * speed;
        }
        else if (isGrounded == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, originalPosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator EnemyCooldown()
    {
        isSpawned = false;
        yield return new WaitForSeconds(0.8f);
        isGrounded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PEGO CONTRA AAAAAAAAAAAAAAAAAAAAAA");
        if ((targetLayerMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            Debug.Log("PEGO CONTRA EL PISO CABRON");
            transform.parent.GetComponent<Wheel>().isGrounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var healthComponent = collision.GetComponent<HealthController>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(32);
            }
        }
    }

}
