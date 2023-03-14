using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody RigidComponent;
    public float timeOut;
    public bool hitDetected;

    private void Start()
    {
        timeOut = 10;
        hitDetected = false;
    }

    private void FixedUpdate()
    {
        if(hitDetected == false)
        {
            timeOut -= Time.deltaTime;
            if(timeOut <= 0 && hitDetected == false)
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
            transform.parent = contactObj.transform;
            contactObj.gameObject.SendMessage("RecieveDamage", "Arrow");
    }
}
