
using TMPro;
using UnityEngine;


public class AttackLogic : MonoBehaviour
{
    private ItemHandler itemHandler;
    private Raycaster raycaster;
    public int shotStr;
    public TextMeshProUGUI ammoUI;
    public InputData inputData;

    private void Awake()
    {
        itemHandler = GetComponent<ItemHandler>();
        raycaster = GetComponent<Raycaster>();
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
        if (itemInstance.ammo == 0) { Debug.Log("No ammo"); return; }       // might need to check if less than zero


        itemInstance.ammo -= 1;
        ammoUI.text = itemInstance.ammo.ToString();
        Debug.Log("Ammo: " + itemInstance.ammo);

        GameObject target = raycaster.lookingOnObject;
        if (target.name.StartsWith("Enemy"))
        {
            if (raycaster.distanceBetween < 10)
            {
                target.GetComponent<EnemyHealth>().UpdateHealth(-20);
            }

            Vector3 shotDir = (target.transform.position - transform.position).normalized;

            target.GetComponent<Enemy>().ReceiveHit(shotDir, shotStr);

            //Debug.Log("shot dir: " + shotDir);
            //Debug.Log("shotdir x knockbackstr: " + shotDir * knockbackStr);
            //Debug.Log("knockbackstr: " + knockbackStr);
            

            target.GetComponent<ParticlePlayer>().PlayBloodParticles();
        }
    }
    

    private void OnEnable()
    {
        inputData.AttackPressed += Attack;
    }

    private void OnDisable()
    {
        inputData.AttackPressed -= Attack;
    }
}
