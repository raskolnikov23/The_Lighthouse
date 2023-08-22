using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    // stamina points regenerate, while not sprinting, for now
    // if depleted completely, a small time penalty, only after which stamina starts to regenerate
    // connected to visual stamina bar
    // stamina regen timeout - after deplete and after short sprint
    // stamina regen function as coroutine?

    public int maxStaminaPoints;
    public int staminaPoints;
    public int regenRate;
    public int drainRate;
    public float regenTimeout;
    public float regenTimeoutLong = 1.0f;
    public float regenTimeoutShort = 0.5f;



    private void Start()
    {
        staminaPoints = maxStaminaPoints;
    }

    public void Update()
    {
        if (staminaPoints < maxStaminaPoints && regenTimeout == 0) // also if not sprinting
        {
            StaminaRegen();
        }

        if (regenTimeout > 0)
        {
            regenTimeout -= Time.deltaTime;

            if (regenTimeout < 0) regenTimeout = 0;
        }
    }

    public void StaminaDrain()
    {
        staminaPoints -= drainRate;

        if (staminaPoints < 0) 
        {
            staminaPoints = 0;
            regenTimeout = regenTimeoutLong;
        }
    }

    public void StaminaRegen()
    {
        staminaPoints += regenRate;
    }

    // if stamina points < max stamina points
    // and regentimeout = 0
    // start regenerating



}
