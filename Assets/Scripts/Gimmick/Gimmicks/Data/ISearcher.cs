using UnityEngine;

namespace Gimmikc
{
    public interface ISearcher
    {
        Vector3 SearchPosition(int id);
        GameObject SearchObject(int id);
    }
}
