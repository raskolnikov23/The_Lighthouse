using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName = "";
    public string description;
    public Sprite icon;
    public bool stackable;
    public ItemType itemType;
    public GameObject prefab;
    public Vector3 equipLocation;
    public Vector3 equipRotation;
    



}

public enum ItemType
{
    tool,
    firearm,
    consumable,
    armor
}



