using System;

/// <summary>
/// Responsible for creating instances of <see cref="IAbility" />.
/// </summary>
public class AbilityFactory {

    /// <summary>
    /// Create an ability identified by the specified <paramref name="ability" />.
    /// </summary>
    /// <param name="ability">The ability to create</param>
    /// <returns>The ability</returns>
    public IAbility Create(Ability ability) {
        switch (ability) {
            case Ability.Fireball:
                return new AbilityFireball();
            case Ability.Lightning:
                return new AbilityLightning();
            case Ability.Heal:
                return new AbilityHeal();
            default:
                throw new ArgumentException($"Unknown ability {ability} encountered", nameof(ability));
        }
    }
}