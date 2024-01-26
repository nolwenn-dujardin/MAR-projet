using UnityEngine;

public class RagdollTriggerLock : MonoBehaviour
{
    private RagdollBehaviour ragdollBehaviour;

    private void Start()
    {
        ragdollBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<RagdollBehaviour>();
    }

    // Permettre l'état ragdoll s'il n'est pas déjà actif
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Water"))
        {
            ragdollBehaviour.StartRagdollTimerNLock();
        }
    }
}
