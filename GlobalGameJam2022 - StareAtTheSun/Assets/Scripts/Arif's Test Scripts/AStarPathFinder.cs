using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PathMarker
{
    public MapLocation location;
    public float G;
    public float H;
    public float F;
    public GameObject marker;
    public PathMarker parent;


    public PathMarker(MapLocation l, float g, float h, float f, GameObject marker, PathMarker p)
    {
        location = l;
        G = g;
        H = h;
        F = f;
        this.marker = marker;
        parent = p;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            return location == ((PathMarker)obj).location;
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

public class AStarPathFinder : MonoBehaviour
{
    public Maze maze;
    public Color closedColor;
    public Color openColor;

    List<PathMarker> open = new List<PathMarker>();
    List<PathMarker> closed = new List<PathMarker>();

    public GameObject start;
    public GameObject end;
    public GameObject pathP;

    PathMarker goalNode;
    PathMarker startNode;

    PathMarker lastPos;
    bool done = false;
    
    //my codes
    public List<PathMarker> chosenPath = new List<PathMarker>();
    public List<GameObject> chosenPathMarkers = new List<GameObject>();
    public GameObject startNodeMarker;

    public GameObject enemy;
    public GameObject player;
    //int pathMarkerIndex = 1;
    bool startMoving = false;

    bool isPathCalculated = false;

    void RemoveAllMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("Path");
        foreach(GameObject marker in markers)
        {
            Destroy(marker);
        }
    }

    public void BeginSearch()
    {
        done = false;
        RemoveAllMarkers();
        chosenPath.Clear();
        chosenPathMarkers.Clear();

        List<MapLocation> locations = new List<MapLocation>();
        for(int y = 1; y < maze.yMax - 1; y++)
            for(int x = 1; x < maze.xMax - 1;x++)
            {
                if (maze.map[x, y] != 1)
                    locations.Add(new MapLocation(x, y));
            }

        locations.Shuffle();

        Vector2 startLocation = new Vector2Int(Mathf.RoundToInt(enemy.transform.position.x), Mathf.RoundToInt(enemy.transform.position.y));
        startNode = new PathMarker(new MapLocation((int)enemy.transform.position.x, (int)enemy.transform.position.y), 0, 0, 0, Instantiate(start, startLocation, Quaternion.identity), null);
        startNodeMarker = startNode.marker;

        Vector2 goalLocation = new Vector2Int(Mathf.RoundToInt(player.transform.position.x) , Mathf.RoundToInt(player.transform.position.y));
        goalNode = new PathMarker(new MapLocation(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y)), 0, 0, 0, Instantiate(end, goalLocation, Quaternion.identity), null);

        open.Clear();
        closed.Clear();

        open.Add(startNode);
        lastPos = startNode;
    }

    public void Search(PathMarker thisNode)
    {
        if(thisNode.Equals(goalNode))
        {
            done = true;
            return;
        }

        foreach(MapLocation dir in maze.directions)
        {
            MapLocation neighbour = dir + thisNode.location;

            if(maze.map[neighbour.x, neighbour.y] == 1)
            {
                continue;
            }

            if (neighbour.x < 1 || neighbour.x >= maze.xMax || neighbour.y < 1 || neighbour.y >= maze.yMax)
            {
                continue;
            }

            if (IsClosed(neighbour))
            {
                continue;
            }

            float G = Vector2.Distance(thisNode.location.ToVector(), neighbour.ToVector()) + thisNode.G;
            float H = Vector2.Distance(neighbour.ToVector(), goalNode.location.ToVector());
            float F = G + H;

            GameObject pathBlock = Instantiate(pathP, new Vector2(neighbour.x /** maze.scale*/, neighbour.y /** maze.scale*/), Quaternion.identity);

            TextMesh[] values = pathBlock.GetComponentsInChildren<TextMesh>();
            values[0].text = "G: " + G.ToString("0.00");
            values[1].text = "H: " + H.ToString("0.00");
            values[2].text = "F: " + F.ToString("0.00");

            if (!UpdateMarker(neighbour, G,H,F,thisNode))
            {
                open.Add(new PathMarker(neighbour, G, H, F, pathBlock, thisNode));
                pathBlock.GetComponent<SpriteRenderer>().color = openColor;
            }

            if(Vector2.Distance(pathBlock.transform.position, player.transform.position) < 1)
            {
                isPathCalculated = true;
            }
        }

        open = open.OrderBy(p => p.F).ToList<PathMarker>();
        PathMarker pm = (PathMarker)open.ElementAt(0);
        closed.Add(pm);

        open.RemoveAt(0);
        pm.marker.GetComponent<SpriteRenderer>().color = closedColor;

        lastPos = pm;
    }

    private bool UpdateMarker(MapLocation pos, float g, float h, float f, PathMarker prt)
    {
        foreach(PathMarker p in open)
        {
            if(p.location.Equals(pos))
            {
                p.G = g;
                p.H = h;
                p.F = f;
                p.parent = prt;
                return true;
            }
        }
        return false;
    }

    bool IsClosed(MapLocation marker)
    {
        foreach (PathMarker p in closed)
        {
            if (p.location.Equals(marker))
                return true;
        }
        return false;
    }

    public void GetPath()
    {
        RemoveAllMarkers();
        PathMarker begin = lastPos;

        while(!startNode.Equals(begin) && begin != null)
        {
            GameObject beginMarker = Instantiate(pathP, new Vector2(begin.location.x /** maze.scale*/, begin.location.y /** maze.scale*/), Quaternion.identity);
            begin = begin.parent;
            chosenPath.Add(begin);
            chosenPathMarkers.Add(beginMarker);
        }

        chosenPath.Reverse();
        chosenPathMarkers.Reverse();
        startMoving = true;

        Debug.Log(chosenPath.Count);
    }

    public bool IsStartMoving
    {
        get
        {
            return startMoving;
        }
        set
        {
            startMoving = value;
        }
    }

    public bool IsPathCalculated
    {
        get
        {
            return isPathCalculated;
        }
        set
        {
            isPathCalculated = value;
        }
    }

    public PathMarker GetLastPos()
    {
        return lastPos;
    }

    public PathMarker GetStartNode()
    {
        return startNode;
    }

    public List<PathMarker> GetChosenPath()
    {
        return chosenPath;
    }
}
