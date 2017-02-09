using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage;

    public float speed;

    public Transform target;
    
	void Start () {

	}
	
	void Update () {
	    if(target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } else {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other) {
        if(other.transform == target) {
            Destroy(target.gameObject); // TODO change to deal damage to enemy
            Destroy(gameObject);
        }
    }
}
