using UnityEngine;

namespace Data.Runtime
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
    public class DialogueScriptableObject : ScriptableObject
    {
        public AudioClip m_audio;
        [TextArea] public string m_text;
    }
}
