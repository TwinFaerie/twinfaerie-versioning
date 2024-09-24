using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TF.Versioning
{
    [Serializable]
    public struct VersionData
    {
        public const string RegexPattern = @"^(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)(-(?<labelName>[a-zA-Z0-9]+))?(\.(?<labelVersion>\d+))?$";
        
        [HorizontalGroup("Main", width:33)] [HideLabel]
        [SerializeField] private int major;
        [HorizontalGroup("Main", width:40)] [LabelText(".")] [LabelWidth(5)]
        [SerializeField] private int minor;
        [HorizontalGroup("Main", width:40)] [LabelText(".")] [LabelWidth(5)]
        [SerializeField] private int patch;
        
        [HorizontalGroup("Main")] [LabelText("-")] [LabelWidth(10)]
        [SerializeField] private string labelName;
        [HorizontalGroup("Main", width:40)] [LabelText(".")] [LabelWidth(5)]
        [SerializeField] private int labelVersion;
            
        public string ShortVersion => $"{major}.{minor}.{patch}";
        public string Label => $"{(string.IsNullOrWhiteSpace(labelName) ? "Undefined" : labelName)}.{labelVersion}";
        public string FullVersion => $"{ShortVersion}-{Label}";

        public void Setup(int major, int minor, int patch, string labelName, int labelVersion)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
            this.labelName = labelName;
            this.labelVersion = labelVersion;
        }
    }
}