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
        [SerializeField] private Transform _cameraTransform = null;
        [SerializeField] private Transform _playerTransform = null;
        [SerializeField] private Text _moneyText = null;
        [SerializeField] private ItemViewController _itemViewController = null;

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
        }

        private void _UpdateViews(GamePlayEngine.State state)
        {
            /*if (_cameraTransform != null) _cameraTransform.localPosition =
                    state.Player.Position.ToVector2().WithZ(_cameraTransform.localPosition.z);

            if (_playerTransform != null) _playerTransform.localPosition = state.Player.Position.ToVector3();
            */

            if (_moneyText != null) _moneyText.text = state.Money.ToString();

            if (_itemViewController != null) _itemViewController.SetItems(state.Items.Select(item =>
                new ItemViewController.ViewData
                {
                    PrefabName = item.Id,
                    AreaRange = new Rectangle(
                        item.AreaRange.Position - state.Player.Position,
                        item.AreaRange.Size).ToRect()
                }).ToArray());
        }
    }
}