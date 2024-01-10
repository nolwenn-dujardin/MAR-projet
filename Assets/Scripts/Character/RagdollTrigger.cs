using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTrigger : MonoBehaviour
{
    private RagdollBehaviour ragdollBehaviour;

    private void Start()
    {
        ragdollBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<RagdollBehaviour>();
    }

    // Permettre l'�tat ragdoll s'il n'est pas d�j� actif
    private void OnTriggerEnter(Collider collider)
    {
        if (!ragdollBehaviour.ragdollOn)
        {
            if (collider.CompareTag("ObstacleDeath") || collider.CompareTag("Projectile"))
            {
                StartCoroutine(ragdollBehaviour.RagdollTimer());
            }
        }
    }
}
