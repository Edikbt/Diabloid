using UnityEngine;
using UnityEngine.SceneManagement;

namespace Diabloid
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStatsDataService _statsData;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IPersistentProgressService progressService, IStatsDataService statsData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _statsData = statsData;
        }

        public void Enter(string sceneName)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() { }

        private void OnLoaded()
        {
            InitSpawners();

            _gameFactory.CreateHero();
            _gameFactory.CreateHud();
            CameraFollow(_gameFactory.Hero);

            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitSpawners()
        {
            //foreach (EnemySpawner spawnerObj in GameObject.FindObjectsOfType<EnemySpawner>())
            //{
            //    EnemySpawner spawner = spawnerObj.GetComponent<EnemySpawner>();
            //    _gameFactory.Register(spawner);
            //}

            string sceneName = SceneManager.GetActiveScene().name;
            LevelData levelData = _statsData.Level(sceneName);

            foreach (EnemySpawnerData spawner in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawner.Position, spawner.Id, spawner.EnemyTypeId);
            }

        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void CameraFollow(GameObject hero) => 
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}
