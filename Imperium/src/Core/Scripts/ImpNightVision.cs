#region

using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

#endregion

namespace Imperium.Core.Scripts;

public class ImpNightVision : MonoBehaviour
{
    private ImpNightVisionLight Near;
    private ImpNightVisionLight Far;

    internal static ImpNightVision Create() => new GameObject("Imp_NightVision").AddComponent<ImpNightVision>();

    private void Awake()
    {
        var mapCamera = GameObject.Find("MapCamera").transform;
        var position = mapCamera.position;
        transform.SetParent(Imperium.Player.gameplayCamera.transform);

        Near = SetupLight("Near", transform);
        Near.GameObject.transform.position = position + Vector3.down * 80f;
        Near.Data.range = 70f;
        Near.Data.color = new Color(0.875f, 0.788f, 0.791f, 1);

        Far = SetupLight("Far", transform);
        Far.GameObject.transform.position = position + Vector3.down * 30f;
        Far.Data.range = 500f;
    }

    private static ImpNightVisionLight SetupLight(string gameObjectName, Transform parent)
    {
        var obj = new GameObject(gameObjectName);
        obj.transform.SetParent(parent);
        return new()
        {
            GameObject = obj,
            Light = obj.AddComponent<Light>(),
            Data = obj.AddComponent<HDAdditionalLightData>(),
        };
    }

    private void Update()
    {
        // disabling HDAdditionalLightData won't do anything useful
        Near.Light.enabled = Imperium.Player.nightVision.enabled;
        Far.Light.enabled = Imperium.Player.nightVision.enabled;

        // intensity must be set on HDAdditionalLightData as the source of truth
        Near.Data.intensity = Imperium.Settings.Player.NightVision.Value * 100f * 4f * Mathf.PI;
        Far.Data.intensity = Imperium.Settings.Player.NightVision.Value * 1100f * 4f * Mathf.PI;
    }
}

internal record ImpNightVisionLight
{
    internal GameObject GameObject { get; init; }
    internal Light Light { get; init; }
    internal HDAdditionalLightData Data { get; init; }
}