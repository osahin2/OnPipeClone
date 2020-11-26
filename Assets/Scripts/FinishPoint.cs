using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public static event Action<FinishPoint> OnEnterFinish;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
        {
            OnEnterFinish?.Invoke(this);
        }
    }
}
