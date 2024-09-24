using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace TF.Versioning
{
    [RequireComponent(typeof(TMP_Text))]
    public class SimpleVersionTextController : MonoBehaviour
    {
        [SerializeField] private bool simulateRelease;
        
        private TMP_Text text;
        
        private void Start()
        {
            text = GetComponent<TMP_Text>();

            text.text = !Debug.isDebugBuild || simulateRelease ? 
                GetShortVersion() : 
                Application.version;
        }

        private string GetShortVersion()
        {
            if (!Regex.IsMatch(Application.version, VersionData.RegexPattern)) return Application.version;
            
            var data = Regex.Match(Application.version, VersionData.RegexPattern);
            return $"{data.Groups["major"].Value}.{data.Groups["minor"].Value}.{data.Groups["patch"].Value}";
        }
    }
}
