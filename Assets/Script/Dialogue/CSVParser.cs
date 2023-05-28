using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVParser : MonoBehaviour
{
    public static CSVParser instance;

    [Header("제목 행 인덱스")]
    public int titleLine;

    [Header("파일명")]
    public string fileName;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    public List<Dictionary<string, string>> ParseCSVFile()
    {
        if (File.Exists(fileName) == false)
        {
            Debug.Log("파일 이름 혹은 경로를 확인하세요");
            return null;
        }

        // CSV 파일 읽기
        string[] csvLines = File.ReadAllLines(fileName);

        if (csvLines.Length < 4)
        {
            Debug.LogError("CSV 파일이 유효하지 않습니다.");
            return null;
        }

        // 컬럼명 추출
        string[] columnNames = csvLines[titleLine - 1].Split(',');

        // 데이터 파싱
        List<Dictionary<string, string>> parsedData = new List<Dictionary<string, string>>();
        for (int i = titleLine; i < csvLines.Length; i++)
        {
            Debug.Log(csvLines[i]);
            // 따옴표로 감싸진 텍스트인 경우 따옴표 제거하고 이스케이프된 따옴표 처리
            if (csvLines[i].StartsWith("\"") && csvLines[i].EndsWith("\""))
            {
                csvLines[i] = csvLines[i].Substring(1, csvLines[i].Length - 2); // 따옴표 제거
                csvLines[i] = csvLines[i].Replace("\"\"", "\""); // 이스케이프된 따옴표 처리
            }
            Debug.Log(csvLines[i]);

            string[] dataValues = csvLines[i].Split(',');
            Dictionary<string, string> rowData = new Dictionary<string, string>();

            // 컬럼명과 데이터 값을 매칭하여 딕셔너리에 저장
            for (int j = 0; j < columnNames.Length; j++)
            {
                string columnName = columnNames[j];
                string dataValue = (j < dataValues.Length) ? dataValues[j] : string.Empty;

                rowData[columnName] = dataValue;
            }

            parsedData.Add(rowData);
        }

        return parsedData;
    }

    public List<Dictionary<string, string>> ParseCSVFile_EventID(string eventID)
    {
        List<Dictionary<string, string>> parsedData = ParseCSVFile();
        List<Dictionary<string, string>> returnData = parsedData.Where(row => row.ContainsKey("Event ID") && row["Event ID"] == eventID).ToList();

        return returnData;
    }
}
