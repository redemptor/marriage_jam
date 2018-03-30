using UnityEngine;
using System.Collections;

public class Inimigo : MonoBehaviour {

    public float velocidade;
    public float xMin;
    public float xMax;
    private Animator animatorEnemy;

    private GameObject player;
    private SpriteRenderer sp;
    private Rigidbody2D rb;
    bool walk = false, punch = false, hit = false;
   

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        sp = GetComponent<SpriteRenderer>();
        animatorEnemy = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, player.transform.position) > xMin && Vector3.Distance(transform.position, player.transform.position) < xMax)
        {

            if (transform.position.x > player.transform.position.x)
            {
                transform.Translate(new Vector3(-velocidade * Time.deltaTime, 0, 0));
                walk = false;
            }
            if (transform.position.x < player.transform.position.x)
            {
                transform.Translate(new Vector3(velocidade * Time.deltaTime, 0, 0));
                walk = false;
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) < xMax)
        {
            if (transform.position.y > player.transform.position.y)
            {
                transform.Translate(new Vector3(0, -velocidade * Time.deltaTime, 0));
                
            }
            if (transform.position.y < player.transform.position.y)
            {
                transform.Translate(new Vector3(0, velocidade * Time.deltaTime, 0));
                
            }
            walk = true;
        }

        if (transform.position.y >= player.transform.position.y)
        {
            sp.sortingOrder = 0;
        }
        else
        {
            sp.sortingOrder = 2;
        }

        if (transform.position.x >= player.transform.position.x)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }

        animatorEnemy.SetBool("walk", walk);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            print("tomei dano");
        }
    }
}
