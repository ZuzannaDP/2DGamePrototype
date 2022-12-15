using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private int currentHealth;
    private int maxHealth;

    public Health(int startHealth, int startMaxHealth) {
        currentHealth = startHealth;
        currentHealth = startMaxHealth;
    }

    public int getHealth() {
        return currentHealth;
    }

    public int getMaxHealth() {
        return maxHealth;
    }

    public void SetMaxHealth(int newMaxHealth) {
        maxHealth = newMaxHealth;
    }

    /// <summary>
    /// Removes <paramref name="dmg"/> amount from the health.
    /// </summary>
    /// <returns> The new current health. </returns>
    public int dmgHealth(int dmg) {
        if (currentHealth > dmg) {
            currentHealth -= dmg;
        } else {
            currentHealth = 0;
        }

        return currentHealth;
    }

    /// <summary>
    /// Adds <paramref name="healing"/> amount to the health.
    /// </summary>
    /// <returns> The new current health. </returns>
    public int healHealth(int healing) {
        if (currentHealth < maxHealth) {
            currentHealth += healing;
        }

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        return currentHealth;
    }

    /// <returns> True if the current health is 0. </returns>
    public bool isEmpty() {
        return (currentHealth == 0);
    }

    /// <returns> True if the current health is equal to the max health. </returns>
    public bool isFull() {
        return (currentHealth == maxHealth);
    }
}