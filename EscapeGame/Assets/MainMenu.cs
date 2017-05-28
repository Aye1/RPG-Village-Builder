using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Text.RegularExpressions;

public class MainMenu : MonoBehaviour {

    public enum MenuState { Main, CreateConfig, LoadConfig }
    public MenuState menuState;

    public Canvas mainCanvas;
    public Canvas createConfCanvas;

    public Button createConfigButton;
    public Button loadConfigButton;

    public Dropdown typeDropdown;
    public InputField ipInputField;
    public InputField portInputField;

    public Text fileSavePathText;
    public Text errorText;

    private string savePath = "Choisir un fichier";
    private string error = "";
    private string ipPattern = "^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

	// Use this for initialization
	void Start () {
        menuState = MenuState.Main;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCanvas();
        fileSavePathText.text = savePath;
        errorText.text = error;
	}

    private void UpdateErrorMessage()
    {
        if (error != "")
        {
            errorText.text = error;
        }
    }

    private void ResetValues()
    {
        savePath = "Choisir un fichier";
        ipInputField.text = "";
        portInputField.text = "";
        fileSavePathText.text = "";
        typeDropdown.value = 0;
    }

    #region Config Validation
    private bool ValidateConfig()
    {
        error = "";
        bool res = true;
        res = isIpValid(ipInputField.text) && res;
        res = isPortValid(portInputField.text) && res;
        res = isPathValid(fileSavePathText.text) && res;
        return res;
    }

    private bool isIpValid(string ip)
    {
        bool res = Regex.IsMatch(ip, ipPattern);
        if (!res)
            error = error + " IP invalide";
        return res;
    }

    private bool isPortValid(string port)
    {
        int intValue;
        bool isNumeric = int.TryParse(port, out intValue);
        bool res = isNumeric && intValue <= 65535;
        if (!res)
            error = error + " Port invalide";
        return res;
    }

    private bool isPathValid(string path)
    {
        bool res = true;
        if (!res)
            error = error + " Chemin invalide";
        return res;
    }
    #endregion

    #region Canvas visibility management
    private void UpdateCanvas()
    {
        switch(menuState)
        {
            case MenuState.CreateConfig:
                DisplayCanvas(createConfCanvas);
                HideCanvas(mainCanvas);
                break;
            case MenuState.LoadConfig:
                HideCanvas(mainCanvas);
                HideCanvas(createConfCanvas);
                break;
            case MenuState.Main:
                DisplayCanvas(mainCanvas);
                HideCanvas(createConfCanvas);
                break;
        }
    }

    private void HideButton(Button button)
    {
        button.enabled = false;
        button.gameObject.transform.localScale = Vector3.zero;
    }

    private void DisplayButton(Button button)
    {
        button.enabled = true;
        button.gameObject.transform.localScale = Vector3.one;
    }

    private void HideCanvas(Canvas canvas)
    {
        canvas.enabled = false;
        canvas.gameObject.transform.localScale = Vector3.zero;
    }

    private void DisplayCanvas(Canvas canvas)
    {
        canvas.enabled = true;
        canvas.gameObject.transform.localScale = Vector3.one;
    }
    #endregion

    #region Buttons callbacks                  
    public void CreateConfigButtonClicked()
    {
        menuState = MenuState.CreateConfig;
        ResetValues();
    }

    public void LoadConfigButtonClicked()
    {
        menuState = MenuState.LoadConfig;
    }

    public void CancelButtonClicked()
    {
        menuState = MenuState.Main;
    }

    public void SelectFileButtonClicked()
    {
        savePath = EditorUtility.SaveFilePanel("Title", Application.dataPath, "config", "json");
    }

    public void SaveButtonClicked()
    {
        if (ValidateConfig())
        {
            Config config = new Config
            {
                ip = ipInputField.text,
                port = portInputField.text,
                type = typeDropdown.value == 0 ? Config.ConfigType.Server : Config.ConfigType.Client
            };
            string serializedConfig = JsonUtility.ToJson(config);
            System.IO.File.WriteAllText(savePath, serializedConfig);
        }
    }
    #endregion
}
