using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private BoxCollider2D BoxCollider2D;

    private void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var healthComponent = collision.GetComponent<HealthController>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(1);
            }
        }
    }
}
