using UnityEngine;
using System;

public class PlayerInventory : InventoryHolder
{
    public GameObject toolbar;

    protected override void Awake() {
        base.Awake();
        toolbar.SetActive(true);
    }

    private void OnEnable() {
        Gatherer.OnGather += inventory.Add;
        PickUp.OnPickUp += inventory.Add;
    }

    private void OnDisable() {
        Gatherer.OnGather -= inventory.Add;
        PickUp.OnPickUp -= inventory.Add;
    }
}