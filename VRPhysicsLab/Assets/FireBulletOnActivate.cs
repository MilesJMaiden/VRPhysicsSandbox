using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bullet;
    public Transform bulletOrigin;
    public float fireSpeed = 20f;
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = bulletOrigin.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = bulletOrigin.forward * fireSpeed;
        Destroy(spawnedBullet, 5f);
    }
}
