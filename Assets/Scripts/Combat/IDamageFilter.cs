using UnityEngine;

public interface IDamageFilter
{
    bool CanTakeDamage(DamageComponent context);
}
