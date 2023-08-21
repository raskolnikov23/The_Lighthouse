using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;



    private void Start()
    {
        health = 100f;
    }


    public void UpdateHealth(float value)
    {
        health += value;
        Debug.Log(health);
    }
}
