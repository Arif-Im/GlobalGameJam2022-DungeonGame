using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapLocation
{
    public int x;
    public int y;

    public MapLocation(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public Vector2 ToVector()
    {
        return new Vector2(x, y);
    }

    public static MapLocation operator +(MapLocation a, MapLocation b)
        => new MapLocation(a.x + b.x, a.y + b.y);

    public override bool Equals(object obj)
    {
        if(obj == null || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            return x == ((MapLocation)obj).x && y == ((MapLocation)obj).y;
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

public class Maze : MonoBehaviour
{
    public List<MapLocation> directions = new List<MapLocation>()
    {
        new MapLocation(1,0),
        new MapLocation(0,1),
        new MapLocation(-1,0),
        new MapLocation(0,-1)
    };

    public int xMin;
    public int yMin;
    public int xMax;
    public int yMax;
    public byte[,] map;
    public int scale = 6;
    public GameObject tileMarkerPrefab;

    private Tilemap tilemap;
    private Vector3Int tilePosition;
    private Vector3Int coordinate;

    private void Start()
    {
        InitializeMap();
        Generate();
    }

    void InitializeMap()
    {
        tilemap = GetComponent<Tilemap>();

        //xMin = tilemap.cellBounds.xMin;
        //yMin = tilemap.cellBounds.yMin;
        //xMax = tilemap.cellBounds.xMax;
        //yMax = tilemap.cellBounds.yMax;

        xMin = 0;
        yMin = 0;
        xMax = tilemap.size.x;
        yMax = tilemap.size.y;

        //Debug.Log("yMin: " + yMin);
        //Debug.Log("xMin: " + xMin);
        //Debug.Log("yMax: " + yMax);
        //Debug.Log("xMax: " + xMax);
        //Debug.Log("======================================");

        map = new byte[xMax, yMax];
        for(int y = yMin; y < yMax; y++)
            for(int x = xMin; x < xMax; x++)
            {
                //Debug.Log("y: " + y);
                //Debug.Log("x: " + x);
                map[x, y] = 1;
            }
    }

    public virtual void Generate()
    {
        for(int y = yMin; y < yMax; y++)
            for(int x = xMin; x < xMax; x++)
            {
                coordinate.x = x;
                coordinate.y = y;
                tilePosition = tilemap.WorldToCell(coordinate);
                if(tilemap.HasTile(tilePosition))
                {
                    map[x, y] = 1;
                    //Instantiate(tileMarkerPrefab, new Vector2(tilePosition.x + 0.5f, tilePosition.y + 0.5f), Quaternion.identity);
                }
                else
                {
                    map[x, y] = 0;
                }
            }
    }
}
