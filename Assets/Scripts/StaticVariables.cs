using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables
{  
    public static LayerMask GetLayerMaskFromVar(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }

   public class Layers
    {
        public static LayerMask Default => GetLayerMaskFromVar("Default");
        public static LayerMask TransparentFX => GetLayerMaskFromVar("TransparentFX");
        public static LayerMask IgnoreRaycast => GetLayerMaskFromVar("IgnoreRaycast");
        public static LayerMask Water => GetLayerMaskFromVar("Water");
        public static LayerMask UI => GetLayerMaskFromVar("UI");
        public static LayerMask Paint => GetLayerMaskFromVar("Paint");
        public static LayerMask Player => GetLayerMaskFromVar("Player");
        public static LayerMask Lantern => GetLayerMaskFromVar("Lantern");
        public static LayerMask Edge => GetLayerMaskFromVar("Edge");
        public static LayerMask Ground => GetLayerMaskFromVar("Ground");
    }

    public class Tags
    {
        public static string Untagged = "Untagged";
        public static string Respawn = "Respawn";
        public static string Finish = "Finish";
        public static string EditorOnly = "EditorOnly";
        public static string MainCamera = "MainCamera";
        public static string Player = "Player";
        public static string GameController = "GameController";
    }
}
