using System.Collections.Generic;
using UnityEngine;

public interface IBoidsRule
{
    public Vector3 GetDirection(Transform agent, List<Transform> neighbor);
}
