using UnityEngine;
using System.Collections;

public class CSVParser  {

	private static CSVParser _instance;

	private CSVParser() {}

	public static CSVParser Instance
	{
		get
		{
			if (_instance == null)
				_instance = new CSVParser();

			return _instance;
		}
	}

	public static ArrayList ParseCSV(string rawCSV)
	{
		ArrayList rows = new ArrayList();
		// Remove the first \r\n which is useless
		if (rawCSV.StartsWith("\r\n"))
		{
			rawCSV = rawCSV.Substring(2, rawCSV.Length-2);
		}
		string[] rowSplit = rawCSV.Split(new string[] {"\r\n"}, System.StringSplitOptions.RemoveEmptyEntries);
		foreach (string row in rowSplit)
		{
			string[] elemSplit = row.Split (new string[] {","}, System.StringSplitOptions.RemoveEmptyEntries);
			ArrayList currentRow = new ArrayList();
			foreach (string elem in elemSplit)
			{
				currentRow.Add(elem);
			}
			rows.Add(currentRow);
		}
		return rows;
	}
}
