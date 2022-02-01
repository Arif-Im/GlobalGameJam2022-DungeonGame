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

    public PathMarker(MapLocation l, float g, float h, float f, PathMarker p)
    {
        location = l;
        G = g;
        H = h;
        F = f;
        parent = p;
    }

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
    
    //my codes
    public List<PathMarker> chosenPath = new List<PathMarker>();
    public Vector2 startNodeMarker;

    bool startMoving = false;
    bool isPathCalculated = false;
    bool hasDoneBeginSearch = false;

    int noOfPath = 0;

    void RemoveAllMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("Path");
        foreach(GameObject marker in markers)
        {
            Destroy(marker);
        }
    }

    public void BeginSearch(GameObject enemy, GameObject player)
    {
        RemoveAllMarkers();
        //chosenPath.Clear();

        List<MapLocation> locations = new List<MapLocation>();
        for (int y = 1; y < maze.yMax - 1; y++)
            for (int x = 1; x < maze.xMax - 1; x++)
            {
                if (maze.map[x, y] != 1)
                    locations.Add(new MapLocation(x, y));
            }

        locations.Shuffle();

        startNode = new PathMarker(new MapLocation((int)enemy.transform.position.x, (int)enemy.transform.position.y), 0, 0, 0, null);
        startNodeMarker = startNode.location.ToVector();

        goalNode = new PathMarker(new MapLocation(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y)), 0, 0, 0, null);

        open.Clear();
        closed.Clear();

        open.Add(startNode);
        lastPos = startNode;
    }

    public void Search(PathMarker thisNode)
    {
        if(thisNode.Equals(goalNode))
        {
            return;
        }

        if (noOfPath > 5)
        {
            Debug.Log($"No of Path: {noOfPath}");
            noOfPath = 0;
            isPathCalculated = true;
            GetPath();
        }
        else
        {
            Debug.Log("Creating new neighbours");
            foreach (MapLocation dir in maze.directions)
            {
                MapLocation neighbour = dir + thisNode.location;

                if (maze.map[neighbour.x, neighbour.y] == 1)
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

                Vector2 pathVector = new Vector2(neighbour.x, neighbour.y);

                if (!UpdateMarker(neighbour, G, H, F, thisNode))
                {
                    Debug.Log("Adding neighbours to open");
                    open.Add(new PathMarker(neighbour, G, H, F, thisNode));
                    Debug.Log($"Open Neighbour Count = {open.Count}");
                }
            }

            open = open.OrderBy(p => p.F).ToList<PathMarker>();
            PathMarker pm = (PathMarker)open.ElementAt(0);
            closed.Add(pm);

            open.RemoveAt(0);

            lastPos = pm;
            noOfPath++;
        }
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
        PathMarker begin = lastPos;

        while (!startNode.Equals(begin) && begin != null)
        {
            begin = begin.parent;
            chosenPath.Add(begin);
        }

        chosenPath.Reverse();

        Debug.Log($"Chosen Path Count: {chosenPath.Count}");
        IsStartMoving = true;
    }

    public void ResetValues()
    {
        chosenPath.Clear();
        IsPathCalculated = false;
        IsStartMoving = false;
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
