using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStaminaManager : MonoBehaviour
{
    private Stamina playerStamina;
    public StaminaBar staminaBar;
    public int startStamina;
    public int startMaxStamina;
    public int recoverAmount;
    public float regenTime;
    private WaitForSeconds regenTick;
    private Coroutine regen;

    private void Start() {
        playerStamina = new Stamina(startStamina, startMaxStamina);
        staminaBar.SetStamina(startStamina);
        staminaBar.SetMaxStamina(startMaxStamina);

        regenTick = new WaitForSeconds(regenTime);
    }

    public void decreaseStamina(float amount) {
        staminaBar.SetStamina(playerStamina.decreaseStamina(amount));

        if (regen != null) {
            StopCoroutine(regen);
        }

        regen = StartCoroutine(RegenStamina());
    }

    public void recoverStamina(float amount) {
        staminaBar.SetStamina(playerStamina.recoverStamina(amount));
    }

    public IEnumerator RegenStamina() {
        yield return new WaitForSeconds(2);

        while (!playerStamina.isFull()) {
            recoverStamina(recoverAmount);
            yield return regenTick;
        }

        regen = null;
    }

    public bool enoughStamina(float amountToDecrease) {
        return playerStamina.enoughStamina(amountToDecrease);
    }
}