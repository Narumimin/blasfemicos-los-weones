using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var healthComponent = collision.GetComponent<HealthController>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(1000000);
            }
        }
    }
}
