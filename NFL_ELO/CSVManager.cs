using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;

public class CSV_Manager
{
    public DataTable Table { get; }
	public CSV_Manager()
	{
		string filePath = Path.Combine(Environment.CurrentDirectory, "games.csv");
		Table = ReadCsvAsTable(filePath);
	}

	static DataTable ReadCsvAsTable(string filePath)
	{
		DataTable dataTable = new DataTable();

		using (TextFieldParser parser = new TextFieldParser(filePath))
		{
			parser.TextFieldType = FieldType.Delimited;
			parser.SetDelimiters(",");

			if (!parser.EndOfData)
			{
				string[] fields = parser.ReadFields();
				foreach (var field in fields)
				{
					dataTable.Columns.Add(new DataColumn(field, typeof(string)));
				}
			}
			while (!parser.EndOfData)
			{
				string[] fields = parser.ReadFields();
				DataRow row = dataTable.NewRow();
				for (int i = 0; i < fields.Length; i++)
				{
					row[i] = fields[i];
				}
				dataTable.Rows.Add(row);
			}
		}
		return dataTable;
	}

	public List<string> GetDistinctValues(DataTable dataTable, string columnName)
	{
		List<string> distinctValues = new List<string>();
		foreach (DataRow row in dataTable.Rows)
		{
			string value = row[columnName].ToString();
			if (!distinctValues.Contains(value))
			{
				distinctValues.Add(value);
			}
		}
		return distinctValues;
	}
}
