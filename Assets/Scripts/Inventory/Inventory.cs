using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event Action<InventorySlot[]> OnInventoryChange;

    private InventorySlot[] inventory;

    public InventorySlot[] InventorySlots => inventory;
    public int InventorySize => inventory.Length;

    public Inventory(int size) {
        inventory = new InventorySlot[size];

        for (int i = 0; i < size; i ++) {
            inventory[i] = new InventorySlot();
        }
    }

    public bool Add(ItemData itemData, int quantity) {
        // try to add item to stack if stackable
        if (itemData.maxStackSize > 1) {
            // find items in inventory
            List<int> spaces = FindAll(itemData);

            // if any items were found
            if (spaces.Count > 0) {
 
                // loop through all spaces containing the item to add
                for (int i = 0; i < spaces.Count; i++) {

                    // if enough space add to stack
                    if (inventory[spaces[i]].isEnoughSpace(quantity)) {
                        inventory[spaces[i]].AddToStack(quantity);
                        Debug.Log($"{inventory[spaces[i]].ItemData.name} total stack is now {inventory[spaces[i]].StackSize}");
                        //OnInventoryChange?.Invoke(inventory);

                        // all added so exit
                        return true;
                    } 

                    // otherwise if not full add until stack is full
                    else if (!inventory[spaces[i]].isFull()) {
                        inventory[spaces[i]].AddToStack(inventory[spaces[i]].spaceLeft());
                        Debug.Log($"{inventory[spaces[i]].ItemData.name} total stack is now {inventory[spaces[i]].StackSize}");

                        // calculate number of items still to add
                        int leftover = quantity - inventory[spaces[i]].spaceLeft();
                        quantity = leftover;
                    }
                }
            }
        }

        // check there is space
        if (FirstFreeSpace() != -1) {

            // if adding less than/the max stack size then add to space
            if (quantity <= itemData.maxStackSize) {
                InventorySlot newItem = new InventorySlot(itemData, quantity);
                inventory[FirstFreeSpace()].MoveSlot(newItem);
                Debug.Log($"Added {itemData.name} to the inventory for the first time");
                //OnInventoryChange?.Invoke(inventory);
                return true;
            } 
            
            // otherwise add maximum possible and add again
            else {
                InventorySlot newItem = new InventorySlot(itemData, itemData.maxStackSize);
                inventory[FirstFreeSpace()].MoveSlot(newItem);
                Debug.Log($"Added {itemData.name} to the inventory for the first time");
                
                // calculate number of items still to add and add again
                int leftover = itemData.maxStackSize - quantity;
                return Add(itemData, leftover);
            }
        } else {
            Debug.Log($"Inventory is full, couldn't add {quantity} {itemData.name}");
            //OnInventoryChange?.Invoke(inventory);
            return false;
        }
    }

    private List<int> FindAll(ItemData itemData) {
        List<int> allSpaces = new List<int>();

        for (int i = 0; i < InventorySize; i++) {
            if (!inventory[i].isEmpty()) {
                if (inventory[i].ItemData.name.Equals(itemData.name)) {
                    allSpaces.Add(i);
                }
            }
        }

        return allSpaces;
    }

    private int FirstFreeSpace() {
        for (int i = 0; i < inventory.Length; i++) {
            if (inventory[i].isEmpty()) {
                return i;
            }
        }

        return -1;
    }
}
