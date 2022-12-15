using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Item/Weapon")]
public class Weapon : ItemData
{
    public int damage;
    public Type type;
    public enum Type {
        Sword,
        Dagger,
        Polearm,
        Bow
    }
}