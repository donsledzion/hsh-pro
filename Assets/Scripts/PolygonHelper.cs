using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PolygonHelper
{
    enum WindingOrder
    {

    }

    public static float FindPolygonArea(Vector2[] vertices)
    {
        return 0f;
    }

    public static bool Triangulate(Vector2[] vertices, out int [] triangles, out string errorMessage)
    {
        triangles = null;
        errorMessage = string.Empty;

        if(vertices is null)
        {
            errorMessage = "The vertices list is null.";
            return false;
        }

        if(vertices.Length < 3)
        {
            errorMessage = "The vertices list must have at lesat 3 vertices.";
            return false;
        }

        if(vertices.Length > 1024)
        {
            errorMessage = "The max vertices list length is 1024.";
            return false;
        }

        /*if(!PolygonHelper.IsSimplePolygon(vertices))
        {
            errorMessage = "";
            return false;
        }*/

        /*if(PolygonHelper.ContainsColinearEdges(vertices))
        {
            errorMessage = "The vertex list contains colinear edges.";
            return false;
        }*/

        /*PolygonHelper.ComputePolygonArea(vertices, out float area, out WindingOrder windingOrder);

        if (windingOrder is WindingOrder.Invalid)
        {
            errorMessage = "The vertices list does not contain a valid polygon";
            return false;
        }

        if (windingOrder is WindingOrder.CounterClockwise)
        {
            Array.Reverse(vertices);
        }*/

        List<int> indexList = new List<int>();
        for(int i = 0; i < vertices.Length; i++)
        {
            indexList.Add(i);
        }

        int totalTriangleCount = vertices.Length - 2;
        int totalTriangleIndexCount = totalTriangleCount * 3;

        triangles = new int[totalTriangleIndexCount];
        int triangleIndexCount = 0;

        while(indexList.Count > 3)
        {
            for(int i = 0; i < indexList.Count; i++)
            {
                int a = indexList[i];
                int b = MathHelpers.GetItem(indexList,i-1);
                int c = MathHelpers.GetItem(indexList,i+ 1);

                Vector2 va = vertices[a];
                Vector2 vb = vertices[b];
                Vector2 vc = vertices[c];

                Vector2 ab = vb - va;
                Vector2 ac = vc - va;

                if(MathHelpers.Cross(ab,ac) < 0f)
                    continue;

                bool isEar = true;

                for (int j = 0; j < vertices.Length; j++)
                {
                    if (j == a || j == b || j == c)
                        continue;

                    Vector2 p = vertices[j];
                    if (PolygonHelper.PointInTriangle(p, vb, va, vc))
                    {
                        isEar = false;
                        break;
                    }
                }

                if(isEar)
                {
                    triangles[triangleIndexCount++] = b;
                    triangles[triangleIndexCount++] = a;
                    triangles[triangleIndexCount++] = c;

                    indexList.RemoveAt(i);
                    break;
                }
            }
        }

        triangles[triangleIndexCount++] = indexList[0];
        triangles[triangleIndexCount++] = indexList[1];
        triangles[triangleIndexCount++] = indexList[2];


        return true;
    }

    private static bool PointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 ab = b - a;
        Vector2 bc = c - b;
        Vector2 ca = a - c;

        Vector2 ap = p - a;
        Vector2 bp = p - b;
        Vector2 cp = p - c;

        float cross1 = MathHelpers.Cross(ab, ap);
        float cross2 = MathHelpers.Cross(bc, bp);
        float cross3 = MathHelpers.Cross(ca, cp);

        if (cross1 > 0f || cross2 > 0f || cross3 > 0f)
            return false;
        return true;
    }

    public static bool IsSimplePolygon(Vector2[] vertices)
    {
        throw new NotImplementedException();
    }

    public static bool ContainsColinearEdges(Vector2[] vertices)
    {
        throw new NotImplementedException();
    }

    /*public static void ComputePolygonArea(Vector2[] vertices, out float area, out WindingOrder windingOrder)
    {
        throw new NotImplementedException();
    }*/
}
