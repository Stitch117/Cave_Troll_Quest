using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class MenuManager : MonoBehaviour
{
    public GameObject m_controls_Settings_Panel;

    public GameObject Player;
    public GameObject UICanvas;

    // begin the first room
    public void LoadRoom1()
    {
        
        SceneManager.LoadScene("level 1");
        GameObject.Instantiate(UICanvas); // make the UI
        GameObject.Instantiate(Player); // make the player
    }

    // quit button method
    public void OnApplicationQuit()
    {
       Application.Quit();
    }
}
