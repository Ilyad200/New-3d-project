using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Combat
{
    class CombatEntity
    {
        public IHealthProvider HealthProvider { get; private set; }
        public EnvironmentVariableTarget a;
    }
}
