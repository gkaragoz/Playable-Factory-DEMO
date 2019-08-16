using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton

    public static GameManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public Action OnGetScored;
    public Action OnGetFailured;

    public int Scores { get; set; }
    public int Failures { get; set; }

    public void AddScore() {
        Scores++;

        OnGetScored?.Invoke();
    }

    public void AddFailure() {
        Failures++;

        OnGetFailured?.Invoke();
    }
    
}
