using UnityEngine;

public class CameraShake : MonoBehaviour {

    #region Singleton

    public static CameraShake instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public float shakeDuration = 0f;

    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector3 _originalPos;

    private void OnEnable() {
        _originalPos = transform.localPosition;
    }

    private void Update() {
        if (shakeDuration > 0) {
            transform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDuration = 0f;
            transform.localPosition = _originalPos;
        }
    }

}