using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSway : MonoBehaviour
{

    public GameObject parent;
    public ItemHandler itemHandler;
    public int swayState;

    void Update()
    {
        if (itemHandler.currentlyEquippedItem != null) 
        {
            Sway();
        }
    }

    private void Sway()
    {
        //itemHandler.currentlyEquippedItem.transform.localPosition = Vector3.SmoothDamp()
    }
}
