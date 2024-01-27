using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRepulsion : MonoBehaviour
{
    public float repulsionForce;
    public float deccelerationForce;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            // Decc�l�ration � l'entr�e dans l'eau
            if (rb.velocity.y < -1)
            {
                rb.AddForce(Vector3.up * deccelerationForce, ForceMode.Acceleration);
            }

            // R�pulsion vers le haut
            if (rb.velocity.y >= -1) 
            {
                rb.AddForce(Vector3.up * repulsionForce, ForceMode.Acceleration);
            }
        }
    }
}
