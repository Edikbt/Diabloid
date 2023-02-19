using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

namespace Diabloid
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private NavMeshAgent _navMesh;
        [SerializeField] private MoveTo _moveTo;
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _inputService.OnGroundTouched += MoveTo;
        }

        private void OnDestroy() => 
            _inputService.OnGroundTouched -= MoveTo;

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                    transform.position = savedPosition.AsUnityVector();
            }
        }

        private void MoveTo(Vector3 position)
        {
            _moveTo.ClearTarget();
            _navMesh.SetDestination(position);
        }

        public void UpdateProgress(PlayerProgress progress) => 
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

        private string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}