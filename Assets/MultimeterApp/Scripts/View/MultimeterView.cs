using System;
using System.Collections.Generic;
using System.Globalization;
using MultimeterApp.Model;
using TMPro;
using UnityEngine;

namespace MultimeterApp.View
{
    public class MultimeterView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text displayText;

        [SerializeField]
        private TMP_Text ACVoltageText;

        [SerializeField]
        private TMP_Text DCVoltageText;

        [SerializeField]
        private TMP_Text resistanceText;

        [SerializeField]
        private TMP_Text currentText;

        private void Awake()
        {
            ValidateRequiredFields(new[]
            {
                (field: displayText, fieldName: nameof(displayText)),
                (field: ACVoltageText, fieldName: nameof(ACVoltageText)),
                (field: DCVoltageText, fieldName: nameof(DCVoltageText)),
                (field: resistanceText, fieldName: nameof(resistanceText)),
                (field: currentText, fieldName: nameof(currentText))
            });
        }

        private void ValidateRequiredFields(IEnumerable<(TMP_Text field, string fieldName)> fields)
        {
            foreach (var (field, fieldName) in fields)
            {
                if (field == null)
                {
                    throw new InvalidOperationException($"Missing required field: {fieldName}");
                }
            }
        }

        public void UpdateDisplay(MultimeterMode mode, float value)
        {
            UpdateNativeDisplay(value);
            UpdateUIDisplay(mode, value);
        }

        private void UpdateNativeDisplay(float value)
        {
            string formattedValue = value.ToString("F2", CultureInfo.InvariantCulture);
            displayText.text = formattedValue;
        }

        private void UpdateUIDisplay(MultimeterMode mode, float value)
        {
            string formattedValue = value.ToString("F2", CultureInfo.InvariantCulture);
            string defaultValue = "0";

            DCVoltageText.text = defaultValue;
            currentText.text = defaultValue;
            ACVoltageText.text = defaultValue;
            resistanceText.text = defaultValue;

            switch (mode)
            {
                case MultimeterMode.Neutral:
                    break;

                case MultimeterMode.DCVoltage:
                    DCVoltageText.text = formattedValue;
                    break;

                case MultimeterMode.Current:
                    currentText.text = formattedValue;
                    break;

                case MultimeterMode.ACVoltage:
                    ACVoltageText.text = formattedValue;
                    break;

                case MultimeterMode.Resistance:
                    resistanceText.text = formattedValue;
                    break;
            }
        }
    }
}