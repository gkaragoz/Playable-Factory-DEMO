using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class WoodSpawner : MonoBehaviour {

    #region Singleton

    public static WoodSpawner instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [Header("Options")]
    [SerializeField]
    private bool _startOnAwake = true;
    [SerializeField]
    private float _spawnFrequency = 2f;

    [Header("Debug Only")]
    [SerializeField]
    private bool _isRunning = true;

    private Coroutine _spawnerCoroutine = null;

    private void Start() {
        ObjectPooler.instance.InitializePool("Wood");

        if (_startOnAwake) {
            StartSpawner();
        }
    }

    private IEnumerator ISpawner() {
        Vector3 spawnPosition = transform.position;

        while (_isRunning) {
            ObjectPooler.instance.SpawnFromPool("Wood", spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(_spawnFrequency);
        }
    }

    public void StartSpawner() {
        if (_isRunning) {
            Debug.LogWarning("Wood Spawner is already running...");
            return;
        }

        _isRunning = true;
        _spawnerCoroutine = StartCoroutine(ISpawner());
    }

    public void StopSpawner() {
        _isRunning = false;

        StopCoroutine(_spawnerCoroutine);
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(WoodSpawner))]
public class WoodSpawnerEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        WoodSpawner woodSpawner = (WoodSpawner)target;
        GUIStyle style = new GUIStyle() {
            fontStyle = FontStyle.Bold
        };

        GUILayout.Space(10);

        GUILayout.Label("Spawner Controller", style);
        if (GUILayout.Button("Start Spawner")) {
            woodSpawner.StartSpawner();
        }

        if (GUILayout.Button("Stop Spawner")) {
            woodSpawner.StopSpawner();
        }

        Repaint();
    }
}
#endif