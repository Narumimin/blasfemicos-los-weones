using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    //public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            //animator.SetTrigger("Dead");
            Die();
        }
    }

    private void Die()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
    }
}
