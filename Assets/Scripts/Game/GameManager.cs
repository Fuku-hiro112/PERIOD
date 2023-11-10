using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private GameState _state = GameState.Title;
        [SerializeField] private GameMode _mode;
        [SerializeField] private GameController _controller;
        private CancellationToken _token;
        void Start()
        {
            _token = this.GetCancellationTokenOnDestroy();
            _controller = new GameController(_mode, _token);
        }

        void Update()
        {
            _controller.OnUpdate();
        }
    }
}
// シーンごとに分けるつもり
public enum GameState
{
    Title,
    Tutorial,
    GamePlay,

}
// ゲームモードの種類
public enum GameMode
{
    Survival,
    Cycle
}