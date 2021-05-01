using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectible : MonoBehaviour
{
    public event Action<Collectible> OnEnterCollectible;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animation anim;


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
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.localPosition = startPos;
        transform.rotation = startRot;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
            rb.AddForce(direction * forceSpeed, ForceMode.Impulse);
            anim.Play();
            OnEnterCollectible?.Invoke(this);
        }
    }

    private void OnDestroy()
    {
        ExitControl.OnExitCollider -= SetCollectiblePos;
    }
}
