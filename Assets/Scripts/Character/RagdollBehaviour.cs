using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollBehaviour : MonoBehaviour
{
    public GameObject character;
    public GameObject hips;
    public int ragdollDurationSeconds = 3;
    public bool ragdollOn = false;

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

    private void DisableRagdoll()
    {
        // Reset character position to ragdoll position
        character.transform.position = hips.transform.position;

        charBody.isKinematic = false;
        foreach(Collider collider in colliders)
        {
            collider.enabled = true;
        }
        animator.enabled = true;
        basicBehaviour.enabled = true;
        moveBehaviour.enabled = true;
        aimBehaviourBasic.enabled = true;

        ragdollOn = false;
    }

    public void EnableRagdoll()
    {
        if (!lockRagdoll)
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

            ragdollOn = true;
        }
    }

    public IEnumerator RagdollTimer()
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
