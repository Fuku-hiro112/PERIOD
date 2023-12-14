using UnityEngine;

namespace Item
{
    // アイテムのデータベース
    [CreateAssetMenu(fileName = "DataBase", menuName = "ScriptableObject/Item/DataBase")]
    public class ItemDataBase : ScriptableObject, IItemDataBase
    {
        [SerializeField]
        private ItemSourceDataBase[] _itemSourceDataBases = new ItemSourceDataBase[0];

        public ItemSourceDataBase[] ItemSourceDataBases { get => _itemSourceDataBases; }
    }
}
