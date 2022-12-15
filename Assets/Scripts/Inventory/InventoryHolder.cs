using UnityEngine;
using System;

public abstract class InventoryHolder : MonoBehaviour
{
    protected Inventory inventory;
    public int size;

    public Inventory HolderInventory => inventory;

    protected virtual void Awake() {
        Debug.Log("Inventory Holder Awake()");
        inventory = new Inventory(size);
    }

}