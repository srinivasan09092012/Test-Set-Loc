using HPP.OneClick.XAML.Migration.Log4Net;
using HPP.OneClick.XAML.Migration.DevContext;
using HtmlAgilityPack;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HPP.OneClick.XAML.Migration
{
  public class MigrateDependency
  {
    List<DependencyModel> consolList = new List<DependencyModel>();
    JOB_CONFIG jc = new JOB_CONFIG();
    private IOneClickLog log;

    public IOneClickLog Log
    {
      get
      {
        return this.log;
      }
    }

    public MigrateDependency()
    { this.log = new OneClickLog(MethodBase.GetCurrentMethod().DeclaringType); GetData(); }

    private void GetData()
    {
      this.log.Info("GetData start");
      var tfs = Common.AuthenticateSourceControl();
      List<IBuildDefinition> buildDefinitionList = this.GetXAMLBuildDefinitions(tfs);  //new List<IBuildDefinition>(); //
    }

    private List<IBuildDefinition> GetXAMLBuildDefinitions(TfsTeamProjectCollection tfs)
    {
      var buildServer = (IBuildServer)tfs.GetService(typeof(IBuildServer));
      string configBuildStartName = ConfigurationManager.AppSettings["BuildStartName"];
      List<string> buildList = new List<string>();
      List<IBuildDefinition> buildDefinitionList = new List<IBuildDefinition>(buildServer.QueryBuildDefinitions("USHC_AMER_US_ADU_HSP_Ua3"));
      if (!string.IsNullOrEmpty(configBuildStartName))
      {
        string[] buildItems = configBuildStartName.Split(',');
        foreach (string s in buildItems)
        {
          buildList.Add(s);
        }

        //buildList.Add("D1-BAS");
        //buildList.Add("R2-BT");
      }
      else
      {
        buildList.Add("D1-BAS");
        buildList.Add("D1-BT");
        buildList.Add("D1-UX");
        buildList.Add("D1-DB");
        buildList.Add("D1-RP");
        buildList.Add("R1-BAS");
        buildList.Add("R1-BT");
        buildList.Add("R1-UX");
        buildList.Add("R1-DB");
        buildList.Add("R1-RP");
      }
      if (buildDefinitionList != null && buildDefinitionList.Count > 0)
      {
        foreach (string str in buildList)
        {
          List<DependencyModel> JCLIST = ExtractData(buildDefinitionList.Where(p => p.Name.StartsWith(str)).ToList(), str);
          consolList.AddRange(JCLIST);
        }

     
        if (consolList.Count > 0)
        {
          DBAccess dbaccess = new DBAccess();
          dbaccess.InsertDependency(consolList);
        }
      }
      return buildDefinitionList;
    }
    private List<DependencyModel> ExtractData(List<IBuildDefinition> builddef, string buildtype)
    {
      List<JOB_CONFIG> jclist = new List<JOB_CONFIG>();
      List<DependencyModel> depmodel = new List<DependencyModel>();
      foreach (IBuildDefinition buildef in builddef)
      {
        jc = new JOB_CONFIG();
        string ProcessParameters = buildef.ProcessParameters;      

        try
        {
          try
          {
            ExtractProcessParameters(ProcessParameters, buildtype);
          }
          catch (Exception ex)
          { }

          if (buildtype.Contains("-DB") || buildtype.Contains("-RP"))
          {
            List<IWorkspaceMapping> a = buildef.Workspace.Mappings;
            IWorkspaceMapping c = a.Where(aa => aa.ServerItem.ToLower().Contains("/source/")).First();
            jc.SOLUTION_NAME_FULL_PATH = c.ServerItem.Replace("$/USHC_AMER_US_ADU_HSP_Ua3/", string.Empty);
          }

          List<string> packagedata = buildef.Workspace.Mappings.Where(aa => aa.ServerItem.ToLower().Contains("/source/package"))
            .Select(aa => aa.ServerItem.Replace("$/USHC_AMER_US_ADU_HSP_Ua3/Source/Packages/", string.Empty)).Select(aa=> aa.Replace("_Source/",string.Empty))
            .ToList();
          depmodel.Add(new DependencyModel() { SolutionName = jc.SOLUTION_NAME_FULL_PATH, PackageName = packagedata });
        }
        catch (Exception ex)
        {
          //Log this error record to not bring into db
        }
      }

      return depmodel;
    }

    private void ExtractProcessParameters(string buildef, string buildtype)
    {
      string a = buildef;
      //string path = @"c:\test\process1.txt";
      //using (var sr = new StreamReader(path))
      //{
      //  a = sr.ReadToEnd();
      //}
      HtmlDocument htmdoc = new HtmlDocument();
      string xmlContent = a;
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(xmlContent);
      XmlNode newNode = doc.DocumentElement;
      var js = JsonConvert.SerializeXmlNode(newNode, Newtonsoft.Json.Formatting.None, true);
      ProcessTM ptm = null;
      ProcessTMNA ptmna = null;
      try
      {
        ptm = JsonConvert.DeserializeObject<ProcessTM>(js);
      }
      catch (Exception ex)
      {
        ptmna = JsonConvert.DeserializeObject<ProcessTMNA>(js);
      }

      try
      {
        string solutionName = string.Empty;
        if (ptm != null)
        {
          solutionName = ptm.XArray.Where(aa => aa.XKey == "ProjectsToBuild").Select(aa => aa.XString).FirstOrDefault();
        }
        else
        {
          solutionName = ptmna.XArray.Where(aa => aa.XKey == "ProjectsToBuild").Select(aa => aa.XString).FirstOrDefault();
        }
        if (!string.IsNullOrEmpty(solutionName))
        {
          solutionName = solutionName.Replace("$/USHC_AMER_US_ADU_HSP_Ua3/", string.Empty);
          jc.SOLUTION_NAME_FULL_PATH = solutionName;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }

      if ((jc.SOLUTION_NAME_FULL_PATH == "Source/Core/Dev/BAS/BatchFactory/HP.HSP.UA3.BatchProcessingFactory/HP.HSP.UA3.BatchProcessingFactory.sln")
        || (jc.SOLUTION_NAME_FULL_PATH == "Source/PlanManagement/082/Batch/RateLoader/RateLoader.sln")
        || (jc.SOLUTION_NAME_FULL_PATH == "Source/PlanManagement/082/Batch/FDBNDCRatesLoader/FDBNDCRatesLoader.sln")
        || (jc.SOLUTION_NAME_FULL_PATH == "Source/PlanManagement/082/Batch/ICD10DiagLoader/ICD10DiagLoader.sln"))
      {
        string errordata = string.Empty;
      }

    }

   
  }
}
