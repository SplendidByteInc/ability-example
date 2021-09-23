using UnityEngine;
/// <summary>
/// Describes an object used to execute an ability.
/// </summary>
public interface IAbility {
    /// <summary>
    /// The type of ability being executed.
    /// </summary>
    Ability Type { get; }

    /// <summary>
    /// How to select targets for this ability.
    /// </summary>
    TargetingMode TargetingMode { get; }

    /// <summary>
    /// Executes the ability caring only about the caster.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    void Execute(CharacterBehaviour source);

    /// <summary>
    /// Executes the ability caring about source and target.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    /// <param name="target">The target of the ability</param>
    void Execute(CharacterBehaviour source, Vector3 target);

    /// <summary>
    /// Executes the ability caring about source and target.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    /// <param name="target">The target of the ability</param>
    void Execute(CharacterBehaviour source, CharacterBehaviour target);
}
