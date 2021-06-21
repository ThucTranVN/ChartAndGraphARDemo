using UnityEngine;
using System.Collections;
using ChartAndGraph;

public class BarChartFeed : MonoBehaviour {

    public ChartDynamicMaterial material;

    void Start () {
        BarChart barChart = GetComponent<BarChart>();
        if (barChart != null)
        {
            //barChart.DataSource.SetValue("Thuc", "Physic", 15);
            //barChart.DataSource.SlideValue("Thuc", "Physic", 15, 2f);
            barChart.DataSource.AddCategory("TEst", material);
            barChart.DataSource.SlideValue("TEst", "Math", 5, 2f);
            barChart.DataSource.SlideValue("TEst", "Physic", 10, 2f);
            barChart.DataSource.SlideValue("TEst", "Chemistry", 15, 2f);
        }
    }
    private void Update()
    {

    }
}
