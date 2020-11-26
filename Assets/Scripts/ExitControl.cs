using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitControl : MonoBehaviour
{
    public static event Action<ExitControl> OnExitCollider;

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "cylinder")
        {
            col.gameObject.SetActive(false);

            OnExitCollider?.Invoke(this);
        }
    }
}
