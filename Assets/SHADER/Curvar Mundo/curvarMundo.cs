using UnityEngine;

public class curvarMundo : MonoBehaviour
{
    public float Speed = 10f;

    [Range(-1f, 1f)] public float BendX = 0.1f;
    [Range(-1f, 1f)] public float BendY = 0.1f;

    public Material[] materials;

    void Start()
    {
    }

    void LateUpdate()
    {
        foreach (var m in materials)
        {
            m.SetFloat("_CurveX", BendX);
            m.SetFloat("_CurveY", BendY);
        }

        transform.Translate(Vector3.back * Speed * Time.deltaTime);
    }
}