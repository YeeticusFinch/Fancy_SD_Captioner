using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Listing : MonoBehaviour
{
    int writeDelay = 0;
    string fancyText = "";
    public string file;
    public Text BoxText;
    public InputField input;

    public Text titleText;

    public GameObject image;

    bool initing = false;

    public void Init(int index, string textFile, string imgFile, Sprite image)
    {
        initing = true;

        this.file = textFile;
        fancyText = FileIO.ReadString(file);
        input.text = fancyText;
        input.textComponent.text = fancyText;

        this.image.GetComponent<Image>().sprite = image;

        Debug.Log(textFile);
        titleText.text = index + ") " + imgFile.Substring(imgFile.LastIndexOf(Path.DirectorySeparatorChar)+1);

        initing = false;
    }
    
    public void ReadStringInput(string s)
    {
        //Debug.Log("ReadStringInput: " + s);
        if (initing)
            return;
        fancyText = s;
        StartCoroutine(WriteToFile());
    }

    string modFile;
    string modString;

    IEnumerator WriteToFile()
    {
        modFile = file;
        modString = fancyText;
        if (writeDelay == 0)
        {
            writeDelay = 5;
            while (writeDelay > 0)
            {
                yield return new WaitForSeconds(0.08f);
                writeDelay--;
            }
            writeDelay = 0;

            FileIO.WriteString(modString, modFile);
            Debug.Log("ReadStringInput: " + fancyText);

        }
        else writeDelay = 5;
    }

}
