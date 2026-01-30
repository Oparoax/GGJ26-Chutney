using UnityEngine;

public static class Extentions
{
    public static Vector3 RandomVector(this Vector3 a, float _min = 0, float _max = 1)
    {
        return new Vector3(Random.Range(_min, _max), Random.Range(_min, _max), Random.Range(_min, _max));
    }
}
