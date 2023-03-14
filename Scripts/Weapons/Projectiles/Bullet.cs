using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Scripts
    //Ints
    //Doubles
    //Floats
    public float timeOut;
    //Strings
    //Bools
    public bool hitDetected;
    //Lists
    //GameObjects
    //Animations
    //Other
    public Rigidbody RigidComponent;

    private void Start()
    {
        timeOut = 2;
        hitDetected = false;
    }

    private void FixedUpdate()
    {
        if (hitDetected == false)
        {
            timeOut -= Time.deltaTime;
            if (timeOut <= 0 && hitDetected == false)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision contactObj)
    {
        hitDetected = true;
        Vector3 vel = new Vector3(0, 0, 0);
        RigidComponent = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().velocity = (vel);
        RigidComponent.isKinematic = true;
        RigidComponent.detectCollisions = false;
        //transform.parent = contactObj.transform;
        contactObj.gameObject.SendMessage("RecieveDamage", "Bullet");
        Destroy(gameObject);
    }
}