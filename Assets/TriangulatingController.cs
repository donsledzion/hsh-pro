using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class TriangulatingController : MonoBehaviour
{
    [SerializeField] GameObject _trianglePrefab;
    [SerializeField] Transform _targetTansform;
    [SerializeField] UILineRenderer drawnLine;
        
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            int[] triangles;
            string message;
            PolygonHelper.Triangulate(drawnLine.Points, out triangles, out message);
            GenerateTriangles(drawnLine.Points, triangles);
        }
    }

    public void GenerateTriangles(Vector2[] vertices, int[] triangles)
    {
        for(int i = 0; i < triangles.Length-1; i+=3)
        {
            GameObject triangle = Instantiate(_trianglePrefab, _targetTansform);
            UILineRenderer lineRenderer = triangle.GetComponent<UILineRenderer>();
            GenerateTraingle(lineRenderer, vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]]);
        }
    }

    public void GenerateTraingle(UILineRenderer lineRenderer, Vector2 a, Vector2 b, Vector2 c)
    {        
        Vector2[] points = { a,b,c,};
        lineRenderer.Points = points;
        lineRenderer.LineThickness += 0.1f;
        lineRenderer.LineThickness -= 0.1f;
    }
}
