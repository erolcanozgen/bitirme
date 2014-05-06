using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MetaAnalaysis
/// </summary>
/// 

public class Experiments
{
    public double success;
    public double fail;
};

public class Studies 
{
    public Experiments Control;
    public Experiments Case;
    public double or_value;
    public double weight;
    public double variance;
    public double related_weight;
    

    public Studies(Experiments Cntrl, Experiments Case)
    {
        this.Control = Cntrl;
        this.Case = Case;
        this.or_value = (float)(Case.success / Case.fail) / (float)(Control.success / Control.fail);
        this.variance = (double)(((double)1 / Case.success) + ((double)1 / Case.fail) + ((double)1 / Control.success) + ((double)1 / Control.fail));
        //this.weight = (Case.fail + Control.success)/(Case.success+Case.fail+Control.success+Control.fail);
        this.weight = (double)(1 / this.variance);

    }
};

public class MetaAnalaysis
{
    string disease_name;
    string snp;
    double i_square;
    double cochran_q;
    List<Studies> studies;
    double sumofWeight;
    double sumofWeigtedEffect;
    double ec;
    double SE;
    double Z;
    public double combined_or;

	public MetaAnalaysis(string disease_name, string snp)
	{
        this.disease_name = disease_name;
        this.snp = snp;
        studies = new List<Studies>();
        i_square = 0.0; cochran_q = 0.0; this.SE = 0.0; this.Z = 0.0;combined_or=0.0;
        sumofWeight = 0.0;
        sumofWeigtedEffect = 0.0;
        DataTable tmp = new DataTable();
        DiseaseDetails desease = new DiseaseDetails();
        tmp = desease.SelectWithSnp(this.disease_name,this.snp);

        for (int i = 0; i < tmp.Rows.Count; i++)
        {
            Experiments Case = new Experiments();
            Experiments Cntrl = new Experiments();

            Case.success = (double)(Convert.ToInt32(tmp.Rows[i].ItemArray[2])) * Convert.ToDouble((tmp.Rows[i].ItemArray[8].ToString()).Replace(",", "."));
            Case.fail = (double)(Convert.ToInt32(tmp.Rows[i].ItemArray[2])) - Case.success;

            Cntrl.success = (double)(Convert.ToInt32(tmp.Rows[i].ItemArray[3])) * Convert.ToDouble((tmp.Rows[i].ItemArray[7].ToString()).Replace(",", "."));
            Cntrl.fail = (double)(Convert.ToInt32(tmp.Rows[i].ItemArray[3])) - Cntrl.success;

            Studies std = new Studies(Cntrl, Case);
            studies.Add(std);
        }

	}

    public void DoMetaAnalysis()
    {
        calculateHeterogenity();
        CalculateCombineValue();
        InsertDatabase();
    }

    public void UpdateMetaAnalysis()
    {
        calculateHeterogenity();
        CalculateCombineValue();
        UpdateDatabase();
    }

    private void calculateHeterogenity()
    {

        CalculateWeightandEffect();

        this.ec = (double)(sumofWeigtedEffect / sumofWeight);

        for (int i = 0; i < studies.Count; i++)
            this.cochran_q += ((studies[i].weight * Math.Pow(studies[i].or_value - ec, 2)));


        //this.cochran_q = Math.Sqrt(cochran_q);
        if (this.cochran_q <= (studies.Count - 1)) this.i_square = 0;
        else this.i_square = ((cochran_q - (studies.Count - 1)) / cochran_q) * 100;
    }

    private void CalculateWeightandEffect()
    {
        for (int i = 0; i < this.studies.Count; i++)
            this.sumofWeight += this.studies[i].weight;


        for (int i = 0; i < this.studies.Count; i++)
        {
            this.studies[i].related_weight = ((this.studies[i].weight) / this.sumofWeight) * 100;
            this.sumofWeigtedEffect += (this.studies[i].weight * this.studies[i].or_value);
        }
            
    }


    public void CalculateCombineValue()
    {
        if (this.i_square > 50) RandomEffectModel();
        else FixedEffectModel();
    }


    private void InsertDatabase()
    {
        MetaAnalysisDB metadb = new MetaAnalysisDB(this.disease_name, this.snp, (decimal)this.ec, this.Z.ToString(), this.i_square.ToString(), studies.Count);
        metadb.insertMetaAnalysis();
    }

    private void UpdateDatabase()
    {
        MetaAnalysisDB metadb = new MetaAnalysisDB(this.disease_name, this.snp, (decimal)this.ec, this.Z.ToString(), this.i_square.ToString(), studies.Count);
        metadb.updateMetaAnalysis();
    }

    private void FixedEffectModel()
    {
        this.SE = (double)1 / Math.Sqrt(this.sumofWeight);
        this.Z = (double)(this.ec / this.SE);
    }

    private void RandomEffectModel()
    {
        double sumofweights2 = 0.0; double C = 0.0;
        double t_square;

        for (int i = 0; i < studies.Count; i++)
        {
            sumofweights2 += Math.Pow(studies[i].weight,2);
        }

     
        C = sumofWeight - (sumofweights2 / sumofWeight);
        t_square = (this.cochran_q - (studies.Count - 1)) / C;

        for (int i = 0; i < studies.Count; i++)
        {
            studies[i].weight = (double) 1 / (studies[i].variance + t_square);
        }

        CalculateWeightandEffect();
        this.ec = (double)(sumofWeigtedEffect / sumofWeight);
        this.SE = (double)1 / Math.Sqrt(this.sumofWeight);
        this.Z = (double)(this.ec / this.SE);
    }


}