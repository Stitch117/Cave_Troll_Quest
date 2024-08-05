using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private Animator animator;

    int health;
    private float cooldown = 0f;

    private SpriteRenderer SR;

    //check if health is 0
    void checkDead()
    {
        if (health == 0)
        {
            animator.SetTrigger("Dead"); // play death animation
            GetComponent<NavMeshAgent>().speed = 0;
            Destroy(this.gameObject, 1.3f); //destroy when animation is finished
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 10;

        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown = cooldown - Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //take one damage whne hit by arrow or boulder
        if (collision.CompareTag("Arrow") || collision.CompareTag("Boulder"))
        {
            health = health - 1;
            StartCoroutine(FlashColour(0.25f)); // flash red
            checkDead();
        }

        // take 2 damage when shot by pistol or SMG
        if (collision.CompareTag("Pistol Bullet") || collision.CompareTag("SMG Bullet"))
        {
            health = health - 2;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }

        // kill when hit by rocket from RPG
        if (collision.CompareTag("Explosion"))
        {
            health = health - 10;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }

        // attack when overlapping player
        if (collision.CompareTag("Player") && cooldown <= 0)
        {
            animator.SetTrigger("Attacking"); //attack animation
            cooldown = 2; // set cooldown on being able to attack again
        }
    }

    //flash red for when damaged
    IEnumerator FlashColour(float duration)
    {

        SR.color = Color.red;

        yield return new WaitForSeconds(duration);

        SR.color = Color.white;

    }
}
