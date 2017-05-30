using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;

public class MainMenu : MonoBehaviour {

    public enum MenuState { Main, CreateConfig, ServerLaunched }
    public MenuState menuState;

    public Canvas mainCanvas;
    public Canvas createConfCanvas;
    public ServerWidget serverWidget;
    public ClientWidget clientWidget;

    public Button createConfigButton;
    public Button loadConfigButton;

    public Dropdown typeDropdown;
    public InputField nameInputField;
    public InputField ipInputField;
    public InputField portInputField;

    public Text fileSavePathText;
    public Text errorText;

    private string savePath = "Choisir un fichier";
    private string error = "";
    private string ipPattern = "^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

    private const int NAME_MAX_LENGTH = 100;

	// Use this for initialization
	void Start () {
        menuState = MenuState.Main;
        HideObject(clientWidget);
        HideObject(serverWidget);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCanvas();
        UpdateTypeDropdown();
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

    private void UpdateTypeDropdown()
    {
        if (typeDropdown.value == 0)
        {
            // Server
            ipInputField.readOnly = true;
        } else if (typeDropdown.value == 1)
        {
            ipInputField.readOnly = false;
        }
    }

    private void ResetValues()
    {
        savePath = "Choisir un fichier";
        ipInputField.text = "";
        portInputField.text = "";
        fileSavePathText.text = "";
        typeDropdown.value = 0;
        ipInputField.text = GetCurrentIp();
        error = "";
    }

    private string GetCurrentIp()
    {
        // Internet code, be careful
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

    private void LoadConfigInProperWidget(Config config)
    {
        if (config.type == Config.ConfigType.Server)
        {
            serverWidget.config = config;
            DisplayObject(serverWidget);
            HideObject(clientWidget);
        }
        else
        {
            clientWidget.config = config;
            DisplayObject(clientWidget);
            HideObject(serverWidget);
        }
    }

    #region Config Validation

    // Creates a config using the data in the form
    // The Config may not be valid at this moment
    private Config CreateConfigFromForm()
    {
        Config config = new Config
        {
            name = nameInputField.text,
            ip = ipInputField.text,
            port = portInputField.text,
            type = typeDropdown.value == 0 ? Config.ConfigType.Server : Config.ConfigType.Client
        };
        return config;
    }

    public bool ValidateConfig(Config config)
    {
        error = "";
        bool res = true;
        res = isNameValid(config.name) && res;
        // IP is for client only
        if (typeDropdown.value == 1)
            res = isIpValid(config.ip) && res;
        res = isPortValid(config.port) && res;
        //res = isPathValid(fileSavePathText.text) && res;
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

    private bool isNameValid(string name)
    {
        bool res = name.Length <= NAME_MAX_LENGTH ;
        res = res && name.Length > 0;
        if (!res)
            error = error + " Nom invalide";
        return res;
    }
    #endregion

    #region Object visibility management
    private void UpdateCanvas()
    {
        switch(menuState)
        {
            case MenuState.CreateConfig:
                DisplayCanvas(createConfCanvas);
                HideCanvas(mainCanvas);
                break;
            case MenuState.ServerLaunched:
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

    private void HideObject(Behaviour obj)
    {
        obj.enabled = false;
        obj.gameObject.transform.localScale = Vector3.zero;
    }

    private void DisplayObject(Behaviour obj)
    {
        obj.enabled = true;
        obj.gameObject.transform.localScale = Vector3.one;
    }
    #endregion

    #region UI callbacks                  
    public void CreateConfigButtonClicked()
    {
        menuState = MenuState.CreateConfig;
        ResetValues();
    }

    public void LoadConfigButtonClicked()
    {
        string file = EditorUtility.OpenFilePanel("Ouvrir un fichier de configuration", Application.dataPath, "json");
        string rawText = System.IO.File.ReadAllText(file);
        Config config = JsonUtility.FromJson<Config>(rawText);
        if(ValidateConfig(config))
        {
            LoadConfigInProperWidget(config);
        }
    }

    public void CancelButtonClicked()
    {
        menuState = MenuState.Main;
    }

    public void SelectFileButtonClicked()
    {
        savePath = EditorUtility.SaveFilePanel("Fichier de configuration", Application.dataPath, "config", "json");
    }

    public void SaveButtonClicked()
    {
        Config config = CreateConfigFromForm();
        if (ValidateConfig(config))
        {
            string serializedConfig = JsonUtility.ToJson(config);
            System.IO.File.WriteAllText(savePath, serializedConfig);
            menuState = MenuState.Main;
            LoadConfigInProperWidget(config);
        }
    }

    public void TypeDropdownValueChanged()
    {
        if (typeDropdown.value == 0)
        {
            // Switch to server
            ipInputField.text = GetCurrentIp();
        } else if (typeDropdown.value == 1)
        {
            // Switch to client
            ipInputField.text = "";
        }
    }
    #endregion
}
