using UnityEngine;

[ExecuteInEditMode]
public class WoodCutArea : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private WoodCreator _woodCreator = null;

    [Header("Settings")]
    [SerializeField]
    private float _scaleMinLimitY = 0.15f;
    [SerializeField]
    private float _scaleMaxLimitY = 0.3f;
    [SerializeField]
    private float _scaleMinLimitWidth = 0.3f;
    [SerializeField]
    private float _scaleMaxLimitWidth = 1f;

    [SerializeField]
    private float _scaleValY = 0.15f;
    [SerializeField]
    private float _scaleValWidth = 0.5f;

    public float GetScaleValWidth() {
        return _scaleValWidth;
    }

    private void SetScale() {
        do {
            _scaleValY = Random.Range(_scaleMinLimitY, _scaleMaxLimitY);

            if (_scaleValY < _scaleMinLimitY) {
                _scaleValY = _scaleMinLimitY;
            }
            if (_scaleValY >= _scaleMaxLimitY) {
                _scaleValY = _scaleMaxLimitY;
            }
        } while (_scaleValY >= _woodCreator.GetMainWoodTotalLengthVal() * 0.5f);

        _scaleValWidth = Random.Range(_scaleMinLimitWidth, _scaleMaxLimitWidth);
        transform.localScale = new Vector3(_scaleValWidth, _scaleValY, _scaleValWidth);
    }

    private void SetOffsetValX() {
        transform.position = new Vector3(_woodCreator.GetDestructedPointX(), transform.position.y, transform.position.z);
    }

    public void SetRing() {
        SetScale();
        SetOffsetValX();
    }

}
