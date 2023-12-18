using UnityEngine;

namespace Gimmick
{
    public interface ISearcher
    {
        Vector3 SearchPosition(int id);
        GameObject SearchObject(int id);
    }
}
