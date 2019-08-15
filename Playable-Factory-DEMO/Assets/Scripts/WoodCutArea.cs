using UnityEngine;

public class WoodCutArea : MonoBehaviour {

    [SerializeField]
    private WoodCreator _woodCreator;

    [SerializeField]
    private float _extraMultiplier = 1.025f;
    [SerializeField]
    private float _minLength = 0.15f;
    [SerializeField]
    private float _maxLength = 0.5f;

    public bool IsCreationValid() {
        if (GetRightCornerPosXOfTheRing() > _woodCreator.GetRightCornerPosXOfWood() || GetLeftCornerPosXOfTheRing() < _woodCreator.GetLeftCornerPosXOfWood()) {
            return false;
        } else {
            return true;
        }
    }

    public void ReCreate() {
        IsCreationValid();
        do {
            ReScale();
            RePosition();
        } while (!IsCreationValid());
    }

    public void RePosition() {
        float randomPosition = Random.Range(_woodCreator.GetLeftCornerPosXOfWood(), _woodCreator.GetRightCornerPosXOfWood()) - transform.localScale.y;

        transform.position = new Vector3(randomPosition, transform.position.y, transform.position.z);
    }

    public void ReScale() {
        float randomLength = Random.Range(_minLength, _maxLength);

        // Set random length and width to left wood.
        transform.localScale = new Vector3(
            _woodCreator.GetWidth() * _extraMultiplier,
            Random.Range(_minLength, _maxLength),
            _woodCreator.GetWidth() * _extraMultiplier);
    }

    public float GetRightCornerPosXOfTheRing() {
        return transform.position.x + (transform.localScale.y * 2f);
    }

    public float GetLeftCornerPosXOfTheRing() {
        return transform.position.x - (transform.localScale.y * 2f);
    }

}
