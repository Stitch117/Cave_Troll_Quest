using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room3_Teleport : MonoBehaviour
{

    public TopDownCharacterController EntranceNumber;

    Vector2 startPos;

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
        if (collision.CompareTag("Player"))
        {
            EntranceNumber.entranceNumber = 2;
            SceneManager.LoadScene("Room 4");
            
            GameObject player = GameObject.FindWithTag("Player");


            // finds entrance number and spawns the player at that door they've just walked through 
            if (EntranceNumber.entranceNumber == 0)

            {
                startPos = new Vector2(-1, 0);
                player.transform.position = startPos;
            }
            else if (EntranceNumber.entranceNumber == 1)
            {
                startPos = new Vector2(-11, -1);
                player.transform.position = startPos;
            }
            else if (EntranceNumber.entranceNumber == 2)
            {
                startPos = new Vector2(-1, 3);
                player.transform.position = startPos;
            }
            else if (EntranceNumber.entranceNumber == 3)
            {
                startPos = new Vector2(9, -1);
                player.transform.position = startPos;
            }
            else
            {
                startPos = new Vector2(-1, -4);
                player.transform.position = startPos;
            }
        }
    }
}
