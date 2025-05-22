using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 500; // vida total
    public int health; // vida actual
    private bool isDead = false;
    public Slider slider;
    private EnemyMovement jefe;
    public GameObject requiemAeternam;
    public AudioSource AudioSource;
    private EnemySounds sound;
    //public Animator animator;

    private void Start()
    {
        sound = GetComponent<EnemySounds>();
        slider.maxValue = maxHealth;
        health = maxHealth;
        jefe = gameObject.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
        if (health <= 0 && isDead == false)
        {
            //animator.SetTrigger("Dead");
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        AudioSource.Pause();
        StartCoroutine(ShowRequiemAeternam());
        sound.bossEnd();
        jefe.enabled = false;
    }

    private IEnumerator ShowRequiemAeternam()
    {
        requiemAeternam.SetActive(true);
        yield return new WaitForSeconds(5f);
        requiemAeternam.SetActive(false);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(3);
    }

}
