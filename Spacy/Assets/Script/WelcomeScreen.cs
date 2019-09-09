using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreen : MonoBehaviour
{
    public GameObject welcome_panel;
    public GameObject Instruction_panel;

    public void onplaybutton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void oninsturction()
    {
        welcome_panel.SetActive(false);
        Instruction_panel.SetActive(true);
    }
}
