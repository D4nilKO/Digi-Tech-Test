using UnityEngine;

namespace MultimeterApp.View
{
    public class DialView : MonoBehaviour
    {
        [SerializeField]
        private Renderer dialRenderer;

        [SerializeField]
        private Collider dialCollider;

        [SerializeField]
        private Color highlightColor = Color.yellow;

        private Color _originalColor;

        public void Initialize()
        {
            if (dialRenderer == null || dialCollider == null)
            {
                Debug.LogError("Missing references in DialView!", gameObject);
                return;
            }

            _originalColor = dialRenderer.material.color;
        }

        public bool IsMouseOver(Camera _camera)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out RaycastHit hit) && hit.collider == dialCollider;
        }

        public void SetHighlight(bool isHighlighted)
        {
            dialRenderer.material.color = isHighlighted
                ? highlightColor
                : _originalColor;
        }

        public void SetRotation(float angle)
        {
            Quaternion rotation = transform.localRotation;
            transform.localRotation = Quaternion.Euler(rotation.x, angle, rotation.z);
        }
    }
}