using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
   public float ReloadTime;
    public GameObject Bullet;
    public float ShootSpeed;
    public Transform firePosition;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootCoroutine(ReloadTime));
    }

    // Update is called once per frame
    void Update()
    {
        //Target a specific object
        if(target != null){
            transform.parent.LookAt(target);
        }
    }

    public IEnumerator ShootCoroutine(float x)
    {
        while(true)
        {
            CreateBullet();
            yield return new WaitForSeconds(x);
        }
    }

    private void CreateBullet()
    {
        GameObject newBullet = Instantiate(Bullet, firePosition.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().AddForce(transform.up * ShootSpeed, ForceMode.Impulse);
    }
}
