using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagDollBehaviour : MonoBehaviour
{
    private Animator animator;
    private BasicBehaviour basicBehaviour;
    private MoveBehaviour moveBehaviour;
    private AimBehaviourBasic aimBehaviourBasic;

    void Awake()
    {
        animator = GetComponent<Animator>();
        basicBehaviour = GetComponent<BasicBehaviour>();
        moveBehaviour = GetComponent<MoveBehaviour>();
        aimBehaviourBasic = GetComponent<AimBehaviourBasic>();

        DisableRagdoll();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("ObstacleDeath"))
        {
            EnableRagdoll();
        }
    }

    private void DisableRagdoll()
    {
        animator.enabled = true;
        basicBehaviour.enabled = true;
        moveBehaviour.enabled = true;
        aimBehaviourBasic.enabled = true;
    }

    private void EnableRagdoll()
    {
        animator.enabled = false;
        basicBehaviour.enabled = false;
        moveBehaviour.enabled = false;
        aimBehaviourBasic.enabled = false;
    }
}
