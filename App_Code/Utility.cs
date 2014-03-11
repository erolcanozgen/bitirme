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
        try
        {
            float or_value = 0.0f;
            or_value = (float)((float)case_yes / (float)case_no) / (float)((float)control_yes / (float)control_no);
            return or_value;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static float CalculateRRValue(int case_yes, int case_no, int control_yes, int control_no)
    {
        try
        {
            float rr_value = 0.0f;
            rr_value = (float)((float)case_yes / (float)(case_no + case_yes)) / (float)((float)control_yes / (float)(control_no + control_yes));
            return rr_value;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static float CalculateOrVariance(int case_yes, int case_no, int control_yes, int control_no)
    {
        try
        {
            float variance = 0.0f;
            variance = (float)((float)1 / (float)case_yes) + (float)((float)1 / (float)case_no) + (float)((float)1 / (float)control_yes)
                        + (float)((float)1 / (float)control_no);
            return variance;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static float CalculateRrVariance(int case_yes, int case_count, int control_yes, int control_count)
    {
        try
        {
            float variance = 0.0f;
            variance = (float)((float)1 / (float)case_yes) - (float)((float)1 / (float)case_count) + (float)((float)1 / (float)control_yes)
                        - (float)((float)1 / (float)control_count);
            return variance;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}