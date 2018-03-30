using UnityEngine;
using System.Collections;

public class PlayerAntigo : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator animatorPlayer;

    private float posY;
    private float y;
    private bool pulando;
    public float forcaPulo;

    public Transform groundCheck;
    private bool grounded;

    private float axisX;
    private float axisY;
    public float velocidade;

    public bool faccingRight;
    private bool walk;
    private bool punch1;

    private float time1;
    private float time2;
    private float time3;

    private float damage;

    
    // Use this for initialization
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");

        if (axisX != 0 || axisY != 0)
        {
            walk = true;
        }
        else
        {
            walk = false;
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);

        if (!grounded && pulando)
        {
            playerRb.gravityScale = 1;
        }
        else if (!pulando && grounded)
        {
            playerRb.gravityScale = 0;
        }

        if (axisX > 0 && !faccingRight)
        {
            Virar();
        }
        else if (axisX < 0 && faccingRight)
        {
            Virar();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            punch1 = true;
        }

     

        if (punch1)
        {
            time1 += Time.deltaTime;

            if (time1 >= 0.2)
            {
                punch1 = false;
                time1 = 0;
            }
        }

        if (Input.GetButtonDown("Jump") && !pulando)
        {
            posY = transform.position.y; //armazena a posição y no momento do pulo
            playerRb.gravityScale = 1; // liga a gravidade do personagem
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.AddForce(new Vector2(0, forcaPulo));
            pulando = true;
        }

        if (!pulando && grounded)
        {
            playerRb.velocity = new Vector2(axisX * velocidade, axisY * velocidade);
        }


        //código para impedir que o player está indo para a cima ou baixo além dos limites da tela
        if (!pulando)
        {
            /**      if (transform.position.y > top.position.y)
                  {
                      transform.position = new Vector3(transform.position.x, top.transform.position.y, transform.position.z);
                  }
                  else if (transform.position.y < bottom.position.y)
                  {
                      transform.position = new Vector3(transform.position.x, bottom.transform.position.y, transform.position.z);
                  }*/
        }

        if (pulando)
        {
            y = transform.position.y;
            if (y < posY)
            {
                playerRb.gravityScale = 0;
                pulando = false;
            }
        }

        animatorPlayer.SetBool("andar", walk);
        animatorPlayer.SetBool("jump", pulando);
        animatorPlayer.SetBool("soco1", punch1);
    }

    void Virar()
    {
        faccingRight = !faccingRight;
        Vector3 aEscala = transform.localScale;
        aEscala.x *= -1;
        transform.localScale = aEscala;
    }
}
