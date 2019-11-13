using Maplewing.LeapBound.Engine;
using Maplewing.LeapBound.Engine.Data;
using Maplewing.LeapBound.Engine.Unity;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Rayark.Mast;
using System.Collections;
using System;

namespace Maplewing.LeapBound.Unity.GamePlay
{
    public class GamePlayScene : MonoBehaviour
    {
        private const float PLAYER_POSITION_X = -4f;
        private const string PLAYER_IDLE_ANIMATOR_STATE = "Idle";
        private const string PLAYER_INVISIBLE_ANIMATOR_STATE = "Invisible";
        private const string PLAYER_DEAD_ANIMATOR_STATE = "Dead";

        private const string PLAYER_IDLE_ANIMATOR_TRIGGER = "Idle";
        private const string PLAYER_INVISIBLE_ANIMATOR_TRIGGER = "Invisible";
        private const string PLAYER_DEAD_ANIMATOR_TRIGGER = "Dead";

        [SerializeField] private Text _moneyText = null;
        [SerializeField] private Text _hpText = null;
        [SerializeField] private Animator _playerAnimator = null;
        [SerializeField] private ItemViewController _itemViewController = null;
        [SerializeField] private EnemyViewController _enemyViewController = null;
        [SerializeField] private BulletViewController _bulletViewController = null;
        [SerializeField] private Transform _groundTransform = null;
        [SerializeField] private GameObject _endGamePanelGameObject = null;
        [SerializeField] private Text _bestScoreText = null;

        private Executor _mainExecutor = new Executor();
        private long _bestScore = 0;

        private void Start()
            => _mainExecutor.Add(_Main());

        private void Update()
            => _mainExecutor.Resume(Time.deltaTime);

        private IEnumerator _Main()
        {
            while (true)
            {
                var gamePlayEngine = new GamePlayEngine(
                    new GamePlayEngine.State
                    {
                        Player = new Player(Vector2D.ZERO),
                        Money = 0,
                        Items = new IItem[0],
                        Enemies = new IEnemy[0]
                    });
                yield return _PlayMainGame(gamePlayEngine);

                if (_bestScoreText != null)
                {
                    _bestScore = Math.Max(_bestScore, gamePlayEngine.CurrentState.Money);
                    _bestScoreText.text = "Best Score: " + _bestScore;
                }

                if (_endGamePanelGameObject != null) _endGamePanelGameObject.SetActive(true);

                while (!Input.GetKeyUp(KeyCode.Return))
                {
                    yield return null;
                }

                if (_endGamePanelGameObject != null) _endGamePanelGameObject.SetActive(false);
            }
        }

        private IEnumerator _PlayMainGame(GamePlayEngine gamePlayEngine)
        {
            var state = gamePlayEngine.CurrentState;
            while (!state.Player.IsDead())
            {
                state = gamePlayEngine.Update(Time.deltaTime);

                _UpdateViews(state);
                _UpdateInputState(gamePlayEngine);
                yield return null;
            }
        }

        private void _UpdateViews(GamePlayEngine.State state)
        {
            if (_moneyText != null) _moneyText.text = "Money: " + state.Money.ToString();
            if (_hpText != null) _hpText.text = "HP: " + state.Player.HP.ToString() + " / " + state.Player.MaxHP.ToString();

            if(state.Player.IsDead() &&
                !_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_DEAD_ANIMATOR_STATE))
            {
                _playerAnimator.SetTrigger(PLAYER_DEAD_ANIMATOR_TRIGGER);
            }
            else if(state.Player.IsInvisible() &&
                !_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_INVISIBLE_ANIMATOR_STATE))
            {
                _playerAnimator.SetTrigger(PLAYER_INVISIBLE_ANIMATOR_TRIGGER);
            }
            else if(!state.Player.IsInvisible() &&
                !_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_IDLE_ANIMATOR_STATE))
            {
                _playerAnimator.SetTrigger(PLAYER_IDLE_ANIMATOR_TRIGGER);
            }
            
            if (_itemViewController != null) _itemViewController.SetItems(state.Items.Select(item =>
                new ItemViewController.ViewData
                {
                    PrefabName = item.Id,
                    AreaRange = new Rectangle(
                        item.AreaRange.Position - state.Player.AreaRange.Position + new Vector2D(PLAYER_POSITION_X, 0),
                        item.AreaRange.Size).ToRect()
                }).ToArray());

            if (_enemyViewController != null) _enemyViewController.SetEnemies(state.Enemies.Select(enemy =>
                 new EnemyViewController.ViewData
                 {
                     PrefabName = enemy.Id,
                     AreaRange = new Rectangle(
                         enemy.AreaRange.Position - state.Player.AreaRange.Position + new Vector2D(PLAYER_POSITION_X, 0),
                         enemy.AreaRange.Size).ToRect()
                 }).ToArray());

            if (_bulletViewController != null) _bulletViewController.SetBullets(state.Bullets.Select(bullet =>
                 new BulletViewController.ViewData
                 {
                     PrefabName = bullet.Id,
                     AreaRange = new Rectangle(
                         bullet.AreaRange.Position - state.Player.AreaRange.Position + new Vector2D(PLAYER_POSITION_X, 0),
                         bullet.AreaRange.Size).ToRect()
                 }).ToArray());

            if (_groundTransform != null)
                _groundTransform.localPosition = new Vector3(0, -0.55f - state.Player.AreaRange.Position.Y, 0);
        }

        private void _UpdateInputState(GamePlayEngine gamePlayEngine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                gamePlayEngine.ExecuteCommand(new PlayerJumpCommand());

            if (Input.GetKeyDown(KeyCode.Z))
                gamePlayEngine.ExecuteCommand(new PlayerAttackCommand());
        }
    }
}