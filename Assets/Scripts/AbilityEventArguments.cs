using UnityEngine;
/// <summary>
/// Describes an event where a character attempted to cast an ability.
/// </summary>
public class AbilityEventArguments {

    /// <summary>
    /// The character that invoked the ability.
    /// </summary>
    public CharacterBehaviour Invoker { get; private set; }

    /// <summary>
    /// The ability that was invoked.
    /// </summary>
    public Ability Ability { get; private set; }

    /// <summary>
    /// The target of this ability.
    /// </summary>
    public Targeting Target { get; private set; }

    /// <summary>
    /// Constructs the read-only properties and indicates that no target has been set.
    /// </summary>
    /// <param name="invoker">The character that invoked the spell</param>
    /// <param name="ability">The ability being invoked</param>
    /// <param name="targetSelf">Indicates if the invoker is the target</param>
    public AbilityEventArguments(CharacterBehaviour invoker, Ability ability, bool targetSelf = false) {
        Invoker = invoker;
        Ability = ability;
        Target = targetSelf ? new Targeting { TargetCharacter = invoker } : null;
    }

    /// <summary>
    /// Constructs the read-only properties and indicates that no target has been set.
    /// </summary>
    /// <param name="invoker">The character that invoked the spell</param>
    /// <param name="ability">The ability being invoked</param>
    /// <param name="target">The ground target for the ability</param>
    public AbilityEventArguments(CharacterBehaviour invoker, Ability ability, Vector3 target) {
        Invoker = invoker;
        Ability = ability;
        Target = new Targeting { TargetGround = target };
    }

    /// <summary>
    /// Constructs the read-only properties and indicates that no target has been set.
    /// </summary>
    /// <param name="invoker">The character that invoked the spell</param>
    /// <param name="ability">The ability being invoked</param>
    /// <param name="targetSelf">The character being targeted for the ability</param>
    public AbilityEventArguments(CharacterBehaviour invoker, Ability ability, CharacterBehaviour target) {
        Invoker = invoker;
        Ability = ability;
        Target = new Targeting { TargetCharacter = target };
    }

    /// <summary>
    /// Contains targeting information.
    /// </summary>
    public class Targeting {

        /// <summary>
        /// The ground target if applicable.
        /// </summary>
        public Vector3 TargetGround { get; internal set; } = Vector3.zero;

        /// <summary>
        /// The character target if applicable.
        /// </summary>
        public CharacterBehaviour TargetCharacter { get; internal set; } = null;
    }
}