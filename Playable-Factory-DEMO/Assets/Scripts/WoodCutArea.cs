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
    private float _scaleValY = 0.15f;

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

        transform.localScale = new Vector3(_woodCreator.GetScaleWidth() * 1.025f, _scaleValY, _woodCreator.GetScaleWidth() * 1.025f);
    }

    private void SetOffsetValX() {
        transform.position = new Vector3(_woodCreator.GetDestructedPointX(), transform.position.y, transform.position.z);
    }

    public void SetRing() {
        SetScale();
        SetOffsetValX();
    }

}
