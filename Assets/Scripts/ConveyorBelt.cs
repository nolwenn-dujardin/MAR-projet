using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var gameObject in onBelt){
            gameObject.GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        onBelt.Add(other.gameObject);
    }

    private void OnCollisionExit(Collision other) 
    {
        onBelt.Remove(other.gameObject);
    }
}
