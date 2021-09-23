using System;
using UnityEngine;

/// <summary>
/// Responsible for handling heal logic
/// </summary>
public class HealBehaviour : MonoBehaviour {

    /// <summary>
    /// How many seconds the spell is in effect.
    /// </summary>
    private const float EffectTime = 1.0f;

    /// <summary>
    /// How large the lightning gets.
    /// </summary>
    private const float MaxScale = 10.0f;

    /// <summary>
    /// Ground target
    /// </summary>
    public Vector3 targetGround = Vector3.zero;

    /// <summary>
    /// Game Object target
    /// </summary>
    public Transform targetGameObject = null;

    private float _currentTime = 0.0f;

    /// <summary>
    /// Calculates the position of this effect.
    /// </summary>
    /// <returns>The position.</returns>
    private Vector3 CalculatePosition() {
        var pos = targetGameObject == null ? targetGround : targetGameObject.transform.position;
        return new Vector3(pos.x, pos.y - 0.5f, pos.z);
    }

    /// <summary>
    /// Called when this behaviour initializes.
    /// </summary>
    private void Start() {
        this.transform.position = CalculatePosition();
    }

    /// <summary>
    /// Called each frame.
    /// </summary>
    private void Update() {
        _currentTime += Time.deltaTime;
        var clampedTime = Math.Min(_currentTime / EffectTime, 1.0f);
        var scaleLarge = 1.0f + (MaxScale - 1.0f) * clampedTime;
        var scaleSmall = 1.0f - clampedTime;
        this.transform.localScale = new Vector3(scaleSmall, scaleLarge, scaleSmall);
        this.transform.localPosition = CalculatePosition();
        if (_currentTime > EffectTime) 
            Destroy(this.gameObject);
    }
}