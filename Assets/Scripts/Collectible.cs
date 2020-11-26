using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectible : MonoBehaviour
{
    public static event Action<Collectible> OnEnterCollectible;


    Vector3 direction;
    float randomPush;
    Vector3 startPos;
    Quaternion startRot;
    float changeTime;
    public float forceSpeed;

    public void Initialized()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        ExitControl.OnExitCollider += SetCollectiblePos;
    }
    private void OnEnable()
    {
        randomPush = Random.Range(-1.0f, 1.0f);
        direction = new Vector3(randomPush, 1.0f, -2.0f);
    }

    private void SetCollectiblePos(ExitControl exitControl)
    {
        if (gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
        }
        transform.localPosition = startPos;
        transform.rotation = startRot;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
        {
            if (gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(direction * forceSpeed, ForceMode.Impulse);
            }
            if (gameObject.TryGetComponent(out Animation anim))
            {
                anim.Play();
            }
            OnEnterCollectible?.Invoke(this);
        }
    }

    private void OnDestroy()
    {
        ExitControl.OnExitCollider -= SetCollectiblePos;
    }
}
