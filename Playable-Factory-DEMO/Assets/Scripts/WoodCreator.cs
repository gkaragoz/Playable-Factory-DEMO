using UnityEngine;

public class WoodCreator : MonoBehaviour {

    [SerializeField]
    private WoodCutArea _woodCutArea = null;
    [SerializeField]
    private Wood _woodLeft = null;
    [SerializeField]
    private Wood _woodRight = null;
    [SerializeField]
    private float _minLength = 0.2f;
    [SerializeField]
    private float _maxLength = 1f;
    [SerializeField]
    private float _minWidth = 0.33f;
    [SerializeField]
    private float _maxWidth = 1.5f;

    public void ReCreate() {
        float randomWidth = Random.Range(_minWidth, _maxWidth);

        // Set random length and width to left wood.
        _woodLeft.transform.localScale = new Vector3(
            randomWidth,
            Random.Range(_minLength, _maxLength),
            randomWidth);

        // Set random length and width to right wood.
        _woodRight.transform.localScale = new Vector3(
            randomWidth,
            Random.Range(_minLength, _maxLength),
            randomWidth);

        // Set right wood position next to the left one.
        _woodRight.transform.position = new Vector3(
            _woodLeft.transform.position.x + GetTotalLengthOfWood(), 
            _woodRight.transform.position.y, 
            _woodRight.transform.position.z);

        // ReCreate green loop ring.
        _woodCutArea.ReCreate();
    }

    public float GetWidth() {
        return _woodLeft.transform.localScale.x;
    }

    public float GetTotalLengthOfWood() {
        return _woodLeft.GetLength() + _woodRight.GetLength();
    }

    public float GetRightCornerPosXOfWood() {
        return _woodRight.transform.position.x;
    }

    public float GetLeftCornerPosXOfWood() {
        return _woodLeft.transform.position.x;
    }

    public float GetCenterPosXOfWood() {
        return (transform.position.x + GetTotalLengthOfWood()) * 0.5f;
    }

    public float GetDestructionPosX() {
        return _woodLeft.transform.position.x + _woodLeft.GetLength();
    }

}
