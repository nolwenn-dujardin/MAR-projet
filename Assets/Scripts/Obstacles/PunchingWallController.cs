using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PunchingWallController : MonoBehaviour
{
    public bool isExtending = true;

    public bool waiting = true;

    public Transform restPos;
    public Transform extendedPos;

    public float extendingSpeed = 2;
    public float retractingSpeed = 1;

    public float delay = 2;

    public float delayBeforeStart = 0f;

    private float minDist = 0.1f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        Debug.Log("Start after "+delayBeforeStart+" secondes");
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!waiting){
            if(isExtending) 
        {
            var step = extendingSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,
            extendedPos.position, step);

            if(Vector3.Distance(extendedPos.position, transform.position) < minDist)
            {
                //isExtending = false;
                Invoke(nameof(StartRetracting), delay);
            }
        }
        else
        {
            var step = retractingSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, restPos.position, step);
            
            if(Vector3.Distance(restPos.position, transform.position) < minDist)
            {
                //isExtending = true;
                Invoke(nameof(StartExtending), delay);
            }
        }
        }

        
    }

    private void StartExtending()
    {
        isExtending = true;
    }

    private void StartRetracting()
    {
        isExtending = false;
    }

}
