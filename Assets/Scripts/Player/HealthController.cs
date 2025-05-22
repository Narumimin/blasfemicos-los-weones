using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 150; // vida total
    public int health; // vida actual

    public int flasks = 2;
    public int flaskHeal = 70;

    private bool takingDamage = false; //verificar si esta tomando daño pal cooldown de daño

    public Image[] flaskImages;

    private PlayerMovement Player;
    public Slider slider;
    public GameObject deathPannel;
    private Sounds Sounds;
    public AudioSource AudioSource;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.maxValue = maxHealth;
        health = maxHealth;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); // encontrar el game object que es larry
        Sounds = GameObject.FindGameObjectWithTag("Player").GetComponent<Sounds>();
    }

    public void TakeDamage(int damage)
    {
        if (takingDamage == false)
        {
            Sounds.damage();
            health -= damage;
            //Player.animator.SetTrigger("Damage");
            StartCoroutine(DamageCooldown());
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
        AudioSource.Pause();
        deathPannel.SetActive(true);
        Player.enabled = false;
        Sounds.death();
        Sounds.playerDeath();
        Time.timeScale = 0;
        
    }

    public void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        slider.value = health;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flasks > 0)
            {
                animator.SetTrigger("heal");
                flasks--;
                health += flaskHeal;
            }
        }

        for (int i = 0; i < flaskImages.Length; i++)
        {
            if (i < flasks)
            {
                flaskImages[i] = flaskImages[i];
            }
            else
            {
                flaskImages[i].enabled = false;
            }
        }
        
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
