using UnityEngine;

namespace Diabloid
{
    public class InputService : IInputService
    {
        private const float MaxRaycastDistance = 1000f;
        private LayerMask MoveLayer = LayerMask.NameToLayer("Ground");

        public Vector3 GetPosition()
        {
            //if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, MaxRaycastDistance, ~MoveLayer))
                    return raycastHit.point;
            }

            return Vector3.zero;
        }
    }
}