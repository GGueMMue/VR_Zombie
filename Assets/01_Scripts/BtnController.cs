using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    public void GoToMainBtnEventListener()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartBtnEventListener()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitBtnEventListener()
    {
        Application.Quit();
    }
}
