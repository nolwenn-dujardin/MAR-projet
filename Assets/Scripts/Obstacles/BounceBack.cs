using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    public float bounceForce = 100;
    public float bounceDuration = 0.2f;
    
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bounce");
            StartCoroutine(ApplyBounceForce(other));
        }
    }

    IEnumerator ApplyBounceForce(Collision other)
    {
        Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
        Vector3 normal = other.GetContact(0).normal;

        float timer = 0f;
        while (timer < bounceDuration)
        {
            float force = Mathf.Lerp(0, bounceForce, timer / bounceDuration);
            otherRB.AddForce(-normal * force);
            timer += Time.deltaTime;
            yield return null;
        }
    }

}