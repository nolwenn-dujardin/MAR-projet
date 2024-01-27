using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LaunchGame(){
        SceneManager.LoadScene("ParcoursDemo");
        AudioManager.Instance.PlayMusic("RollerCoasterTycoonMusic");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
