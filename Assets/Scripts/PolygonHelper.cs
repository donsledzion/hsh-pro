using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PolygonHelper
{
    
    enum WindingOrder
    {
        Clockwise,
        CounterClockwise
    }

    public static float FindPolygonArea(Vector2[] vertices)
    {
        float area = 0f;
        for(int i = 0; i < vertices.Length; i++)
        {
            Vector2 va = vertices[i];
            Vector2 vb = vertices[(i + 1) % vertices.Length];

            float width = vb.x - va.x;
            float height = (vb.y + va.y) / 2f;

            area += width * height;
        }
        return Mathf.Abs(area);
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
        */

        WindingOrder windingOrder = CheckWindingOrder(vertices);

        if (windingOrder is WindingOrder.CounterClockwise)  
        {
            Array.Reverse(vertices);
        }

        List<int> indexList = new List<int>();
        for(int i = 0; i < vertices.Length; i++)
        {
            indexList.Add(i);
        }

        int totalTriangleCount = vertices.Length - 2;
        int totalTriangleIndexCount = totalTriangleCount * 3;

        triangles = new int[totalTriangleIndexCount];
        int triangleIndexCount = 0;
        int counter = 0;
        while(indexList.Count > 3 && counter <= 1000)
        {
            counter++;
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

    private static WindingOrder CheckWindingOrder(Vector2[] vertices)
    {
        float sum = 0;
        for(int i = 0; i < vertices.Length-1; i++)
        {
            sum += (vertices[i + 1].x - vertices[i].x) * (vertices[i + 1].y + vertices[i].y);
        }
        sum += (vertices[0].x - vertices[vertices.Length-1].x) * (vertices[0].y + vertices[vertices.Length - 1].y);

        return (sum < 0 ? WindingOrder.CounterClockwise : WindingOrder.Clockwise);
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

    public static Vector2 FindCentroid(List<Vector2> pts)
    {
        Vector2 off = pts[0];
        float twicearea = 0;
        float x = 0;
        float y = 0;
        Vector2 p1, p2;
        float f;
        for (int i = 0, j = pts.Count - 1; i < pts.Count; j = i++)
        {
            p1 = pts[i];
            p2 = pts[j];
            f = (p1.x - off.x) * (p2.y - off.y) - (p2.x - off.x) * (p1.y - off.y);
            twicearea += f;
            x += (p1.x + p2.x - 2 * off.x) * f;
            y += (p1.y + p2.y - 2 * off.y) * f;
        }

        f = twicearea * 3;

        return new Vector2(x / f + off.x, y / f + off.y);
    }

    public static Vector2[] PlaneRange(Vector2[] points)
    {
        Vector2[] outVectors = { new Vector2() };

        if (points.Length == 0) return outVectors;

        float minX = points[0].x;
        float minY = points[0].y;
        float maxX = points[0].x;
        float maxY = points[0].y;

        foreach (Vector2 point in points)
        {
            if (point.x < minX) minX = point.x;
            if (point.y < minY) minY = point.y;
            if (point.x > maxX) maxX = point.x;
            if (point.y > maxY) maxY = point.y;
        }

        Vector2[] newOutVectors = { new Vector2(minX, minY), new Vector2(maxX,maxY) };
        return newOutVectors;
    }


    public static Vector2[] RangeToRect(Vector2[] range, float offset=0f)
    {
        Vector2[] rect ={
            new Vector2(range[0].x - offset,range[0].y - offset),
            new Vector2(range[0].x - offset,range[1].y + offset),
            new Vector2(range[1].x + offset,range[1].y + offset),
            new Vector2(range[1].x + offset,range[0].y - offset)
        };
        return rect;
    }


    public static Vector2 AdjustScale(Vector2[] points)
    {
        if (points.Length == 0) return new Vector2(0, 0);


        Vector2 outScale = points[0];
        float minX = outScale.x;
        float minY = outScale.y;
        float maxX = outScale.x;
        float maxY = outScale.y;

        foreach (Vector2 point in points)
        {
            if (point.x < minX) minX = point.x;
            if (point.y < minY) minY = point.y;
            if (point.x > maxX) maxX = point.x;
            if (point.y > maxY) maxY = point.y;
        }
        outScale.x = Mathf.Abs(maxX - minX);
        outScale.y = Mathf.Abs(maxY - minY);


        return outScale;
    }
}
