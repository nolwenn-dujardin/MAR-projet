using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            TmpManagerInput.Instance.checkpointUpdate(this.gameObject);
            Debug.Log("Checkpoint updated");
        }
    }
}
