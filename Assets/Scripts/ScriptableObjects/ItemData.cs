using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public new string name;
    public string description;
    public int maxStackSize;
    public int value;

    protected virtual void OnUse() {
        Debug.Log($"{name} used");
    }
}
