using UnityEditor;
using System;

namespace TF.Versioning.Editor
{
    public static class VersioningCI
    {
        public static void SetupBuild()
        {
            SetVersion();
            SetAndroidKeystore();
        }
        
        public static void SetVersion()
        {
            var version = GetArgValue("-buildVersion") ?? "0.1.0";
            PlayerSettings.bundleVersion = version;

            var buildIdStr = GetArgValue("-buildId") ?? "1";
            if (int.TryParse(buildIdStr, out var buildId))
            {
                PlayerSettings.Android.bundleVersionCode = buildId;
                
                // just hardcode it here for now
                PlayerSettings.bundleVersion += $"+build.{buildId}";
            }
            
            AssetDatabase.SaveAssets();
            
            Console.WriteLine($"[TF.VersioningCI] Applied Version: {PlayerSettings.bundleVersion}");
            Console.WriteLine($"[TF.VersioningCI] Applied Android BuildCode: {PlayerSettings.Android.bundleVersionCode}");
        }

        public static void SetAndroidKeystore()
        {
            var keystorePath = GetArgValue("-keystorePath");
            if (string.IsNullOrWhiteSpace(keystorePath)) return;
            
            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = GetArgValue("-keystorePath");
            PlayerSettings.Android.keystorePass = GetArgValue("-keystorePass");
            PlayerSettings.Android.keyaliasName = GetArgValue("-keystoreAliasName");
            PlayerSettings.Android.keyaliasPass = GetArgValue("-keystoreAliasPass");
            
            AssetDatabase.SaveAssets();
            
            Console.WriteLine($"[TF.VersioningCI] Using Keystore at path: {PlayerSettings.Android.keystoreName}");
            Console.WriteLine($"[TF.VersioningCI] Using Keystore alias: {PlayerSettings.Android.keyaliasName}");
        }
        
        private static string GetArgValue(string argName)
        {
            var args = Environment.GetCommandLineArgs();

            for (var i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Equals(argName, StringComparison.OrdinalIgnoreCase))
                {
                    return args[i + 1];
                }
            }

            return null;
        }
    }
}
