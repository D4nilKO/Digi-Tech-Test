using System.Collections.Generic;

namespace MultimeterApp.Model
{
    public class DialModel
    {
        private readonly List<float> _positions;

        public DialModel(List<float> positions)
        {
            _positions = positions;
        }

        public int CurrentIndex { get; private set; }

        public float CurrentAngle => _positions[CurrentIndex];

        public void RotateUp()
        {
            CurrentIndex = (CurrentIndex + 1) % _positions.Count;
        }

        public void RotateDown()
        {
            CurrentIndex = (CurrentIndex - 1 + _positions.Count) % _positions.Count;
        }
    }
}