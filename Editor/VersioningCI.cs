using UnityEditor;
using System;

namespace TF.Versioning.Editor
{
    public static class VersioningCI
    {
        public static void SetVersion()
        {
            var version = Environment.GetEnvironmentVariable("CI_TAG") ?? "0.0.1";
            PlayerSettings.bundleVersion = version;

            var buildIdStr = Environment.GetEnvironmentVariable("CI_BUILD_ID") ?? "1";
            if (int.TryParse(buildIdStr, out int buildId))
            {
                PlayerSettings.Android.bundleVersionCode = buildId;
                
                // just hardcode it here for now
                PlayerSettings.bundleVersion += $"-build.{buildId}";
            }
            
            Console.WriteLine($"[TF.VersioningCI] Applied Version: {PlayerSettings.bundleVersion}");
            Console.WriteLine($"[TF.VersioningCI] Applied Android BuildCode: {PlayerSettings.Android.bundleVersionCode}");
        }

        public static void SetAndroidKeystore()
        {
            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = Environment.GetEnvironmentVariable("CI_KEYSTORE_PATH");
            PlayerSettings.Android.keystorePass = Environment.GetEnvironmentVariable("CI_KEYSTORE_PASS");
            PlayerSettings.Android.keyaliasName = Environment.GetEnvironmentVariable("CI_ALIAS_NAME");
            PlayerSettings.Android.keyaliasPass = Environment.GetEnvironmentVariable("CI_ALIAS_PASS");
            
            Console.WriteLine($"[TF.VersioningCI] Using Keystore at path: {PlayerSettings.Android.keystoreName}");
            Console.WriteLine($"[TF.VersioningCI] Using Keystore alias: {PlayerSettings.Android.keyaliasName}");
        }
    }
}
