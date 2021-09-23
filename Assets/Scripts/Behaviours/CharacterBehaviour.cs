using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A character that can be the source or target of abilities.
/// </summary>
public class CharacterBehaviour : MonoBehaviour {
    /// <summary>
    /// Called in response to any character attempting casting an ability.
    /// </summary>
    public static event EventHandler<AbilityEventArguments> AbilityInvoked;
    
    // Inspector properties
    /// <summary>
    /// The list of abilities that this character can cast.
    /// </summary>
    public List<Ability> Abilities = new List<Ability>();

    /// <summary>
    /// Does all the checks necessary to ensure an ability can be executed.
    /// </summary>
    /// <param name="ability">The ability to execute</param>
    /// <param name="useTargeting">True if targeting must be accepted from the player, false if the ability should be cast using currently available information.</param>
    /// <returns>True if the ability was cast, false otherwise</returns>
    public bool Execute(Ability ability, bool useTargeting = false) {
        if (!CanCast(ability)) return false;
        AbilityInvoked?.Invoke(this, new AbilityEventArguments(this, ability, !useTargeting)); 
        return true;
    }

    /// <summary>
    /// Does all the checks necessary to ensure an ability can be executed.
    /// </summary>
    /// <param name="ability">The ability to execute</param>
    /// <param name="target">The target of the ability</param>
    /// <returns>True if the ability was cast, false otherwise</returns>
    public bool Execute(Ability ability, CharacterBehaviour target) {
        // Can't use an ability we don't have
        if (!Abilities.Contains(ability)) return false;
        AbilityInvoked?.Invoke(this, new AbilityEventArguments(this, ability, target));
        return true;
    }

    /// <summary>
    /// Does all the checks necessary to ensure an ability can be executed.
    /// </summary>
    /// <param name="ability">The ability to execute</param>
    /// <param name="target">The target of the ability</param>
    /// <returns>True if the ability was cast, false otherwise</returns>
    public bool Execute(Ability ability, Vector3 target) {
        if (!Abilities.Contains(ability)) return false;
        AbilityInvoked?.Invoke(this, new AbilityEventArguments(this, ability, target));
        return true;
    }

    /// <summary>
    /// Checks to see if the specified <paramref name="ability" /> can be cast.
    /// </summary>
    /// <param name="ability">The ability to check</param>
    /// <returns>True if the ability can be cast, false otherwise</returns>
    private bool CanCast(Ability ability) {
        // Can't use an ability we don't have
        if (!Abilities.Contains(ability)) return false;

        // TODO: Do mana checks and etc here and return false if the ability can't be cast.

        return true;
    }
}