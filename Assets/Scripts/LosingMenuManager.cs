using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingMenuManager : MonoBehaviour
{
    public GameObject m_controls_Settings_Panel;

    //functionality of quit button
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
   
