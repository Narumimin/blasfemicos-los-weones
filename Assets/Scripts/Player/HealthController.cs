using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 3; // vida total
    public int health; // vida actual

    private bool takingDamage = false; //verificar si esta tomando daño pal cooldown de daño

    private PlayerMovement Player;
    public GameObject deathPannel;
    //private Sounds Sounds;

    [SerializeField] private Image[] hearts; // pa los corazones meanwhile
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); // encontrar el game object que es larry
        //Sounds = GameObject.FindGameObjectWithTag("Player").GetComponent<Sounds>();
    }

    public void TakeDamage(int damage)
    {
        if (takingDamage == false)
        {
            health -= damage;
            //Player.animator.SetTrigger("Damage");
            StartCoroutine(DamageCooldown());
            for (int i = 0; i < hearts.Length; i++) // para cambiar el color de corazones
            {
                if (i < health)
                {
                    hearts[i].color = Color.red;
                }
                else
                {
                    hearts[i].color = Color.black;
                }
            }
            if (health <= 0)
            {
                Death();
            }
        }
    }

    public IEnumerator DamageCooldown() //cooldown para el daño
    {
        takingDamage = true;
        yield return new WaitForSeconds(1f);
        takingDamage = false;
    }

    private void Death() // se explica solo XD
    {
        deathPannel.SetActive(true);
        Player.enabled = false;
        //Sounds.death();
        Time.timeScale = 0;
        
    }

    public void Update()
    {
        if (Player.enabled == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Player.enabled = true;
            }
        }
    }
}
