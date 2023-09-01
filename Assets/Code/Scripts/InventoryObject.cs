using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Serializable]
public class InventoryObject
{

    public string itemName;
    public string description;
    public Sprite icon;
    public GameObject prefab;
    public int quantity;

    public ItemInstance itemInstance;


    public InventoryObject(ItemInstance _itemInstance)
    {
        itemName = _itemInstance.itemData.itemName;
        description = _itemInstance.itemData.description;
        icon = _itemInstance.itemData.icon;
        prefab = _itemInstance.itemData.prefab;
        itemInstance = _itemInstance;
        

    }

    public InventoryObject() {}

}


