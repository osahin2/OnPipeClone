using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    Animation anim;
    Rigidbody rb;
    Vector3 direction;
    float randomPush;
    Vector3 startPos;
    Quaternion startRot;
    float changeTime;
    public float forceSpeed;

    void Awake()
    {
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();

        randomPush = Random.Range(-1.0f, 1.0f);
        direction = new Vector3(randomPush, 1.0f, -2.0f);

        startPos = transform.position;
        startRot = transform.rotation;

    }
    private void Update()
    {
        changeTime += Time.deltaTime;

        if (changeTime > 5.0f)
        {
            rb.velocity = Vector3.zero;
            transform.localPosition = startPos;
            transform.rotation = startRot;
            changeTime = 0;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        RingController controller = col.gameObject.GetComponentInParent<RingController>();
        if (col.gameObject.tag=="Player")
        {
            rb.velocity = direction * forceSpeed;
            anim.Play();
        }
    }
}
