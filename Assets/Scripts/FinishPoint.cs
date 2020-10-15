using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        RingController controller = col.gameObject.GetComponentInParent<RingController>();
        if (col.gameObject.tag=="Player")
        {
            controller.Finish();
        }
    }
}
