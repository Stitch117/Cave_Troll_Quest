using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Boulder_Script : MonoBehaviour
{
    int speed = 3;

    [SerializeField] Transform m_startPoint;
    [SerializeField] Transform m_endPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // move the boulder to a set end point
        transform.position = Vector2.MoveTowards(transform.position, m_endPoint.position, speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // destroy when reaching end location
        if (collision.CompareTag("EndPointBoulder"))
        {
            Destroy(this.gameObject);
        }
    }
}
