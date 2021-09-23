using UnityEngine;
/// <summary>
/// A single target fire element ability.
/// </summary>
public class AbilityFireball : IAbility {

    /// <summary>
    /// The type of ability being executed.
    /// </summary>
    public Ability Type => Ability.Fireball;

    /// <summary>
    /// How to select targets for this ability.
    /// </summary>
    public TargetingMode TargetingMode => TargetingMode.Single;

    /// <summary>
    /// Executes the ability caring only about the caster.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    public void Execute(CharacterBehaviour source) {
        // Note: Ideally, you would want to handle this rather than throw an exception.  Consider using nearest target or playing some kind of error noise instead.
        throw new System.NotImplementedException($"Executing the {Type} ability without a target is not supported by {nameof(TargetingMode)} {TargetingMode}");
    }

    /// <summary>
    /// Executes the ability caring about source and target.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    /// <param name="target">The target of the ability</param>
    public void Execute(CharacterBehaviour source, Vector3 target) {
        Debug.Log($"{source.gameObject.name} has cast {Type} at {target}");
        var fireball = GameObject.Instantiate(Resources.Load("Fireball") as GameObject);
        fireball.transform.position = source.transform.position;
        fireball.GetComponent<FireballBehaviour>().targetGround = target;
    }

    /// <summary>
    /// Executes the ability caring about source and target.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    /// <param name="target">The target of the ability</param>
    public void Execute(CharacterBehaviour source, CharacterBehaviour target) {
        Debug.Log($"{source.gameObject.name} has cast {Type} at {target.name}");
        var fireball = GameObject.Instantiate(Resources.Load("Fireball") as GameObject);
        fireball.transform.position = source.transform.position;
        fireball.GetComponent<FireballBehaviour>().targetGameObject = target.gameObject.transform;
    }
}
