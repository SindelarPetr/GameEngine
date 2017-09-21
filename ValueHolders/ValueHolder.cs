using System;
using GameEngine.Options;

namespace GameEngine.ValueHolders
{
    public class ValueHolder
    {
        public float Value = 1;
        public float ValueTolerance = 0.001f;
        public float ValueToGo = 1;

        public float Speed = 0;
        public float SpeedTolerance = 0.0001f;

        /// <summary>
        /// speed * Friction, every frame
        /// </summary>
        public float Friction = 0.8f;
        public float BackForce = 0.006f;
        public float Mass = 1;

        public ValueHolder(float basicValue)
        {
            SetDefaultValue(basicValue);
        }

        public void Update()
        {
            if (Speed != 0 || Value != ValueToGo)
            {
                ApplyForce(BackForce * (ValueToGo - Value));
                Speed *= Friction;
                Value += Speed * (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;

                if (Math.Abs(Value - ValueToGo) < ValueTolerance && Math.Abs(Speed) < SpeedTolerance)
                {
                    Value = ValueToGo;
                    Speed = 0;
                }
            }
        }

        public void ApplyForce(float force)
        {
            Speed += force / Mass;
        }

        public void SetDefaultValue(float newValue)
        {
            Value = ValueToGo = newValue;
        }
    }
}
