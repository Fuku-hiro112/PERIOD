using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Garden
{
    public class GardenGenerater
    {
        /// <summary>
        /// 箱庭生成
        /// </summary>
        /// <param name="gardens">出現させる箱庭達</param>
        /// <param name="gardenPos">出現させる位置</param>
        /// <returns>箱庭出現位置を返す</returns>
        public Vector3 GenerateGarden(GameObject[] gardens, Vector3 gardenPos)
        {
            foreach (GameObject garden in gardens)
            {
                // 出現位置を更新
                float gardenScalZ = garden.transform.localScale.z * 10;// meshの大きさによって変わるので変更が必要
                gardenPos += new Vector3(0, 0, gardenScalZ);// 箱庭のスケール分足す

                // 箱庭生成
                Object.Instantiate(garden, gardenPos, Quaternion.Euler(Vector3.zero));
            }
            return gardenPos;
        }
    }
}
