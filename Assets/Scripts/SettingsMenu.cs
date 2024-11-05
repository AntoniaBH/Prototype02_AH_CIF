using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void LoadMM(string MainMenu)
    {
        Debug.Log("MainMenu:" + MainMenu);
        SceneManager.LoadScene(MainMenu);
    }
}
