using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Node
{
    public bool walkable;
    public Vector3 worldPosition;

    public M_Node(bool _walkable, Vector3 _worldPos)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
    }
}
