using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIFailures : MonoBehaviour {

    private string _defaultText = "";
    private TextMeshProUGUI _txtFailures = null;

    private void Start() {
        _txtFailures = GetComponent<TextMeshProUGUI>();
        _defaultText = _txtFailures.text;

        GameManager.instance.OnGetFailured += OnGetFailured;
    }

    private void OnGetFailured() {
        _txtFailures.text = _defaultText + GameManager.instance.Failures;
    }
}
