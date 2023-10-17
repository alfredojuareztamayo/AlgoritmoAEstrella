using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    //public Transform player;
    public LayerMask unawalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    M_Node[,] grid;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }


    void CreateGrid()
    {
        grid = new M_Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position -Vector3.right * gridWorldSize.x/2 - Vector3.forward*gridWorldSize.y/2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3  worldPoint = worldBottomLeft + Vector3.right *(x * nodeDiameter + nodeRadius) + Vector3.forward *(y*nodeDiameter+nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unawalkableMask));
                grid[x,y] = new M_Node(walkable,worldPoint);
            }
        }
    }

    public M_Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x,y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x,1,gridWorldSize.y));
        if(grid != null)
        {
            //Node playerNode = NodeFromWorldPoint(player.position);
            foreach(M_Node node in grid)
            {
                Gizmos.color = (node.walkable)? Color.white : Color.red;
                //if(playerNode == node)
                //{
                //    Gizmos.color = Color.cyan;
                //}
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }

}
