using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public Transform water;
    public GameObject waterObj;
    float kek;
    float reff;
    public float smoothTime;
    float highrise;
    public float num;
    public Vector3 newPos;
    public Vector3 refz;
    public bool toggle = false;

    public GameObject rockTest;
    public Vector3 newPos2;
    public Vector3 refz2;

    private void Start()
    {
        water = waterObj.transform;
        highrise = water.position.y + 5;
        kek = rockTest.transform.position.y + 5;
        updateHighrise(); 
    }

    private void Update()
    {
        

        //num = Mathf.SmoothDamp(kek, kek + 20, ref reff, smoothTime);
        //water.position = new Vector3(water.position.x, num, water.position.z);

        newPos = new Vector3(water.position.x, highrise, water.position.z);
        newPos2 = new Vector3(rockTest.transform.position.x, kek, rockTest.transform.position.z);

        water.position = Vector3.SmoothDamp(water.position, newPos, ref refz, smoothTime);
        rockTest.transform.position = Vector3.SmoothDamp(rockTest.transform.position, newPos2, ref refz2, smoothTime);




    }


    void updateHighrise()
    {
        
        if(toggle)
        {
            highrise -= 5;
            kek -= 5;
            toggle = false;
        }
        else
        {
            highrise += 5;
            kek += 5;
            toggle = true;
        }

        Invoke("updateHighrise", 4);
    }
}
