using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{

    [SerializeField] Transform m_StartPoint;
    [SerializeField] Transform m_EndPoint;

    float speed = 0.75f;

    Transform Target;
       

    // change direction moving for the floating look
    void ChangeTarget()
    {
        if (Target == m_EndPoint)
        {
            Target = m_StartPoint;
        }
        else
        {
            Target = m_EndPoint;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // set moving target to end point location
        Target = m_EndPoint;
    }

    // Update is called once per frame
    void Update()
    {
        // move towards either the start or end point, depedning on what target is set to
        transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // change the target your moving towards when hitting the current target
        if (collision.CompareTag("EndPointHeart") || collision.CompareTag("StartPointHeart"))
        {
            ChangeTarget();
        }

        //destroy when player touches it
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
