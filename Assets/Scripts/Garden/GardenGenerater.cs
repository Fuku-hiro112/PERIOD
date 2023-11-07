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
        /// ���됶��
        /// </summary>
        /// <param name="gardens">�o�������锠��B</param>
        /// <param name="gardenPos">�o��������ʒu</param>
        /// <returns>����o���ʒu��Ԃ�</returns>
        public Vector3 GenerateGarden(GameObject[] gardens, Vector3 gardenPos)
        {
            foreach (GameObject garden in gardens)
            {
                // �o���ʒu���X�V
                float gardenScalZ = garden.transform.localScale.z * 10;// mesh�̑傫���ɂ���ĕς��̂ŕύX���K�v
                gardenPos += new Vector3(0, 0, gardenScalZ);// ����̃X�P�[��������

                // ���됶��
                Object.Instantiate(garden, gardenPos, Quaternion.Euler(Vector3.zero));
            }
            return gardenPos;
        }
    }
}
