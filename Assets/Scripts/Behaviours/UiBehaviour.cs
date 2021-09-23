using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Responsible for handling ability cast logic.
/// </summary>
public class UiBehaviour : MonoBehaviour {
    
    /// <summary>
    /// The character this UI reflects.
    /// </summary>
    public CharacterBehaviour owner;

    /// <summary>
    /// The prefab that is used when creating abilities.
    /// </summary>
    public GameObject buttonAbilityPrefab;

    private IDictionary<Ability, GameObject> _abilityToButton = new Dictionary<Ability, GameObject>();

    /// <summary>
    /// Creates a UI button.
    /// </summary>
    /// <param name="ability">The ability to create the button for</param>
    /// <returns>The new button</returns>
    private GameObject CreateButton(Ability ability) {
        var gameObject = Instantiate(buttonAbilityPrefab, buttonAbilityPrefab.transform.position, buttonAbilityPrefab.transform.rotation);
        var text = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = ability.ToString();
        var transform = gameObject.GetComponent<RectTransform>();
        transform.position = new Vector3(0, -35 * (int)ability, 0);
        transform.SetParent(this.transform, false);
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => OnClickButton(ability));
        return gameObject;
    }

    /// <summary>
    /// Called when the UI is initialized.
    /// </summary>
    private void Awake() {
        if (buttonAbilityPrefab == null) throw new InvalidOperationException($"Can not initialize the UI without first setting {nameof(buttonAbilityPrefab)}");
    }

    /// <summary>
    /// Called each frame.
    /// </summary>
    private void Update() {
        // Note: I'm doing this in update but you could use more events on the character for AbilityGained or AbilityLost to prevent having to do this each frame.

        // Remove all buttons if there is no owner
        if (owner == null) {
            if (_abilityToButton.Count > 0) {
                foreach (var obj in _abilityToButton.Values) {
                    obj.GetComponent<Button>().onClick.RemoveAllListeners();
                    Destroy(obj);
                }
                _abilityToButton.Clear();
            }
        }
        else {
            // Remove buttons if needed
            foreach (var ability in _abilityToButton.Keys.ToArray()) { // Note: ToArray prevents concurrent modification
                if (!owner.Abilities.Contains(ability)) {
                    var obj = _abilityToButton[ability];
                    obj.GetComponent<Button>().onClick.RemoveAllListeners();
                    Destroy(obj);
                    _abilityToButton.Remove(ability);
                }
            }
            // Add buttons if needed
            foreach (var ability in owner.Abilities) {
                if (!_abilityToButton.ContainsKey(ability))
                    _abilityToButton[ability] = CreateButton(ability);
            }
        }
    }

    /// <summary>
    /// Responds to the user clicking an ability button.
    /// </summary>
    /// <param name="ability">The ability clicked</param>
    private void OnClickButton(Ability ability) {
        if (owner != null)
            owner.Execute(ability, true);
    }
}