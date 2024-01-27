using UnityEngine;

public class RagdollTrigger : MonoBehaviour
{
    public RagdollBehaviour ragdollBehaviour;

    // Permettre l'�tat ragdoll s'il n'est pas d�j� actif
    private void OnTriggerEnter(Collider collider)
    {
        if (ragdollBehaviour! != null && (collider.CompareTag("ObstacleDeath") || collider.CompareTag("Projectile")))
        {
            ragdollBehaviour.StartRagdollTimer();
        }
    }
}
