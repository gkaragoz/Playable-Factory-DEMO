#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class WoodCreator : MonoBehaviour, IPooledObject {

    public Wood leftWoodPiece = null;
    public Wood rightWoodPiece = null;
    public WoodCutArea woodCutArea = null;
    public CapsuleCollider mainCollider = null;
    public Renderer platformRenderer = null;

    // Main Wood
    public float main_wood_total_length_minVal = 0.1f;
    public float main_wood_total_length_maxVal = 4f;

    public float main_wood_scale_minWidth = 0.30f;
    public float main_wood_scale_maxWidth = 0.5f;

    // Left Wood
    public float left_wood_minVal = 0.1f;
    public float left_wood_maxVal = 1f;

    // Right Wood
    public float right_wood_minVal = 0.1f;
    public float right_wood_maxVal = 1f;

    private float _main_wood_total_lengthVal = 4f;
    private float _main_wood_scaleVal = 0.3f;

    public float GetMainWoodTotalLengthVal() {
        return _main_wood_total_lengthVal;
    }

    public float GetDestructedPointX() {
        return rightWoodPiece.transform.position.x + rightWoodPiece.GetLength();
    }

    public float GetScaleWidth() {
        return _main_wood_scaleVal;
    }

    public void Create() {
        InitWoods();

        woodCutArea.SetRing();
    }

    public void InitWoods() {
        float leftWoodScaleValY = 1f;
        float rightWoodScaleValY = 1f;

        do {
            leftWoodScaleValY = Random.Range(left_wood_minVal, left_wood_maxVal);
            rightWoodScaleValY = Random.Range(right_wood_minVal, right_wood_maxVal);

            _main_wood_scaleVal = Random.Range(main_wood_scale_minWidth, main_wood_scale_maxWidth);

            leftWoodPiece.SetScale(leftWoodScaleValY, _main_wood_scaleVal);
            rightWoodPiece.SetScale(rightWoodScaleValY, _main_wood_scaleVal);

            // Snap left wood piece to right wood piece.
            leftWoodPiece.transform.position = new Vector3(rightWoodPiece.transform.position.x + rightWoodPiece.GetLength() + leftWoodPiece.GetLength(), leftWoodPiece.transform.position.y, leftWoodPiece.transform.position.z);

            SetMainWoodPosition();
        } while (leftWoodScaleValY + rightWoodScaleValY > _main_wood_total_lengthVal);

        SetMainWoodCollider();
    }

    public void OnObjectReused() {
        Create();
    }

    private void SetMainWoodPosition() {
        if (platformRenderer == null) {
            platformRenderer = GameObject.FindGameObjectWithTag("Platform").GetComponent<Renderer>();
        }

        transform.position = new Vector3((platformRenderer.bounds.center.x) - ((rightWoodPiece.GetLength() + leftWoodPiece.GetLength()) * 0.5f), transform.position.y, transform.position.z);
    }

    private void SetMainWoodCollider() {
        float collisionHeight = leftWoodPiece.transform.position.x - rightWoodPiece.transform.position.x;

        mainCollider.height = collisionHeight;
        mainCollider.radius = _main_wood_scaleVal * 0.5f;

        float centerXOfWood = (leftWoodPiece.GetLength() + rightWoodPiece.GetLength()) * 0.5f;
        mainCollider.center = new Vector3(centerXOfWood, 0f, 0f);
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(WoodCreator))]
public class WoodCreatorEditor : Editor {

    string info_wood_length = "Total length:";
    string info_wood_piece_length = "Piece length:";
    string info_wood_scale = "Wood scale:";

    // Wood total length props
    float wood_total_length_minLimit = 0.25f;
    float wood_total_length_maxLimit = 4f;

    float wood_scale_width_minLimit = 0.30f;
    float wood_scale_width_maxLimit = 0.5f;

    // Left wood props
    // Scale
    float left_wood_minLimit = 0.1f;
    float left_wood_maxLimit = 2f;

    // Right wood props
    // Scale
    float right_wood_minLimit = 0.1f;
    float right_wood_maxLimit = 2f;

    public override void OnInspectorGUI() {
        GUILayout.Space(10);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        WoodCreator woodCreator = (WoodCreator)target;
        GUIStyle style = new GUIStyle() {
            fontStyle = FontStyle.Bold
        };

        GUILayout.Label("Main collider", style);
        woodCreator.mainCollider = EditorGUILayout.ObjectField(woodCreator.mainCollider, typeof(CapsuleCollider), true) as CapsuleCollider;
        GUILayout.Label("Left wood piece", style);
        woodCreator.leftWoodPiece = EditorGUILayout.ObjectField(woodCreator.leftWoodPiece, typeof(Wood), true) as Wood;
        GUILayout.Label("Right wood piece", style);
        woodCreator.rightWoodPiece = EditorGUILayout.ObjectField(woodCreator.rightWoodPiece, typeof(Wood), true) as Wood;
        GUILayout.Label("Wood cut area", style);
        woodCreator.woodCutArea = EditorGUILayout.ObjectField(woodCreator.woodCutArea, typeof(WoodCutArea), true) as WoodCutArea;

        // Wood total length
        GUILayout.Label("MAIN WOOD", style);
        GUILayout.Box(info_wood_length);
        EditorGUILayout.LabelField("Min Val:", woodCreator.main_wood_total_length_minVal.ToString());
        EditorGUILayout.LabelField("Max Val:", woodCreator.main_wood_total_length_maxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref woodCreator.main_wood_total_length_minVal, ref woodCreator.main_wood_total_length_maxVal, wood_total_length_minLimit, wood_total_length_maxLimit);

        // Wood total scale
        GUILayout.Box(info_wood_scale);
        EditorGUILayout.LabelField("Min Val:", woodCreator.main_wood_scale_minWidth.ToString());
        EditorGUILayout.LabelField("Max Val:", woodCreator.main_wood_scale_maxWidth.ToString());
        EditorGUILayout.MinMaxSlider(ref woodCreator.main_wood_scale_minWidth, ref woodCreator.main_wood_scale_maxWidth, wood_scale_width_minLimit, wood_scale_width_maxLimit);

        // Left wood length
        GUILayout.Label("LEFT WOOD", style);
        GUILayout.Box(info_wood_piece_length);
        EditorGUILayout.LabelField("Min Val:", woodCreator.left_wood_minVal.ToString());
        EditorGUILayout.LabelField("Max Val:", woodCreator.left_wood_maxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref woodCreator.left_wood_minVal, ref woodCreator.left_wood_maxVal, left_wood_minLimit, left_wood_maxLimit);

        // Right wood length
        GUILayout.Label("RIGHT WOOD", style);
        GUILayout.Box(info_wood_piece_length);
        EditorGUILayout.LabelField("Min Val:", woodCreator.right_wood_minVal.ToString());
        EditorGUILayout.LabelField("Max Val:", woodCreator.right_wood_maxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref woodCreator.right_wood_minVal, ref woodCreator.right_wood_maxVal, right_wood_minLimit, right_wood_maxLimit);

        // Create wood
        if (GUILayout.Button("Create Wood")) {
            woodCreator.Create();
        }
    }
}
#endif