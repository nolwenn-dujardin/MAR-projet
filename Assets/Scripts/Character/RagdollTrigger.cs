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

    // Permettre l'état ragdoll s'il n'est pas déjà actif
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
