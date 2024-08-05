using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEye : MonoBehaviour
{
    [SerializeField] GameObject m_EnemyBullet;

    [SerializeField] private Animator animator;

    int health = 0;
    int projectileSpeed = 6;

    float cooldown = 0;

    private SpriteRenderer SR;

    //Play death animation when dead
    void checkDead()
    {
        if (health == 0)
        {
            animator.SetTrigger("Dead"); //death animation
            Destroy(this.gameObject, 1.5f); // destroy when animation done
        }
    }

    //shoot at player
    private void Attack()
    {
        Vector3 enemyLocation = this.transform.position; //find current location
        Vector3 PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform.position; //find player location

        //get angle for rotation for bullet
        Vector3 projectileDirection = PlayerLocation - enemyLocation;
        float rotZ = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg; 

        // make and shoot the bullet
        GameObject projectileSpawn = Instantiate(m_EnemyBullet, transform.position, Quaternion.Euler(0, 0, rotZ));

        //add force to the bullet
        if (projectileSpawn.GetComponent<Rigidbody2D>() != null)
        {
            projectileSpawn.GetComponent<Rigidbody2D>().AddForce(projectileDirection.normalized * projectileSpeed, ForceMode2D.Impulse);
        }

        // set a cooldown till shooting again
        cooldown = 1.5f;
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

        //attack whne cooldown is 0
        if (cooldown <= 0 && health > 0)
        {
            Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // take one damage when shot by arrow
        if (collision.CompareTag("Arrow"))
        {
            health = health - 1;
            StartCoroutine(FlashColour(0.25f)); //flash red
            checkDead();
        }

        // take 2 damage when shot by psitol or SMG
        if (collision.CompareTag("Pistol Bullet") || collision.CompareTag("SMG Bullet"))
        {
            health = health - 2;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }

        // kill whne hit by rocket 
        if (collision.CompareTag("Explosion"))
        {
            health = health - 8;
            StartCoroutine(FlashColour(0.25f));
            checkDead();
        }
    }

    //flash red when taking damage
    IEnumerator FlashColour(float duration)
    {

        SR.color = Color.red;

        yield return new WaitForSeconds(duration);

        SR.color = Color.white;

    }
}
