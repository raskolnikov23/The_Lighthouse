using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackLogic : MonoBehaviour
{
    private ItemHandler itemHandler;
    private InputHandler _inputHandler;
    private Raycaster raycaster;
    public int knockbackStr;
    //public 

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
    // executes Attack() where it checks ItemHandler's equipped item,
    // then checks in with Raycaster, gets info,
    // then if enemy in front, THROUGH RAYCASTER accesses EnemyHealth... wrong?
    // yes wrong, because raycaster only gives us information, we dont use it as an access tool, i think.

    // maybe executes a method in itemhandler, that returns the item, not read straight from a public variable? THE SMART WAY

    // every weapon has its own reach. every gun has its distance / damage ratio






    private void Attack()
    {
        if (itemHandler.currentlyEquippedItem == null) return;

        ItemInstance itemInstance = itemHandler.currentlyEquippedItem.GetComponent<ItemInstance>();
        if (itemInstance.itemData.itemType != ItemType.firearm) return;     // for now, only firearms can attack

        GameObject target = raycaster.lookingOnObject;
        if (target.name == "Enemy")
        {
            if (raycaster.distanceBetween < 10)
            {
                target.GetComponent<EnemyHealth>().UpdateHealth(-20);
            }

            Vector3 shotDir = (target.transform.position - transform.position).normalized;
            target.GetComponent<Rigidbody>().AddForce(shotDir * knockbackStr, ForceMode.VelocityChange);

            Debug.Log("shot dir: " + shotDir);
            Debug.Log("shotdir x knockbackstr: " + shotDir * knockbackStr);
            Debug.Log("knockbackstr: " + knockbackStr);

            target.GetComponent<ParticlePlayer>().PlayBloodParticles();
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
