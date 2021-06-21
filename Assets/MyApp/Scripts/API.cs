using FlexFramework.Excel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
    const string csv = "https://docs.google.com/spreadsheets/d/1LnQJGui7RP4uJIdrF7n0vCfDfNT-Ucv8CqW5xBsPXc4/export?format=csv";
    const string xlsx = "https://docs.google.com/spreadsheets/d/1LnQJGui7RP4uJIdrF7n0vCfDfNT-Ucv8CqW5xBsPXc4/export?format=xlsx";

    List<Row> rowData = new List<Row>();
    [HideInInspector]
    public DataContainer dataList = new DataContainer();
    public void GetData(UnityAction<DataContainer> callback)
    {
        StartCoroutine(GetDataRoutine(callback));
    }

    IEnumerator GetDataRoutine(UnityAction<DataContainer> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(csv);
        yield return request.SendWebRequest();
        rowData = new List<Row>();
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

    DataContainer ParseData(List<Row> rowData)
    {
        DataContainer dataList = new DataContainer();
        //Get First Line Data 
        Row rowHeader = rowData[0];
        for (int i = 0; i < rowHeader.Count; i++)
        {
            Cell cell = rowHeader[i];
            dataList.Header.Add(cell.Text);
        }
        //Get Content Data
        for (int i = 1; i < rowData.Count; i++)
        {
            Row rowContent = rowData[i];
            RowData rD = new RowData();
            for (int j = 0; j < rowContent.Cells.Count; j++)
            {
                rD.Row.Add(rowContent.Cells[j]);
            }
            dataList.Content.Add(rD);
        }
        return dataList;
    }
}
