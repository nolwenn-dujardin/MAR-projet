using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpManagerInput : MonoBehaviour
{
    public GameObject checkpointPos;
    public GameObject player;

    public static TmpManagerInput Instance;


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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            player.transform.position = checkpointPos.transform.position;
        }
    }

    public void checkpointUpdate(GameObject checkpoint){
        checkpointPos = checkpoint;
    }
}
