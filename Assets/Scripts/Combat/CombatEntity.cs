using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class CombatEntity : MonoBehaviour, ICombatSource, IDamageable, IHealable
    {
        public IHealthProvider HealthProvider { get; private set; }
        public Faction Faction { get; private set; }
        public IHaveStats Stats { get; private set; }
        public CombatProfile Profile { get; private set; }

        private void Awake()
        {
            HealthProvider = GetComponent<IHealthProvider>();
            Stats = GetComponent<IHaveStats>();
        }
        public void TakeDamage(DamageContext context)
        {
            float finalDamage = 0f;
            foreach (var component in context.Components)
            {
                finalDamage += component.Amount;
            }
            HealthProvider.Reduce(finalDamage);
        }

        public void Heal(HealContext context)
        {

        }
    }
}
