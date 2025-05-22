using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius = 1;
    public LayerMask attackableLayer;
    public int damage;
    public float timeBetweenAttacks = 0.3f;
    private PlayerMovement Player;
    private float attackTimeCounter;
    private Sounds sounds;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        attackTimeCounter = timeBetweenAttacks;
        sounds = GameObject.FindGameObjectWithTag("Player").GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && attackTimeCounter >= timeBetweenAttacks)
        {            
            attackTimeCounter = 0f;
            sounds.attack();
            Player.animator.SetTrigger("attacking");
        }
        attackTimeCounter += Time.deltaTime;
    }

    public void DashNotAllowed()
    {
        Player.canDash = false;
        Player.isDashing = true;
    }

    public void DashAllower()
    {
        Player.canDash = true;
        Player.isDashing = false;
    }

    public void Attack()
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackableLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<EnemyHealth>() != null)
            {
                sounds.hit();
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
