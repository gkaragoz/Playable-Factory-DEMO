using UnityEngine;

public class WoodDestructor : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private Wood[] _woodPieces = null;
    [SerializeField]
    private CapsuleCollider _mainCollider = null;
    [SerializeField]
    private Rigidbody _rigidbody = null;

    [Header("Debug Only")]
    [SerializeField]
    private bool _hasDestructed = false;
    [SerializeField]
    private BoxCollider _bladeCollider = null;

    private void Awake() {
        _bladeCollider = GameObject.FindGameObjectWithTag("Blade").GetComponent<BoxCollider>();

        if (_woodPieces == null || _mainCollider == null || _rigidbody == null) {
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

        foreach (Wood wood in _woodPieces) {
            wood.OnDestructed(_bladeCollider);
        }
    }

    private void RemoveMainCollider() {
        _mainCollider.enabled = false;
    }

    private void RemoveMainRigidbody() {
        _rigidbody.detectCollisions = false;
    }

}
