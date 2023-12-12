using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BalayetteController : MonoBehaviour
{
    public int yRotation = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (new Vector3 (0, yRotation, 0) * Time.deltaTime);
    }
}
