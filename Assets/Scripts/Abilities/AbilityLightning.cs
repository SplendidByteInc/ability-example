using UnityEngine;
/// <summary>
/// A area of effect lightning element ability.
/// </summary>
public class AbilityLightning : IAbility {

    /// <summary>
    /// The type of ability being executed.
    /// </summary>
    public Ability Type => Ability.Lightning;

    /// <summary>
    /// How to select targets for this ability.
    /// </summary>
    public TargetingMode TargetingMode => TargetingMode.Ground;

    /// <summary>
    /// Executes the ability caring only about the caster.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    public void Execute(CharacterBehaviour source) {
        Execute(source, source.gameObject.transform.position);
    }

    /// <summary>
    /// Executes the ability caring about source and target.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    /// <param name="target">The target of the ability</param>
    public void Execute(CharacterBehaviour source, Vector3 target) {
        Debug.Log($"{source.gameObject.name} has cast {Type} at {target}");
        var lightning = GameObject.Instantiate(Resources.Load("Lightning") as GameObject);
        lightning.GetComponent<LightningBehaviour>().targetGround = target;
    }

    /// <summary>
    /// Executes the ability caring about source and target.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    /// <param name="target">The target of the ability</param>
    public void Execute(CharacterBehaviour source, CharacterBehaviour target) {
        Execute(source, target.gameObject.transform.position);
    }
}
