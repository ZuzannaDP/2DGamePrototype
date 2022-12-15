using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUp : MonoBehaviour
{
    public ItemData item;
    public int quanity;

    SpriteRenderer spriteRenderer;

    public static event HandlePickUp OnPickUp;
    public delegate bool HandlePickUp(ItemData itemData, int quanity);
    // TODO: maybe rework the bool return from event thing???
    // only last event handler returns

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Display correct sprite
        spriteRenderer.sprite = item.icon;
    }

    private void Reset() {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        bool success = (bool) OnPickUp?.Invoke(item, quanity);
        
        if (success) {
            Destroy(transform.gameObject);
        }
    }
}
