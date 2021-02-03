using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Attractor : MonoBehaviour {
    const float G = 667.4f;

    float minDist = 0.0001f;

    Rigidbody rb;
    static List<Attractor> attractors = new List<Attractor>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        attractors.Add(this);
    }
    
    void OnDisable()
    {
        attractors.Remove(this);
    }

    void FixedUpdate()
    {
        foreach(Attractor attractor in attractors)
        {
            if (attractor != this) Attract(attractor);
        }
    }

    void Attract(Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance <= minDist) return;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}
