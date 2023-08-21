using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogic : MonoBehaviour
{
    private ItemHandler itemHandler;
    private InputHandler _inputHandler;
    private Raycaster raycaster;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        itemHandler = GetComponent<ItemHandler>();
        raycaster = GetComponent<Raycaster>();
    }

    private void Start()
    {
        
        
    }

    // AttackLogic listens to InputHandler mouse1 click event,
    // executes on the event a method where checks ItemHandler's equipped item,
    // then checks in with Raycaster, gets info,
    // then if enemy in front, THROUGH RAYCASTER accesses EnemyHealth... wrong?
    // yes wrong, because raycaster only gives us information, we dont use it as an access tool, i think.

    // maybe executes a method in itemhandler, that returns the item, not read straight from a public variable? THE SMART WAY

    // every weapon has its own reach. every gun has its distance / damage ratio


    private void Attack()
    {
        if (itemHandler.currentlyEquippedItem == null) return;

        if (itemHandler.currentlyEquippedItem.TryGetComponent<WeaponController>(out WeaponController weaponController))
        {
            weaponController.Attack(itemHandler.currentlyEquippedItem.GetComponent<ItemInstance>().itemData.itemType, raycaster.lookingOnObject);

            if (raycaster.lookingOnObject.name == "Enemy")
            {
                if (raycaster.distanceBetween < 10)
                {
                    raycaster.lookingOnObject.GetComponent<EnemyHealth>().UpdateHealth(-20);
                    //raycaster.lookingOnObject.GetComponent<Rigidbody>().AddForce((transform.position +
                    //raycaster.lookingOnObject.transform.position)*0.01f, ForceMode.Impulse);
                }
            }
        }
    }
    

    private void OnEnable()
    {
        _inputHandler.AttackPressed += Attack;
    }

    private void OnDisable()
    {
        _inputHandler.AttackPressed -= Attack;
    }
}
