using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishControl : MonoBehaviour
{
    public static event Action<FinishControl> OnEnterFinishControl;
    [SerializeField] private BoxCollider collider;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            OnEnterFinishControl?.Invoke(this);
            collider.enabled = false;
            StartCoroutine(FinishControlColliderTrue());
        }
    }

    private IEnumerator FinishControlColliderTrue()
    {
        yield return new WaitForSeconds(2.0f);
        collider.enabled = true;
    }
}
