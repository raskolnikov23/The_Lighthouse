using System;
using UnityEngine;


public class ItemHandler : MonoBehaviour
{
    #region declarations

    public GameObject currentlyEquippedItem;
    private InventoryManager inventoryManager;
    [SerializeField] private GameObject[] toolbarItems;
    [SerializeField] private int activeToolbarSlot = 1;
    [SerializeField] private GameObject toolbarItemParent;
    public Transform armPosition;
    public float throwArc;
    public float throwForce;
    public Vector3 throwVector;

    
        #endregion
    
    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        toolbarItems = new GameObject[9];
    }
    

    public void OnToolbarSlotChange(int id)
    {

        // if the new slot is empty
        if (inventoryManager.itemArray[id - 1].itemName == null)
        {
            // and previous slot has an item
            if (toolbarItems[activeToolbarSlot-1] != null)
                
                // disable it (or else it won't disable)
                toolbarItems[activeToolbarSlot-1].SetActive(false);
        }

        
        
        // disable previous active item (if not dropped)
        if (toolbarItems[activeToolbarSlot-1] != null)
            toolbarItems[activeToolbarSlot-1].SetActive(false); 
        
        activeToolbarSlot = id;
        
        

        // if the new slot has an item, activate it
        if (toolbarItems[id-1] != null) toolbarItems[id-1].SetActive(true);
        

        currentlyEquippedItem = toolbarItems[id-1];
        
 
    }

    private void ToolbarItemDrop(int id)
    {
        // if no item equipped, no business here
        if (currentlyEquippedItem == null) return; 
        
        // instantiate item near player
        GameObject droppedItem = Instantiate(currentlyEquippedItem, armPosition.position, transform.rotation);

        //Vector3 throwVector = new Vector3(transform.forward.x * throwForce, transform.forward.y * throwArc * transform.up, transform.forward.z * throwForce);
        Vector3 throwVector = transform.forward * throwForce + transform.up * throwArc;

        var rb = droppedItem.AddComponent<Rigidbody>();
        rb.AddForce(throwVector, ForceMode.Impulse);
        
 

        // sets currently equipped to none
        currentlyEquippedItem = null; 
                                        
        // removes item from toolbar_items GameObject
        Destroy(toolbarItems[id]);
        
        // resets GameObject to none
        toolbarItems[id] = null;
    }

    private void ToolbarItemAdded(int id)
    {
        // create model of added item
        toolbarItems[id] = Instantiate(inventoryManager.itemArray[id].prefab, toolbarItemParent.transform);
        toolbarItems[id].GetComponent<ItemInstance>().ammo = inventoryManager.itemArray[id].itemInstance.ammo; 

        // set model local location and rotation
        toolbarItems[id].transform.localEulerAngles = toolbarItems[id].GetComponent<ItemInstance>().itemData.equipRotation;
        toolbarItems[id].transform.localPosition = toolbarItems[id].GetComponent<ItemInstance>().itemData.equipLocation;



        // if new item added to active slot
        if (inventoryManager.currentSlot == id+1)
            // set currentlyEquippedItem to it
            currentlyEquippedItem = toolbarItems[id];
        
        // if slot inactive, disable new item
        else toolbarItems[id].SetActive(false);
    }


    private void OnEnable()
    {
        inventoryManager.ItemAdd += ToolbarItemAdded;
        inventoryManager.SlotSwitch += OnToolbarSlotChange;
        inventoryManager.ToolbarItemDrop += ToolbarItemDrop;
    }
    private void OnDisable()
    {
        inventoryManager.ItemAdd -= ToolbarItemAdded;
        inventoryManager.SlotSwitch -= OnToolbarSlotChange;
        inventoryManager.ToolbarItemDrop -= ToolbarItemDrop;
    }

}
