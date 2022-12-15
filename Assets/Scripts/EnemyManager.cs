using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyManager : MonoBehaviour
{
    public Enemy enemy;
    private Health enemyHealth;

    public static event HandleDamage OnDamage;
    public delegate void HandleDamage(int dmg);

    private SpriteRenderer sr;

    private void Start() {
        enemyHealth = new Health(enemy.health, enemy.health);
        sr = transform.GetComponent<SpriteRenderer>();
        sr.sprite = enemy.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            OnDamage?.Invoke(enemy.attack);
        }
    }
}