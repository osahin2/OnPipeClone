using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RingController : MonoBehaviour
{
    private const float RING_SCALE_Z = 0.2f;

    [SerializeField] private Transform ringRayTransform;
    [SerializeField] private GameObject cam;
    [SerializeField] private float ringSpeed;
    [SerializeField] private float scaleLerpFactor;
    public LayerMask ringRayLayer;

    private Vector3 cameraFirstPos, cameraLastPos;
    private Vector3 rayTargetScale;
    private Vector3 targetPos;

    private RaycastHit raycastHit;

    
    private bool scaleControl;
    

    public void Initialized()
    {
        InputEventHandler.PointerDowned += MinimizeRingScale;
        InputEventHandler.PointerUpped += GetRingOriginalSize;
        cameraFirstPos = cam.transform.position - transform.position;
    }

    public void StopInputs()
    {
        InputEventHandler.PointerDowned -= MinimizeRingScale;
        InputEventHandler.PointerUpped -= GetRingOriginalSize;
    }

    private void MinimizeRingScale(PointerEventData eventData)
    {
        scaleControl = true;
    }

    private void GetRingOriginalSize(PointerEventData eventData)
    {
        scaleControl = false;
        transform.localScale = new Vector3(2f, 2f, 0.2f);
    }


    private void FixedUpdate()
    {
        CameraFollow();
        MoveRing();
        if (scaleControl && Physics.Raycast(ringRayTransform.position, Vector3.down, out raycastHit, 1000, ringRayLayer))
        {
            var transform1 = raycastHit.collider.transform;
            var localScale = transform1.localScale;
            rayTargetScale = new Vector3(localScale.x, localScale.z, RING_SCALE_Z);
            Debug.DrawLine(ringRayTransform.position, raycastHit.point, Color.magenta);
            transform.localScale = Vector3.Lerp(transform.localScale, rayTargetScale, scaleLerpFactor);
        }
    }

    private void MoveRing()
    {
        transform.position += Time.deltaTime * ringSpeed * Vector3.forward;
    }

    private void CameraFollow()
    {
        if (GameController.Instance.ControlFinish) return;
        cameraLastPos = cameraFirstPos + transform.position;
        cam.transform.position = cameraLastPos;
    }
    
}
