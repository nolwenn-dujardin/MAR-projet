using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaTriggerCanon : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("area enter");
            onTriggerEnter.Invoke();
        }
        
    }

    void OnTriggerExit(Collider other) 
    {

        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("area exit");
            onTriggerExit.Invoke();
        }
    }
}
