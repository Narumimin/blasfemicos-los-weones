using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioClip dashClip;
    public AudioClip throwClip;
    public AudioClip bossEndingClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void dash()
    {
        AudioSource.PlayClipAtPoint(dashClip, transform.position, 0.8f);
    }

    public void throwWheel()
    {
        AudioSource.PlayClipAtPoint(throwClip, transform.position, 1f);
    }

    public void bossEnd()
    {
        AudioSource.PlayClipAtPoint(bossEndingClip, transform.position, 1f);
    }
}
