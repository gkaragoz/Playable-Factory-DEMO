using UnityEngine;

public class BladeController : MonoBehaviour {

    [Header("Movement")]
    [SerializeField]
    private KeyCode _movementKey = KeyCode.Mouse2;
    [SerializeField]
    private float _movementSpeed = 10f;

    private int _fingerId;
    private Vector3 _lastTouchPosition;

    [Header("Movement Limits")]
    [SerializeField]
    private bool _limitMovement = true;
    [SerializeField]
    private float[] _boundsX = new float[] { -10f, 5f };

    private Camera _camera;

    private GameObject _destructedObject = null;

    private Vector2 MouseAxis {
        get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
    }

    private void Start() {
        _camera = Camera.main;
    }

    private void Update() {
        if (Input.touchSupported) {
            TouchMovement();
        } else {
            MouseMovement();
        }

        LimitPosition();
    }

    private void TouchMovement() {
        switch (Input.touchCount) {
            case 1:
                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    _lastTouchPosition = touch.position;
                    _fingerId = touch.fingerId;
                } else if (touch.fingerId == _fingerId && touch.phase == TouchPhase.Moved) {
                    MoveBlade(touch.position);
                }
                break;
        }
    }

    private void MouseMovement() {
        if (Input.GetKey(_movementKey) && MouseAxis != Vector2.zero) {
            Vector3 desiredMove = new Vector3(MouseAxis.x, 0, 0);

            desiredMove *= _movementSpeed;
            desiredMove *= Time.deltaTime;
            desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
            desiredMove = transform.InverseTransformDirection(desiredMove);

            transform.Translate(desiredMove, Space.Self);
        }
    }

    private void MoveBlade(Vector3 newPosition) {
        // Determine how much to move the blade
        Vector3 offset = _camera.ScreenToViewportPoint(_lastTouchPosition - newPosition);
        Vector3 move = new Vector3(offset.x * _movementSpeed, 0, 0);

        // Perform the movement
        transform.Translate(-1 * move, Space.World);

        // Cache the position
        _lastTouchPosition = newPosition;
    }

    private void LimitPosition() {
        if (!_limitMovement)
            return;

        // Ensure the blade remains within bounds.
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, _boundsX[0], _boundsX[1]);
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Cut_Area") {
            GameManager.instance.AddScore();

            _destructedObject = other.transform.parent.gameObject;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Wood") {
            if (_destructedObject != other.gameObject) {
                GameManager.instance.AddFailure();
            }

            _destructedObject = null;
        }
    }

}
