using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    class CombatEntity : MonoBehaviour, ICombatSource
    {
        public IHealthProvider HealthProvider { get; private set; }
        public Faction Faction { get; private set; }
        public IHaveStats Stats { get; private set; }

        private void Awake()
        {
            HealthProvider = GetComponent<IHealthProvider>();
            Stats = GetComponent<IHaveStats>();
        }
        public void TakeDamage(DamageContext context)
        {

        }
    }
}
