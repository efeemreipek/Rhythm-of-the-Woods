using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IInteractable
{
    [SerializeField] private float batteryAmount = 4f;

    public void Interact(Transform interactor)
    {
        interactor.GetComponentInChildren<Flashlight>().batteryRemaining += batteryAmount;
        Destroy(gameObject);
    }

    public override string ToString()
    {
        return gameObject.name;
    } 
}
