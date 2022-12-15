using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicInventory : InventoryUI 
{

    public static event HandleOpenInventory OnOpenInventory;
    public delegate void HandleOpenInventory();
    public static event HandleCloseInventory OnCloseInventory;
    public delegate void HandleCloseInventory();

    public MouseItemData mouseItem;
    private bool selectModifier = false;
    public InputAction selectModifierAction;

    void Start()
    {
        selectModifierAction.performed += OnModifySelect;
    }

    public override void SlotClicked(InventorySlotUI slot, PointerEventData eventData) {
        // already have a selected slot
        if (!mouseItem.AssignedInventorySlot.isEmpty()) {
            Debug.Log("Moving item to slot");
            // moving all items selected from mouse
            if (eventData.button == PointerEventData.InputButton.Left) {
                Move(mouseItem.AssignedInventorySlot, mouseItem.AssignedInventorySlot.StackSize, slot.AssignedInventorySlot);

            // moving one item from the mouse
            } else if (eventData.button == PointerEventData.InputButton.Right) {
                Move(mouseItem.AssignedInventorySlot, 1, slot.AssignedInventorySlot);
            }

            // if no items left on mouse then disable it
            if (mouseItem.AssignedInventorySlot.isEmpty()) {
                DisableMouseSlot();
            }
        }

        // selecting a slot
        else {
            Debug.Log("Selecting item");
            // if that slot is not empty then select it
            if (!slot.AssignedInventorySlot.isEmpty()) {
                // detect type of select
                if (eventData.button == PointerEventData.InputButton.Left && !selectModifier) {
                    // select all
                    Debug.Log("Selecting all");
                    EnableMouseSlot(slot, slot.AssignedInventorySlot.StackSize);
                } else if (eventData.button == PointerEventData.InputButton.Right && !selectModifier) {
                    // select one
                    Debug.Log("Selecting one");
                    EnableMouseSlot(slot, 1);
                } else if (eventData.button == PointerEventData.InputButton.Right && selectModifier) {
                    // select half
                    Debug.Log("Selecting half");
                    EnableMouseSlot(slot, slot.AssignedInventorySlot.StackSize/2);
                }
            }
        }
    }

    public void Swap(InventorySlot slot1, InventorySlot slot2) {
        // swap the slots
        InventorySlot tempSlot1 = new InventorySlot(slot1.ItemData, slot1.StackSize);
        slot1.MoveSlot(slot2);
        slot2.MoveSlot(tempSlot1);
    }

    public void Move(InventorySlot slot1, int quantity, InventorySlot slot2) {
        // Can't move an empty space
        if (slot1.isEmpty()) {
            return;
        }

        // if the slots have the same item
        if (slot2.ItemData == slot1.ItemData) {
            // if there is enough space for all items then add them all
            if (slot2.isEnoughSpace(quantity)) {
                slot1.RemoveFromStack(quantity);
                slot2.AddToStack(quantity);
            // otherwise add as many as possible
            } else {
                slot1.RemoveFromStack(slot2.spaceLeft());
                slot2.AddToStack(slot2.spaceLeft());
            }
        }

        // moving all items from slot 1 then swap
        else if (quantity == slot1.StackSize) {
            Swap(slot1, slot2);
        }

        // moving some items from slot 1
        else if (quantity < slot1.StackSize) {
            // slot moving to must be empty
            if (slot2.isEmpty()) {
                slot1.RemoveFromStack(quantity);
                slot2.MoveSlot(new InventorySlot(slot1.ItemData, quantity));
            }
        } 
        
        else {
            Debug.LogWarning("Trying to move an impossible quantity");
        }
    }

    public void OnModifySelect(InputAction.CallbackContext context) {
        if (context.performed == true) {
            selectModifier = true;
        } else if (context.performed == false) {
            selectModifier = false;
        }
    }

    protected override void OnEnable() {
        base.OnEnable();

        selectModifierAction.Enable();
        DrawInventory(inventory.InventorySlots);

        //GameObject toolbarObject = GameObject.Find("Toolbar");
        //toolbarObject.GetComponent<ToolbarManager>().enabled = false;
        OnOpenInventory?.Invoke();
    }

    protected override void OnDisable() {
        base.OnDisable();

        selectModifierAction.Enable();

        OnCloseInventory?.Invoke();
    }

    private void EnableMouseSlot(InventorySlotUI slot, int quantity) {
        mouseItem.gameObject.SetActive(true);
        mouseItem.AssignedInventorySlot.MoveSlot(new InventorySlot(slot.AssignedInventorySlot.ItemData, quantity));
        slot.AssignedInventorySlot.RemoveFromStack(quantity);
    }

    private void DisableMouseSlot() {
        mouseItem.gameObject.SetActive(false);
    }
}



















    /*
    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("Entering");
        EnableInputActions();
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("Exiting");
        DisableInputActions();
    }
    */

       /*
    void OnSelect(InputAction.CallbackContext context) {
        // already have a selected slot
        if (selectedSlot != null) {
            // selecting a slot to move item to
            if (hoverSlot != null) {
                Debug.Log("Moving (all) " + selectedSlot.slotNumber + " to " + hoverSlot.slotNumber);
                inventory.Move(selectedSlot.slotNumber, hoverSlot.slotNumber);
                selectedSlot = null;
                Destroy(dragItem);
            // selecting empty space to drop item (for now move back)
            } else {
                Debug.Log("Moving " + selectedSlot.slotNumber + " back");
                selectedSlot.ReturnSlot();
                selectedSlot = null;
                Destroy(dragItem);
            }
        }

        // selecting a slot
        else if (hoverSlot != null) {
            Debug.Log("Selected (all from) slot " + hoverSlot.slotNumber);
            
            // if that slot is not empty then select it
            if (inventory.InventorySlots[hoverSlot.slotNumber] != null) {
                selectedSlot = hoverSlot;

                //TODO: create moving item gameobject
                CreateDragItem(selectedSlot);

                selectedSlot.ClearSlot();
            }
        }
    } */