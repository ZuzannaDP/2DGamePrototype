using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthManager : MonoBehaviour
{
    private Health playerHealth;
    public HealthBar healthBar;
    public int startHealth;
    public int startMaxHealth;

    private void Start() {
        playerHealth = new Health(startHealth, startMaxHealth);
        healthBar.SetHealth(startHealth);
        healthBar.SetMaxHealth(startMaxHealth);

        EnemyManager.OnDamage += dmgHealth;
    }

    public void dmgHealth(int dmg) {
        // set health bar to new health
        healthBar.SetHealth(playerHealth.dmgHealth(dmg));

        Debug.Log("Player health damaged by " + dmg + ", new health: " + playerHealth.getHealth());

        // check if the new health is empty
        if (playerHealth.isEmpty()) {
            Debug.Log("Player died. rip");
        }
    }

    public void healHealth(int healing) {
        // set health bar to new health
        healthBar.SetHealth(playerHealth.healHealth(healing));
    }
}