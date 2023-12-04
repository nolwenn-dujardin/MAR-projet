using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float timeToLive = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Player got hit
            Debug.Log("Player hit !");
        }
            
        Destroy(this.gameObject, timeToLive);
    }
}
