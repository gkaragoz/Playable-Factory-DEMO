using UnityEngine;

public class Wood : MonoBehaviour {

    [SerializeField]
    private Side _side;

    private MeshCollider _collider = null;
    private Rigidbody _rigidbody = null;

    public enum Side {
        Right,
        Left
    }

    private void AddCollider() {
        _collider = this.gameObject.AddComponent<MeshCollider>();
        _collider.convex = true;
    }

    private void IgnoreCollision(BoxCollider other) {
        Physics.IgnoreCollision(_collider, other);
    }

    private void AddRigidbody() {
        _rigidbody = this.gameObject.AddComponent<Rigidbody>();
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void OnDestructed(BoxCollider ignoredCollision) {
        AddCollider();
        IgnoreCollision(ignoredCollision);
        AddRigidbody();

        _rigidbody.velocity = Vector3.forward;
        _rigidbody.AddForceAtPosition(Vector3.forward * Random.Range(10, 80), transform.position);
    }

}
