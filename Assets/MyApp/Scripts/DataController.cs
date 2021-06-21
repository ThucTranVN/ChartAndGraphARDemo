using ChartAndGraph;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public API api;
    BarChart barChart;
    public List<ChartDynamicMaterial> dynamicMaterial;
    private float time = 2f;
    private int index;
    public DataContainer dataContainers, newDataContainers;
    private List<string> lstCategory = new List<string>();
    private List<string> lstGroup = new List<string>();
    double maxValue = 0;

    void Awake()
    {
        barChart = GetComponent<BarChart>();
        StartCoroutine(RefreshData());
        barChart.DataSource.AutomaticMaxValue = true;
    }

    IEnumerator RefreshData()
    {
        yield return new WaitForSeconds(.5f);
        api.GetData(OnDataRecievied);
    }

    void OnDataRecievied(DataContainer dataList)
    {
        newDataContainers = dataList; //for debug
                                      //if (newDataContainers == dataContainers)
                                      //return;
        dataContainers = newDataContainers;
        StartCoroutine(GetAndSetDataToChart(newDataContainers));
    }

    IEnumerator GetAndSetDataToChart(DataContainer dataList)
    {
        //GetCategories
        for (int i = 0; i < dataList.Content.Count; i++)
        {
            if (!barChart.DataSource.HasCategory(dataList.Content[i].Row[0].ToString()))
                barChart.DataSource.AddCategory(dataList.Content[i].Row[0].ToString(), dynamicMaterial[index++]);
        }

        yield return new WaitForEndOfFrame();

        //GetGroups
        for (int i = 1; i < dataList.Header.Count; i++)
        {
            if (!barChart.DataSource.HasGroup(dataList.Header[i].ToString()))
                barChart.DataSource.AddGroup(dataList.Header[i].ToString());
        }
        yield return new WaitForEndOfFrame();

        //SetValue
        for (int i = 0; i < barChart.DataSource.TotalCategories; i++)
        {
            //check row remove and catelory
            if (barChart.DataSource.TotalCategories > dataList.Content.Count)
            {
                barChart.DataSource.RemoveCategory(dataList.Content[i].Row[0]);
                break;
            }
            string cat = dataList.Content[i].Row[0];
            int index = i;
            for (int j = 1; j < dataList.Header.Count; j++)
            {

                string group = dataList.Header[j];
                double value = 0;
                double.TryParse(dataList.Content[index].Row[j], out value);
                if (value > maxValue)
                    maxValue = value;
                // double value = Convert.ToDouble(dataList.Content[index].Row[j].ToString());
                barChart.DataSource.SlideValue(cat, group, value, time);
            }
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(RefreshData());
    }

    //IEnumerator GetCategories(DataContainer dataList)
    //{
    //    for (int i = 0; i < dataList.Content.Count; i++)
    //    {
    //        barChart.DataSource.AddCategory(dataList.Content[i].Row[0].ToString(), dynamicMaterial[index++]);
    //    }
    //    yield return new WaitForEndOfFrame();
    //}

    //IEnumerator GetGroups(DataContainer dataList)
    //{
    //    for (int i = 1; i < dataList.Header.Count; i++)
    //    {
    //        barChart.DataSource.AddGroup(dataList.Header[i].ToString());
    //    }
    //    yield return new WaitForEndOfFrame();
    //}


    //IEnumerator SetValue(DataContainer dataList)
    //{
    //    for (int i = 0; i < barChart.DataSource.TotalCategories; i++)
    //    {
    //        string cat = dataList.Content[i].Row[0];
    //        int index = i;
    //        for (int j = 1; j < dataList.Header.Count; j++)
    //        {
    //            string group = dataList.Header[j];
    //            double value = Convert.ToDouble(dataList.Content[index].Row[j]);
    //            barChart.DataSource.SlideValue(cat, group, value, time);
    //        }
    //    }

    //    //barChart.DataSource.SetValue(category, group, amount);
    //    //Debug.Log(dataList.Content[0].Row[0]); //cat
    //    //Debug.Log(dataList.Header[1]); //group
    //    //Debug.Log(dataList.Content[0].Row[1]); //value

    //    //Debug.Log(dataList.Header[2]); //group
    //    //Debug.Log(dataList.Content[0].Row[2]); //value

    //    //Debug.Log(dataList.Header[3]); //group
    //    //Debug.Log(dataList.Content[0].Row[3]); //value
    //    ////Math
    //    //barChart.DataSource.SlideValue(dataList.Content[0].Row[0], dataList.Header[1], Convert.ToDouble(dataList.Content[0].Row[1]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[1].Row[0], dataList.Header[1], Convert.ToDouble(dataList.Content[1].Row[1]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[2].Row[0], dataList.Header[1], Convert.ToDouble(dataList.Content[2].Row[1]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[3].Row[0], dataList.Header[1], Convert.ToDouble(dataList.Content[3].Row[1]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[4].Row[0], dataList.Header[1], Convert.ToDouble(dataList.Content[4].Row[1]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[5].Row[0], dataList.Header[1], Convert.ToDouble(dataList.Content[5].Row[1]), time);
    //    ////Physic
    //    //barChart.DataSource.SlideValue(dataList.Content[0].Row[0], dataList.Header[2], Convert.ToDouble(dataList.Content[0].Row[2]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[1].Row[0], dataList.Header[2], Convert.ToDouble(dataList.Content[1].Row[2]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[2].Row[0], dataList.Header[2], Convert.ToDouble(dataList.Content[2].Row[2]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[3].Row[0], dataList.Header[2], Convert.ToDouble(dataList.Content[3].Row[2]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[4].Row[0], dataList.Header[2], Convert.ToDouble(dataList.Content[4].Row[2]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[5].Row[0], dataList.Header[2], Convert.ToDouble(dataList.Content[5].Row[2]), time);
    //    ////Chemistry
    //    //barChart.DataSource.SlideValue(dataList.Content[0].Row[0], dataList.Header[3], Convert.ToDouble(dataList.Content[0].Row[3]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[1].Row[0], dataList.Header[3], Convert.ToDouble(dataList.Content[1].Row[3]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[2].Row[0], dataList.Header[3], Convert.ToDouble(dataList.Content[2].Row[3]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[3].Row[0], dataList.Header[3], Convert.ToDouble(dataList.Content[3].Row[3]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[4].Row[0], dataList.Header[3], Convert.ToDouble(dataList.Content[4].Row[3]), time);
    //    //barChart.DataSource.SlideValue(dataList.Content[5].Row[0], dataList.Header[3], Convert.ToDouble(dataList.Content[5].Row[3]), time);
    //    yield return new WaitForEndOfFrame();
    //}
}
