using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class NewFile : MonoBehaviour
{
    public InputField subjectID;

    public void MakeNewFile()
    {
        string fileName = subjectID.text;
        string pathway = Environment.CurrentDirectory;

        string path = pathway + "/TestTextFolder/" + fileName;

        Debug.Log(path);



        StreamWriter newFile = File.CreateText(path);
        newFile.WriteLine("Sucess!");
        newFile.WriteLine("This is the subject ID {0}", fileName);
        newFile.Close();
    }
    
}
