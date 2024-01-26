using System.Collections;
using UnityEngine;

public class RagdollBehaviour : GenericBehaviour
{
    public GameObject character;
    public GameObject hips;
    public int ragdollDurationSeconds = 3;

    private Rigidbody charBody;
    private Collider[] colliders;

    private bool ragdollOn = false;
    private bool ragdollLocked = false;
    private int ragdollBool;                           // Animator variable related to ragdoll.

    public bool GetRagdollLocked => ragdollLocked;

    private void Start()
    {
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
        ragdollLocked = true;
        StartRagdollTimer();
    }
}
