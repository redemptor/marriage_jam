using UnityEngine;

public class Actor : MonoBehaviour
{
    protected AudioSource audioSource;
    protected SpriteRenderer spriteRenderer;
    protected bool facingRight;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void PlaySounfFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        
    }
}
