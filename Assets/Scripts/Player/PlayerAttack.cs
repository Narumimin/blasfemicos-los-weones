using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius = 1;
    public LayerMask attackableLayer;
    public float damage = 1f;
    public float timeBetweenAttacks = 0.3f;
    private PlayerMovement Player;
    private float attackTimeCounter;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        attackTimeCounter = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) //&& attackTimeCounter >= timeBetweenAttacks)
        {            
            attackTimeCounter = 0f;
            //Player.animator.SetTrigger("Attacking");
            Attack();
        }
        attackTimeCounter += Time.deltaTime;
    }

    private void Attack()
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackableLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<EnemyHealth>() != null)
            {
                hit.GetComponent<EnemyHealth>().health -= damage;
                //hit.GetComponent<EnemyHealth>().animator.SetTrigger("Damage");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
