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

        ItemInstance itemInstance = itemHandler.currentlyEquippedItem.GetComponent<ItemInstance>();

        if (raycaster.lookingOnObject.name == "Enemy")
        {
            if (raycaster.distanceBetween < 10)
            {
                raycaster.lookingOnObject.GetComponent<EnemyHealth>().UpdateHealth(-20);
            }

            Vector3 shotDir = (raycaster.lookingOnObject.transform.position - transform.position).normalized;
            raycaster.lookingOnObject.GetComponent<Rigidbody>().AddForce(shotDir * knockbackStr, ForceMode.VelocityChange);

            Debug.Log("shot dir: " + shotDir);
            Debug.Log("shotdir x knockbackstr: " + shotDir * knockbackStr);
            Debug.Log("knockbackstr: " + knockbackStr);

            raycaster.lookingOnObject.GetComponent<ParticlePlayer>().PlayBloodParticles();
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
