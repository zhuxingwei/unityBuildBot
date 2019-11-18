using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Build.Android {
    public static class BuildAndroidPipeline {
        [MenuItem("Build/BuildAndroid")]
        public static void Build() {
            PrepareBuild();
            DoBuild();
        }

        static void PrepareBuild() {

        }

        static void DoBuild() {
            {
                BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
                buildPlayerOptions.scenes = new[] {BuildSettings.buildScene};
                buildPlayerOptions.locationPathName = BuildSettings.buildTargetFolder + "androidBuild";
                buildPlayerOptions.target = BuildTarget.Android;
                buildPlayerOptions.options = BuildOptions.AcceptExternalModificationsToPlayer;

                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, BuildSettings.bundleIdentifier);
                PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, true);
                PlayerSettings.productName = BuildSettings.productName;

                PlayerSettings.bundleVersion = BuildSettings.buildVersion;

                PlayerSettings.Android.bundleVersionCode = BuildSettings.buildCode;
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, BuildSettings.il2cpp_Android ? ScriptingImplementation.IL2CPP : ScriptingImplementation.Mono2x);

                PlayerSettings.statusBarHidden = true;
                PlayerSettings.Android.startInFullscreen = false;
                PlayerSettings.Android.renderOutsideSafeArea = true;
                PlayerSettings.SplashScreen.show = true;

                BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
                BuildSummary summary = report.summary;

                if (summary.result == BuildResult.Succeeded) {
                    Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
                }

                if (summary.result == BuildResult.Failed) {
                    Debug.Log("Build failed");
                }
            }
        }
    }
}