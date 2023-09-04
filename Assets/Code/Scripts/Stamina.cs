using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    // stamina regen function as coroutine?

    public int maxStaminaPoints;
    public float staminaPoints;
    public int regenRate;
    public int drainRate;
    public float regenTimeout;
    public float regenTimeoutLong = 1.0f;
    public float regenTimeoutShort = 0.5f;
    public GameObject staminaBar;
    public Image barFill;
    public bool sprinting;

    private void Awake()
    {
        barFill = staminaBar.GetComponent<Image>();
    }

    private void Start()
    {
        staminaPoints = maxStaminaPoints;
    }

    public void Update()
    {
        if (staminaPoints < maxStaminaPoints && regenTimeout == 0 && !sprinting) // also if not sprinting
        {
            StaminaRegen();
        }

        if (regenTimeout > 0 && !sprinting)
        {
            regenTimeout -= Time.deltaTime;
            if (regenTimeout < 0) regenTimeout = 0;
        }

        barFill.fillAmount = (float) staminaPoints / 100;
    }

    public void StaminaDrain()  // gets called by PlayerMovement
    {
        staminaPoints -= drainRate * Time.deltaTime; 
        regenTimeout += drainRate / 100f * Time.deltaTime;

        if (staminaPoints < 0) 
        {
            staminaPoints = 0;
        }
    }

    public void StaminaRegen()
    {
        staminaPoints += regenRate * Time.deltaTime;
    }
}
