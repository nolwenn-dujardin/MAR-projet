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
    public float elevation;
    public AudioSource soundEffect;

    public bool activateCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootCoroutine(ReloadTime));
    }

    // Update is called once per frame
    void Update()
    {
        //Target a specific object
        if(target != null && activateCoroutine){
            transform.parent.LookAt(new Vector3(target.position.x, target.position.y + elevation, target.position.z));
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
        newBullet.GetComponent<Rigidbody>().AddForce(transform.up * ShootSpeed, ForceMode.Impulse);

        soundEffect.PlayOneShot(soundEffect.clip,0.5f);
    }

    public void ActivateCoroutine()
    {
        activateCoroutine = true;
        
    }

    public void DeactivateCoroutine()
    {
        activateCoroutine = false;
    }
}
