using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventorySlot
{
    private ItemData itemData;
    private int stackSize;

    public ItemData ItemData => itemData;
    public int StackSize => stackSize;
    public event Action OnSlotChange;

    public InventorySlot() {
        stackSize = 0;
    }

    public InventorySlot(ItemData item) {
        itemData = item;
        AddToStack();
    }

    public InventorySlot(ItemData item, int quanity) {
        itemData = item;
        stackSize = quanity;
    }

    public void MoveSlot(InventorySlot newSlot) {
        itemData = newSlot.itemData;
        stackSize = newSlot.stackSize;
        OnSlotChange?.Invoke();
    }

    public void AddToStack() {
        stackSize++;
        OnSlotChange?.Invoke();
    }

    public void AddToStack(int quantity) {
        stackSize += quantity;
        OnSlotChange?.Invoke();
    }

    public void RemoveFromStack() {
        stackSize--;
        OnSlotChange?.Invoke();
    }

    public void RemoveFromStack(int quantity) {
        stackSize -= quantity;
        OnSlotChange?.Invoke();
    }

    public bool isEmpty() {
        return (stackSize == 0);
    }

    public bool isFull() {
        return (stackSize == itemData.maxStackSize);
    }

    public bool isEnoughSpace(int quanity) {
        return (stackSize + quanity <= itemData.maxStackSize);
    }

    public int spaceLeft() {
        return (itemData.maxStackSize - stackSize);
    }
}
