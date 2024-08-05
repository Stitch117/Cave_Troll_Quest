using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // destroy if hitting wall or player
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
