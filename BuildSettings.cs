using System;
using UnityEngine;

namespace Build {

    [Serializable]
    public class SettingsJson {
        public string productName;
        public string buildVersion;
        public int buildCode;
        public string bundleIdentifier;
        public string buildScene;
        public string buildTargetFolder;

        public SettingsIOS ios;
        public SettingsAndroid android;
    }

    [Serializable]
    public class SettingsIOS {
        public bool il2cpp;
    }

    [Serializable]
    public class SettingsAndroid {
        public bool il2cpp;
    }
    
    public static class BuildSettings {

        const string settingsPath = "./Assets/Editor/Build/config.json";

        static SettingsJson settings;
        
        static void Prepare() {
            if (settings != null) {
                return;
            }
            
            string content = System.IO.File.ReadAllText(settingsPath);
            settings = JsonUtility.FromJson<SettingsJson>(content);
        }

        public static string productName {
            get {
                Prepare();
                return settings.productName;
            }
        }

        public static string buildVersion {
            get {
                Prepare();
                return settings.buildVersion;
            }
        }

        public static int buildCode {
            get {
                Prepare();
                return settings.buildCode;
            }
        }

        public static string bundleIdentifier {
            get {
                Prepare();
                return settings.bundleIdentifier;
            }
        }

        public static string buildScene {
            get {
                Prepare();
                return settings.buildScene;
            }
        }

        public static string buildTargetFolder {
            get {
                Prepare();
                return settings.buildTargetFolder;
            }
        }

        public static bool il2cpp_IOS {
            get {
                Prepare();
                return settings.ios.il2cpp;
            }
        }

        public static bool il2cpp_Android {
            get {
                Prepare();
                return settings.android.il2cpp;
            }
        }
    }
}