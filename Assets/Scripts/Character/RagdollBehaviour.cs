using System.Collections;
using UnityEngine;

public class RagdollBehaviour : GenericBehaviour
{
    public GameObject character;
    public GameObject hips;
    public float ragdollDurationSeconds;

    private GameObject ragdollsStock;                    // Gameobject to stock only ragdolls

    private Rigidbody charBody;
    private Collider[] colliders;                       // Contains ragdoll colliders

    private bool ragdollOn = false;
    private bool ragdollLocked = false;
    private int ragdollBool;                            // Animator variable related to ragdoll.

    public bool GetRagdollLocked => ragdollLocked;

    private void Start()
    {
        ragdollsStock = GameObject.Find("RagdollsStock");
        charBody = GetComponent<Rigidbody>();
        colliders = GetComponents<Collider>();
        ragdollBool = Animator.StringToHash("Ragdoll");
    }

    public void LockRagdoll()
    {
        ragdollLocked = true;
    }

    private void DisableRagdoll()
    {
        // Reset character position to ragdoll position
        character.transform.position = hips.transform.position;

        charBody.isKinematic = false;
        foreach(Collider collider in colliders)
        {
            collider.enabled = true;
        }

        ragdollOn = false;

        behaviourManager.GetAnim.enabled = true;

        behaviourManager.GetAnim.Play("Getting Up");

        behaviourManager.GetAnim.Update(0);

        StartCoroutine(EnableBehaviourTimed());
    }

    private IEnumerator EnableBehaviourTimed()
    {
        float gettingUpAnimDuration = behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(gettingUpAnimDuration);

        // Ragdoll end after getting up animation
        behaviourManager.GetAnim.SetBool(ragdollBool, false);
    }

    private void EnableRagdoll()
    {
        charBody.isKinematic = true;
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        ragdollOn = true;

        behaviourManager.GetAnim.SetBool(ragdollBool, true);

        behaviourManager.GetAnim.enabled = false;
    }

    private IEnumerator RagdollTimer()
    {
        EnableRagdoll();

        if (ragdollLocked)
        {
            // Change ragdoll parent
            GameObject ragdoll = new GameObject("ragdoll");
            ragdoll.transform.parent = ragdollsStock.transform;
            transform.Find("skeleton").parent = ragdoll.transform;
            transform.Find("shadow_mesh").parent = ragdoll.transform;

            // Remove current useless object
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(ragdollDurationSeconds);

        if (!ragdollLocked)
        {
            DisableRagdoll();
        }
    }

    public void StartRagdollTimer()
    {
        if (!ragdollOn)
        {
            StartCoroutine(RagdollTimer());
        }
    }

    public void StartRagdollTimerNLock()
    {
        if (!ragdollLocked)
        {
            ragdollLocked = true;
            StartCoroutine(RagdollTimer());
        }
        
    }
}
