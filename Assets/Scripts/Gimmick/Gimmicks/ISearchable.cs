using UnityEngine;

namespace Gimmikc
{
    public interface ISearchable
    {
        Vector3 SearchPosition(int id);
        GameObject SearchObject(int id);
    }
}
