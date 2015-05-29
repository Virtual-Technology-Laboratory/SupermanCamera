using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using VTL;
using VTL.TrendGraph;
using VTL.SupermanCamera;

public class TrendGraphDataAdapter : MonoBehaviour
{
    public TrendGraphController trendGraph;
    public SupermanCamera supermanCamera;


    void Update()
    {
        var time = System.DateTime.UtcNow;
        trendGraph.Add(time, supermanCamera.gravity);
    }
}
