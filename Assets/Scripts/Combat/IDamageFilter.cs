using UnityEngine;

public interface IDamageFilter
{
    bool CanTakeDamage(DamageContext context);
}
