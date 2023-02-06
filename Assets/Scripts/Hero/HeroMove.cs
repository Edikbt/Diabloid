using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

namespace Diabloid
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private NavMeshAgent _navMesh;
        private IInputService _inputService;

        public event Action<bool> HeroMoving;
        private bool _isMoving = false;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            Vector3 touchPosition = _inputService.GetPosition();

            if (touchPosition != Vector3.zero)
            {
                Debug.Log("touched");

                _isMoving = true;                
                _navMesh.SetDestination(touchPosition);
                HeroMoving?.Invoke(_isMoving);
            }

            if (_isMoving == true && _navMesh.remainingDistance < 0.5f && _navMesh.remainingDistance != 0)
            {
                Debug.Log($"_navMesh.remainingDistance = {_navMesh.remainingDistance}");

                _isMoving = false;
                HeroMoving?.Invoke(_isMoving);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                    transform.position = savedPosition.AsUnityVector();
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        private string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}