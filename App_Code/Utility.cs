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
    public static string CalculateCI(double or_value,double or_variance)
    {
        try
        {
            double ln_or = Math.Log(or_value);
            string CI = (Math.Exp(ln_or - (1.96 * Math.Sqrt(or_variance)))).ToString("0.0000")
                              + "  -  "
                              + (Math.Exp(ln_or + (1.96 * Math.Sqrt(or_variance)))).ToString("0.0000");
            return CI;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

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

    public static double CalculatePostHocPower(double p1,double p2, int n1, int n2, double alpha = 0.05)
    {
        double power = 0;
        double result = 0;
        try
        {
            p1 /= 100;
            p2 /= 100;
            //double n1 = case_yes + case_no;
            //double p1 = (double)case_yes / (case_yes + case_no);
            
            //double n2 =  control_yes + control_no;
            //double p2 = (double)control_yes / (control_yes + control_no);

            double delta = Math.Abs(p2-p1);
            double q1 = 1 - p1;
            double q2 = 1 - p2;

            double pPrime = (p1 + ((double)n1 / n2) * p2) / (1 + ((double)n1 / n2));
            double qPrime = 1 - pPrime;

            power = (delta / Math.Sqrt((double)((p1 * q1) / n1) + (double)((p2 * q2) / n2))) - (alglib.normaldistr.invnormaldistribution(1 - (alpha / 2)) * (Math.Sqrt(pPrime * qPrime * ((double)(1 / n1) + (double)(1 / n2)))) / (Math.Sqrt(p1 * q1 / (double)n1 + p2 * q2 / (double)n2)));
            result = alglib.normaldistr.normaldistribution(power);
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static Dictionary<string, double> CalculateHardyWeingbergEquilibrium(int common_hmozgt, int heterozgt, int rare_homozgt)
    {
        Dictionary<string, double> result = new Dictionary<string, double>();
        double p_allele = 0.0, q_allele = 0.0, x_square=0.0;
        int total = common_hmozgt + heterozgt + rare_homozgt;

        p_allele = (double)(2*common_hmozgt + heterozgt) / (2*(common_hmozgt+heterozgt+rare_homozgt));
        q_allele = 1 - p_allele;
        result.Add("P_allele", p_allele);
        result.Add("Q_allele", q_allele);
        result.Add("Expected_CH", (Math.Pow(p_allele, 2) * total));
        result.Add("Expected_H", (2*p_allele*q_allele*total) );
        result.Add("Expected_RH", (Math.Pow(q_allele, 2) * total));

        x_square = (Math.Pow((common_hmozgt - result["Expected_CH"]), 2) / result["Expected_CH"])
                 + (Math.Pow((heterozgt - result["Expected_H"]), 2) / result["Expected_H"])
                 + (Math.Pow((rare_homozgt - result["Expected_RH"]), 2) / result["Expected_RH"]);


        result.Add("X_square",x_square);


        return result;
    }

    public static double CalculateBonferroniFactor(double alpha, int num_test, double corelation)
    {
        double result = 0.0; double g = 0.0;

        if (corelation == 0) result = alpha / num_test;
        else
        {
            g = Math.Pow(num_test, (1 - corelation));
            result = alpha / g;
        }

        return result;

    }

}