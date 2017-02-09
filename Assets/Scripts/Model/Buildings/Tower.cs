using UnityEngine;

public class Tower : Building {

    private GameObject projectilePrefab;

    public float attackSpeed = 42f;

    public float attackDamage = 1337f;

    public float range = 5f;

    private float lastAttack = 0f;

    TargetingSystem targetingSystem;

    void Start() {
        projectilePrefab = (GameObject) Resources.Load("Prefabs/BasicProjectile"); // TODO ProjectileFactory
        targetingSystem = GetComponentInChildren<TargetingSystem>();
        if (targetingSystem == null) {
            Debug.Log("NO TARGETING SYSTEM FOUND");
        }
    }

    void Update() {
        lastAttack += Time.fixedDeltaTime;
        Transform target = targetingSystem.target;
        if (target != null && lastAttack > attackSpeed) {
            FireProjectile(target);
        }
    }

    void FireProjectile(Transform target) {
        GameObject p = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        Projectile projectile = p.GetComponent<Projectile>();
        projectile.target = targetingSystem.target;
        lastAttack = 0f;
    }
}
