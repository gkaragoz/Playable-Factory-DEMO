using UnityEngine;

public class WoodCreator : MonoBehaviour {

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
    }

    public float GetTotalLengthOfWood() {
        return _woodLeft.GetLength() + _woodRight.GetLength();
    }

}
