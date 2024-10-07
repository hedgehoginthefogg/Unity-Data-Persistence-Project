using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import text mesh pro namespace
using TMPro;
// import UI namespace so can use buttons
using UnityEngine.UI;


public class MenuUIHandler : MonoBehaviour
{
    // Variable to store ref to TMP input game object
    public TMP_InputField inputField;
    // Variable to store ref to Start button
    public Button startButton;

    // Variable to store text component of the TMP input game object
    private string userInput;


    // Start is called before the first frame update
    void Start()
    {
        // Attach the OnStartButtonPressed method to button's onClick event
        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // function called when start button is pressed
    public void OnStartButtonPressed()
    {
        // Get text from the input field and store in userInput variable
        userInput = inputField.text;
        Debug.Log("User input stored: " + userInput);
        // TODO: make sure userInput variable has data persistence between scenes
        // TODO: Launch game
    }




  
}
