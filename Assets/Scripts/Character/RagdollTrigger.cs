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
        if (collider.CompareTag("ObstacleDeath") || collider.CompareTag("Projectile"))
        {
            ragdollBehaviour.StartRagdollTimer();
        }
    }
}
