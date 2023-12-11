using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagDollBehaviour : MonoBehaviour
{
    private Rigidbody charBody;
    private Collider[] colliders;
    private Animator animator;
    private BasicBehaviour basicBehaviour;
    private MoveBehaviour moveBehaviour;
    private AimBehaviourBasic aimBehaviourBasic;

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

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Ragdoll collider tag : " + collider.tag);
        if (collider.CompareTag("ObstacleDeath") || collider.CompareTag("Projectile"))
        {
            EnableRagdoll();
        }
    }

    private void DisableRagdoll()
    {
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
}
