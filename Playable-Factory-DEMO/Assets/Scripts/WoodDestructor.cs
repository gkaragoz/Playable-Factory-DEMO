using UnityEngine;

public class WoodDestructor : MonoBehaviour, IPooledObject {

    [Header("Initializations")]
    [SerializeField]
    private Wood[] _woodPieces = null;
    [SerializeField]
    private CapsuleCollider _mainCollider = null;
    [SerializeField]
    private Rigidbody _rigidbody = null;

    [Header("Settings")]
    [SerializeField]
    private float _hideAfterSeconds = 3f;

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

            Destruct(other.transform.position.x);

            _hasDestructed = true;
        }
    }

    private void Destruct(float hitPointX) {
        DisableMainRigidbody();
        DisableMainCollider();

        foreach (Wood wood in _woodPieces) {
            wood.OnDestructed(_bladeCollider, hitPointX);
        }

        Invoke("SetPassive", _hideAfterSeconds);
    }

    private void EnableMainCollider() {
        _mainCollider.enabled = true;
    }

    private void EnableMainRigidbody() {
        _rigidbody.detectCollisions = true;
    }

    private void DisableMainCollider() {
        _mainCollider.enabled = false;
    }

    private void DisableMainRigidbody() {
        _rigidbody.detectCollisions = false;
    }

    private void SetPassive() {
        Debug.Log("SET PASSIVE");
        _rigidbody.useGravity = false;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        this.transform.position = WoodSpawner.instance.transform.position;
        this.transform.eulerAngles = Vector3.zero;

        foreach (Wood wood in _woodPieces) {
            wood.ResetStatus();
        }

        this.gameObject.SetActive(false);
    }

    public void OnObjectReused() {
        EnableMainCollider();
        EnableMainRigidbody();

        _rigidbody.useGravity = true;

        _hasDestructed = false;
    }
}
