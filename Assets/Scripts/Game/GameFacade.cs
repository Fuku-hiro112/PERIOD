using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    using Garden;
    using Character;
    //[System.Serializable]
    public class GameFacade
    {
        public IGardenController IGardenController { get; private set; }
        // ÉvÉåÉCÉÑÅ[Ç‡
        //[SerializeField]
        private GardenController _controller;

        public GameFacade()
        {
            _controller = new GardenController();
            IGardenController = _controller;
            IGardenController.OnStart();
        }
    }
}
