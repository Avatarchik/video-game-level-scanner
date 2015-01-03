using UnityEngine;
using System.Collections;

public class Edge {

    public int U;
    public int V;
    public Edge(int u, int v)
    {
        U = u;
        V = v;
    }

    public override bool Equals(System.Object obj)
    {
        // If parameter is null return false.
        if (obj == null)
        {
            return false;
        }

        // If parameter cannot be cast to Edge return false.
        Edge otherEdge = obj as Edge;
        if ((System.Object)otherEdge == null)
        {
            return false;
        }

        // Return true if the fields match:
        return ((U == otherEdge.U && V == otherEdge.V) || (U == otherEdge.V && V == otherEdge.U));
    }

    public bool Equals(Edge otherEdge)
    {
        // If parameter is null return false:
        if ((object)otherEdge == null)
        {
            return false;
        }

        // Return true if the fields match:
        return ((U == otherEdge.U && V == otherEdge.V) || (U == otherEdge.V && V == otherEdge.U));
    }
}
