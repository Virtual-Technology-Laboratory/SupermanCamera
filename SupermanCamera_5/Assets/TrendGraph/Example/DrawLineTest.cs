using UnityEngine;
using System.Collections;

using VTL.TrendGraph;
public class DrawLineTest : MonoBehaviour
{

    void OnGUI()
    {
        Drawing.DrawLine(new Vector2(0,0), new Vector2(100,100), Color.white, 1, true);
    }
}