using UnityEngine;
using System.Collections.Generic;

public class TargetingSystem : MonoBehaviour {
    
    public Transform target;

    // System.Collections.Queue does not allow for remove of specific object
    // TODO find a better data structure then array list...perhaps a linked list?
    private List<Collider> targetQueue;

    private Tower tower;

    // Use this for initialization
    void Start () {
        tower = GetComponentInParent<Tower>();

        SphereCollider rangeSphere = GetComponent<SphereCollider>();
        rangeSphere.center = (transform.parent.transform.position.y * -Vector3.up); // set on ground
        rangeSphere.radius = tower.range;

        targetQueue = new List<Collider>();
        target = null;
	}

    // TODO Implement different strategies for targeting (i.e. closest, furthest, lowest HP, groups)
    // currently targets by order that creeps entered into tower's range
    void UpdateTarget() {
        if (targetQueue.Count > 0) {
            target = targetQueue[0].transform;
            targetQueue.RemoveAt(0);
        } else {
            target = null;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Creep"))
            targetQueue.Add(other);

        if (target == null)
            UpdateTarget();
    }

    void OnTriggerExit(Collider other) {
        if (targetQueue.Contains(other)) {
            targetQueue.Remove(other);
        }

        if (other.transform == target) {
            UpdateTarget();
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, tower.range);
        if (target != null) Gizmos.DrawLine(transform.position, target.position);
    }
}
