using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Water") && other.gameObject.layer != LayerMask.NameToLayer("CheckpointTrigger") && other.gameObject.layer != LayerMask.NameToLayer("CanonTrigger"))
        {
            Debug.Log("Foot Step sound on : " + other);
            /*audioSource.clip = stepSounds[Random.Range(0, stepSounds.Count)];
            audioSource.Play();*/

            //Random select between the 7 step sound
            AudioManager.Instance.PlaySFX("Step"+Random.Range(1,7));
        }
    }
}
