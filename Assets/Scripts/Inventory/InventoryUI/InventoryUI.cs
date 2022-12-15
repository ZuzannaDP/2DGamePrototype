using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public abstract class InventoryUI : MonoBehaviour 
{
    public GameObject slotPrefab;
    protected Inventory inventory;
    public int numSlots;
    protected List<InventorySlotUI> inventorySlots;
    public InventoryHolder inventoryHolder;

    protected virtual void Awake() {
        ResetInventory();

        inventory = inventoryHolder.HolderInventory;
        
        for (int i = 0; i < inventorySlots.Capacity; i++) {
            CreateInventorySlot(inventory.InventorySlots[i]);
        }
    }

    protected virtual void OnEnable() {
        inventory.OnInventoryChange += DrawInventory;
    }

    protected virtual void OnDisable() {
        inventory.OnInventoryChange -= DrawInventory;
    }

    public void ResetInventory() {
        foreach(Transform childTransform in transform) {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InventorySlotUI>(numSlots);
    }

    public void DrawInventory(InventorySlot[] inventory) {
        for (int i = 0; i < numSlots; i++) {
            InventorySlot slot = inventory[i];
            inventorySlots[i].DrawSlot(slot);
        }
    }

    public void CreateInventorySlot(InventorySlot slot) {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlotUI newSlotComponent = newSlot.GetComponent<InventorySlotUI>();
        newSlotComponent.ClearSlot();
        newSlotComponent.assignSlot(slot);
        newSlotComponent.assignInventoryUI(transform.GetComponent<InventoryUI>());
        newSlotComponent.OnEnable();

        inventorySlots.Add(newSlotComponent);
    }

    public abstract void SlotClicked(InventorySlotUI slotClicked, PointerEventData eventData);
}
