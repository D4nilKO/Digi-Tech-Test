using MultimeterApp.Model;
using MultimeterApp.View;
using UnityEngine;

namespace MultimeterApp.Controller
{
    public class MultimeterController : MonoBehaviour
    {
        [SerializeField]
        private DialController dialController;

        [SerializeField]
        private MultimeterView multimeterView;

        private MultimeterModel _multimeterModel = new();

        private void Awake()
        {
            _multimeterModel.OnValueChanged += UpdateDisplay;
            dialController.OnDialPositionChanged += OnDialChanged;

            OnDialChanged(0);
        }

        private void OnDestroy()
        {
            _multimeterModel.OnValueChanged -= UpdateDisplay;
            dialController.OnDialPositionChanged -= OnDialChanged;
        }

        private void OnDialChanged(int positionIndex)
        {
            MultimeterMode mode = positionIndex switch
            {
                (int)MultimeterMode.Neutral => MultimeterMode.Neutral,
                (int)MultimeterMode.DCVoltage => MultimeterMode.DCVoltage,
                (int)MultimeterMode.Current => MultimeterMode.Current,
                (int)MultimeterMode.ACVoltage => MultimeterMode.ACVoltage,
                (int)MultimeterMode.Resistance => MultimeterMode.Resistance,
                _ => MultimeterMode.Neutral
            };

            _multimeterModel.SetMode(mode);
        }

        private void UpdateDisplay(MultimeterMode mode, float value)
        {
            multimeterView.UpdateDisplay(mode, value);
        }
    }
}