using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;



public class LoadSceneOnClick : MonoBehaviour
{
    public InputField subjectID;
    public InputField age;
    public InputField gender;
    public Dropdown dropDown;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadByIndex(int sceneIndex)
    {

        SceneManager.LoadScene(sceneIndex);

        string fileName = subjectID.text;
        string pathway = Environment.CurrentDirectory;

        string path = pathway + "/TestDataFolder/" + fileName + ".txt";

        int value = dropDown.value;

        StreamWriter newFile = File.CreateText(path);
        newFile.WriteLine("Subject ID, {0}", fileName);
        newFile.WriteLine("Age, {0}", age.text);
        newFile.WriteLine("Gender, {0}", gender.text);
        newFile.WriteLine("Condition, {0}", value);
        newFile.Close();
    }



    private void Update()
    {
        //Debug.Log(age.text);
    }


}













/*
       File.AppendAllText("C:/Users/taylo/Desktop/Vanderbilt/Lab/TestUnityText.txt", subjectID.text + Environment.NewLine);
       File.AppendAllText("C:/Users/taylo/Desktop/Vanderbilt/Lab/TestUnityText.txt", age.text + Environment.NewLine);
       File.AppendAllText("C:/Users/taylo/Desktop/Vanderbilt/Lab/TestUnityText.txt", gender.text + Environment.NewLine);
       //File.AppendAllText("C:/Users/taylor/Desktop/Vanderbilt/Lab/TestUnityText.txt",  + Environment.NewLine);
       */
