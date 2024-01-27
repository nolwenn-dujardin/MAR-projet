using UnityEngine;

public class RagdollTrigger : MonoBehaviour
{
    public RagdollBehaviour ragdollBehaviour;

    // Permettre l'état ragdoll s'il n'est pas déjà actif
    private void OnTriggerEnter(Collider collider)
    {
        if (ragdollBehaviour! != null && (collider.CompareTag("ObstacleDeath") || collider.CompareTag("Projectile")))
        {
            ragdollBehaviour.StartRagdollTimer();
        }
    }
}
