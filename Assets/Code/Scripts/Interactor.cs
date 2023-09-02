using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    
    public float interactionRange = 5;
    public float distanceBetween;
    public GameObject interactionObject;
    public InputData inputData;
    private Raycaster raycaster;
    
    //temp
    public InventoryManager invmanager;

    private void Awake()
    {
        raycaster = GetComponent<Raycaster>();
    }

    public void Interact()
    {
        interactionObject = raycaster.lookingOnObject;
        distanceBetween = raycaster.distanceBetween;
        

        // TO DO: vienkarshot (nevajadzetu chekot parentu)
        if (distanceBetween > 0 && distanceBetween < 5)
        {
            ItemInstance itemInstance = interactionObject.GetComponent<ItemInstance>();

            if (itemInstance == null)
            {
                itemInstance = interactionObject.GetComponentInParent<ItemInstance>();
            }

            if (itemInstance == null) return;
            
            invmanager.AddItem(itemInstance);

            Destroy(interactionObject); // not sure if here is best, maybe some independent service that does this
                                        // we just let it know that it has happened
            
        }
    }

    private void OnEnable()
    {
        inputData.InteractPressed += Interact;
    }
    private void OnDisable()
    {
        inputData.InteractPressed -= Interact;
    }
}
