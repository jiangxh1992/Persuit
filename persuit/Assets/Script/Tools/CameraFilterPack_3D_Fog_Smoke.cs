using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraFilterPack_3D_Fog_Smoke : MonoBehaviour
{
    public Shader SCShader;
    private float TimeX = 1.0f;
    private Material SCMaterial;
    private Texture2D Texture2;
    [Range(0,1.0f)]
    public float _FogIntensity = 0.3f;

    Material material
    {
        get
        {
            if (SCMaterial == null)
            {
                SCMaterial = new Material(SCShader);
                SCMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return SCMaterial;
        }
    }

    void Start()
    {
        Texture2 = Resources.Load("CameraFilterPack_3D_Myst1") as Texture2D;
        SCShader = Shader.Find("CameraFilterPack/3D_Myst");

        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (SCShader != null)
        {
           // TimeX += Time.deltaTime;
          //  if (TimeX > 100) TimeX = 0;
          //  material.SetFloat("_TimeX", TimeX);
            material.SetTexture("_MainTex2", Texture2);
            material.SetFloat("_FogIntensity", _FogIntensity);
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying != true)
        {
            SCShader = Shader.Find("CameraFilterPack/3D_Myst");
            Texture2 = Resources.Load("CameraFilterPack_3D_Myst1") as Texture2D;
        }
#endif
    }

    void OnDisable()
    {
        if (SCMaterial)
        {
            DestroyImmediate(SCMaterial);
        }
    }
}