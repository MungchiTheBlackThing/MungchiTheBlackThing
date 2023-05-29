using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

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

    public List<string> GetParseKeys(string csvContent)
    {
        string[] lines = csvContent.Split('\n');
        string[] headers = lines[titleLine - 1].Split(',').Select(header => header.Replace("\r", "")).ToArray();

        return headers.ToList();
    }

    public List<Dictionary<string, string>> ParseCSVFile()
    {
        if (File.Exists(fileName) == false)
        {
            Debug.Log("파일 이름 혹은 경로를 확인하세요");
            return null;
        }

        // CSV 파일 읽기
        string[] lines = File.ReadAllLines(fileName);

        if (lines.Length < 4)
        {
            Debug.LogError("CSV 파일이 유효하지 않습니다.");
            return null;
        }

        // 컬럼명 추출
        string[] columnNames = lines[titleLine - 1].Split(',');

        // 데이터 파싱
        List<Dictionary<string, string>> parsedData = new List<Dictionary<string, string>>();

        string[] headers = lines[2].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line))
                continue;

            string[] values = SplitCSVLine(line);

            Dictionary<string, string> rowData = new Dictionary<string, string>();
            for (int j = 0; j < headers.Length; j++)
            {
                string header = headers[j].Trim();
                string value = values[j].Trim();

                // 따옴표로 감싸진 텍스트인 경우 따옴표 제거하고 이스케이프된 따옴표 처리
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    value = value.Substring(1, value.Length - 2); // 따옴표 제거
                    value = value.Replace("\"\"", "\""); // 이스케이프된 따옴표 처리
                }
                if (value.Contains(@"\n")) value = value.Replace("\\n", "\n"); // 자동으로 문자처리된 \\n을 개행문자 (\n)으로 변경

                rowData[header] = value;
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

    private string[] SplitCSVLine(string line)
    {
        List<string> values = new List<string>();

        Regex csvRegex = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");

        MatchCollection matches = csvRegex.Matches(line);
        foreach (Match match in matches)
        {
            string value = match.Value.TrimStart(',').TrimEnd(','); // 시작과 끝의 쉼표 제거
            values.Add(value);
        }

        return values.ToArray();
    }
}
