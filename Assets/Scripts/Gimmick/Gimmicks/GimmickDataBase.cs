using UnityEngine;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "GimmickDataBase", menuName = "ScriptableObject/Gimmick/GimmickDataBase")]
    public class GimmickDataBase : ScriptableObject
    {
        [SerializeField]
        private GimmickSourceData[] _dataArray = new GimmickSourceData[0];

        public GimmickSourceData[] DataArray { get => _dataArray; }
    }
}

