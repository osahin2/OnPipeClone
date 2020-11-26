using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public static event Action<Damager> OnDamagerEnter;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            OnDamagerEnter?.Invoke(this);
        }
    }
}
