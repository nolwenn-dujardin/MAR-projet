using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpManagerInput : MonoBehaviour
{
    public GameObject checkpointPos;
    public GameObject playerPrefab;
    public GameObject currentPlayer;

    public GameObject checkpointText;

    public GameObject pauseUI;

    public bool gameIsPaused = false;

    public GameObject deathUI;

    public int textDisplayTime = 3;

    public static TmpManagerInput Instance;

    public CanonController[] canons;

    void Awake(){
      if(Instance != null){
        Debug.LogWarning("Il y a plus d'une instance de TmpManagerInput dans la scène");
        return;
      }
      Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
          if(gameIsPaused){
            onResume();
          }
          else {
            onPause();
          }
        }

        if(Input.GetKeyDown(KeyCode.R) && !gameIsPaused){
            checkpointTP();
            //player.transform.position = checkpointPos.transform.position;
        }
    }

    public void checkpointUpdate(GameObject checkpoint){
      checkpointPos = checkpoint;
      StartCoroutine(displayCheckpointText());
    }

    IEnumerator displayCheckpointText(){
      Debug.Log("text active");
      checkpointText.SetActive(true);
      yield return new WaitForSeconds(textDisplayTime);
      checkpointText.SetActive(false);
    }

    private void checkpointTP(){
        //Destroy currentPlayer, create new with prefab and place it on correct position
        Destroy(currentPlayer);
        currentPlayer = Instantiate(playerPrefab, checkpointPos.transform);

        /*
        IMPORTANT
        Désactiver les coroutines des canons
        La tp peut casser le trigger des canons -> si tp, on stop les canons
        */
        foreach(CanonController canon in canons){
          canon.DeactivateCoroutine();
        }

        //Désactive affichage mort (cas où joueur respawn après avoir toucher l'eau)
        deathUI.SetActive(false);
    }

    //Affichage du texte de mort
    public void onDeath(){
      deathUI.SetActive(true);
    }

    private void onPause(){
      //Stop time, changer statut du jeu et afficher le menu
      pauseUI.SetActive(true);
      Time.timeScale = 0;
      gameIsPaused = true;
    }

    public void onResume(){
      pauseUI.SetActive(false);
      Time.timeScale = 1;
      gameIsPaused = false;
    }

    public void quit(){
      SceneManager.LoadScene("MainMenu");
    }
}
