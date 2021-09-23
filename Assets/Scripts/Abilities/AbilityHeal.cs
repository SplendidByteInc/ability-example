using UnityEngine;
/// <summary>
/// A single target healing ability.
/// </summary>
public class AbilityHeal : IAbility {

    /// <summary>
    /// The type of ability being executed.
    /// </summary>
    public Ability Type => Ability.Heal;

    /// <summary>
    /// How to select targets for this ability.
    /// </summary>
    public TargetingMode TargetingMode => TargetingMode.Single;

    /// <summary>
    /// Executes the ability caring only about the caster.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    public void Execute(CharacterBehaviour source) {
        Execute(source, source);
    }
    
    /// <summary>
    /// Executes the ability caring about source and target.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    /// <param name="target">The target of the ability</param>
    public void Execute(CharacterBehaviour source, Vector3 target) {
        Debug.Log($"{source.gameObject.name} has cast {Type} at {target}");
        var heal = GameObject.Instantiate(Resources.Load("Heal") as GameObject);
        heal.GetComponent<HealBehaviour>().targetGround = target;
    }

    /// <summary>
    /// Executes the ability caring about source and target.
    /// </summary>
    /// <param name="source">The caster of the ability</param>
    /// <param name="target">The target of the ability</param>
    public void Execute(CharacterBehaviour source, CharacterBehaviour target) {
        Debug.Log($"{source.gameObject.name} has cast {Type} on {target.gameObject.name}");
        var heal = GameObject.Instantiate(Resources.Load("Heal") as GameObject);
        heal.GetComponent<HealBehaviour>().targetGameObject = target.transform;
    }
}
