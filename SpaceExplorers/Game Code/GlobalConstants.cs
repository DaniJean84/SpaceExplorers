using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceExplorers.Game_Code.Constants
{
    static class GlobalConstants
    {
        public const float PI = 3.14159265f;     

        public const double ANGLE_TO_RADIAN = 0.0174533f;

        public const double RADIAN_TO_ANGLE = 57.2958;

        public enum StarType { O, B, A, F, G, K, M, Unknown };

        public enum GalaxySize { GALACTIC, HUGE, LARGE, MEDIUM, SMALL, XSMALL}

        public enum GalaxyType { GLOBULAR, SPIRAL, OVAL, RING, DISK }

        public enum PlanetoidType { TERRAN, FROZEN, ROCKY, GAS_GIANT, TOXIC, VOLCANIC, WATER_WORLD }
    }
}
