using System;
using Unity.VisualScripting;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    #region declarations
    
    public int currentSlot = 1;
    public InventoryObject[] itemArray;
    public int maxSlots;
    public InputData inputData;

    public delegate void IntDelegateVoid(int id);
    
    public event IntDelegateVoid ItemAdd;
    public event IntDelegateVoid SlotSwitch;
    public event IntDelegateVoid ToolbarItemDrop;

    
    #endregion

    private void Awake()
    {
        itemArray = new InventoryObject[9];
    }

    private void Start()
    {
        FillInventory();
    }

    
    private void FillInventory()
    {
        for (int i = 0; i <= 8; i++)
        {
            itemArray[i] = new InventoryObject();
        }
    }

    public void AddItem(ItemInstance itemInstance)
    {
        for (int i = 0; i <= maxSlots; i++)
        {
            if (itemArray[i].itemName == null)
            {
                InventoryObject itemToAdd = new InventoryObject(itemInstance);
                itemArray[i] = itemToAdd;
                Debug.Log($"{itemToAdd.itemName} was added to inventory");
                ItemAdd(i); // fires event
                return;
            }
        }

        Debug.Log("Inventory full.");
    }

    private void OnSlotSwitch(int key)
    {
        // return if slot already active
        if (key == currentSlot) return;
        
        // set currentSlot to key
        currentSlot = key;
        
        // raise slot switched event
        SlotSwitch(key);
    }
    
    // FOR DROPPING ITEMS FROM TOOLBAR
    private void OnToolbarItemDrop()
    {
        // if there are no items in active slot, do nothing
        if (itemArray[currentSlot-1].prefab == null) return;
        
        // if there would be a stack of items, we'd need to drop a piece
        itemArray[currentSlot - 1] = null;
        itemArray[currentSlot - 1] = new InventoryObject();
        
        // event raise for ItemHandler
        ToolbarItemDrop(currentSlot-1); 
    }



    private void OnEnable()
    {
        inputData.NumberPress += OnSlotSwitch;
        inputData.ItemDropped += OnToolbarItemDrop;
    }
    private void OnDisable()
    {
        inputData.NumberPress -= OnSlotSwitch;
        inputData.ItemDropped -= OnToolbarItemDrop;

    }
}



