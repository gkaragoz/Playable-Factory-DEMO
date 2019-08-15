#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class WoodCreator : MonoBehaviour {

    public Wood leftWoodPiece = null;
    public Wood rightWoodPiece = null;
    public WoodCutArea woodCutArea = null;

    // Main Wood
    public float main_wood_total_length_minVal = 0.1f;
    public float main_wood_total_length_maxVal = 4f;

    // Left Wood
    public float left_wood_minVal = 0.1f;
    public float left_wood_maxVal = 1.8f;

    // Right Wood
    public float right_wood_minVal = 0.1f;
    public float right_wood_maxVal = 1.8f;

    private float _main_wood_total_lengthVal = 4f;

}

#if UNITY_EDITOR
[CustomEditor(typeof(WoodCreator))]
public class WoodCreatorEditor : Editor {

    string info_wood_length = "Total length:";
    string info_wood_piece_length = "Piece length:";
    string info_offset = "Offset:";

    // Wood total length props
    float wood_total_length_minLimit = 0.25f;
    float wood_total_length_maxLimit = 4f;
    
    // Left wood props
    // Scale
    float left_wood_minLimit = 0.9f;
    float left_wood_maxLimit = 3.8f;

    // Right wood props
    // Scale
    float right_wood_minLimit = 0.9f;
    float right_wood_maxLimit = 3.8f;

    public override void OnInspectorGUI() {
        GUILayout.Space(10);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        WoodCreator woodCreator = (WoodCreator)target;
        GUIStyle style = new GUIStyle() {
            fontStyle = FontStyle.Bold
        };

        GUILayout.Label("Left wood piece", style);
        woodCreator.leftWoodPiece = EditorGUILayout.ObjectField(woodCreator.leftWoodPiece, typeof(Wood), true) as Wood;
        GUILayout.Label("Right wood piece", style);
        woodCreator.rightWoodPiece = EditorGUILayout.ObjectField(woodCreator.rightWoodPiece, typeof(Wood), true) as Wood;

        // Wood total length
        GUILayout.Label("MAIN WOOD", style);
        GUILayout.Box(info_wood_length);
        EditorGUILayout.LabelField("Min Val:", woodCreator.main_wood_total_length_minVal.ToString());
        EditorGUILayout.LabelField("Max Val:", woodCreator.main_wood_total_length_maxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref woodCreator.main_wood_total_length_minVal, ref woodCreator.main_wood_total_length_maxVal, wood_total_length_minLimit, wood_total_length_maxLimit);

        // Left wood size
        GUILayout.Label("LEFT WOOD", style);
        GUILayout.Box(info_wood_piece_length);
        EditorGUILayout.LabelField("Min Val:", woodCreator.left_wood_minVal.ToString());
        EditorGUILayout.LabelField("Max Val:", woodCreator.left_wood_maxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref woodCreator.left_wood_minVal, ref woodCreator.left_wood_maxVal, left_wood_minLimit, left_wood_maxLimit);

        // Right wood size
        GUILayout.Label("RIGHT WOOD", style);
        GUILayout.Box(info_wood_piece_length);
        EditorGUILayout.LabelField("Min Val:", woodCreator.right_wood_minVal.ToString());
        EditorGUILayout.LabelField("Max Val:", woodCreator.right_wood_maxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref woodCreator.right_wood_minVal, ref woodCreator.right_wood_maxVal, right_wood_minLimit, right_wood_maxLimit);
    }
}
#endif