using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    public float ReloadTime;
    public GameObject Bullet;
    public float ShootSpeed;
    public Transform firePosition;
    public AudioSource soundEffect;

    private bool activateCoroutine = false;
    private Transform canon;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        canon = transform.GetChild(0);
        StartCoroutine(ShootCoroutine(ReloadTime));
    }

    // Update is called once per frame
    void Update()
    {
        //Target a specific object
        if(target != null && activateCoroutine){
            canon.LookAt(new Vector3(target.position.x, target.position.y + target.localScale.y, target.position.z));
        }
    }

    public IEnumerator ShootCoroutine(float x)
    {
        while(true)
        {
            if(activateCoroutine) {
                CreateBullet();
            }
            yield return new WaitForSeconds(x);
        }
    }

    private void CreateBullet()
    {
        GameObject newBullet = Instantiate(Bullet, firePosition.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().AddForce(canon.forward * ShootSpeed, ForceMode.Impulse);

        soundEffect.PlayOneShot(soundEffect.clip,0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
            activateCoroutine = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            target = null;
            activateCoroutine = false;
        }
    }

    public void DeactivateCoroutine(){
        activateCoroutine = false;
    }
}
