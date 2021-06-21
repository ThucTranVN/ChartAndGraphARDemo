using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using System;

public class TestDataObject : MonoBehaviour
{
    public Test API;
    BarChart barChart;
    public List<ChartDynamicMaterial> dynamicMaterial;
    float time = 2f;
    int index;
    private void Start()
    {
        barChart = GetComponent<BarChart>();
        API.GetData(OnDataRecievied);
    }

    void OnDataRecievied(List<TestData> dataList)
    {
        StartCoroutine(GetCategories(dataList));
        StartCoroutine(GetGroups(dataList));
    }

    IEnumerator GetCategories(List<TestData> dataList)
    {
        barChart.DataSource.ClearCategories();
        
        foreach (var item in dataList)
        {
            if(item.Name == "Name")
            {
                continue;
            } else
            {
                barChart.DataSource.AddCategory(item.Name, dynamicMaterial[index++]);
            }
        }
        yield return new WaitForEndOfFrame();
    }
    IEnumerator GetGroups(List<TestData> dataList)
    {
        barChart.DataSource.ClearGroups();
        barChart.DataSource.AddGroup(dataList[0].Math);
        barChart.DataSource.AddGroup(dataList[0].Physics);
        barChart.DataSource.AddGroup(dataList[0].Chemistry);
        yield return new WaitForEndOfFrame();
        AddValueToBar(dataList);
    }

    void AddValueToBar(List<TestData> dataList)
    {
        foreach (var item in dataList)
        {
            if (item.Name == "Name")
            {
                continue;
            }
            else
            {
                barChart.DataSource.SlideValue(item.Name, dataList[0].Math, Convert.ToDouble(item.Math), time);
                barChart.DataSource.SlideValue(item.Name, dataList[0].Physics, Convert.ToDouble(item.Physics), time);
                barChart.DataSource.SlideValue(item.Name, dataList[0].Chemistry, Convert.ToDouble(item.Chemistry), time);
            }
        }
    }
}
