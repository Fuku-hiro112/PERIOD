using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garden
{
    [CreateAssetMenu(fileName = "", menuName = "ScriptableObject/")]
    public class Garden : ScriptableObject
    {
        [Header("箱庭")]
        public GameObject GardenObject;

        // 難易度
        // ギミックランダム配置？
    }
}
