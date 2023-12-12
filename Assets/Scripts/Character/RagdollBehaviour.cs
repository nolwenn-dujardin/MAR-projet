using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagDollBehaviour : MonoBehaviour
{
    public GameObject character;
    public GameObject ragdollTarget;
    public int ragdollDurationSeconds = 3;

    private Rigidbody charBody;
    private Collider[] colliders;
    private Animator animator;
    private BasicBehaviour basicBehaviour;
    private MoveBehaviour moveBehaviour;
    private AimBehaviourBasic aimBehaviourBasic;
    private bool lockRagdoll = false;

    void Awake()
    {
        charBody = GetComponent<Rigidbody>();
        colliders = GetComponents<Collider>();
        animator = GetComponent<Animator>();
        basicBehaviour = GetComponent<BasicBehaviour>();
        moveBehaviour = GetComponent<MoveBehaviour>();
        aimBehaviourBasic = GetComponent<AimBehaviourBasic>();

        DisableRagdoll();
    }

    public void LockRagdoll()
    {
        lockRagdoll = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!lockRagdoll)
        {
            if (collider.CompareTag("ObstacleDeath") || collider.CompareTag("Projectile"))
            {
                EnableRagdoll();
                StartCoroutine(RagdollTimer());
            }
        }
    }

    private void DisableRagdoll()
    {
        // Reset character position to ragdoll position
        Debug.Log("Reset position : " + ragdollTarget.transform.position);
        Debug.Log("Old position : " + character.transform.position);
        character.transform.position = ragdollTarget.transform.position;

        charBody.isKinematic = false;
        foreach(Collider collider in colliders)
        {
            collider.enabled = true;
        }
        animator.enabled = true;
        basicBehaviour.enabled = true;
        moveBehaviour.enabled = true;
        aimBehaviourBasic.enabled = true;
    }

    private void EnableRagdoll()
    {
        charBody.isKinematic = true;
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        animator.enabled = false;
        basicBehaviour.enabled = false;
        moveBehaviour.enabled = false;
        aimBehaviourBasic.enabled = false;
    }

    private IEnumerator RagdollTimer()
    {
        int timeRemaining = ragdollDurationSeconds;

        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining -= 1;
        }

        DisableRagdoll();
    }
}
