using UnityEngine;

public class RagdollTriggerLock : MonoBehaviour
{
    public RagdollBehaviour ragdollBehaviour;

    // Permettre l'état ragdoll s'il n'est pas déjà actif
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Water"))
        {
            ragdollBehaviour.StartRagdollTimerNLock();
        }
    }
}
