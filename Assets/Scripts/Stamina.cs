using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina
{
    private float currentStamina;
    private float maxStamina;

    public Stamina(float startStamina, float startMaxStamina) {
        currentStamina = startStamina;
        maxStamina = startMaxStamina;
    }

    public float getStamina() {
        return currentStamina;
    }

    public float getMaxStamina() {
        return maxStamina;
    }

    public void SetMaxStamina(float newMaxStamina) {
        maxStamina = newMaxStamina;
    }

    /// <summary>
    /// Removes <paramref name="amount"/> amount from the stamina.
    /// </summary>
    /// <returns> The new current stamina. </returns>
    public float decreaseStamina(float amount) {
        if (currentStamina > amount) {
            currentStamina -= amount;
        } else {
            currentStamina = 0;
        }

        return currentStamina;
    }

    /// <summary>
    /// Adds <paramref name="amount"/> amount to the stamina.
    /// </summary>
    /// <returns> The new current stamina. </returns>
    public float recoverStamina(float amount) {
        if (currentStamina < maxStamina) {
            currentStamina += amount;
        }

        if (currentStamina > maxStamina) {
            currentStamina = maxStamina;
        }

        return currentStamina;
    }

    /// <summary>
    /// Returns true if there is enough stamina to decrease stamina.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool enoughStamina(float amountToDecrease) {
        return (currentStamina >= amountToDecrease);
    }

    /// <returns> True if the current stamina is 0. </returns>
    public bool isEmpty() {
        return (currentStamina == 0);
    }

    /// <returns> True if the current stamina is equal to the max stamina. </returns>
    public bool isFull() {
        return (currentStamina == maxStamina);
    }
}