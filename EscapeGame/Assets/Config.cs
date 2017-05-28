using System;

[Serializable]
public class Config
{
    public enum ConfigType { Server, Client };

    public string ip;
    public string port;
    public ConfigType type;
}
