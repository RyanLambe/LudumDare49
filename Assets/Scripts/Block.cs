using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Rigidbody rb;
    Collider collider;

    public BlockTypes type;
    public bool Prefab;

    Vector3 StartPos;
    Quaternion StartRot;

    void Start()
    {
        //get physics components
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        StartPos = transform.position;
        StartRot = transform.rotation;
    }

    void Update()
    {
        //allow physics when moving player
        if (Cam.PlayMode)
        {
            collider.enabled = true;
            rb.useGravity = true;
        }
        //otherwise disable physics
        else
        {
            collider.enabled = false;
            rb.useGravity = false;
            rb.Sleep();

            //set start position when placed into game
            if (!Prefab)
            {
                transform.position = StartPos;
                transform.rotation = StartRot;
            }
        }
    }
}
