using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermsOfUseButton : MonoBehaviour
{
    public void OpenTermsOfUse()
    {
        Application.OpenURL("https://wearerosenoire.notion.site/Rulebook-21a4d61df3b948a2837a5144901ac4a2");
    }

    /*public void OpenDesignerLink()
    {
        Application.OpenURL("https://www.linkedin.com/in/fregonese-tom");
    }*/

    public void OpenEditorLink()
    {
        Application.OpenURL("https://www.wearerosenoire.com");
    }
}
