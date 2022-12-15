using System.Collections.Generic;
using UnityEngine;

public class Gatherer : Interacter
{

    public Gatherable gatherable;
    public bool isGathered;
    public int quantity;
    
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;

    public static event HandleGather OnGather;
    public delegate bool HandleGather(ItemData itemData, int quanity);

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();

        // Display correct sprite
        Display();

        // Update collider
        // https://answers.unity.com/questions/722748/refreshing-the-polygon-collider-2d-upon-sprite-cha.html
        polygonCollider2D.pathCount = 0;
        polygonCollider2D.pathCount = spriteRenderer.sprite.GetPhysicsShapeCount();
 
        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < polygonCollider2D.pathCount; i++) {
            path.Clear();
            spriteRenderer.sprite.GetPhysicsShape(i, path);
            polygonCollider2D.SetPath(i, path.ToArray());
        }
    }

    public override void Interact()
    {
        if (!isGathered) {
            Debug.Log("Gathered a " + gatherable.item.name);
            bool success = (bool) OnGather?.Invoke(gatherable.item, quantity);
            isGathered = success;
            Display();
        } else {
            Debug.Log("Empty " + gatherable.name);
        }
    }

    private void Display() {
        if (isGathered) {
            spriteRenderer.sprite = gatherable.emptySprite;
        } else {
            spriteRenderer.sprite = gatherable.fullSprite;
        }
    }
}
