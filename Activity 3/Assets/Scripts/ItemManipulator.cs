using System.Collections;
using UnityEngine;

public class ItemManipulator : MonoBehaviour
{
    [SerializeField] private Transform camPos;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform grabLocation;
    [SerializeField] private bool isGrabbing;

    private ItemPickup ItemPickup;
    private float pickUpDistance = 5f;
    private RaycastHit raycastHit;

    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject grab;
    [SerializeField] private GameObject pull;

    [SerializeField] private float pullSpeed = 5f;
    [SerializeField] private float pullDistance = 10f;
    [SerializeField] private float pullForce = 10f;

    [SerializeField] private LineRenderer pullLineRenderer;
    [SerializeField] private float lineMaxLength = 20f;

    private Coroutine pullCoroutine;
    private float pullDuration = 1f;

    void Start()
    {
        InitializeLineRenderer();
    }

    void Update()
    {
        if (Physics.Raycast(camPos.position, camPos.forward, out raycastHit, pullDistance, pickUpLayerMask))
        {
            indicator.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TryGrabObject();
            }

            if (isGrabbing)
            {
                drop.SetActive(true);
                pull.SetActive(false);
                grab.SetActive(false);

                // Check for pull input
                if (Input.GetKey(KeyCode.F))
                {
                    PullObject();
                    UpdateLineRenderer();
                }
            }
            else
            {
                grab.SetActive(true);
                pull.SetActive(true);
                drop.SetActive(false);
            }
        }
        else
        {
            indicator.SetActive(false);
            pullLineRenderer.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryFireObject();
        }
    }

    private void TryGrabObject()
    {
        if (ItemPickup == null)
        {
            if (raycastHit.transform != null && raycastHit.transform.TryGetComponent(out ItemPickup))
            {
                ItemPickup.Grab(grabLocation);
                isGrabbing = true;
            }
        }
        else
        {
            ItemPickup.Drop();
            ItemPickup = null;
            isGrabbing = false;
            drop.SetActive(false);
        }
    }

    private void TryFireObject()
    {
        if (isGrabbing && ItemPickup != null)
        {
            ItemPickup.Fired();
            isGrabbing = false;
            drop.SetActive(false);
        }
    }

    private void PullObject()
    {
        if (ItemPickup != null)
        {
            if (pullCoroutine == null)
            {
                pullCoroutine = StartCoroutine(PullObjectCoroutine());
            }
        }
    }

    private IEnumerator PullObjectCoroutine()
    {
        Rigidbody rb = ItemPickup.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 targetPosition = grabLocation.position;

            grab.SetActive(false);
            pull.SetActive(true);

            while (Vector3.Distance(ItemPickup.transform.position, targetPosition) > 0.1f)
            {
                Vector3 pullDirection = (targetPosition - ItemPickup.transform.position).normalized;
                rb.AddForce(pullDirection * pullForce);

                yield return null;
            }
        }

        grab.SetActive(false);
        pull.SetActive(false);
        pullCoroutine = null;
    }

    private void InitializeLineRenderer()
    {
        pullLineRenderer.positionCount = 2;
        pullLineRenderer.enabled = false;
    }

    private void UpdateLineRenderer()
    {
        pullLineRenderer.enabled = true;
        pullLineRenderer.SetPosition(0, grabLocation.position);
        pullLineRenderer.SetPosition(1, raycastHit.point);
    }
}