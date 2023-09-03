
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    public bool showName;
    public TextMeshProUGUI text;
    public RayData rayData;


    void Update()
    {
        if (text != null)
        {
            text.text = rayData.lookingOn;
            text.text += $"\n Distance: {rayData.distanceBetween}";

            if (showName == true && rayData.inFocus == true)
            {
                text.enabled = true;
            }
            else
            {
                text.enabled = false;
            }
        }
    }
}
