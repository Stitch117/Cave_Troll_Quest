using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    [SerializeField] private Animator animator;

    int health;
    private float cooldown = 0f;

    private SpriteRenderer SR;

    // check to see if spider is dead
    void checkDead()
    {
        if (health == 0)
        {
            animator.SetTrigger("Dead");
            GetComponent<NavMeshAgent>().speed = 0;
            Destroy(this.gameObject, 1.3f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 8;

        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown = cooldown - Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //take one damage when hit with boulder or player arrow shot
        if (collision.CompareTag("Arrow") || collision.CompareTag("Boulder"))
        {
            health = health - 1;
            StartCoroutine(FlashColour(0.25f));
            checkDead();

        }

        // take 2 damage when hit by a pistol or SMG bullet
        if (collision.CompareTag("Pistol Bullet") || collision.CompareTag("SMG Bullet"))
        {
            health = health - 2;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }

        // die is hit by explosion
        if (collision.CompareTag("Explosion"))
        {
            health = health - 8;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }

        // attack when hit the player
        if (collision.CompareTag("Player") && cooldown <= 0)
        {
            animator.SetTrigger("Attacking"); // play attack animation
            cooldown = 2; // set cooldown on being able to attack again
        }
    }

    //flash red hen damaged
    IEnumerator FlashColour(float duration)
    {

        SR.color = Color.red;

        yield return new WaitForSeconds(duration);

        SR.color = Color.white;

    }
}
