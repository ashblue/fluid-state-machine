using System.Linq;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ExportProject {
        [MenuItem("Export/Build Fluid State Machine")]
        static void BuildProject () {
            var files = GetFiles();
            var path = $"{Application.dataPath}/../FluidStateMachine.unitypackage";
            
            AssetDatabase.ExportPackage(files.ToArray(), $"{path}/Fluid Behavior Tree.unitypackage");
        }

        private static List<string> GetFiles () {
            return AssetDatabase
                .FindAssets("", new[] {
                    "Assets/FluidBehaviorTree",
                }).ToList()
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(file => !file.Contains("Test.cs")).ToList();
        }
    }
}