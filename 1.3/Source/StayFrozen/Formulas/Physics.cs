using System;

namespace StayFrozen.Formulas
{
    public static class Physics
    {
        /// <summary>
        /// A formula that calculates the temperature of an object in a consistently tempered room.
        /// </summary>
        /// <param name="ambientTemp">The temperature of the environment</param>
        /// <param name="objectTemp">The temperature of the object in the environment</param>
        /// <param name="balanceTime">After what interval the thermal should be balanced to >99%.</param>
        /// <param name="interval">How many time steps should be calculated.</param>
        /// <returns>The new heat </returns>
        public static float ThermalBalance(float ambientTemp, float objectTemp, float balanceTime, float interval = 1)
        {
            double r = balanceTime / 8f;
            double result = ambientTemp + (objectTemp - ambientTemp) * Math.Exp(-interval / r);
            return (float) result;
        }
    }
}
