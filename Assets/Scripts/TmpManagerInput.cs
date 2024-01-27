using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    private bool gameEnded = false;

    private bool isDead = false;

    public GameObject deathUI;

    public GameObject endingUI;

    public TextMeshProUGUI endingText;

    public int textDisplayTime = 3;

    public static TmpManagerInput Instance;

    public CanonController[] canons;

    private ThirdPersonOrbitCamBasic playerCamControl;

    [SerializeField]
    private int deathCount = 0;
    [SerializeField]
    private int tpCount = 0;

    private float startTime;

    void Awake()
    {
        if(Instance != null)
        {
        Debug.LogWarning("Il y a plus d'une instance de TmpManagerInput dans la scène");
        return;
        }
        Instance = this;

        playerCamControl = currentPlayer.transform.Find("Main Camera").gameObject.GetComponent<ThirdPersonOrbitCamBasic>();
    }

    private void Start() {
      startTime = Time.time;
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

        if(Input.GetKeyDown(KeyCode.R) && !gameIsPaused && !gameEnded){
            checkpointTP();
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
        playerCamControl = currentPlayer.transform.Find("Main Camera").gameObject.GetComponent<ThirdPersonOrbitCamBasic>();

        /*
        IMPORTANT
        Désactiver les coroutines des canons
        La tp peut casser le trigger des canons -> si tp, on stop les canons
        */
        foreach(CanonController canon in canons){
          canon.DeactivateCoroutine();
        }

        //Désactive affichage mort (cas où joueur respawn après avoir toucher l'eau)
        if(isDead){
          deathUI.SetActive(false);
          deathCount++;
          isDead = false;
        }
        else {
          tpCount++;
        }

        AudioManager.Instance.PlaySFX("TeleportFX");
    }

    //Affichage du texte de mort
    public void onDeath(){
      isDead = true;
      deathUI.SetActive(true);
    }

    private void onPause(){
        //Stop time, changer statut du jeu et afficher le menu
        pauseUI.SetActive(true);
        playerCamControl.enabled = false;
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void onResume(){
        pauseUI.SetActive(false);
        playerCamControl.enabled = true;
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void quit(){
      Time.timeScale = 1;
      SceneManager.LoadScene("MainMenu");
      AudioManager.Instance.PlayMusic("WipeoutTheme");
    }

    public void endingScreen(){
      float endTime = Time.time;
      float duration = endTime - startTime;
      string durationString = TimeSpan.FromSeconds(duration).ToString(@"mm\mss\s");

      string endTxt = $"Vous avez bu la tasse {deathCount} fois,\n"+
      $"et êtes revenus au checkpoint {tpCount} fois,\n"+
      $"le tout en {durationString}";
      endingUI.SetActive(true);
      endingText.SetText(endTxt);
      gameEnded = true;
    }
}
