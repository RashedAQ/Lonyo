
using UnityEngine;

public class DamageableTable : MonoBehaviour
{
    public float health = 1000f;
    private Collider tableCollider;

    void Start()
    {
        gameObject.name = "WoodenTable"; 
        tableCollider = GetComponent<Collider>();
        Debug.Log($"{name} spawned with health: {health}");
    }

    public void ChangeHealth(float damage)
    {
        if (this == null || !enabled) return; // Fail-safe

        health -= Mathf.Abs(damage);
        Debug.Log($"{name} took {damage} damage. Health: {health}");

        if (health <= 0)
        {
            tableCollider.enabled = false; 
            Debug.Log($"{name} destroyed properly");
            Destroy(gameObject, 0.1f); 
        }
    }
    void OnDrawGizmos()
    {
        if (tableCollider == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}