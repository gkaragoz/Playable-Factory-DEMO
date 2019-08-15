using UnityEngine;

public class WoodDestructor : MonoBehaviour {

    [Header("Debug Only")]
    [SerializeField]
    private bool _hasDestructed = false;
    [SerializeField]
    private Transform _woodLeftTransform = null;
    [SerializeField]
    private Transform _woodRightTransform = null;
    [SerializeField]
    private CapsuleCollider _mainCollider = null;
    [SerializeField]
    private Rigidbody _rigidbody = null;
    [SerializeField]
    private BoxCollider _bladeCollider = null;

    private void Awake() {
        _bladeCollider = GameObject.FindGameObjectWithTag("Blade").GetComponent<BoxCollider>();

        _woodLeftTransform = transform.GetChild(0);
        _woodRightTransform = transform.GetChild(1);

        _mainCollider = transform.GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();

        if (_woodLeftTransform == null || _woodRightTransform == null || _mainCollider == null || _rigidbody == null) {
            Debug.LogError("Initialization failed. Check assignments of the Wood parts.");
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (_hasDestructed) {
            return;
        }

        if (other.gameObject.tag == "Blade") {
            Debug.Log("Blade");

            Destruct();

            _hasDestructed = true;
        }
    }

    private void Destruct() {
        RemoveMainRigidbody();
        RemoveMainCollider();

        AddCollidersToPieces();
        AddRigidbodiesToPieces();
    }

    private void RemoveMainCollider() {
        Destroy(_mainCollider);
    }

    private void RemoveMainRigidbody() {
        Destroy(_rigidbody);
    }

    private void AddCollidersToPieces() {
        MeshCollider colliderLeft = _woodLeftTransform.gameObject.AddComponent<MeshCollider>();
        colliderLeft.convex = true;
        Physics.IgnoreCollision(colliderLeft, _bladeCollider);

        MeshCollider colliderRight = _woodRightTransform.gameObject.AddComponent<MeshCollider>();
        colliderRight.convex = true;
        Physics.IgnoreCollision(colliderRight, _bladeCollider);
    }

    private void AddRigidbodiesToPieces() {
        Rigidbody _rbLeft = _woodLeftTransform.gameObject.AddComponent<Rigidbody>();
        Rigidbody _rbRight = _woodRightTransform.gameObject.AddComponent<Rigidbody>();

        _rbLeft.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _rbLeft.velocity = -1 * Vector3.forward;
        _rbLeft.AddForceAtPosition(Vector3.forward * Random.Range(10, 80), transform.position);
        _rbRight.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _rbRight.velocity = -1 * Vector3.forward;
        _rbRight.AddForceAtPosition(Vector3.forward * Random.Range(10, 80), transform.position);
    }

}
