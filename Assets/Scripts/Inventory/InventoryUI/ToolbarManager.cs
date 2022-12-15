using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolbarManager : InventoryUI //, IPointerEnterHandler, IPointerExitHandler
{

    private InventorySlotUI selectedSlot;

    public bool disabledInput = false;

    protected override void Awake() {
        base.Awake();
        selectSlot(inventorySlots[0]);
    }

    public override void SlotClicked(InventorySlotUI slotClicked, PointerEventData eventData)
    {
        if (!disabledInput) {
            resetSlot(selectedSlot);
            selectSlot(slotClicked);
        }
    }
    
    private void resetSlot(InventorySlotUI slot) {
        slot.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    private void selectSlot(InventorySlotUI slot) {
        selectedSlot = slot;
        slot.GetComponent<Image>().color = new Color32(219, 105, 105, 255);
    }
    
    /*
    protected override void OnEnable() {
        PlayerInventory.OnInventoryChange += DrawInventory;
        PlayerInventory.OnInventoryChange += ShowSelection;

        DisableInputActions();

        DynamicInventory.OnOpenInventory += Hide;
        DynamicInventory.OnCloseInventory += Show;
    }

    protected override void OnDisable() {
        PlayerInventory.OnInventoryChange -= DrawInventory;
        PlayerInventory.OnInventoryChange -= ShowSelection;

        DisableInputActions();

        DynamicInventory.OnOpenInventory -= Hide;
        DynamicInventory.OnCloseInventory -= Show;
    }

    public override void DisableInputActions() {
        OnSelectAction.Disable();
    }

    public override void EnableInputActions() {
        OnSelectAction.Enable();
    }

    private void OnSelect(InputAction.CallbackContext context) {
        Debug.Log("Toolbar item selected");

        // set selected slot
        if (hoverSlot != null) {
            // revert previously selected space
            if (selectedSlot != null) {
                selectedSlot.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }

            selectedSlot = hoverSlot;
            ShowSelection(inventory.InventorySlots);
        }
    }

    private void ShowSelection(InventorySlot[] inventoryItems) {
        Debug.Log("ShowSelection()");
        if (selectedSlot != null) {
            Debug.Log("Setting colour for slot " + selectedSlot.slotNumber);
            selectedSlot.GetComponent<Image>().color = new Color32(219, 105, 105, 255);
        }
    } 

    public void OnPointerEnter(PointerEventData eventData) {
        if (!disabledInput) EnableInputActions();
    }

    public void OnPointerExit(PointerEventData eventData) {
        DisableInputActions();
    } */

    private void Hide() {
        transform.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
        disabledInput = true;
    }

    private void Show() {
        transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        disabledInput = false;
    }
}
