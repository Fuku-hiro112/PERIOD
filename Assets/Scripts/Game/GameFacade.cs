using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    using Garden;
    using Character;

    [System.Serializable]
    public class GameFacade
    {
        public IGardenController IGardenController { get; private set; }
        //TODO: ÉvÉåÉCÉÑÅ[Ç‡
        [SerializeField]
        private GardenController _controller;

        public GameFacade(Progres progres)
        {
            _controller = new GardenController(progres);
            IGardenController = _controller;
            IGardenController.OnStart();
        }
    }
}
