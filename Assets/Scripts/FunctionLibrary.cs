using UnityEngine;

using static UnityEngine.Mathf;

public static class FunctionLibrary
{

	public delegate Vector3 Function(float u, float v, float t);

	public enum FunctionName { Wave, MultiWave, Ripple, Sphere, Torus , Flower , Something , FlowerSnap }

	static Function[] functions = { Wave, MultiWave, Ripple, Sphere, Torus , Flower , Something, FlowerSnap };

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
		p.x = (float)((1 + 0.5 * Cos(8 * Mathf.PI * u)) * Cos(2 * Mathf.PI * v));
		p.y = (float)((1 + 0.5 * Cos(8 * Mathf.PI * u)) * Sin(2 * Mathf.PI * v));
		p.z = (float)(0.5 * Sin(8 * Mathf.PI * u));
		return p;
	}
}