﻿using Maplewing.LeapBound.Engine;
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
            _gamePlayEngine = new GamePlayEngine(new Player(Vector2D.ZERO));
        }

        private void Update()
        {
            _gamePlayEngine = _gamePlayEngine.Update(Time.deltaTime);

            _UpdateViews(_gamePlayEngine);
        }

        private void _UpdateViews(GamePlayEngine gamePlayEngine)
        {
            if (_cameraTransform != null) _cameraTransform.localPosition =
                    _gamePlayEngine.Player.Position.ToVector2().WithZ(_cameraTransform.localPosition.z);

            if (_playerTransform != null) _playerTransform.localPosition = _gamePlayEngine.Player.Position.ToVector3();
        }
    }
}