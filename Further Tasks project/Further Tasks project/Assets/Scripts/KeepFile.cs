using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEditor;

public class KeepFile : MonoBehaviour
{

    public int count = 0;

    public InputField subjectID; //Initiates the variable subjectID, which will correspond to the text input box "Subject ID"
    public InputField age; //Initiates the variable age, which will correspond to the text input box "Age"
    public InputField gender; //Initiates the variable gender, which will correspond to the text input box "Gender"
    public Dropdown dropDown; //Initiates the variable dropDown, which will correspond to the Dropdown menu "Choose Condition Dropdown"
    string path; //This string will correlate to the path in which the text files will be written ex: "C:\Users\taylo\Desktop\Vanderbilt\Lab\Lab Code"
    StreamWriter newFile; //This variable will represent the new .txt file that is created from the information gathered in the Application / scenes

    
    void Start() //Used for initialization
    {
        DontDestroyOnLoad(gameObject); //Ensures that the gameObject (in this case GameObject) will not be destroyed when the second scene loads. This is so you can continue to read data even after you switch scenes
    }

    public void LoadByIndex(int sceneIndex) //Main function
    {

        if (subjectID.text == "" || age.text == "" || gender.text == "" || dropDown.value == 0) //If the subjectID or age or gender inputs are blank
        {
            LoadByIndex(sceneIndex); //Restart the function LoadByIndex (Don't let the application switch to the next scene until all the necessary information is filled out)
        }

        
        SceneManager.LoadScene(sceneIndex); //Loads the next scene by scene index (Indicies of scenes are managed in the build settings)

        string fileName = subjectID.text; //The subjectID input is put into the string fileName
        string pathway = Environment.CurrentDirectory; //The current path of the file (ex: "C:\Users\taylo\Desktop\Vanderbilt\Lab\Lab Code") is put into the string pathway

        path = pathway + "/TestDataFolder/" + fileName + ".txt"; //The string path is equal to the pathway + the folder in which the data will be stored + the name of the text file (in this care the name of the text file is the Subject ID) + the file type extension (.txt for a text file)

        int value = dropDown.value; //The integer "value" is set equal to the index of the option chosen in dropDown. The index for the first option is 0, 1 for the second, so on and so forth. This can also be seen as the "condition" of the experiment

        newFile = File.CreateText(path); //The new text file is created at the location "path"

        
        newFile.Write("Subject ID,{0};", fileName); //The Subject ID is written to the file with "Subject ID, " proceeding it
        newFile.Write("Age,{0};", age.text); //The age is written to the file with "Age, " proceeding it
        newFile.Write("Gender,{0};", gender.text); //The gender is written to the file with "Gender, " proceeding it
        newFile.Write("Condition,{0};", value); //The condition is written to the file with "Condition, " proceeding it
        newFile.Write("Data,");

        /*
        newFile.WriteLine("SubjectID, Age, Gender, Condition, Data");
        newFile.WriteLine("{0},{1},{2},{3},{4}", fileName, age.text, gender.text, value,"");
        */

    }



    void Update () //This funtion updates every frame (hence the name Update)
    {

        if (Application.isPlaying) //If the application is currently playing
        {
            if (Input.GetKeyDown("return")) //If the user presses the "enter" key
            {

                newFile.Write("This code runs correctly,"); //If the enter key is pressed the line "This code runs correctly!" is written to the file
            }

            if (Input.GetKeyDown(KeyCode.Escape)) //If the escape key is pressed
            {
                EditorApplication.isPlaying = false; //Allows for the Application.Quit() function to properly execute
                Application.Quit(); //Fairly self explanitory, quits the application / playback
            }
        }       

    }

    void OnApplicationQuit () //When the application / playback is stopped
    {
        newFile.Close(); //Close the newly created text file, and all the data written to it
    }
}