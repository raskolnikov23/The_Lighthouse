using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    public ItemHandler itemHandler;
    public PlayerMovement playerMovement;
    
    public Vector3 standardPos;
    public Vector3 middlePos;
    public Vector3 leftPos;
    public Vector3 rightPos;
    public Vector3 newPos;
    public Vector3 _ref;

    public float xOffsetL;
    public float xOffsetR;
    public float yOffset;
    public float normalizeTime;
    public float swayTime;
    public float timer;
    public float timerValue;

    public int direction;
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

        if (playerMovement.walking) 
        {
            standardPos = itemHandler.currentlyEquippedItem.GetComponent<ItemInstance>().itemData.equipLocation;
            leftPos = new Vector3(standardPos.x + xOffsetL, standardPos.y + yOffset, standardPos.z);
            rightPos = new Vector3(standardPos.x + xOffsetR, standardPos.y + yOffset, standardPos.z);
            middlePos = new Vector3(standardPos.x, standardPos.y, standardPos.z);

            itemHandler.currentlyEquippedItem.transform.localPosition = Vector3.SmoothDamp(itemHandler.currentlyEquippedItem.transform.localPosition, newPos, ref _ref, swayTime);

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SwitchPos();
            }

        }
        else if (playerMovement.standing)
        {
            itemHandler.currentlyEquippedItem.transform.localPosition = Vector3.SmoothDamp(itemHandler.currentlyEquippedItem.transform.localPosition, standardPos, ref _ref, normalizeTime);
        }

        // if standing


        //itemHandler.currentlyEquippedItem.transform.localPosition = Vector3.SmoothDamp()
    }

    public void SwitchPos()
    {
        if (swayState == -1 || swayState == 1)
        {
            newPos = middlePos;
            swayState = 0;
            direction *= -1;
        }

        else if (swayState == 0)
        {
            if (direction == -1)
            {
                newPos = leftPos;
                swayState = -1;
            }
            if (direction == 1)
            {
                newPos = rightPos;
                swayState = 1;
            }
        }

        timer = timerValue;
    }
}
