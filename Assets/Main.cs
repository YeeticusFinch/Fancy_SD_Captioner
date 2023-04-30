using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using System.IO;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public bool selected = false;

    public string filePath;

    public FileBrowser.OnSuccess onSuccess;
    public FileBrowser.OnCancel onCancel;

    public GameObject[] listings;

    public List<string> textFiles = new List<string>();
    public List<string> imgFiles = new List<string>();
    public List<Sprite> images = new List<Sprite>();

    public int scroll = 0;
    
    void Start()
    {
        FileBrowser.ShowLoadDialog(onSuccess, onCancel, FileBrowser.PickMode.Folders);
        
    }

    void Update()
    {
        if (!FileBrowser.IsOpen && !selected)
        {
            selected = true;
            filePath = FileBrowser.Result[0];
            Debug.Log("FilePath: " + filePath);
            StartCoroutine(LoadThumbnails());
            scroll = 0;
            /*for (int i = 0; i < Mathf.Min(listings.Length, Mathf.Min(textFiles.Count, images.Count)); i++)
            {
                listings[i].GetComponent<Listing>().Init(textFiles[scroll+i], images[scroll+i]);
            }*/
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            scroll += (int)Mathf.Sign(Input.GetAxis("Mouse ScrollWheel"));
            scroll %= textFiles.Count - listings.Length;
            while (scroll < 0)
                scroll += textFiles.Count - listings.Length;
            Debug.Log("Scroll = " + scroll + ", " + Input.GetAxis("Mouse ScrollWheel"));
            for (int i = 0; i < listings.Length; i++)
            {
                listings[i].GetComponent<Listing>().Init(scroll + i, textFiles[scroll+i], imgFiles[scroll + i], images[scroll+i]);
            }
        }
    }

    Texture2D LoadIMG(string filePath)
    {
        //return Resources.Load<Texture2D>(filePath);

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            //var img = System.Drawing.Image.FromFile(filePath);
            //Console.WriteLine(img.Width + "x" + img.Height);
            tex = new Texture2D(1, 1);
            //tex.wrapMode = TextureWrapMode.Clamp;
            ImageConversion.LoadImage(tex, fileData); //..this will auto-resize the texture dimensions.
        }

        return tex;
    }

    IEnumerator LoadThumbnails()
    {
        yield return new WaitForSeconds(0f);
        foreach (string file in System.IO.Directory.GetFiles(filePath))
        {
            if (file.LastIndexOf(".txt") < file.Length - 6)
            {
                Debug.Log("Loading Image: " + file);
                imgFiles.Add(file);
                string textFile = file.Substring(0, file.LastIndexOf('.')) + ".txt";
                textFiles.Add(textFile);
                Debug.Log("Loading Text: " + textFile);
                if (!System.IO.File.Exists(textFile))
                {
                    Debug.Log("Textfile missing, creating " + textFile);
                    FileIO.WriteString("", textFile);
                }

                Texture2D texture = LoadIMG(file);
                images.Add(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero));
                if (images.Count - 1 - scroll < 10)
                {
                    listings[images.Count - 1].GetComponent<Listing>().Init(images.Count - 1 + scroll, textFiles[images.Count - 1 + scroll], imgFiles[images.Count - 1 + scroll], images[images.Count - 1 + scroll]);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

}
