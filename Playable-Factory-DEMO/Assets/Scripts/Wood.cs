﻿using UnityEngine;

public class Wood : MonoBehaviour {

    [SerializeField]
    private Side _side = Side.Left;

    private MeshCollider _collider = null;
    private Rigidbody _rigidbody = null;

    public Side GetSide() {
        return _side;
    }

    public enum Side {
        Right,
        Left
    }

    private void AddCollider() {
        _collider = this.gameObject.AddComponent<MeshCollider>();
        _collider.convex = true;
    }

    private void IgnoreCollision(BoxCollider other) {
        Physics.IgnoreCollision(_collider, other);
    }

    private void AddRigidbody() {
        _rigidbody = this.gameObject.AddComponent<Rigidbody>();
    }

    public void SetScaleY(float scaleY) {
        this.transform.localScale = new Vector3(this.transform.localScale.x, scaleY, this.transform.localScale.z);
    }

    public void ResetStatus() {
        Destroy(this._rigidbody);
        Destroy(this._collider);

        switch (_side) {
            case Side.Right:
                this.transform.localPosition = new Vector3(4, 0, 0);
                break;
            case Side.Left:
                this.transform.localPosition = new Vector3(0, 0, 0);
                break;
        }

        this.transform.localEulerAngles = new Vector3(0, 0, 90);
        this.transform.localScale = Vector3.one;
    }

    public void OnDestructed(BoxCollider ignoredCollision, float hitPointX) {

        AddCollider();
        IgnoreCollision(ignoredCollision);
        AddRigidbody();

        _rigidbody.velocity = Vector3.back;
        _rigidbody.AddForceAtPosition(Vector3.back * Random.Range(10, 80), transform.position);
    }

    public float GetLength() {
        return this.transform.localScale.y * 2f;
    }

}
