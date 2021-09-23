using System;
using UnityEngine;

/// <summary>
/// Responsible for handling fireball logic
/// </summary>
public class FireballBehaviour : MonoBehaviour {

    /// <summary>
    /// How many seconds to travel to target.
    /// </summary>
    private const float TravelTime = 0.35f;

    /// <summary>
    /// Ground target
    /// </summary>
    public Vector3 targetGround = Vector3.zero;

    /// <summary>
    /// Game Object target
    /// </summary>
    public Transform targetGameObject = null;

    private float _currentTime = 0.0f;
    private Vector3 _startPosition;

    /// <summary>
    /// Called when this behaviour initializes;
    /// </summary>
    private void Start() {
        _startPosition = this.transform.position;
    }

    /// <summary>
    /// Called each frame.
    /// </summary>
    private void Update() {
        _currentTime += Time.deltaTime;
        var clampedTime = Math.Min(_currentTime / TravelTime, 1.0f);
        if (targetGameObject == null)
            this.transform.position = Vector3.Lerp(_startPosition, targetGround, clampedTime);
        else 
            this.transform.position = Vector3.Lerp(_startPosition, targetGameObject.position, clampedTime);
        if (_currentTime > TravelTime) 
            Destroy(this.gameObject);
    }
}