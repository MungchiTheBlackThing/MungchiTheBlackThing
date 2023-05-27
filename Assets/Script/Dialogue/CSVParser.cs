using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVParser : MonoBehaviour
{
    [Header("제목 행 인덱스")]
    public int titleLine;

    [Header("파일명")]
    public string fileName;

    private void Start()
    {
        ParseCSVFile(fileName);
    }

    public void ParseCSVFile(string filePath)
    {
        if (File.Exists(filePath) == false)
        {
            Debug.Log("파일 이름 혹은 경로를 확인하세요");
            return;
        }

        // CSV 파일 읽기
        string[] csvLines = File.ReadAllLines(filePath);

        Debug.Log(csvLines.Length);

        if (csvLines.Length < 4)
        {
            Debug.LogError("CSV 파일이 유효하지 않습니다.");
            return;
        }

        // 컬럼명 추출
        string[] columnNames = csvLines[titleLine - 1].Split(',');

        // 데이터 파싱
        List<Dictionary<string, string>> parsedData = new List<Dictionary<string, string>>();
        for (int i = titleLine; i < csvLines.Length; i++)
        {
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

        // 파싱된 데이터 사용 예시
        foreach (var row in parsedData)
        {
            Debug.Log("Row Data:");
            foreach (var kvp in row)
            {
                Debug.Log(kvp.Key + ": " + kvp.Value);
            }
        }
    }
}
