using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);
            return;
        }
        else {
            Instance = this;
        }

          DontDestroyOnLoad(this.gameObject);
    }

    public void LaunchGame(){
        SceneManager.LoadScene("ParcoursDemo");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
