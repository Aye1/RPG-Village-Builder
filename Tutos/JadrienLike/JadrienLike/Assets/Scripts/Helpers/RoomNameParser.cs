public static class RoomNameParser
{
    /// <summary>
    /// Extracts the filename without path nor extension
    /// </summary>
    /// <param name="filename"></param>
    /// <returns>The filename without path nor extension</returns>
    public static string GetShortFilename(string filename)
    {
        string shortfile = filename.Replace(".xml", "");
        if (shortfile.StartsWith("Assets"))
        {
            shortfile = shortfile.Replace("Assets/Resources/Rooms\\", "");
        }
        return shortfile;
    }

    /// <summary>
    /// Parses the room number from its short filename
    /// </summary>
    /// <param name="filename"></param>
    /// <returns>The number of the room</returns>
    public static int GetNumberFromFilename(string filename)
    {
        string[] splitName = filename.Split(new char[] { '_' });
        int number = int.Parse(splitName[1]);
        return number;
    }
}
