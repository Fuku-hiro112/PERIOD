using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmick
{
    [Serializable]
    public class GimmickCon
    {
        [SerializeField]
        private GimmickDataManager _dataManager;

        public void OnAwake()
        {
            /*foreach (GimmickData gimmickData in _dataManager.)
            {
                GameObject obj = GameObject.Instantiate(gimmickData.Prefab);
                obj.SetActive(false);
            }*/
        }
        public void OnStart()
        {
            
        }
        public void OnUpdate()
        {

        }
    }
}
