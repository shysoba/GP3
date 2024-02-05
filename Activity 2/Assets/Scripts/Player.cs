using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private TextMeshProUGUI pickUpUIText;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;

    [SerializeField]
    private InputActionReference interactionInput, dropInput;

    private RaycastHit hit;
    #endregion 
    private void Start()
    {
        interactionInput.action.performed += Interact;
        dropInput.action.performed += Drop;
        UpdatePickUpUI();
    }

    private void Drop(InputAction.CallbackContext obj)
    {
        // drop the held item
        if (inHandItem != null)
        {
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            // ui update when drop
            UpdatePickUpUI();
        }
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            // check if item
            if (hit.collider.GetComponent<Item>())
            {
                Debug.Log("Carry");
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.SetParent(pickUpParent.transform, true);
                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                // ui update when picked
                UpdatePickUpUI();
                return;
            }
        }
    }

    private void UpdatePickUpUI()
    {
        // ui check if item is picked
        if (inHandItem != null)
        {
            pickUpUIText.text = "Drop";
        }
        else
        {
            pickUpUIText.text = "Pick Up";
        }
    }

    private void Update()
    {
        // draws ray
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        // check for a hit 
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
        {
            // highlight the hit object and update ui
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUIText.gameObject.SetActive(true);
            UpdatePickUpUI();
        }
        else
        {
            // if no hit ui is off
            pickUpUIText.gameObject.SetActive(false);
            return;
        }

        // item in hand no checks
        if (inHandItem != null)
        {
            return;
        }
    }
}
