using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    WeaponAnimator weaponAnimator;
    public ParticleSystem bloodParticles;
    public int knockbackStr;

    // bullethole sprite
    // blood particle
    // muzzle flash

    private void Awake()
    {
        weaponAnimator = GetComponent<WeaponAnimator>();
        knockbackStr = 2;
    }

    /*  */
    public void Attack(ItemType itemType, GameObject target)
    {
        /* do nothing, if not holding a gun */ 
        if (itemType != ItemType.firearm) return;
        
        if (target.name == "Enemy")
        {
            Vector3 shotDir = (target.transform.position - transform.position).normalized;
            target.GetComponent<Rigidbody>().AddForce(shotDir * knockbackStr, ForceMode.VelocityChange);

            Debug.Log("shot dir: " + shotDir);
            Debug.Log("shotdir x knockbackstr: " + shotDir * knockbackStr);
            Debug.Log("knockbackstr: " + knockbackStr);

            target.GetComponent<ParticlePlayer>().PlayBloodParticles();
        }









            // check bullets?

            // shoot at aimed point:
            // 1. cast ray
            // 2. where ray hits, check what it is
            // 3. if object is static, like terrain - spawn bullethole at ray point
            // 4. if something like enemy, blood particles outwards and knockback
            // 5. of course weapon and camera animation
        

        // trigger animation
        //weaponAnimator.Attack();
    }
}
