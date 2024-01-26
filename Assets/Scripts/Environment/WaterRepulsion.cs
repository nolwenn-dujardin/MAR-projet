using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRepulsion : MonoBehaviour
{
    public float repulsionForce;
    public float deccelerationForce;
    public float movingForce;


    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            if (rb.velocity.y < -1)
            {
                rb.AddForce(Vector3.up * deccelerationForce, ForceMode.Acceleration);
            }

            // Répulsion vers le haut
            if (rb.velocity.y >= -1) 
            {
                rb.AddForce(Vector3.up * repulsionForce, ForceMode.Acceleration);
            }

            // Mouvement suivant le courrant
            rb.AddForce(Vector3.forward * movingForce, ForceMode.VelocityChange);
        }
    }
}
