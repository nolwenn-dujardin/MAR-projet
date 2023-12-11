using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    public float bounceForce = 100;
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bounce");
            Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
            otherRB.AddForce(other.GetContact(0).normal * bounceForce, ForceMode.Impulse);

        }
    }

}