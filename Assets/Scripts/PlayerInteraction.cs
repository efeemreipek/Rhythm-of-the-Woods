using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    [SerializeField] private TextMeshProUGUI interactionText;

    private void Start()
    {
        interactionText.enabled = false;
    }

    private void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, interactionRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactionText.enabled = true;
                interactionText.text = interactable.ToString();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(transform);

                }
            }
            else
            {
                interactionText.enabled = false;
            }
        }
    }
}
