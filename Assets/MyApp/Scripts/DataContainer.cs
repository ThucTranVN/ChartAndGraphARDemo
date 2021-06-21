using System;
using System.Collections.Generic;

[Serializable]
public class DataContainer
{
    public List<string> Header = new List<string>();
    public List<RowData> Content = new List<RowData>();
}
