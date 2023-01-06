using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab;
    [SerializeField, Range(10, 1000)]
    int resolution = 10;
    Transform[] points;

    [SerializeField]
    FunctionLibrary.FunctionName function;

    [SerializeField, Min(0f)]
    float transitionDuration;
    [SerializeField, Min(0f)]
    float duration;
    [SerializeField]
    bool transitioning;
    [SerializeField]
    FunctionLibrary.FunctionName transitionFunction;
    [SerializeField]
    bool key;
    // Start is called before the first frame update
    void Start()
    {
        float step = 2f / resolution;
        var scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!transitioning)
        {
            RegularFunction();
        }
        else 
        {
            duration += Time.deltaTime;
            UpdateFunctionTransition();
                if (duration >= transitionDuration)
                {
                    duration -= transitionDuration;
                if(transitionFunction!= FunctionLibrary.FunctionName.Heart) { 
                    transitionFunction++;
                }
                else
                {
                    transitionFunction = FunctionLibrary.FunctionName.Wave;
                }
                transitioning = false;
                }
        }
    }

    void UpdateFunctionTransition()
    {
        FunctionLibrary.Function
            from = FunctionLibrary.GetFunction(transitionFunction),
            to = FunctionLibrary.GetFunction(function);
        float progress = duration / transitionDuration;
        float time = Time.time;
        float step = 2f / resolution;
        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
        

            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = FunctionLibrary.Morph(
                u, v, time, from, to, progress
            );
        }
       
    }
    public void keycheck()
    {
        key = true;
    }
    void RegularFunction()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        float step = 2f / resolution;
        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            Vector3 vector3 = f(u, v, time);
            points[i].localPosition = f(u, v, time);

        }
        if (Input.GetKeyDown(KeyCode.Space)||key==true)
        {
            key = false;
            transitioning = true;
            if (function == FunctionLibrary.FunctionName.Heart)
            {
                function = FunctionLibrary.FunctionName.Wave;
            }
            else
            {
                function += 1;
            }

        }
    }
}