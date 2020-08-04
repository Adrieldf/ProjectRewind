using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public sealed class PositionState
    {
        public Vector3 Position { get; }
        public float BatteryLeft { get; }

        public PositionState(Vector3 position, float batteryLevel)
        {
            Position = position;
            BatteryLeft = batteryLevel;
        }

        public static PositionState CreateDefault()
            => new PositionState(Vector3.zero, 0);
    }
}