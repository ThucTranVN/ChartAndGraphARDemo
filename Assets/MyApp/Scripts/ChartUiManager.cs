using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ChartAndGraph;

public class ChartUiManager : MonoBehaviour
{
    public GameObject[] Charts;
    public TMP_Text ChartName;
    private int index;

    private void Start()
    {
        ChartName.text = Charts[index].name;
        Charts[index].GetComponent<BarChart>().enabled = true;
        Charts[index].GetComponent<BarChart>().DataSource.AutomaticMaxValue = true;
    }

    public void NextChart()
    {
        // Hide current chart
        Charts[index].GetComponent<BarChart>().enabled = false;

        index++;

        if (index > Charts.Length - 1)
        {
            index = 0;
        }

        // Show next chart
        Charts[index].GetComponent<BarChart>().enabled = true;       
        ChartName.text = Charts[index].name;
        
    }

    public void PreviousChart()
    {
        // Hide current chart
        Charts[index].GetComponent<BarChart>().enabled = false;

        index--;
        if (index < 0)
        {
            index = Charts.Length - 1;
        }

        // Show previous chart
        Charts[index].GetComponent<BarChart>().enabled = true;
        ChartName.text = Charts[index].name;
    }
}
