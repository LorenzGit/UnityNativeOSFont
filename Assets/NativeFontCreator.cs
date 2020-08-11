using TMPro;
using UnityEngine;

public class NativeFontCreator : MonoBehaviour
{
    //the font to add the fallback to.
    [SerializeField]
    private TMP_FontAsset _fontAsset = null;

    //A list of string to look for in the list of all fonts, add more than one if you want more than one fallback.
    [SerializeField]
    private string[] _requiredFonts = null;
    
    //Only the first available font in this list will be added
    [SerializeField]
    private string[] _additionalFirstAvailableFont = null;

    private void Start()
    {
        string[] allFontsPaths = Font.GetPathsToOSFonts();
        Debug.Log(string.Join("\n", allFontsPaths));

        for (int i = 0; i < _requiredFonts.Length; i++)
        {
            AddFallback(_fontAsset, allFontsPaths, _requiredFonts[i]);
        }
        
        for (int i = 0; i < _additionalFirstAvailableFont.Length; i++)
        {
            if (AddFallback(_fontAsset, allFontsPaths, _additionalFirstAvailableFont[i]))
            {
                break;
            }
        }
    }

    private bool AddFallback(TMP_FontAsset primaryFont, string[] allFontsPaths, string containsName)
    {
        string path = null;
        for (int i = 0; i < allFontsPaths.Length; i++)
        {
            if (allFontsPaths[i].ToLower().Contains(containsName.ToLower()))
            {
                path = allFontsPaths[i];
                break;
            }
        }

        if (string.IsNullOrEmpty(path))
        {
            Debug.Log("Font not found: " + containsName);
            return false;
        }

        Debug.Log("Adding Font: " + path);
        Font osFont = new Font(path);
        TMP_FontAsset fontAsset = TMP_FontAsset.CreateFontAsset(osFont);
        primaryFont.fallbackFontAssetTable.Add(fontAsset);
        return true;
    }
}