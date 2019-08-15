using UnityEngine;

[ExecuteInEditMode]
public class WoodCutArea : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private WoodCreator _woodCreator = null;
    [SerializeField]
    private float _scaleValY = 0.15f;
    [SerializeField]
    private float _scaleMinLimitY = 0.15f;
    [SerializeField]
    private float _scaleMaxLimitY = 0.3f;

    [SerializeField]
    private float _offsetValX = 0;
    [SerializeField]
    private float _offsetMinLimitX = 0;

    private void Update() {
        transform.localScale = new Vector3(transform.localScale.x, _scaleValY, transform.localScale.z);

        if (_woodCreator == null) {
            return;
        }

        SetScaleVal();
        SetOffsetValX();
    }

    private void SetScaleVal() {
        if (_scaleValY < _scaleMinLimitY) {
            _scaleValY = _scaleMinLimitY;
        }
        if (_scaleValY >= _scaleMaxLimitY) {
            _scaleValY = _scaleMaxLimitY;
        }
    }

    private void SetOffsetValX() {
        transform.position = new Vector3(_offsetValX, transform.position.y, transform.position.z);

        if (_offsetValX >= _woodCreator.main_wood_total_length_maxVal - (_scaleValY * 0.5f)) {
            _offsetValX = _woodCreator.main_wood_total_length_maxVal - (_scaleValY * 0.5f);
        }
        if (_offsetValX < _offsetMinLimitX + (_scaleValY * 0.5f)) {
            _offsetValX = _offsetMinLimitX + (_scaleValY * 0.5f);
        }
    }

}
