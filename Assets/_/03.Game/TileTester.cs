using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTester : MonoBehaviour
{
    public GameObject tile;
    public int diceRes;
    public Player player;
    private List<GameObject> tiles = new List<GameObject>();
    private Vector3[] directions = {
        new Vector3(1, 0, 0),  // 오른쪽
        new Vector3(-1, 0, 0), // 왼쪽
        new Vector3(0, 0, 1),  // 위쪽
        new Vector3(0, 0, -1)  // 아래쪽
    };
    private int[,] map = new int[5, 5]
    {
        { 1, 1, 1, 1, 0 },
        { 0, 0, 1, 0, 0 },
        { 1, 1, 1, 1, 1 },
        { 0, 1, 0, 0, 1 },
        { 1, 1, 1, 1, 1 }
    };

    private Dictionary<Vector3, List<Vector3>> reachableTiles = new Dictionary<Vector3, List<Vector3>>();
    private List<TileNode> reachableTileNodes = new List<TileNode>();
    void Start()
    {
        int x = 0;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 1)
                {
                    CreateTile(new Vector3(x, 0, j));
                }
            }
            x = x + 1;
        }
        CalculateReachableTiles(player.transform.position, diceRes);
    }

    void CreateTile(Vector3 position)
    {
        GameObject instance = Instantiate(tile);
        instance.transform.position = position;
        tiles.Add(instance);
    }

    public void CalculateReachableTiles(Vector3 startPos, int steps)
    {
        foreach (var tile in reachableTileNodes) 
        {
            tile.isTouchble = false;
            tile.GetComponent<Renderer>().material.color = Color.white;
        }
        reachableTiles.Clear();
        Queue<(Vector3 position, int stepCount, List<Vector3> path)> positionsQueue = new Queue<(Vector3 position, int stepCount, List<Vector3> path)>();
        HashSet<Vector3> visitedPositions = new HashSet<Vector3>();

        positionsQueue.Enqueue((startPos, 0, new List<Vector3> { startPos }));
        visitedPositions.Add(startPos);

        while (positionsQueue.Count > 0)
        {
            var (currentPos, currentStepCount, currentPath) = positionsQueue.Dequeue();

            if (currentStepCount == steps)
            {
                HighlightTileAtPosition(currentPos);
                reachableTiles[currentPos] = new List<Vector3>(currentPath);
            }
            else if (currentStepCount < steps)
            {
                foreach (Vector3 direction in directions)
                {
                    Vector3 newPos = currentPos + direction;

                    if (!visitedPositions.Contains(newPos) && IsTileAtPosition(newPos))
                    {
                        List<Vector3> newPath = new List<Vector3>(currentPath) { newPos };
                        positionsQueue.Enqueue((newPos, currentStepCount + 1, newPath));
                        visitedPositions.Add(newPos);
                    }
                }
            }
        }
    }

    bool IsTileAtPosition(Vector3 position)
    {
        foreach (GameObject tile in tiles)
        {
            if (tile.transform.position == position)
            {
                return true;
            }
        }
        return false;
    }

    void HighlightTileAtPosition(Vector3 position)
    {
        foreach (GameObject tile in tiles)
        {
            if (tile.transform.position == position)
            {
                Renderer renderer = tile.GetComponent<Renderer>();
                TileNode tileC = tile.GetComponent<TileNode>();
                reachableTileNodes.Add(tileC);
                if (renderer != null)
                {
                    tileC.isTouchble = true;
                    renderer.material.color = Color.green;
                }
            }
        }
    }

    public List<Vector3> GetPathToPosition(Vector3 position)
    {
        if (reachableTiles.ContainsKey(position))
        {
            return reachableTiles[position];
        }
        return null;
    }
}
