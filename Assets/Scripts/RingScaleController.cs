using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScaleController : MonoBehaviour
{
    RingController controller;

    void OnCollisionEnter(Collision col)
    {
        controller = col.gameObject.GetComponentInParent<RingController>();

        controller.scaleChange = new Vector3(0, 0, 0);
        
    }

    void OnCollisionExit(Collision col)
    {
        controller = col.gameObject.GetComponentInParent<RingController>();

        controller.scaleChange = new Vector3(1.0f, 1.0f, 0f);
    }
}
