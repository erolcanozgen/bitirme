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
        this.variance = (double)(((double)1 / Case.success) + ((double)1 / Case.fail) + ((double)1 / Control.success) + ((double)1 / Control.fail));
        //this.weight = (Case.fail + Control.success)/(Case.success+Case.fail+Control.success+Control.fail);
        this.weight = 0.0;

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
    double SE;
    double Z;
    public double combined_or;
    public double mean_es;
    public double sumofweights2;

	public MetaAnalaysis(string disease_name, string snp)
	{
        this.disease_name = disease_name;
        this.snp = snp;
        studies = new List<Studies>();
        i_square = 0.0; cochran_q = 0.0; this.SE = 0.0; this.Z = 0.0;combined_or=0.0;
        this.sumofWeight = 0.0;this.sumofweights2=0.0;
        sumofWeigtedEffect = 0.0; this.mean_es = 0.0;
        DataTable tmp = new DataTable();
        DiseaseDetails desease = new DiseaseDetails();
        tmp = desease.SelectWithSnp(this.disease_name,this.snp);

        for (int i = 0; i < tmp.Rows.Count; i++)
        {
            Experiments Case = new Experiments();
            Experiments Cntrl = new Experiments();

            Case.success = (double)( Convert.ToDouble(tmp.Rows[i]["Case_Count"]) * Convert.ToDouble(tmp.Rows[i]["Frequency_Patient"]));
            Case.fail = (double)(Convert.ToInt32(tmp.Rows[i]["Case_Count"])) - Case.success;

            Cntrl.success = (double)( Convert.ToDouble(tmp.Rows[i]["Control_Count"]) * Convert.ToDouble(tmp.Rows[i]["Frequency_Control"]) );
            Cntrl.fail = (double)(Convert.ToInt32(tmp.Rows[i]["Control_Count"])) - Cntrl.success;


            Case.success = (Case.success == 0) ? 0.5 : Case.success;
            Case.fail = (Case.fail == 0) ? 0.5 : Case.fail;

            Cntrl.success = (Cntrl.success == 0) ? 0.5 : Cntrl.success;
            Cntrl.fail = (Cntrl.fail == 0) ? 0.5 : Cntrl.fail; 

            Studies std = new Studies(Cntrl, Case);
            std.or_value = Convert.ToDouble(tmp.Rows[i]["OR_VALUE"]);
            studies.Add(std);
        }

	}

    public void DoMetaAnalysis()
    {
        calculateHeterogenity();
        CalculateCombineValue();
        
        if (!MetaAnalysisDB.isMetaAnalysisDone(this.disease_name, this.snp))   
            InsertDatabase();

        else UpdateDatabase();
    }

   
    private void calculateHeterogenity()
    {

        if (studies.Count == 1)
        {
            this.combined_or = studies[0].or_value;
            return;
        }

        CalculateWeightandEffect();
        this.combined_or = Math.Exp(this.mean_es);

        for (int i = 0; i < studies.Count; i++)
            this.cochran_q += ((studies[i].weight * Math.Pow(Math.Log(studies[i].or_value) - mean_es, 2)));

        if (this.cochran_q <= (studies.Count - 1)) this.i_square = 0;
        else this.i_square = ((cochran_q - (studies.Count - 1)) / cochran_q) * 100;
    }


    public void CalculateCombineValue()
    {
        if (this.i_square > 50) RandomEffectModel();
        else FixedEffectModel();
    }

    private void FixedEffectModel()
    {
        this.SE = (double)1 / Math.Sqrt(this.sumofWeight);
        this.Z = Math.Abs((double)(this.mean_es / this.SE));
    }

    private void RandomEffectModel()
    {
        double C = 0.0;
        double t_square = 0.0;


        C = sumofWeight - (sumofweights2 / sumofWeight);
        t_square = (this.cochran_q - (studies.Count - 1)) / C;

        for (int i = 0; i < studies.Count; i++)
            studies[i].variance = studies[i].variance + t_square;


        CalculateWeightandEffect();
        this.combined_or = Math.Exp(this.mean_es);
        this.SE = (double)1 / Math.Sqrt(this.sumofWeight);
        this.Z = Math.Abs((double)(this.mean_es / this.SE));
    }

    private void CalculateWeightandEffect()
    {

        this.sumofWeight = 0.0; this.sumofWeigtedEffect = 0.0; this.sumofweights2 = 0.0;

        for (int i = 0; i < this.studies.Count; i++)
        {
            this.studies[i].weight = (double)(1 / this.studies[i].variance);
            this.sumofWeight += this.studies[i].weight;
        }

        for (int i = 0; i < this.studies.Count; i++)
        {
            this.studies[i].related_weight = ((this.studies[i].weight) / this.sumofWeight) * 100;
            this.sumofWeigtedEffect += (this.studies[i].weight * Math.Log(this.studies[i].or_value));
            this.sumofweights2 += Math.Pow(studies[i].weight, 2);
        }

        this.mean_es = (double)(sumofWeigtedEffect / sumofWeight);
            
    }


    private void InsertDatabase()
    {
        MetaAnalysisDB metadb = new MetaAnalysisDB(this.disease_name, this.snp, (decimal)this.combined_or, this.Z.ToString(), this.i_square.ToString(), studies.Count);
        metadb.insertMetaAnalysis();
    }

    private void UpdateDatabase()
    {
        MetaAnalysisDB metadb = new MetaAnalysisDB(this.disease_name, this.snp, (decimal)this.combined_or, this.Z.ToString(), this.i_square.ToString(), studies.Count);
        metadb.updateMetaAnalysis();
    }




}