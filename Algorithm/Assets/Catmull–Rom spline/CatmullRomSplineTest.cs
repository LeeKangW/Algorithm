using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmullRomSpline
{
    public enum SplineType
    {
        Uniform,
        Centripetal,
        Chordal,
    }

    Vector3 _p0, _p1, _p2, _p3;
    SplineType _type;

    public CatmullRomSpline(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, SplineType type)
    {
        (_p0, _p1, _p2, _p3) = (p0, p1, p2, p3);

        _type = type;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="interpolationValue">0: ∞Óº±¿« Ω√¡° / 1: ∞Óº±¿« ¡æ¡°</param>
    /// <returns></returns>
    public Vector3 GetPoint(float interpolationValue)
    {
        float t0 = 0;
        float t1 = GetNextT(t0, _p0, _p1);
        float t2 = GetNextT(t1, _p1, _p2);
        float t3 = GetNextT(t2, _p2, _p3);

        float t = Mathf.Lerp(t1, t2, interpolationValue);

        Vector3 A1 = (t1 - t) / (t1 - t0) * _p0 + (t - t0) / (t1 - t0) * _p1;
        Vector3 A2 = (t2 - t) / (t2 - t1) * _p1 + (t - t1) / (t2 - t1) * _p2;
        Vector3 A3 = (t3 - t) / (t3 - t2) * _p2 + (t - t2) / (t3 - t2) * _p3;
        Vector3 B1 = (t2 - t) / (t2 - t0) * A1 + (t - t0) / (t2 - t0) * A2;
        Vector3 B2 = (t3 - t) / (t3 - t1) * A2 + (t - t1) / (t3 - t1) * A3;
        Vector3 C = (t2 - t) / (t2 - t1) * B1 + (t - t1) / (t2 - t1) * B2;

        return C;
    }

    float GetNextT(float t, Vector3 p0, Vector3 p1)
    {
        return Mathf.Pow(Vector3.SqrMagnitude(p1 - p0), 0.5f * GetAlpha(_type)) + t;
    }

    float GetAlpha(SplineType type)
    {
        switch (type)
        {
            case SplineType.Uniform:
                return 0f;
            case SplineType.Centripetal:
                return 0.5f;
            case SplineType.Chordal:
                return 1f;
            default:
                return 0f;
        }
    }
}

public class CatmullRomSplineTest : MonoBehaviour
{
    [SerializeField, Range(0, 1)]
    float interval = 0;

    [SerializeField]
    CatmullRomSpline.SplineType _splineType;

    [SerializeField]
    List<Transform> _points;


    int _gizmoDetail = 100;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(_points != null &&
            _points.Count > 3)
        {
            CatmullRomSpline spline = new CatmullRomSpline(_points[0].position,
                _points[1].position,
                _points[2].position,
                _points[3].position,
                _splineType);

            Vector3 prevP = spline.GetPoint(0);
            for (int i = 1; i < _gizmoDetail; i++)
            {
                Vector3 p = spline.GetPoint((float)i / _gizmoDetail);
                Gizmos.DrawLine(prevP, p);
                prevP = p;
            }
        }
    }
}
