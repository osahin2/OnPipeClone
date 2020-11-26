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
        StartCoroutine(Move());
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
    

    private IEnumerator Move()
    {
        while (true)
        {
            CameraFollow();
            MoveRing();
            if (scaleControl)
            {
                if (Physics.Raycast(ringRayTransform.position, Vector3.down, out raycastHit, 1000, ringRayLayer))
                {
                    rayTargetScale = new Vector3(raycastHit.collider.transform.localScale.x, raycastHit.collider.transform.localScale.z, RING_SCALE_Z);
                    Debug.DrawLine(ringRayTransform.position, raycastHit.point, Color.magenta);
                    transform.localScale = Vector3.Lerp(transform.localScale, rayTargetScale, scaleLerpFactor);
                }
            }
            yield return null;
        }
    }

    private void MoveRing()
    {
        transform.position += Time.deltaTime * ringSpeed * Vector3.forward;
    }

    private void CameraFollow()
    {
        if (!GameController.Instance.ControlFinish)
        {
            cameraLastPos = cameraFirstPos + transform.position;
            cam.transform.position = cameraLastPos;
        }
    }
    
}
