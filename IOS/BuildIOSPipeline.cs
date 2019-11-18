using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Build.IOS {
    
    public static class BuildIOSPipeline {

        [MenuItem("Build/BuildIOS")]
        public static void Build() {
            PrepareBuild();
            DoBuild();
        }

        static void PrepareBuild() {
            
        }

        static void DoBuild() {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = new[] { BuildSettings.buildScene };
            buildPlayerOptions.locationPathName = BuildSettings.buildTargetFolder + "iOSBuild";
            buildPlayerOptions.target = BuildTarget.iOS;
            buildPlayerOptions.options = BuildOptions.AcceptExternalModificationsToPlayer;

            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, BuildSettings.bundleIdentifier);
            PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.iOS, true);
            PlayerSettings.productName = BuildSettings.productName;
            
            //shared by android&ios
            PlayerSettings.bundleVersion = BuildSettings.buildVersion;
            
            var buildCode = BuildSettings.buildCode;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, BuildSettings.il2cpp_IOS ? ScriptingImplementation.IL2CPP : ScriptingImplementation.Mono2x);
            
            PlayerSettings.iOS.buildNumber = $"{buildCode}";
            
            PlayerSettings.statusBarHidden = true;
            PlayerSettings.SplashScreen.show = false;
            
            //do build
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("Build failed");
            }
        }
    }
}