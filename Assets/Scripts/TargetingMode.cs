/// <summary>
/// Identifies how to select an ability's target.
/// </summary>
public enum TargetingMode {
    /// <summary>
    /// The user must select a valid character.
    /// </summary>
    Single,
    /// <summary>
    /// The user must select a position on the ground.
    /// </summary>
    Ground,
    /// <summary>
    /// The user does not select a target, they are the source of the ability.
    /// </summary>
    Self
}