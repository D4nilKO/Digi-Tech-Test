using UnityEngine;
using MultimeterApp.Model;
using MultimeterApp.View;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace MultimeterApp.Controller
{
    public class DialController : MonoBehaviour
    {
        [SerializeField]
        private DialView dialView;

        [SerializeField, Header("Позиции регулятора в градусах")]
        private List<float> positions;

        [SerializeField, Header("Количество точек, для автоматического создания")]
        private int pointsCount;

        [SerializeField]
        private bool isRotationInverted;

        private DialModel _dialModel;
        private Camera _mainCamera;

        private bool _isDialActive;

        public event Action<int> OnDialPositionChanged;

        private void Awake()
        {
            if (positions.Count == 0)
            {
                Debug.LogError("Dial positions array is empty.", gameObject);
                return;
            }

            if (dialView == null)
            {
                Debug.LogError("Dial view is not assigned.", gameObject);
                return;
            }

            _dialModel = new DialModel(positions);
            _mainCamera = Camera.main;

            dialView.Initialize();
            UpdateView();
        }

        private void Update()
        {
            UpdateHighlight();
            HandleInput();
        }

#if UNITY_EDITOR
        [ContextMenu("Fill positions automatically")]
        private void FillAnglesAutomatically()
        {
            if (pointsCount < 2)
                return;

            float angleStep = 360f / pointsCount;
            positions.Clear();

            for (int i = 0; i < pointsCount; i++)
            {
                positions.Add(angleStep * i);
            }

            EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }

        [ContextMenu("Clear positions")]
        private void ClearPositions()
        {
            positions.Clear();
        }
#endif

        private void UpdateHighlight()
        {
            if (dialView.IsMouseOver(_mainCamera))
            {
                dialView.SetHighlight(true);
                _isDialActive = true;
            }
            else
            {
                dialView.SetHighlight(false);
                _isDialActive = false;
            }
        }

        private void HandleInput()
        {
            if (_isDialActive == false)
                return;

            float input = Input.mouseScrollDelta.y * (isRotationInverted ? -1 : 1);

            switch (input)
            {
                case > 0:
                    _dialModel.RotateDown();
                    NotifyDialPositionChanged();
                    UpdateView();
                    break;

                case < 0:
                    _dialModel.RotateUp();
                    NotifyDialPositionChanged();
                    UpdateView();
                    break;
            }
        }

        private void NotifyDialPositionChanged()
        {
            OnDialPositionChanged?.Invoke(_dialModel.CurrentIndex);
        }

        private void UpdateView()
        {
            dialView.SetRotation(_dialModel.CurrentAngle);
        }
    }
}