using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace TF.Versioning.Editor
{
    internal class VersioningEditorWindow : OdinEditorWindow
    {
        [MenuItem("TwinFaerie/Versioning/Open Version Editor", priority = -150)]
        private static void ShowMenu()
        {
            GetWindow<VersioningEditorWindow>().Show();
        }
        
        [BoxGroup("Data", order:0)] [InlineProperty]
        [SerializeField] private VersionData version;
        
        [BoxGroup("Data", order:1)] [ValidateInput("IsVersionValid")] [ShowInInspector] [ReadOnly] 
        private string CurrentVersion => Application.version;

        [TitleGroup("Data/Android", VisibleIf = "IsAndroid", Order = 10)] [LabelText("Build Number")] [InlineProperty]
        [SerializeField] private AndroidBuildNumberData androidBuildNumber;

        private bool IsAndroid => EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android;
        private bool IsVersionValid => Regex.IsMatch(CurrentVersion, VersionData.RegexPattern);

        [HorizontalGroup("Button", order:50)] [Button("Set Version")]
        private void TestButton()
        {
            PlayerSettings.bundleVersion = version.FullVersion;
            var message = $"Version Update Success: \nCurrent Version is {Application.version}";

            if (IsAndroid)
            {
                PlayerSettings.Android.bundleVersionCode = androidBuildNumber.buildNumber + 1;
                message += $"\nAndroid build number is {androidBuildNumber}";
            }
            
            EditorUtility.DisplayDialog("Version Updated", message, "OK");
        }
        
        [HorizontalGroup("Button", order:50)] [Button("Refresh")]
        private void Refresh()
        {
            if (!IsVersionValid) return;
            
            var data = Regex.Match(Application.version, VersionData.RegexPattern);
            
            var major = int.Parse(data.Groups["major"].Value);
            var minor = int.Parse(data.Groups["minor"].Value);
            var patch = int.Parse(data.Groups["patch"].Value);
            
            var labelName = data.Groups["labelName"].Success ? data.Groups["labelName"].Value : null;
            var labelVersion = data.Groups["labelVersion"].Success ? int.Parse(data.Groups["labelVersion"].Value) : -1;
            
            version.Setup(major, minor, patch, labelName, labelVersion);

            if (IsAndroid)
            {
                androidBuildNumber.buildNumber = PlayerSettings.Android.bundleVersionCode;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            Refresh();
        }
    }
}
