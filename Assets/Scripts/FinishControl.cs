using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishControl : MonoBehaviour
{
    public static event Action<FinishControl> OnEnterFinishControl;
    
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag=="Player")
        {
            OnEnterFinishControl?.Invoke(this);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(FinishControlColliderTrue());
        }
    }

    private IEnumerator FinishControlColliderTrue()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
