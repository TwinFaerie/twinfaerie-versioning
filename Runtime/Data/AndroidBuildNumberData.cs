using System;
using Sirenix.OdinInspector;

namespace TF.Versioning
{
    [Serializable]
    public struct AndroidBuildNumberData
    {
        [HorizontalGroup("Main", width:16)] [HideLabel] [VerticalGroup("Main/Fix", PaddingTop = 2f)]
        public bool canModify;
        [HorizontalGroup("Main")] [EnableIf("canModify")] [HideLabel]
        public int buildNumber;
    }
}