using Maplewing.LeapBound.Engine;
using Maplewing.LeapBound.Engine.Data;
using Maplewing.LeapBound.Engine.Unity;
using UnityEngine;

namespace Maplewing.LeapBound.Unity.GamePlay
{
    public class GamePlayScene : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform = null;
        [SerializeField] private Transform _playerTransform = null;

        private GamePlayEngine _gamePlayEngine;

        private void Start()
        {
            _gamePlayEngine = new GamePlayEngine(new GamePlayEngine.State{
                Player = new Player(Vector2D.ZERO),
                Money = 0
            });
        }

        private void Update()
        {
            var state = _gamePlayEngine.Update(Time.deltaTime);

            _UpdateViews(state);
        }

        private void _UpdateViews(GamePlayEngine.State state)
        {
            if (_cameraTransform != null) _cameraTransform.localPosition =
                    state.Player.Position.ToVector2().WithZ(_cameraTransform.localPosition.z);

            if (_playerTransform != null) _playerTransform.localPosition = state.Player.Position.ToVector3();
        }
    }
}