using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    TopDownCharacterController m_Player;
    

    public Image[] heartImages; // Array of Image components representing player health
    //assign the sprites for the hearts
    public Sprite fullHeartSprite;
    public Sprite halfHeartSprite;
    public Sprite emptyHeartSprite;

    // Start is called before the first frame update
    void Start()
    {
        // find the player when the game starts
        m_Player = FindObjectOfType<TopDownCharacterController>();

       
    }

    // Update is called once per frame
    void Update()
    {
        //find heart one of the ui and get the image componant of the sprite and UI element
        GameObject heart1 = GameObject.FindWithTag("Heart1");
        Image heart1Image = heart1.GetComponent<Image>();
        heartImages[0] = heart1Image;

        //find heart two of the ui and get the image componant of the sprite and UI element
        GameObject heart2 = GameObject.FindWithTag("Heart2");
        Image heart2Image = heart2.GetComponent<Image>();
        heartImages[1] = heart2Image;

        //find heart three of the ui and get the image componant of the sprite and UI element
        GameObject heart3 = GameObject.FindWithTag("Heart3");
        Image heart3Image = heart3.GetComponent<Image>();
        heartImages[2] = heart3Image;


        //assign the health display depending on player health
        if (m_Player.health == 6)
        {
            heartImages[0].sprite = fullHeartSprite;
            heartImages[1].sprite = fullHeartSprite;
            heartImages[2].sprite = fullHeartSprite;
        }
        else if (m_Player.health == 5)
        {
            heartImages[0].sprite = fullHeartSprite;
            heartImages[1].sprite = fullHeartSprite;
            heartImages[2].sprite = halfHeartSprite;
        }
        if (m_Player.health == 4)
        {
            heartImages[0].sprite = fullHeartSprite;
            heartImages[1].sprite = fullHeartSprite;
            heartImages[2].sprite = emptyHeartSprite;
        }
        if (m_Player.health == 3)
        {
            heartImages[0].sprite = fullHeartSprite;
            heartImages[1].sprite = halfHeartSprite;
            heartImages[2].sprite = emptyHeartSprite;
        }
        if (m_Player.health == 2)
        {
            heartImages[0].sprite = fullHeartSprite;
            heartImages[1].sprite = emptyHeartSprite;
            heartImages[2].sprite = emptyHeartSprite;
        }
        else if (m_Player.health == 1)
        {
            heartImages[0].sprite = halfHeartSprite;
            heartImages[1].sprite = emptyHeartSprite;
            heartImages[2].sprite = emptyHeartSprite;
        }
        else if (m_Player.health == 0)
        {
            heartImages[0].sprite = emptyHeartSprite;
            heartImages[1].sprite = emptyHeartSprite;
            heartImages[2].sprite = emptyHeartSprite;
        }

    }
}
