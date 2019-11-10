using Maplewing.LeapBound.Engine;
using Maplewing.LeapBound.Engine.Data;
using Maplewing.LeapBound.Engine.Unity;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Maplewing.LeapBound.Unity.GamePlay
{
    public class GamePlayScene : MonoBehaviour
    {
        [SerializeField] private Text _moneyText = null;
        [SerializeField] private ItemViewController _itemViewController = null;
        [SerializeField] private Transform _groundTransform = null;

        private GamePlayEngine _gamePlayEngine;

        private void Start()
        {
            _gamePlayEngine = new GamePlayEngine(new GamePlayEngine.State{
                Player = new Player(Vector2D.ZERO),
                Money = 0,
                Items = new IItem[0]
            });
        }

        private void Update()
        {
            var state = _gamePlayEngine.Update(Time.deltaTime);

            _UpdateViews(state);
            _UpdateInputState();
        }

        private void _UpdateViews(GamePlayEngine.State state)
        {
            if (_moneyText != null) _moneyText.text = state.Money.ToString();

            if (_itemViewController != null) _itemViewController.SetItems(state.Items.Select(item =>
                new ItemViewController.ViewData
                {
                    PrefabName = item.Id,
                    AreaRange = new Rectangle(
                        item.AreaRange.Position - state.Player.AreaRange.Position,
                        item.AreaRange.Size).ToRect()
                }).ToArray());

            if (_groundTransform != null)
                _groundTransform.localPosition = new Vector3(0, -0.55f - state.Player.AreaRange.Position.Y, 0);
        }

        private void _UpdateInputState()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _gamePlayEngine.ExecuteCommand(new PlayerJumpCommand());
        }
    }
}