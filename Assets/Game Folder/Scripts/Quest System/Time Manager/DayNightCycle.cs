using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] Light DirectionalLight;
    [SerializeField] LightningPreset Preset;

    [SerializeField] int TimeOfDay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Preset == null) return;
        //TimeOfDay = TimeManager.Hour;
        UpdateLight(TimeOfDay/24f);
    }
    
    void UpdateLight(float Time)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(Time);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(Time);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(Time);
            DirectionalLight.transform.rotation = Quaternion.Euler(new Vector3((Time * 360f)-90f, -170f,0));
        }
    }

    private void OnValidate()
    {
        
    }
}
