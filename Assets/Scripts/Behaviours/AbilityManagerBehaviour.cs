using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for handling ability cast logic.
/// </summary>
public class AbilityManagerBehaviour : MonoBehaviour {
    
    // The cache of already loaded abilities
    // Note: You would likely want to clear these after each scene is loaded to remove abilities that will never be used again.
    private IDictionary<Ability, IAbility> _abilityCache = new Dictionary<Ability, IAbility>();

    private TargetingMode? _targeting = null;
    private Ability? _targetingAbility = null;
    private CharacterBehaviour _targetingCharacter = null;

    /// <summary>
    /// Finds a cached ability or creates it.
    /// </summary>
    /// <param name="ability">The ability to find or create</param>
    /// <returns>The ability</returns>
    private IAbility FindOrCreate(Ability ability) {
        if (_abilityCache.TryGetValue(ability, out IAbility found)) return found;
        var factory = new AbilityFactory();
        return CacheAbility(factory.Create(ability));
    }

    /// <summary>
    /// Adds the ability to the cache so it isn't created more times than is necessary.
    /// </summary>
    /// <param name="ability">The ability to cache</param>
    /// <returns>The cached ability</returns>
    private IAbility CacheAbility(IAbility ability) {
        if (_abilityCache.ContainsKey(ability.Type))
            throw new ArgumentException($"An ability of type {ability.Type} has already been cached.  Did you use the correct enum for {nameof(IAbility)}.{nameof(IAbility.Type)}?");
        _abilityCache[ability.Type] = ability;
        return ability;
    }

    /// <summary>
    /// Apply the targeting for the given position.
    /// </summary>
    /// <param name="position">The position to apply the targeting for</param>
    private void FinishTargeting(Vector3 position) {
        _targetingCharacter.Execute(_targetingAbility.Value, position);
        ResetsTargeting();
    }

    /// <summary>
    /// Apply the targeting for the given target.
    /// </summary>
    /// <param name="target">The character to apply the targeting for</param>
    private void FinishTargeting(CharacterBehaviour target) {
        _targetingCharacter.Execute(_targetingAbility.Value, target);
        ResetsTargeting();
    }

    /// <summary>
    /// Resets all variables used for targeting.
    /// </summary>
    private void ResetsTargeting() {
        Debug.Log($"Finished {_targetingAbility.Value} targeting");
        _targetingAbility = null;
        _targetingCharacter = null;
        _targeting = null;
    }

    /// <summary>
    /// Called each frame.
    /// </summary>
    private void Update() {
        if (_targeting != null) {

            // If the player clicks, see if they clicked a valid target
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit)) {
                    var objectHit = hit.transform.gameObject;
                    switch (_targeting.Value) {
                        case TargetingMode.Ground:
                            // Note: Ideally you would want to tag the valid ground surfaces and only respond to those.
                            FinishTargeting(hit.point);
                            break;
                        case TargetingMode.Single:
                            var other = objectHit.GetComponent<CharacterBehaviour>();
                            if (other != null) FinishTargeting(other);
                            // Note: Could play an error nose if the other is null here.
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Called in response to a character trying to cast an ability.
    /// </summary>
    /// <param name="sender">The object that created the event</param>
    /// <param name="args">The arguments that describe the event</param>
    private void OnAbilityInvoked(object sender, AbilityEventArguments args) {
        var ability = FindOrCreate(args.Ability);

        // Do targeting if needed.
        if (args.Target == null) {
            if (_targeting != null) 
                Debug.Log($"Cancelling {_targetingAbility.Value} targeting");
            Debug.Log($"Beginning {args.Ability} targeting");
            _targeting = ability.TargetingMode;
            _targetingAbility = args.Ability;
            _targetingCharacter = args.Invoker;
            return;
        }

        // Otherwise, just cast the ability.
        switch (ability.TargetingMode) {
            case TargetingMode.Self:
                ability.Execute(args.Invoker);
                break;
            case TargetingMode.Ground:
                ability.Execute(args.Invoker, args.Target.TargetGround);
                break;
            case TargetingMode.Single:
                ability.Execute(args.Invoker, args.Target.TargetCharacter);
                break;
            default:
                throw new ArgumentException($"Unknown targeting mode {ability.TargetingMode} encountered");
        }
    }

    /// <summary>
    /// Called when this behaviour is initialized.
    /// </summary>
    private void Awake() {
        _abilityCache.Clear();
        CharacterBehaviour.AbilityInvoked += OnAbilityInvoked;
    }

    /// <summary>
    /// Called when this behaviour is cleaned up.
    /// </summary>
    private void OnDestroy() {
        CharacterBehaviour.AbilityInvoked -= OnAbilityInvoked;
        _abilityCache.Clear();
    }
}