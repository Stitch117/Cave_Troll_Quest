using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bullet : MonoBehaviour
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
        // destroy when hitting anything
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("Boulder"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
