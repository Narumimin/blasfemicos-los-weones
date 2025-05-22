using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip attackClip;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip damageClip;
    public AudioClip dashClip;
    public AudioClip edgeGrabClip;
    public AudioClip runClip;
    public AudioClip hitClip;
    public AudioClip playerDeathClip;

    public void attack()
    {
        AudioSource.PlayClipAtPoint(attackClip, transform.position, 0.8f);
    }

    public void jump()
    {
        AudioSource.PlayClipAtPoint(jumpClip, transform.position, 1f);
    }

    public void death()
    {
        AudioSource.PlayClipAtPoint(deathClip, transform.position, 1f);
    }

    public void damage()
    {
        AudioSource.PlayClipAtPoint(damageClip, transform.position, 1f);
    }

    public void dash()
    {
        AudioSource.PlayClipAtPoint(dashClip, transform.position, 0.7f);
    }

    public void edgeGrab()
    {
        AudioSource.PlayClipAtPoint(edgeGrabClip, transform.position, 1f);
    }

    public void run()
    {
        AudioSource.PlayClipAtPoint(runClip, transform.position, 1f);
    }

    public void hit()
    {
        AudioSource.PlayClipAtPoint(hitClip, transform.position, 1f);
    }
    public void playerDeath()
    {
        AudioSource.PlayClipAtPoint(playerDeathClip, transform.position, 1f);
    }
}
