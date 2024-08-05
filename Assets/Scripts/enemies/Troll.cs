using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Troll : MonoBehaviour
{
    [SerializeField] private Animator animator;

   GameObject player;

    int health;
    private float cooldown = 0f;

    private SpriteRenderer SR;

    // check if the troll has died
    void checkDead()
    {
        if (health == 0)
        {
            animator.SetTrigger("Dead"); // play death aniamtion
            GetComponent<NavMeshAgent>().speed = 0; // stop moving when dead

            Destroy(this.gameObject, 1.5f); // destroy when animation done

            StartCoroutine(WinScreen(1.3f)); // load the winscreen and destroy the player
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<TopDownCharacterController>().gameObject;
        health = 50;

        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown = cooldown - Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //take one damage when shot by an arrow
        if (collision.CompareTag("Arrow"))
        {
            health = health - 1;
            StartCoroutine(FlashColour(0.25f)); //flash red
            checkDead();
        }

        // take 2 damage when shot by pistol or SMG
        if (collision.CompareTag("Pistol Bullet") || collision.CompareTag("SMG Bullet"))
        {
            health = health - 2;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }

        // take 10 damage when hit by a rocket explosion
        if (collision.CompareTag("Explosion"))
        {
            health = health - 10;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }

        // play attack animation when hitting player
        if (collision.CompareTag("Player") && cooldown <= 0)
        {
            animator.SetTrigger("Attacking");
            cooldown = 2; // set cooldown on being able to attack again
        }
    }

    // flash red when damaged
    IEnumerator FlashColour(float duration)
    {

        SR.color = Color.red;

        yield return new WaitForSeconds(duration);

        SR.color = Color.white;

    }

    //destroy player and load win screen 
    IEnumerator WinScreen(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(player);

        SceneManager.LoadScene("WinScreen");
    }
}
