using UnityEngine;

[CreateAssetMenu(fileName = "New Gatherable", menuName = "Gatherable")]
public class Gatherable : ScriptableObject
{
    public new string name;
    
    public Sprite fullSprite;
    public Sprite emptySprite;

    public ItemData item;
}
