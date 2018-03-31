using UnityEngine;

public class Actor : MonoBehaviour
{
    protected AudioSource audioSource;
    protected new Rigidbody2D rigidbody2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected bool facingRight = true;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
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
