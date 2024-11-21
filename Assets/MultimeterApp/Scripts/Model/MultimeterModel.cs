using System;
using UnityEngine;

namespace MultimeterApp.Model
{
    public class MultimeterModel
    {
        public MultimeterMode CurrentMode { get; private set; } = MultimeterMode.Neutral;

        private const float Resistance = 1000f;
        private const float Power = 400f;

        public event Action<MultimeterMode, float> OnValueChanged;

        public void SetMode(MultimeterMode mode)
        {
            CurrentMode = mode;
            CalculateAndNotify();
        }

        private void CalculateAndNotify()
        {
            float value = CurrentMode switch
            {
                MultimeterMode.Resistance => Resistance,
                MultimeterMode.Current => CalculateCurrent(),
                MultimeterMode.DCVoltage => CalculateVoltageDC(),
                MultimeterMode.ACVoltage => CalculateVoltageAC(),
                _ => 0f
            };

            OnValueChanged?.Invoke(CurrentMode, value);
        }

        private float CalculateCurrent()
        {
            // A = sqrt(P/R)
            return Mathf.Sqrt(Power / Resistance);
        }

        private float CalculateVoltageDC()
        {
            // V = sqrt(P*R)
            return Mathf.Sqrt(Power * Resistance);
        }

        private float CalculateVoltageAC()
        {
            // Заданное условие.
            return 0.01f;
        }
    }
}