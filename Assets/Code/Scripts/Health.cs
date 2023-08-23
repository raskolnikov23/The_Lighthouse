// Health component, preferably for every living entity
// 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    public GameObject healthbar;


    private void Start()
    {
        health = 100f;
    }


    public void UpdateHealth(float value)
    {
        health += value;
        healthbar.GetComponent<Image>().fillAmount += value*0.01f;
    }


    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        UpdateHealth(-17);
    //    }
    //}
}
