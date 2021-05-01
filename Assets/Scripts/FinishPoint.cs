using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public event Action<FinishPoint> OnEnterFinish;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            OnEnterFinish?.Invoke(this);
        }
    }
}
