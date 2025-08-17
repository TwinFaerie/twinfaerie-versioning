using UnityEditor;
using System;

namespace TF.Versioning.Editor
{
    public static class VersioningCI
    {
        public static void SetVersion()
        {
            // Set version
            string version = Environment.GetEnvironmentVariable("CI_TAG") ?? "0.0.1";
            PlayerSettings.bundleVersion = version;

            string buildIdStr = Environment.GetEnvironmentVariable("CI_BUILD_ID") ?? "1";
            if (int.TryParse(buildIdStr, out int buildId)) PlayerSettings.Android.bundleVersionCode = buildId;
        }
    }
}
