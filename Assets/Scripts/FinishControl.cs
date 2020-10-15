using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishControl : MonoBehaviour
{

    float changeTime = 0;
    void OnTriggerEnter(Collider col)
    {
        RingController controller = col.gameObject.GetComponentInParent<RingController>();

        if (col.gameObject.tag=="Player")
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            controller.FinishCounter();
        }
    }
    void Update()
    {
        changeTime += Time.deltaTime;
        if (changeTime>5.0f)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        
    }
}
