using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSimple : MonoBehaviour
{
    public float speed;
    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = rBody.position;
        rBody.position += -transform.forward * speed * Time.fixedDeltaTime;
        rBody.MovePosition(pos);
    }
}
