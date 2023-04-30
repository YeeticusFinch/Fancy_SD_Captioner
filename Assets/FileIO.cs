using System.IO;

public class FileIO
{

    public static void WriteString(string text, string path)
    {
        if (System.IO.File.Exists(path))
        {
            //Debug.Log("Textfile missing, creating " + textFile);
            File.Delete(path);
        }
        //string path = "Assets/Resources/test.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();
        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        //TextAsset asset = Resources.Load("test");
        //Print the text from the file
        //Debug.Log(asset.text);
    }

    public static string ReadString(string path)
    {
        //string path = "Assets/Resources/test.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string result = reader.ReadToEnd();
        //Debug.Log(reader.ReadToEnd());
        reader.Close();
        return result;
    }
}