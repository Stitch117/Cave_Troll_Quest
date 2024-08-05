using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TopDownCharacterController : MonoBehaviour
{
    #region Framework Stuff
    private SpriteRenderer SR;

    // assign weapon sprites
    public Image WeaponUIimage;
    public Sprite BowSprite;
    public Sprite PistolSprite;
    public Sprite SMGSprite;
    public Sprite RocketLauncherSprite;

    //Reference to attached animator
    private Animator animator;

    public int health;

    private float cooldown = 0f; // cooldown for taking damage
    private float WeaponCooldown = 0f;

    private Rigidbody2D rb;

    private Vector2 playerDirection;

    private float playerSpeed = 1f;

    string[] weapons = { "Bow","Pistol","UZI","Rocket Launcher" };

    string currentWeapon; //points to element in array 

    [Header("Movement parameters")]

    //The maximum speed the player can move
    [SerializeField] private float playerMaxSpeed = 100f;
    #endregion

    public int entranceNumber = 0; //used to spawn p;layer at a specific location


    Vector3 mousePos;
    Vector2 mouseOnScreen;

    Vector2 startPos;   

    // assign the bullets that the weapons fire 
    [SerializeField] GameObject m_ArrowPrefab;
    [SerializeField] GameObject m_PistolBullet;
    [SerializeField] GameObject m_SMGBullet;
    [SerializeField] GameObject m_RocketProjectile;
    [SerializeField] Transform m_fireProjectile;

    //assign the projectile speeds
    [SerializeField] float m_ArrowSpeed;
    [SerializeField] float m_PistolBulletSpeed;
    [SerializeField] float m_SMGBulletSpeed;
    [SerializeField] float m_RocketBulletSpeed;

    //load the loss screen if the player is dead
    private void CheckDead()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("Losing Screen");

            Destroy(this.gameObject, 0.1f);  
        }
    }

    // take damage mechanic
    public int damage(int damageNumber)
    {
        health = health - damageNumber;
        return health;
    }

    private void Awake()
    {
        //Get the attached components so we can use them later
        GameObject player = GameObject.FindWithTag("Player");

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health = 6; //assign start health
        currentWeapon = weapons[0]; // assign the bow as a starter weapon
    }

    // make the shooting mechanic
    void Fire()
    {
        mouseOnScreen = Input.mousePosition;

        Vector3 playerLoacation = GameObject.FindGameObjectWithTag("Player").transform.position;

        //convert mouse position to world position
        mousePos = Camera.main.ScreenToWorldPoint(mouseOnScreen); 

        //find direction so it will face the right way
        Vector3 projectileDirection = mousePos - transform.position;
        
        //pythagarous to find vector for rotation
        float rotZ = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;

        // shoot a bullet when the cooldown on firing is 0
        if (WeaponCooldown <= 0)
        {
            //shoot an arrow if holding the bow
            if (currentWeapon == weapons[0])
            {
                GameObject ArrowSpawn = Instantiate(m_ArrowPrefab, transform.position, Quaternion.Euler(0, 0, rotZ - 90));
                if (ArrowSpawn.GetComponent<Rigidbody2D>() != null)
                {
                    ArrowSpawn.GetComponent<Rigidbody2D>().AddForce(projectileDirection.normalized * m_ArrowSpeed, ForceMode2D.Impulse);
                    WeaponCooldown = 0.5f;
                }
            }

            // shoot a pistol shot
            else if (currentWeapon == weapons[1])
            {
                GameObject PistolBulletSpawn = Instantiate(m_PistolBullet, transform.position, Quaternion.Euler(0, 0, rotZ));
                if (PistolBulletSpawn.GetComponent<Rigidbody2D>() != null)
                {
                    PistolBulletSpawn.GetComponent<Rigidbody2D>().AddForce(projectileDirection.normalized * m_PistolBulletSpeed, ForceMode2D.Impulse);
                    WeaponCooldown = 0.5f;
                }
            }

            // shoot an smg shot
            else if (currentWeapon == weapons[2])
            {
                GameObject SMGBulletSpawn = Instantiate(m_SMGBullet, transform.position, Quaternion.Euler(0, 0, rotZ));
                if (SMGBulletSpawn.GetComponent<Rigidbody2D>() != null)
                {
                    SMGBulletSpawn.GetComponent<Rigidbody2D>().AddForce(projectileDirection.normalized * m_SMGBulletSpeed, ForceMode2D.Impulse);
                    WeaponCooldown = 0.25f; //less cooldown than others
                }
            }

            // shoot a rocket
            else if (currentWeapon == weapons[3])
            {
                GameObject RocketBulletSpawn = Instantiate(m_RocketProjectile, transform.position, Quaternion.Euler(0, 0, rotZ));
                if (RocketBulletSpawn.GetComponent<Rigidbody2D>() != null)
                {
                    RocketBulletSpawn.GetComponent<Rigidbody2D>().AddForce(projectileDirection.normalized * m_RocketBulletSpeed, ForceMode2D.Impulse);
                    WeaponCooldown = 3f;
                }
            }
        }

    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        currentWeapon = weapons[0]; //assign the bow at start

        SR = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //Set the velocity to the direction they're moving in, multiplied by the speed
        rb.velocity = playerDirection * (playerSpeed * playerMaxSpeed) * Time.fixedDeltaTime;

        // set a max health for the player
        if (health > 6)
        {
            health = 6;
        }
        
    }

    private void Update()
    {
        // read input from WASD keys
        playerDirection.x = Input.GetAxis("Horizontal");
        playerDirection.y = Input.GetAxis("Vertical");

        // reduce the cooldowns by one each second
        cooldown = cooldown - Time.deltaTime;
        WeaponCooldown = WeaponCooldown - Time.deltaTime;



        // check if there is some movement direction, if there is something, then set animator flags and make speed = 1
        if (playerDirection.magnitude != 0)
        {
            animator.SetFloat("Horizontal", playerDirection.x);
            animator.SetFloat("Vertical", playerDirection.y);
            animator.SetFloat("Speed", playerDirection.magnitude);

            //And set the speed to 1, so they move!
            playerSpeed = 1f;

            // rolling mechanic on R pressed
            if (Input.GetKeyDown(KeyCode.R))
                animator.SetTrigger("Rolling");

            // make the player move twice as fast when rolling
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll_Tree"))
                playerSpeed = 2f;

        }

        else
        {
            //Was the input just cancelled (released)? If so, set speed to 0
            playerSpeed = 0f;

            //Update the animator too, and return
            animator.SetFloat("Speed", 0);
        }

        // Was the fire button pressed (mapped to Left mouse button or gamepad trigger)
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        // close game when pressing escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // weapon change UI
        GameObject weaponUI = GameObject.FindWithTag("WeaponUI");
        Image weaponImageUI = weaponUI.GetComponent<Image>();

        if (currentWeapon == weapons[0])
        { 
            weaponImageUI.sprite = BowSprite;
        }
        else if (currentWeapon == weapons[1])
        {
            weaponImageUI.sprite = PistolSprite;
        }
        else if (currentWeapon == weapons[2])
        {
            weaponImageUI.sprite = SMGSprite;
        }
        else if (currentWeapon == weapons[3])
        {
            weaponImageUI.sprite = RocketLauncherSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if damaged within last 2 seconds
        if (cooldown <= 0f)
        {
            //damage the player for one when touching an enemy
            if (collision.CompareTag("Enemy") || collision.CompareTag("Enemy Bullet"))
            {
                damage(1);
                cooldown = 2;
                StartCoroutine(FlashColour(0.25f));

            }

            // do 2 damage when touching boss troll
            if (collision.CompareTag("Boss"))
            {
                damage(2);
                cooldown = 2;
                StartCoroutine(FlashColour(0.25f));
            }

            // do one damage if touching the boulder
            if (collision.CompareTag("Boulder"))
            {
                damage(1);
                cooldown = 2;
                StartCoroutine(FlashColour(0.25f));

            }

            //do 2 damage when rocket explodes on player from teh RPG
            if (collision.CompareTag("Explosion"))
            {
                damage(2);
                cooldown = 2;
                StartCoroutine(FlashColour(0.25f));
            }

            // check dead every time damage is taken
            CheckDead();
        }

        // add health when touching the health pick up
        if (collision.CompareTag("HealthPickUp"))
        {
            health = health + 2;
        }

        // change wheapons when colliding with respective weapons
        if (collision.CompareTag("Pistol"))
        {
            currentWeapon = weapons[1];
        }
        if (collision.CompareTag("SMG"))
        {
            currentWeapon = weapons[2];
        }
        if (collision.CompareTag("Rocket Launcher"))
        {
            currentWeapon = weapons[3];
        }
    }
    
    // flash red when taking damage and then flash invissible and not like invulnerability frames
    IEnumerator FlashColour(float duration)
    {
        SR.color = Color.red; //flash red

        yield return new WaitForSeconds(duration * 2);

        SR.color = Color.white;

        yield return new WaitForSeconds(duration);

        SR.enabled = false; //invissible
        yield return new WaitForSeconds(duration);
        SR.enabled = true;
        yield return new WaitForSeconds(duration);

        
        SR.enabled = false; //invissible
        yield return new WaitForSeconds(duration);
        SR.enabled = true;
        yield return new WaitForSeconds(duration);

        SR.enabled = false; // invissible
        yield return new WaitForSeconds(duration);
        SR.enabled = true;
    }
}
