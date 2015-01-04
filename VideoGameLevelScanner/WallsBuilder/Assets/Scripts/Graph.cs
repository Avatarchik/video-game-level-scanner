using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Graph {
    public List<Edge> Edges = new List<Edge>();
    public List<Edge> Kruskal()
    {
        List<Edge> kruskal = new List<Edge>();
        List<List<int>> forest = new List<List<int>>();
        int uIndex = 0;
        int vIndex = 0;
        //TODO Randomize Edges
        var rand = new System.Random();
        Edges = Edges.OrderBy(edge =>  rand.Next()).ToList();
        foreach (var edge in Edges)
        {
            var indexes = FindSubTreesIndexes(forest, edge);
            uIndex = indexes.Key;
            vIndex = indexes.Value;
            if (uIndex == -1 && vIndex == -1)
            {
                forest.Add(new List<int> { edge.U, edge.V });
                kruskal.Add(edge);
            }
            else if (uIndex == -1)
            {
                forest[vIndex].Add(edge.U);
                kruskal.Add(edge);
            }
            else if (vIndex == -1)
            {
                forest[uIndex].Add(edge.V);
                kruskal.Add(edge);
            }
            else if (uIndex != vIndex)
            {
                forest[uIndex].AddRange(forest[vIndex]);
                forest.RemoveAt(vIndex);
            }
        }
        return kruskal;
    }

    private static KeyValuePair<int,int> FindSubTreesIndexes(List<List<int>> forest, Edge edge)
    {
        int uIndex = -1;
        int vIndex = -1;
        //uIndex = forest.IndexOf(forest.Where(subTree => subTree.Contains(edge.U));
        //vIndex = forest.IndexOf(forest.Where(subTree => subTree.Contains(edge.V));
        foreach (List<int> subTreeVertices in forest)
        {
            if (subTreeVertices.Contains(edge.U))
                uIndex = forest.IndexOf(subTreeVertices);
            
            if (subTreeVertices.Contains(edge.V))
                vIndex = forest.IndexOf(subTreeVertices);
        }
        return new KeyValuePair<int, int>(uIndex, vIndex);
    }
}
