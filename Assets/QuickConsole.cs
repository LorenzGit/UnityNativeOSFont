using TMPro;
using UnityEngine;

public class QuickConsole : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text = null;
    
    void Awake()
    {
        //Every time you write Debug.Log somewhere this handles it.
        Application.logMessageReceived += HandleLog;
    }
    
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        _text.text += $"{type}: {logString}\n{stackTrace}\n\n";
    }
}
