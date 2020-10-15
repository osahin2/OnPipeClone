using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        RingController controller = col.gameObject.GetComponentInParent<RingController>();
        if (col.gameObject.tag=="Player")
        {
            controller.HitRing();
        }
    }
}
