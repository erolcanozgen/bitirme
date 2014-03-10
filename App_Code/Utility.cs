using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{

    public static float CalculateOrValue(int case_yes, int case_no, int control_yes, int control_no)
    {
        float or_value = 0.0f;
        or_value = (float)((float)case_yes / (float)case_no) / (float)((float)control_yes / (float)control_no);
        return or_value;
    }


}