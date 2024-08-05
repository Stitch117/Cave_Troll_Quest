using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Bat : MonoBehaviour
{
    [SerializeField] private Animator animator;

    int health;
    private float cooldown = 0f; // asssign a cooldown on being able to do damage

    private SpriteRenderer SR;

    // check to see if health is 0
    void checkDead()
    {
        if (health == 0)
        {
            animator.SetTrigger("Dead"); //play death animation

            GetComponent<NavMeshAgent>().speed = 0; //wont move when dead and playing agent

            Destroy(this.gameObject, 1.3f); //destroy when animation done
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 6;

        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    { 
        //cooldown ticks down each second
        cooldown = cooldown - Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Take one damage when shot by an arrow or hit by boulder
        if (collision.CompareTag("Arrow") || collision.CompareTag("Boulder"))
        {
            health = health - 1;
            StartCoroutine(FlashColour(0.25f)); //flash red
            checkDead();
        }

        // take 2 damage when shot with pistol or SMG
        if (collision.CompareTag("Pistol Bullet") || collision.CompareTag("SMG Bullet"))
        {
            health = health - 2;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }

        // kill when hit by a rocket
        if (collision.CompareTag("Explosion"))
        {
            health = health - 6;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }

        // attack player
        if (collision.CompareTag("Player") && cooldown <= 0)
        {
            animator.SetTrigger("Attacking"); //attack animation
            cooldown = 2; // set cooldown on being able to attack again
        }
    }

    // flash red for when damaged
    IEnumerator FlashColour(float duration)
    {
        SR.color = Color.red;

        yield return new WaitForSeconds(duration);

        SR.color = Color.white;
    }
}
