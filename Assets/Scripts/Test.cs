using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IInteractable
{
    public void Interact(Transform interactor)
    {
        print(interactor.name + " Interacted w/ " + gameObject.name);
    }
}
