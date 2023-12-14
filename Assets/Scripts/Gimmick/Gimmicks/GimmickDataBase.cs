using UnityEngine;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "GimmickDataBase", menuName = "ScriptableObject/Gimmick/GimmickDataBase")]
    public class GimmickDataBase : ScriptableObject
    {
        [SerializeField]
        private GimmickSourceDataBase[] _dataArray = new GimmickSourceDataBase[0];

        public GimmickSourceDataBase[] DataArray { get => _dataArray; }
    }
}

