using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip attackClip;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip damageClip;

    public void attack()
    {
        AudioSource.PlayClipAtPoint(attackClip, transform.position, 0.5f);
    }

    public void jump()
    {
        AudioSource.PlayClipAtPoint(jumpClip, transform.position, 0.5f);
    }

    public void death()
    {
        AudioSource.PlayClipAtPoint(deathClip, transform.position, 0.5f);
    }

    public void damage()
    {
        AudioSource.PlayClipAtPoint(damageClip, transform.position, 0.5f);
    }
}
