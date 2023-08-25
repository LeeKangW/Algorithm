using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(BoidsTest))]
public class CohesionTest : MonoBehaviour
{
    BoidsTest _test;

    public float radius;

    private void Awake()
    {
        _test = GetComponent<BoidsTest>();
    }

    private void Update()
    {
        var boids = FindObjectsOfType<BoidsTest>();

        var average = Vector3.zero;
        var found = 0;

        foreach (var boid in boids.Where(b => b != _test))
        {
            var diff = boid.transform.position - this.transform.position;
            if(diff.magnitude < radius)
            {
                average += diff;
                found += 1;
            }
        }

        if (found > 0)
        {
            average /= found;
            _test.velocity += Vector3.Lerp(Vector3.zero, average, average.magnitude / radius);
        }
    }
}
