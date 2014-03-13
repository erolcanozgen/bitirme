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

    public static double CalculateChiSquareWithoutYates(int case_yes, int case_no, int control_yes, int control_no)
    {
        try
        {
            double ChiSquare = 0.0f;

            double total = case_yes + case_no + control_yes + control_no;

            double avrgeOfCaseYes = (double)(((double)case_yes + (double)case_no)*(case_yes+control_yes))/total ;
            double avrgeOfCaseNo = (double)(((double)case_yes + (double)case_no) * (case_no + control_no)) / total;
            double avrgeOfControlYes = (double)(((double)control_yes + (double)control_no) * (control_yes + case_yes)) / total;
            double avrgeOfControlNo = (double)(((double)control_yes + (double)control_no) * (control_no + case_no)) / total;

            ChiSquare = (double)(Math.Pow(((double)case_yes - avrgeOfCaseYes), 2) / avrgeOfCaseYes) + (double)(Math.Pow(((double)control_yes - avrgeOfControlYes), 2) / avrgeOfControlYes)
                        + (double)(Math.Pow(((double)case_no - avrgeOfCaseNo), 2) / avrgeOfCaseNo) + (double)(Math.Pow(((double)control_no - avrgeOfControlNo), 2) / avrgeOfControlNo);
           

            return ChiSquare;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static double CalculateChiSquareWithYates(int case_yes, int case_no, int control_yes, int control_no)
    {
        try
        {
            double ChiSquare = 0.0f;
            double N = case_yes + case_no + control_yes + control_no;
            double ad_minus_bc = Math.Abs(case_yes*control_no - case_no*control_yes);
            double numerator = N * Math.Pow((ad_minus_bc - (double)(N / 2)), 2);
            double denominator = (case_yes+case_no)*(control_yes+control_no)*(case_yes+control_yes)*(case_no+control_no);
            
            ChiSquare = (double)(numerator/denominator);

            return ChiSquare;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



}