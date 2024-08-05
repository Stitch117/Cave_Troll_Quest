using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh_Follow : MonoBehaviour
{
    GameObject m_Player;
    
    [SerializeField] private Animator animator;

    private Vector3 facingDirection;

    NavMeshAgent m_agent;

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();

        m_Player = FindObjectOfType<TopDownCharacterController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // make it so the AI walks towards the player
        m_agent.SetDestination(m_Player.transform.position);

        //find direction the agent is facing
        Vector3 facingDirection = m_Player.transform.position - m_agent.transform.position;



        // check if there is some movement direction, if there is something, then set animator flags and make speed = 1
        if (facingDirection.magnitude != 0)
        
            animator.SetFloat("Horizontal", facingDirection.x);
            animator.SetFloat("Vertical", facingDirection.y);
            animator.SetFloat("Speed", facingDirection.magnitude);


    }
}
