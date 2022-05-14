using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GlossaryButton : MonoBehaviour
{
    public BookInteractions interactions;
    public string filename;
    public string title;
    public TMP_Text myText;

    public void Init()
    {
        myText.text = title;
    }
    public void onButtonClick()
    {
        interactions.moveToEntry(filename);
    }

}
