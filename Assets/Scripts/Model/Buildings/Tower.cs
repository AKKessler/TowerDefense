using System.Collections.Generic;
using UnityEngine;

public class Tower : Building {

    public Transform currentTarget;

    public SphereCollider rangeSphere;

    // System.Collections.Queue does not allow for remove of specific object
    // TODO find a better data structure then array list...perhaps a linked list?
    public List<Collider> targetQueue; 

    public float range = 5f;

    void Start() {
        rangeSphere = GetComponent<SphereCollider>();
        rangeSphere.center = (transform.position.y * -Vector3.up); // set on ground
        rangeSphere.radius = range;
        targetQueue = new List<Collider>();
        currentTarget = null;
    }

    void Update() {
        // TODO Logic to fire projectile at current target
    }

    // TODO Implement different strategies for targeting (i.e. closest, furthest, lowest HP, groups)
    // currently targets by order that creeps entered into tower's range
    void UpdateTarget() {
        if (targetQueue.Count > 0) {
            currentTarget = targetQueue[0].transform;
            targetQueue.RemoveAt(0);
        } else {
            currentTarget = null;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
        if (currentTarget != null)
            Gizmos.DrawLine(transform.position, currentTarget.position);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Creep"))
            targetQueue.Add(other);

        if (currentTarget == null)
            UpdateTarget();
    }

    void OnTriggerExit(Collider other) {
        if (targetQueue.Contains(other)) {
            targetQueue.Remove(other);
        }

        if (other.transform == currentTarget) {
            UpdateTarget();
        }
    }
}
