using System;
using UnityEngine;

/// <summary>
/// Responsible for handling lightning logic
/// </summary>
public class LightningBehaviour : MonoBehaviour {

    /// <summary>
    /// How many seconds the spell is in effect.
    /// </summary>
    private const float EffectTime = 1.0f;

    /// <summary>
    /// How large the lightning gets.
    /// </summary>
    private const float MaxScale = 5.0f;

    /// <summary>
    /// Ground target
    /// </summary>
    public Vector3 targetGround = Vector3.zero;

    private float _currentTime = 0.0f;

    /// <summary>
    /// Called when this behaviour initializes.
    /// </summary>
    private void Start() {
        this.transform.position = new Vector3(targetGround.x, targetGround.y - 0.5f, targetGround.z);
    }

    /// <summary>
    /// Called each frame.
    /// </summary>
    private void Update() {
        _currentTime += Time.deltaTime;
        var clampedTime = Math.Min(_currentTime / EffectTime, 1.0f);
        var scale = 1.0f + (MaxScale - 1.0f) * clampedTime;
        this.transform.localScale = new Vector3(scale, scale, scale);
        if (_currentTime > EffectTime) 
            Destroy(this.gameObject);
    }
}