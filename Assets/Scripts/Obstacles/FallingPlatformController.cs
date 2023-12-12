using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    public bool shouldFall = false;
    public float fallDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Collide");

        if(shouldFall && other.gameObject.CompareTag("Player")){
            Debug.Log("Player will fall");
            shouldFall = false;
            StartCoroutine(DropPlatform());
        }
    }

    private IEnumerator DropPlatform(){
        yield return new WaitForSeconds(fallDelay);
            Debug.Log("Falling");
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().useGravity = true;
    }
}
