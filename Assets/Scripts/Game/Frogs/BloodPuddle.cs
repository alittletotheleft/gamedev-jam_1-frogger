using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BloodPuddle : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.75f;
    private Renderer mesh;
    
    private void Start()
    {
        mesh = GetComponent<Renderer>();
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        mesh.material
.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 1f);
        
        float time = 0f;
        float alpha;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            alpha = Mathf.Lerp(1.0f, 0.0f, time/fadeTime);
            mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, alpha);
            Debug.Log(alpha);
            Debug.Log(mesh.material.color);
            yield return null;
        }

        mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 0f);
    }
}
