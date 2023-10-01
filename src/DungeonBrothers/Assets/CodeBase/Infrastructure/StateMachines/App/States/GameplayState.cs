using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.Services.SceneLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.StateMachines.App.States
{
    public class GameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;

        public GameplayState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await UniTask.Delay(2000);
            _sceneLoader.Load(SceneType.Level);
        }

        public void Exit()
        {
        }
    }
}