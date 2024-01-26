using UnityEngine;

public class RagdollTriggerLock : MonoBehaviour
{
    public RagdollBehaviour ragdollBehaviour;

    // Permettre l'�tat ragdoll s'il n'est pas d�j� actif
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Water"))
        {
            ragdollBehaviour.StartRagdollTimerNLock();
        }
    }
}
