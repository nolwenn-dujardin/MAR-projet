using UnityEngine;

public class RagdollTriggerLock : MonoBehaviour
{
    public RagdollBehaviour ragdollBehaviour;

    // Permettre l'�tat ragdoll s'il n'est pas d�j� actif
    private void OnTriggerEnter(Collider collider)
    {
        if (ragdollBehaviour != null && collider.CompareTag("Water"))
        {
            //Contact avec de l'eau -> activer affichage mort
            TmpManagerInput.Instance.onDeath();


            ragdollBehaviour.StartRagdollTimerNLock();
        }
    }
}
