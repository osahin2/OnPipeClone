using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitControl : MonoBehaviour
{
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "cylinder" || col.gameObject.tag == "poolStart")
        {
            col.gameObject.SetActive(false);
        }
    }
}
