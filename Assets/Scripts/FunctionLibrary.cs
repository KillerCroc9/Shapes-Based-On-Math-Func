using UnityEngine;

using static UnityEngine.Mathf;
using static UnityEngine.Rendering.DebugUI;

public static class FunctionLibrary
{

	public delegate Vector3 Function(float u, float v, float t);

	public enum FunctionName { Wave, MultiWave, Ripple, Sphere, Torus , Flower , Something , FlowerSnap , Heart   }

	static Function[] functions = { Wave, MultiWave, Ripple, Sphere, Torus , Flower , Something, FlowerSnap , Heart   };

	public static Function GetFunction(FunctionName name)
	{
		return functions[(int)name];
	}

	public static Vector3 Wave(float u, float v, float t)
	{
		Vector3 p;
		p.x = u;
		p.y = Sin(PI * (u + v + t));
		p.z = v;
		return p;
	}

	public static Vector3 MultiWave(float u, float v, float t)
	{
		Vector3 p;
		p.x = u;
		p.y = Sin(PI * (u + 0.5f * t));
		p.y += 0.5f * Sin(2f * PI * (v + t));
		p.y += Sin(PI * (u + v + 0.25f * t));
		p.y *= 1f / 2.5f;
		p.z = v;
		return p;
	}

	public static Vector3 Ripple(float u, float v, float t)
	{
		float d = Sqrt(u * u + v * v);
		Vector3 p;
		p.x = u;
		p.y = Sin(PI * (4f * d - t));
		p.y /= 1f + 10f * d;
		p.z = v;
		return p;
	}

	public static Vector3 Sphere(float u, float v, float t)
	{
		float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
		float s = r * Cos(0.5f * PI * v);
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r * Sin(0.5f * PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}

	public static Vector3 Torus(float u, float v, float t)
	{
		float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
		float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
		float s = r1 + r2 * Cos(PI * v);
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r2 * Sin(PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}
	public static Vector3 FlowerSnap(float u, float v, float t)
	{
		
		Vector3 p;
		p.x = u * Cos(v)*Sin(t);
		p.y = u * Sin(v);
		p.z = u / 2 * Sin(u);
		return p;
	}
	public static Vector3 Something(float u, float v, float t)
	{

		Vector3 p;
		p.x = u * Sin(v)* Sin(t);
		p.y = u * Cos(v) ;
		p.z = u * Tan(u) ;
		return p;
	}
	public static Vector3 Flower(float u, float v, float t)
	{

		Vector3 p;
		p.x = (float)((1 + 0.5 * Cos(8 * PI * u + t)) * Cos(2 * PI * v));
		p.y = (float)((1 + 0.5 * Cos(8 * PI * u + t)) * Sin(2 * PI * v));
		p.z = (float)(0.5 * Sin(8 * PI * u + t));
      
        return p;
	}
    public static Vector3 Heart(float u, float v, float t)
    {
        Vector3 p;
		if (v <= 0)
		{
			p.x = 0.2f * 5 * (Pow(Sin(u), 3)) * -Sin(v);
			p.y = -0.2f * ((13f * Cos(2 * u)) - (5 * Cos(2 * u)) - (2 * Cos(3 * u)) - Cos(4 * u)) + .5f - .4f;
			p.z = v * -Abs(Sin(t * 2));
		}
	    else
		{
            p.x = 0.2f * 5 * (Pow(Sin(u), 3)) * -Sin((v - 1));
            p.y = 0.2f * ((13f * Cos(u)) - (5 * Cos(u)) - (2 * Cos(2 * u)) - Cos(4 * u)) - .55f - .4f;
            p.z = (v - 1f) * -Abs(Sin(t * 2));
        }
        
        return p;
    }
   
	public static Vector3 Morph(float u, float v, float t, Function from, Function to, float progress)
	{
		return Vector3.LerpUnclamped(from(u, v, t), to(u, v, t), SmoothStep(0f, 1f, progress));
	}

}