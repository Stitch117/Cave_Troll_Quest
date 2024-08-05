using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderInstantiate : MonoBehaviour
{

    public GameObject Boulder;

    float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        cooldown = cooldown - Time.deltaTime;

        //sapwn a boulder every 2 seconds
        if (cooldown <= 0)
        {
            Instantiate(Boulder, this.transform);
            cooldown = 2;
        }
        
    }
}
