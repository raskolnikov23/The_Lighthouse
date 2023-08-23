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
    
    private InputHandler inputHandler;
    private Raycaster raycaster;
    
    //temp
    public InventoryManager invmanager;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
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
            
        }
    }

    private void OnEnable()
    {
        inputHandler.InteractPressed += Interact;
    }
    private void OnDisable()
    {
        inputHandler.InteractPressed -= Interact;
    }
}
