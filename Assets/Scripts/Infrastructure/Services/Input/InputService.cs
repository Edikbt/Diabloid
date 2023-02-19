using System;
using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class InputService : IInputService, ITickable
    {
        private const float MaxRaycastDistance = 1000f;
        private LayerMask _groundLayerMask = LayerMask.NameToLayer("Ground");
        private LayerMask _enemyLayerMask = LayerMask.NameToLayer("EnemyHittable");
        private LayerMask _layerMask;

        public event Action<Vector3> OnGroundTouched;
        public event Action<GameObject> OnEnemyTouched;

        public InputService()
        {
            _layerMask = (1 <<_groundLayerMask) | (1 << _enemyLayerMask);
        }

        public void Tick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, MaxRaycastDistance, _layerMask))
                {
                    int hittedLayer = raycastHit.transform.gameObject.layer;

                    if (hittedLayer == _groundLayerMask)
                        OnGroundTouched?.Invoke(raycastHit.point);
                    else if (hittedLayer == _enemyLayerMask)
                        OnEnemyTouched?.Invoke(raycastHit.transform.gameObject);
                }                
            }
        }
    }
}