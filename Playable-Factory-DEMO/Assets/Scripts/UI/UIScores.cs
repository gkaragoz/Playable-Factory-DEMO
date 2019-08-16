using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIScores : MonoBehaviour {

    private string _defaultText = "";
    private TextMeshProUGUI _txtScores = null;

    private void Start() {
        _txtScores = GetComponent<TextMeshProUGUI>();
        _defaultText = _txtScores.text;

        GameManager.instance.OnGetScored += OnGetScored;
    }

    private void OnGetScored() {
        _txtScores.text = _defaultText + GameManager.instance.Scores;
    }
}
