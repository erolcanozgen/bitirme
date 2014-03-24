using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Numerics;
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

    public static double CalculatePValue(int case_yes, int case_no, int control_yes, int control_no)
    {
        double p = 0;

        BigInteger numerator = new BigInteger(0.0) ;
        BigInteger denominator = new BigInteger(0.0);
        int N = case_yes+case_no+control_yes+control_no;
       

        int sumOfcase = case_yes + case_no;
        int sumOfControl = control_yes + control_no;
        int sumOfYes = case_yes + control_yes;
        int sumOfNo = case_no + control_no;



        try
        {
            numerator = (factorial(sumOfcase) * factorial(sumOfControl) * factorial(sumOfYes) * factorial(sumOfNo));
            denominator = (factorial(case_yes)*factorial(case_no)*factorial(control_yes)*factorial(control_no)*factorial(N));

            p = Math.Exp(BigInteger.Log(numerator) - BigInteger.Log(denominator));
            return p;
        }
        catch(Exception ex){
            throw ex;
        }
    }

    private static BigInteger factorial(int n)
    {
        BigInteger result = new BigInteger(1);

        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    public static double CalculatePostHocPower(int case_yes, int case_no, int control_yes, int control_no, double alpha = 0.05)
    {
        double power = 0;
        try
        {
            double n1 = case_yes + case_no;
            double p1 = (double)case_yes / (case_yes + case_no);
            
            double n2 =  control_yes + control_no;
            double p2 = (double)control_yes / (control_yes + control_no);

            double delta = Math.Abs(p2-p1);
            double q1 = 1 - p1;
            double q2 = 1 - p2;

            double pPrime = (p1 + (n1 / n2) * p2) / (1 + (n1/n2));
            double qPrime = 1 - pPrime;

            power = (delta / Math.Sqrt(((p1 * q1) / n1) + ((p2 * q2) / n2))) - (alglib.normaldistr.invnormaldistribution(1 - (alpha / 2)) * (Math.Sqrt(pPrime * qPrime * (1 / n1 + 1 / n2))) / (Math.Sqrt(p1*q1/n1 + p2*q2/n2)));

            return power;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}