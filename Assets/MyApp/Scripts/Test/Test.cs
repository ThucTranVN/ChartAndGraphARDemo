using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using FlexFramework.Excel;

public class Test : MonoBehaviour
{
    const string csv = "https://docs.google.com/spreadsheets/d/1LnQJGui7RP4uJIdrF7n0vCfDfNT-Ucv8CqW5xBsPXc4/export?format=csv";
    const string xlsx = "https://docs.google.com/spreadsheets/d/1LnQJGui7RP4uJIdrF7n0vCfDfNT-Ucv8CqW5xBsPXc4/export?format=xlsx";

    List<Row> rowData = new List<Row>();
    List<TestData> dataList = new List<TestData>();
   
    public void GetData(UnityAction<List<TestData>> callback)
    {
        StartCoroutine(GetDataRoutine(callback));
    }

    IEnumerator GetDataRoutine(UnityAction<List<TestData>> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(csv);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Network Error" + request.error);
        }
        else
        {
            var bytes = request.downloadHandler.data;
            var doc = Document.Load(bytes);
            foreach (var row in doc)
            {
                rowData.Add(row);
            }
            callback(ParseData(rowData));
        }
    }

    List<TestData> ParseData(List<Row> rowData)
    {
        foreach (var item in rowData)
        {
            TestData testData = new TestData();
            testData.ID = item[0].Text;
            testData.Name = item[1].Text;
            testData.Math = item[2].Text;
            testData.Physics = item[3].Text;
            testData.Chemistry = item[4].Text;
            dataList.Add(testData);
        }
        return dataList;
    }
}
