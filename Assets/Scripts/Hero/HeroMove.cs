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

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            Vector3 touchPosition = _inputService.GetPosition();

            if (touchPosition != Vector3.zero)
                _navMesh.SetDestination(touchPosition);
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