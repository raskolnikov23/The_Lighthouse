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


    public InventoryObject(ItemInstance itemInstance)
    {
        itemName = itemInstance.itemData.itemName;
        description = itemInstance.itemData.description;
        icon = itemInstance.itemData.icon;
        prefab = itemInstance.itemData.prefab;

    }

    public InventoryObject() {}

}


