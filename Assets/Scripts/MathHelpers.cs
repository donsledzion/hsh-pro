using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelpers
{


    public static float TrinomialA1(float a, float b, float c, float delta)
    { 
        return (-b-Mathf.Sqrt(TrinomialDelta(a,b,c)))/(2*a);
    }
    public static float TrinomialA2(float a, float b, float c, float delta)
    { 
        return (-b+Mathf.Sqrt(TrinomialDelta(a,b,c)))/(2*a);
    }

    public static float TrinomialDelta(float a, float b, float c)
    {
        return b*b-4*a*c;
    }

    public static float StraightA(Vector2 P1, Vector2 P2)
    {
        return (P2.y - P1.y) / (P2.x - P1.x);
    }

    public static float StraightB(Vector2 P1, Vector2 P2)
    {
        return P1.y - (P1.y - P2.y) / (P1.x - P2.x) * P1.x;
    }

    public static Vector2 MiddleOf2Vectors(Vector2 P1, Vector2 P2)
    {
        return (P1 + P2) / 2;
    }

    public static float PerpendicularA(Vector2 P1, Vector2 P2)
    {
        return -1 / (StraightA(P1, P2));
    }

    public static float PerpendicularB(Vector2 P1, Vector2 P2)
    {
        Vector2 mid = MiddleOf2Vectors(P1, P2);
        float a = PerpendicularA(P1, P2);
        float b = -a*mid.x+mid.y;
        return b;
    }

    public static float PerpendicularAngle(Vector2 P1, Vector2 P2)
    {
        return Mathf.Atan(PerpendicularA(P1, P2));
    }

    public static Vector2 CloserPoint(Vector2 Point1, Vector2 Point2, Vector2 comparisonVector)
    {
        if ((Point1 - comparisonVector).magnitude < (Point2 - comparisonVector).magnitude) return Point1;
        return Point2;
    }

    public static Vector2 ThirdPeak(Vector2 P1, Vector2 P2, float radius, Vector2 pointer)
    {
        Vector2 mid = MiddleOf2Vectors(P1, P2);
        float angle = PerpendicularAngle(P1, P2);
        Vector2 peakOne = mid + new Vector2(radius*Mathf.Cos(angle),radius*Mathf.Sin(angle));
        Vector2 peakTwo = mid - new Vector2(radius*Mathf.Cos(angle),radius*Mathf.Sin(angle));

        return CloserPoint(peakOne, peakTwo, pointer);
    }

    public static float TriangleField(Vector2 pA, Vector2 pB, Vector2 pC)
    {
        return 0.5f * Mathf.Abs((pB.x - pA.x) * (pC.y - pA.y) - (pB.y - pA.y) * (pC.x - pA.x));
    }

    public static float PointToLineDistance(Vector2 linePointA, Vector2 linePointB, Vector2 distantPoint)
    {
        float field = TriangleField(linePointA, linePointB, distantPoint);
        float t_base = (linePointB - linePointA).magnitude;
        return field/(0.5f*t_base);
    }

    public static Vector2 PointCastOnLine(Vector2 linePointA, Vector2 linePointB, Vector2 distantPoint)
    {
        float b = PointToLineDistance(linePointA, linePointB, distantPoint);
        float c = (distantPoint - linePointA).magnitude;
        float a = Mathf.Sqrt(c * c - b * b);
        return linePointA+(linePointB-linePointA).normalized*a;
    }

    public static bool DoesPointCastsOnLine(Vector2 linePointA, Vector2 linePointB, Vector2 distantPoint)
    {
        Vector2 AC = distantPoint - linePointA;
        Vector2 AB = linePointB - linePointA;
        Vector2 BA = linePointA - linePointB;
        Vector2 BC = distantPoint - linePointB;
        float angleOne = Vector2.Angle(AC, AB);
        float angleTwo = Vector2.Angle(BC, BA);
        //Debug.Log("AngleOne: " + angleOne + " | AngleTwo: " + angleTwo);
        return angleOne <= 90f && angleTwo <= 90f;
    }

    public static float VectorAzimuthDeg(Vector2 vector)
    {  
        return Vector2.SignedAngle(vector, Vector2.right);
    }

    public static float VectorAzimuthRad(Vector2 vector)
    {
        return Vector2.SignedAngle(Vector2.right, vector)*(Mathf.PI/180f); 
    }

    //directional straight line
    public static float FactorA(Vector2 pointA, Vector2 pointB)
    {
        return (pointB.y - pointA.y) / (pointB.x - pointA.x);
    }

    public static float FactorB(Vector2 pointA, Vector2 pointB)
    {
        float A = FactorA(pointA, pointB);

        return pointA.y - A * pointA.x ;
    }

    public static Vector2 LineFactors(Vector2 pointA, Vector2 pointB)
    {
        return new Vector2(FactorA(pointA, pointB),FactorB(pointA,pointB));
    }

    public static float Cross(Vector2 a, Vector2 b)
    {
        return a.x * b.y - a.y * b.x;
    }

    public static T GetItem<T>(T[] array, int index)
    {
        if (index >= array.Length)
            return array[index % array.Length];
        else if (index < 0)
            return array[index % array.Length + array.Length];
        else
            return array[index];
    }
    public static T GetItem<T>(List<T> list, int index)
    {
        if (index >= list.Count)
            return list[index % list.Count];
        else if (index < 0)
            return list[index % list.Count + list.Count];
        else
            return list[index];
    }
}
