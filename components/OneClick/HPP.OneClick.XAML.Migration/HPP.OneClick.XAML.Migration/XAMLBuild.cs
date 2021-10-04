using HPP.OneClick.XAML.Migration.DevContext;
using HPP.OneClick.XAML.Migration.Log4Net;
using HtmlAgilityPack;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.VisualStudio.Services.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Xml;



namespace HPP.OneClick.XAML.Migration
{
    class XAMLBuild
    {
        private IOneClickLog log;
        JOB_CONFIG jc = new JOB_CONFIG();
        string sfProjectName = "";
        string sfBuildArgs = "";
        string msBuildArgs = "";
        List<JOB_CONFIG> consolList = new List<JOB_CONFIG>();
        public XAMLBuild()
        {
            this.log = new OneClickLog(MethodBase.GetCurrentMethod().DeclaringType);
            GetData();
            //ExtractProcessParameters("abc","abc");
        }

        public IOneClickLog Log
        {
            get
            {
                return this.log;
            }
        }

        public void GetData()
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
                    List<JOB_CONFIG> JCLIST = ExtractData(buildDefinitionList.Where(p => p.Name.StartsWith(str)).ToList(), str);
                    consolList.AddRange(JCLIST);
                }

                if (consolList.Where(aa => aa.MSBUILD_ARGS == "Release").Count() > 0)
                {
                    var errlist = consolList.Where(aa => aa.MSBUILD_ARGS == "Release");
                }
                if (consolList.Count > 0)
                {
                    DBAccess dbaccess = new DBAccess();
                    dbaccess.DEV_InsertIntoJobConfig(consolList);
                }
            }
            return buildDefinitionList;
        }
        private List<JOB_CONFIG> ExtractData(List<IBuildDefinition> builddef, string buildtype)
        {
            List<JOB_CONFIG> jclist = new List<JOB_CONFIG>();
            int i = 0;
            foreach (IBuildDefinition buildef in builddef)
            {
                Console.WriteLine(i++);
                jc = new JOB_CONFIG();
                sfProjectName = "";
                sfBuildArgs = "";
                msBuildArgs = "";
                string ProcessParameters = buildef.ProcessParameters;
                string ActivityParams = buildef.Process.Parameters;

                //if (buildef.Name.Contains("D1-BT DR ProviderJsonConversion") ||
                //    buildef.Name.Contains("D1-BT IN IntegrationAPI") ||
                //    buildef.Name.Contains("D1-BT PL RevenueLoader")
                //    )
                //{
                if (buildef.Name.Contains("D1-DBS"))
                {
                    log.Info("a");
                }

                try
                {
                    try
                    {
                        ExtractProcessParameters(ProcessParameters, buildtype);
                    }
                    catch (Exception ex)
                    {

                    }
                    if (string.IsNullOrEmpty(jc.APLCTN_RELEASE_NUM))
                    {

                        if (buildef.Name.StartsWith("D1"))
                        {
                            jc.APLCTN_RELEASE_NUM = ConfigurationManager.AppSettings["D1-AppRelNum"];
                        }
                        else if (buildef.Name.StartsWith("R1"))
                        {
                            jc.APLCTN_RELEASE_NUM = ConfigurationManager.AppSettings["R1-AppRelNum"];
                        }
                        else if (buildef.Name.StartsWith("R2"))
                        {
                            jc.APLCTN_RELEASE_NUM = ConfigurationManager.AppSettings["R2-AppRelNum"];
                        }
                        else if (buildef.Name.StartsWith("R3"))
                        {
                            jc.APLCTN_RELEASE_NUM = ConfigurationManager.AppSettings["R3-AppRelNum"];
                        }
                        else if (buildef.Name.StartsWith("R4"))
                        {
                            jc.APLCTN_RELEASE_NUM = ConfigurationManager.AppSettings["R4-AppRelNum"];
                        }
                        else if (buildef.Name.StartsWith("R5"))
                        {
                            jc.APLCTN_RELEASE_NUM = ConfigurationManager.AppSettings["R5-AppRelNum"];
                        }
                        else if (buildef.Name.StartsWith("R6"))
                        {
                            jc.APLCTN_RELEASE_NUM = ConfigurationManager.AppSettings["R6-AppRelNum"];
                        }
                        else if (buildef.Name.StartsWith("R7"))
                        {
                            jc.APLCTN_RELEASE_NUM = ConfigurationManager.AppSettings["R7-AppRelNum"];
                        }
                        //Log applicaiton release num empty
                    }

                    ExtractActivityParams(ActivityParams, buildtype);

                    if (buildtype.Contains("D1-DBS"))
                    {
                        log.Info("error data");
                    }
                    if (buildtype.Contains("-DB") || buildtype.Contains("-RP"))
                    {
                        List<IWorkspaceMapping> a = buildef.Workspace.Mappings;
                        IWorkspaceMapping c = a.Where(aa => aa.ServerItem.ToLower().Contains("/source/")).First();
                        jc.SOLUTION_NAME_FULL_PATH = c.ServerItem.Replace("$/USHC_AMER_US_ADU_HSP_Ua3/", string.Empty);
                    }


                    if (jc.SOLUTION_NAME_FULL_PATH.Contains("BatchProcessingFactory.sln"))
                    {
                        Console.WriteLine("Error solution");
                    }

                    if (System.Text.Encoding.UTF8.GetByteCount(jc.MSBUILD_ARGS) != jc.MSBUILD_ARGS.Length)
                    {
                        jc.MSBUILD_ARGS = System.Text.RegularExpressions.Regex.Replace(jc.MSBUILD_ARGS, @"[^\u0000-\u007F]+", " ");
                        if (System.Text.Encoding.UTF8.GetByteCount(jc.MSBUILD_ARGS) != jc.MSBUILD_ARGS.Length)
                        {
                            Console.WriteLine("Again error");
                        }

                    }
                    else
                    {
                        //Console.WriteLine("all ascii char");
                    }

                    if (jc.SOLUTION_NAME_FULL_PATH == "Source/Core/Dev/DB/SqlServer/Sql Scripts")
                    {
                        log.Info("duplicate data");
                    }

                    if (string.IsNullOrEmpty(jc.BUILD_CONFIG))
                    {
                        this.log.Error("Build Config data not exist for solution " + jc.SOLUTION_NAME_FULL_PATH);
                    }
                    else if (string.IsNullOrEmpty(jc.DEPLOY_PACKAGE_NAME))
                    {
                        this.log.Error("Deploy package Name not exist for solution " + jc.SOLUTION_NAME_FULL_PATH);
                    }
                    else if (!jc.SOLUTION_NAME_FULL_PATH.ToLower().StartsWith("source/"))
                    {
                        this.log.Error("Invalid solution name.  solution : " + jc.SOLUTION_NAME_FULL_PATH);
                    }
                    else
                    {

                        if (jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() > 0)
                        {

                            string deploypkgname = jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOY_PACKAGE_NAME;
                            if (deploypkgname == jc.DEPLOY_PACKAGE_NAME)
                            {
                                this.log.Error("solution : " + jc.SOLUTION_NAME_FULL_PATH + " already exist");
                            }
                            else
                            {
                                jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOY_PACKAGE_NAME = deploypkgname + "," + jc.DEPLOY_PACKAGE_NAME;

                                sfBuildArgs = sfBuildArgs.Replace("/p:PackageLocation=$(TF_BUILD_BUILDDIRECTORY)\\UA3\\Source\\bin\\\\", string.Empty);
                                sfBuildArgs = sfBuildArgs.Replace("/p:PackageLocation=$(TF_BUILD_BUILDDIRECTORY)\\SFbin\\\\", string.Empty);
                                if ((consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() > 0) || (jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() > 0))
                                {
                                    if (jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() > 0)
                                    {
                                        string dmodel = jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOYMENT_MODEL;
                                        if (dmodel != jc.DEPLOYMENT_MODEL)
                                        {
                                            jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOYMENT_MODEL = dmodel + "," + jc.DEPLOYMENT_MODEL;
                                            if (jc.DEPLOYMENT_MODEL == "SF")
                                            {
                                                // based communication with pradheep on 19 sep 2021 sfprojname removed.
                                                // var combined_build_args = new { msbuildargs = msBuildArgs, sfprojname = sfProjectName, sfbuildargs = sfBuildArgs };

                                                var combined_build_args = new { SolutionBuildArguments = msBuildArgs, SFBuildArguments = sfBuildArgs };
                                                string combbuildargs = JsonConvert.SerializeObject(combined_build_args);

                                                jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().COMBINED_BUILD_ARGS = combbuildargs;

                                            }
                                        }
                                    }


                                    if (consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() > 0)
                                    {
                                        string dmodel = consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOYMENT_MODEL;
                                        if (dmodel != jc.DEPLOYMENT_MODEL)
                                        {
                                            consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOYMENT_MODEL = dmodel + "," + jc.DEPLOYMENT_MODEL;
                                            if (jc.DEPLOYMENT_MODEL == "SF")
                                            {
                                                // based communication with pradheep on 19 sep 2021 sfprojname removed.
                                                // var combined_build_args = new { msbuildargs = msBuildArgs, sfprojname = sfProjectName, sfbuildargs = sfBuildArgs };

                                                var combined_build_args = new { SolutionBuildArguments = msBuildArgs, SFBuildArguments = sfBuildArgs };
                                                string combbuildargs = JsonConvert.SerializeObject(combined_build_args);

                                                consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().COMBINED_BUILD_ARGS = combbuildargs;

                                            }

                                        }
                                    }

                                    this.log.Error("Solution name already exist.  solution : " + jc.SOLUTION_NAME_FULL_PATH);
                                }
                                else
                                {
                                    jc.PRODUCT_NAME = ConfigurationManager.AppSettings["ProductName"].ToString();
                                    string combbuildargs = string.Empty;
                                    if (buildef.Name.Contains("D1-SF"))
                                    {
                                        //var combined_build_args = new { SolutionBuildArguments = msBuildArgs, SFProjectName = sfProjectName, SFBuildArguments = sfBuildArgs };
                                        var combined_build_args = new { SolutionBuildArguments = msBuildArgs, SFBuildArguments = sfBuildArgs };

                                        combbuildargs = JsonConvert.SerializeObject(combined_build_args);
                                    }
                                    else
                                    {
                                        var combined_build_args = new { SolutionBuildArguments = msBuildArgs };
                                        combbuildargs = JsonConvert.SerializeObject(combined_build_args);
                                    }

                                    jc.COMBINED_BUILD_ARGS = combbuildargs;
                                    jclist.Add(jc);
                                    Console.WriteLine("Added :" + i + " Solution Name" + jc.SOLUTION_NAME_FULL_PATH);
                                }

                            }                            
                        }  
                        else
                        {
                            sfBuildArgs = sfBuildArgs.Replace("/p:PackageLocation=$(TF_BUILD_BUILDDIRECTORY)\\UA3\\Source\\bin\\\\", string.Empty);
                            sfBuildArgs = sfBuildArgs.Replace("/p:PackageLocation=$(TF_BUILD_BUILDDIRECTORY)\\SFbin\\\\", string.Empty);
                            if ((consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() > 0) || (jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() > 0))
                            {
                                if (jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() > 0)
                                {
                                    string dmodel = jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOYMENT_MODEL;
                                    if (dmodel != jc.DEPLOYMENT_MODEL)
                                    {
                                        jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOYMENT_MODEL = dmodel + "," + jc.DEPLOYMENT_MODEL;
                                        if (jc.DEPLOYMENT_MODEL == "SF")
                                        {
                                            // based communication with pradheep on 19 sep 2021 sfprojname removed.
                                            // var combined_build_args = new { msbuildargs = msBuildArgs, sfprojname = sfProjectName, sfbuildargs = sfBuildArgs };

                                            var combined_build_args = new { SolutionBuildArguments = msBuildArgs, SFBuildArguments = sfBuildArgs };
                                            string combbuildargs = JsonConvert.SerializeObject(combined_build_args);

                                            jclist.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().COMBINED_BUILD_ARGS = combbuildargs;

                                        }
                                    }
                                }


                                if (consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() > 0)
                                {
                                    string dmodel = consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOYMENT_MODEL;
                                    if (dmodel != jc.DEPLOYMENT_MODEL)
                                    {
                                        consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().DEPLOYMENT_MODEL = dmodel + "," + jc.DEPLOYMENT_MODEL;
                                        if (jc.DEPLOYMENT_MODEL == "SF")
                                        {
                                            // based communication with pradheep on 19 sep 2021 sfprojname removed.
                                            // var combined_build_args = new { msbuildargs = msBuildArgs, sfprojname = sfProjectName, sfbuildargs = sfBuildArgs };

                                            var combined_build_args = new { SolutionBuildArguments = msBuildArgs, SFBuildArguments = sfBuildArgs };
                                            string combbuildargs = JsonConvert.SerializeObject(combined_build_args);

                                            consolList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault().COMBINED_BUILD_ARGS = combbuildargs;

                                        }

                                    }
                                }

                                this.log.Error("Solution name already exist.  solution : " + jc.SOLUTION_NAME_FULL_PATH);
                            }
                            else
                            {
                                jc.PRODUCT_NAME = ConfigurationManager.AppSettings["ProductName"].ToString();
                                string combbuildargs = string.Empty;
                                if (buildef.Name.Contains("D1-SF"))
                                {
                                    //var combined_build_args = new { SolutionBuildArguments = msBuildArgs, SFProjectName = sfProjectName, SFBuildArguments = sfBuildArgs };
                                    var combined_build_args = new { SolutionBuildArguments = msBuildArgs, SFBuildArguments = sfBuildArgs };

                                    combbuildargs = JsonConvert.SerializeObject(combined_build_args);
                                }
                                else
                                {
                                    var combined_build_args = new { SolutionBuildArguments = msBuildArgs };
                                    combbuildargs = JsonConvert.SerializeObject(combined_build_args);
                                }

                                jc.COMBINED_BUILD_ARGS = combbuildargs;
                                jclist.Add(jc);
                                Console.WriteLine("Added :" + i + " Solution Name" + jc.SOLUTION_NAME_FULL_PATH);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Log this error record to not bring into db
                }
                //}
            }

            return jclist;
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
                string buildplatform = string.Empty;
                if (ptm != null)
                {
                    buildplatform = ptm.XArray.Where(aa => aa.XKey == "ConfigurationsToBuild").Select(aa => aa.XString).FirstOrDefault();
                }
                else
                {
                    buildplatform = ptmna.XArray.Where(aa => aa.XKey == "ConfigurationsToBuild").Select(aa => aa.XString).FirstOrDefault();
                }
                if (!string.IsNullOrEmpty(buildplatform))
                {
                    string[] buildconfig = buildplatform.Split('|');
                    string buildConfig = buildconfig[1].ToString();
                    string buildPlatform = buildconfig[0].ToString();
                    jc.BUILD_CONFIG = buildConfig;
                    jc.BUILD_PLATFORM = buildPlatform;
                }

            }
            catch
            (Exception ex)
            {

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

            try
            {
                string SFsolutionName = string.Empty;
                if (ptm != null)
                {
                    SFsolutionName = ptm.XArray.Where(aa => aa.XKey == "ServiceFabricProjectToBuild").Select(aa => aa.XString).FirstOrDefault();
                }
                else
                {
                    SFsolutionName = ptmna.XArray.Where(aa => aa.XKey == "ServiceFabricProjectToBuild").Select(aa => aa.XString).FirstOrDefault();
                }
                if (!string.IsNullOrEmpty(SFsolutionName))
                {
                    SFsolutionName = SFsolutionName.Replace("$/USHC_AMER_US_ADU_HSP_Ua3/", string.Empty);
                    sfProjectName = SFsolutionName;
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

            if (jc != null && (string.IsNullOrEmpty(jc.TARGET_BUILD_ENVRMT)))
            {
                try
                {
                    string targetbuildEnv = string.Empty;

                    if (ptm != null)
                    {
                        targetbuildEnv = ptm.XString.Where(aa => aa.XKey == "TargetEnvironment").Select(aa => aa.XText).FirstOrDefault();
                    }
                    else
                    {
                        targetbuildEnv = ptmna.XString.Where(aa => aa.XKey == "TargetEnvironment").Select(aa => aa.XText).FirstOrDefault();
                    }
                    jc.TARGET_BUILD_ENVRMT = targetbuildEnv;
                }
                catch (Exception ex)
                {

                }
            }

            try
            {
                string appreleaseNum = string.Empty;
                if (ptm != null)
                {
                    appreleaseNum = ptm.XString.Where(aa => aa.XKey == "ApplicationRelease").Select(aa => aa.XText).FirstOrDefault();
                }
                else
                {
                    appreleaseNum = ptmna.XString.Where(aa => aa.XKey == "ApplicationRelease").Select(aa => aa.XText).FirstOrDefault();
                }
                if (!string.IsNullOrEmpty(appreleaseNum))
                {
                    jc.APLCTN_RELEASE_NUM = appreleaseNum;
                }
            }
            catch (Exception ex)
            {

            }

            try
            {
                string deployPkgName = string.Empty;
                if (ptm != null)
                {
                    if (buildtype.Contains("-UX") || buildtype.Contains("-BT") || buildtype.Contains("-AKS") || buildtype.Contains("-DB") || buildtype.Contains("-RP"))
                    {
                        deployPkgName = ptm.XString.Where(aa => aa.XKey == "SolutionName").Select(aa => aa.XText).FirstOrDefault();
                    }
                    else
                    {
                        deployPkgName = ptm.XString.Where(aa => aa.XKey == "ServiceName").Select(aa => aa.XText).FirstOrDefault();
                    }
                }
                else
                {
                    if (buildtype.Contains("-UX") || buildtype.Contains("-BT") || buildtype.Contains("-DB") || buildtype.Contains("-AKS") || buildtype.Contains("-RP"))
                    {
                        deployPkgName = ptmna.XString.Where(aa => aa.XKey == "SolutionName").Select(aa => aa.XText).FirstOrDefault();
                    }
                    else
                    {
                        deployPkgName = ptmna.XString.Where(aa => aa.XKey == "ServiceName").Select(aa => aa.XText).FirstOrDefault();
                    }
                }
                jc.DEPLOY_PACKAGE_NAME = deployPkgName;
            }
            catch (Exception ex)
            {

            }

            try
            {
                string deployModel = string.Empty;
                if (ptm != null)
                {
                    deployModel = ptm.XString.Where(aa => aa.XKey == "DeployToServiceFabric").Select(aa => aa.XText).FirstOrDefault();
                }
                else
                {
                    deployModel = ptmna.XString.Where(aa => aa.XKey == "DeployToServiceFabric").Select(aa => aa.XText).FirstOrDefault();
                }

                if (deployModel == "True" && buildtype.Contains("-AKS"))
                {
                    jc.DEPLOYMENT_MODEL = "AKS";
                }
                else if (deployModel == "True")
                {
                    jc.DEPLOYMENT_MODEL = "SF";
                }
                else
                {
                    jc.DEPLOYMENT_MODEL = "OP";
                }
            }
            catch (Exception ex)
            {

            }
            try
            {
                string skipPkgBuildFlg = string.Empty;
                if (ptm != null)
                {
                    skipPkgBuildFlg = ptm.XBoolean.Where(aa => aa.XKey == "SkipPackageBuild").Select(aa => aa.XText).FirstOrDefault();
                }
                else
                {

                }
                if (!string.IsNullOrEmpty(skipPkgBuildFlg))
                {
                    jc.SKIP_PACKAGE_BUILD_FLAG = Convert.ToBoolean(skipPkgBuildFlg);
                }
            }
            catch (Exception ex)
            {

            }
            try
            {
                string skipPkgDeploymentFlg = string.Empty;
                if (ptm != null)
                {
                    skipPkgDeploymentFlg = ptm.XBoolean.Where(aa => aa.XKey == "SkipPackageDeployment").Select(aa => aa.XText).FirstOrDefault();
                }
                if (!string.IsNullOrEmpty(skipPkgDeploymentFlg))
                {
                    jc.SKIP_PACKAGE_DEPLOYMENT_FLAG = Convert.ToBoolean(skipPkgDeploymentFlg);
                }
            }
            catch (Exception ex)
            {

            }

            try
            {
                string skipPkgStgFlg = string.Empty;
                if (ptm != null)
                {
                    skipPkgStgFlg = ptm.XBoolean.Where(aa => aa.XKey == "SkipPackageStaging").Select(aa => aa.XText).FirstOrDefault();
                }
                if (!string.IsNullOrEmpty(skipPkgStgFlg))
                {
                    jc.SKIP_PACKAGE_STAGING_FLAG = Convert.ToBoolean(skipPkgStgFlg);
                }
            }
            catch (Exception ex)
            {

            }

            try
            {
                string skipworkSpaceclearFlg = string.Empty;

                if (ptm != null)
                {
                    skipworkSpaceclearFlg = ptm.XBoolean.Where(aa => aa.XKey == "SkipWorkspaceClear").Select(aa => aa.XText).FirstOrDefault();
                }
                if (!string.IsNullOrEmpty(skipworkSpaceclearFlg))
                {
                    jc.SKIP_BUILD_FLDR_CLEAN_FLAG = Convert.ToBoolean(skipworkSpaceclearFlg);
                }

            }
            catch (Exception ex)
            {

            }

            try
            {
                ProcessTMbuildparamarray ptma = JsonConvert.DeserializeObject<ProcessTMbuildparamarray>(js);
                string msbuildArgs = ptma.BluildParameter.Where(aa => aa.XKey == "AdvancedBuildSettings").Select(aa => aa.XText).FirstOrDefault();

                BuildArgs bg = JsonConvert.DeserializeObject<BuildArgs>(msbuildArgs);
                if (!string.IsNullOrEmpty(msbuildArgs))
                {
                    jc.MSBUILD_ARGS = bg.MSBuildArguments.Replace("\"", string.Empty);
                    //jc.MSBUILD_ARGS = msbuildArgs.Replace(@"\\", @"\");
                    msBuildArgs = jc.MSBUILD_ARGS;
                    if (!string.IsNullOrEmpty(bg.SFMSBuildArguments))
                    {
                        //jc.MSBUILD_ARGS = bg.MSBuildArguments.Replace("\"", string.Empty);
                        //jc.MSBUILD_ARGS = msbuildArgs.Replace(@"\\", @"\");
                        sfBuildArgs = bg.SFMSBuildArguments.Replace("\"", string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessTMbuildparam ptmb = JsonConvert.DeserializeObject<ProcessTMbuildparam>(js);

                if (ptmb != null || ptmb.BluildParameter != null)
                {

                    string msbuildArgs = ptmb.BluildParameter.XText.ToString();
                    BuildArgs bg = JsonConvert.DeserializeObject<BuildArgs>(msbuildArgs);
                    if (!string.IsNullOrEmpty(msbuildArgs))
                    {
                        if (bg != null && bg.MSBuildArguments != null)
                        {
                            jc.MSBUILD_ARGS = bg.MSBuildArguments.Replace("\"", string.Empty); ;
                            //jc.MSBUILD_ARGS = msbuildArgs.Replace(@"\\", @"\");
                            msBuildArgs = jc.MSBUILD_ARGS;
                            if (!string.IsNullOrEmpty(bg.SFMSBuildArguments))
                            {
                                sfBuildArgs = bg.SFMSBuildArguments;
                            }
                        }
                    }
                }
            }
        }

        private void ExtractActivityParams(string buildef, string buildType)
        {
            string a = buildef;
            HtmlDocument htmdoc = new HtmlDocument();
            htmdoc.LoadHtml(a);
            HtmlAttributeCollection attrCol = htmdoc.DocumentNode.ChildNodes[0].Attributes;
            List<DataModel> datamodel = new List<DataModel>();
            foreach (var item in attrCol)
            {
                if (item.Name.Contains("this:process"))
                {
                    datamodel.Add(new DataModel()
                    {
                        Name = item.Name,
                        Value = item.Value.Replace("&quot;&quot;", "\"").Replace("&quot;", "\'")
                    });
                }
            }
            AutomatedTest amt = new AutomatedTest();
            AdvanceBuildSettings abs = new AdvanceBuildSettings();
            AdvanceTestSettings ats = new AdvanceTestSettings();
            try
            {
                string ActiviReleasebranch = datamodel.Where(aa => aa.Name == "this:process.releasebranch").Select(aa => aa.Value).FirstOrDefault();
                string automatedTests = datamodel.Where(aa => aa.Name == "this:process.automatedtests").Select(aa => aa.Value).FirstOrDefault();
                string advancedTestSettings = datamodel.Where(aa => aa.Name == "this:process.advancedtestsettings").Select(aa => aa.Value).FirstOrDefault();
                string advancedBuildSettings = datamodel.Where(aa => aa.Name == "this:process.advancedbuildsettings").Select(aa => aa.Value).FirstOrDefault();

                try
                {
                    if (!buildType.Contains("-RP"))
                    {
                        automatedTests = automatedTests.Trim().Replace("[{New Microsoft.TeamFoundation.Build.Common.BuildParameter('", string.Empty);
                        automatedTests = automatedTests.Trim().Replace("')}]", string.Empty);
                        amt = JsonConvert.DeserializeObject<AutomatedTest>(automatedTests);
                    }


                }
                catch (Exception ex)
                {
                    if (!buildType.Contains("-RP"))
                    {
                        automatedTests = automatedTests.Trim().Replace("[{ New Microsoft.TeamFoundation.Build.Common.BuildParameter('", string.Empty);
                        automatedTests = automatedTests.Trim().Replace("') }]", string.Empty);
                        amt = JsonConvert.DeserializeObject<AutomatedTest>(automatedTests);
                    }
                }




                advancedTestSettings = advancedTestSettings.Replace("[New Microsoft.TeamFoundation.Build.Common.BuildParameter('", string.Empty);
                advancedTestSettings = advancedTestSettings.Replace("')]", string.Empty);

                advancedBuildSettings = advancedBuildSettings.Replace("[New Microsoft.TeamFoundation.Build.Common.BuildParameter('", string.Empty);
                advancedBuildSettings = advancedBuildSettings.Replace("')]", string.Empty);


                ats = JsonConvert.DeserializeObject<AdvanceTestSettings>(advancedTestSettings);
                abs = JsonConvert.DeserializeObject<AdvanceBuildSettings>(advancedBuildSettings);
            }
            catch (Exception ex)
            {

            }
            try
            {
                string cleanworkspace = datamodel.Where(aa => aa.Name == "this:process.cleanworkspace").Select(aa => aa.Value).FirstOrDefault();
                jc.CLEAN_WORKSPACE_FLAG = cleanworkspace == "[True]" ? true : false;
            }
            catch
            {

            }
            try
            {
                string skipPackageBuild = datamodel.Where(aa => aa.Name == "this:process.skippackagebuild").Select(aa => aa.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(skipPackageBuild))
                {
                    jc.SKIP_PACKAGE_BUILD_FLAG = Convert.ToBoolean(skipPackageBuild);
                }
            }
            catch (Exception ex)
            {

            }
            try
            {
                string skipPackageDeployment = datamodel.Where(aa => aa.Name == "this:process.skippackagedeployment").Select(aa => aa.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(skipPackageDeployment))
                {
                    jc.SKIP_PACKAGE_DEPLOYMENT_FLAG = Convert.ToBoolean(skipPackageDeployment);
                }
            }
            catch (Exception ex)
            {

            }
            try
            {
                string skipPackageStagging = datamodel.Where(aa => aa.Name == "this:process.skippackagestaging").Select(aa => aa.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(skipPackageStagging))
                {
                    jc.SKIP_PACKAGE_STAGING_FLAG = Convert.ToBoolean(skipPackageStagging);
                }
            }
            catch (Exception ex)
            {

            }
            try
            {
                string skipworkspaceclear = datamodel.Where(aa => aa.Name == "this:process.skipworkspaceclear").Select(aa => aa.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(skipworkspaceclear))
                {
                    jc.SKIP_BUILD_FLDR_CLEAN_FLAG = Convert.ToBoolean(skipworkspaceclear);
                }
            }
            catch (Exception ex)
            {

            }

            string targetEnvironment = datamodel.Where(aa => aa.Name == "this:process.targetenvironment").Select(aa => aa.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(jc.TARGET_BUILD_ENVRMT))
            {
                try
                {
                    jc.TARGET_BUILD_ENVRMT = targetEnvironment;
                }
                catch (Exception ex)
                {

                }
            }
            string deploytosericefabric = datamodel.Where(aa => aa.Name == "this:process.deploytoservicefabric").Select(aa => aa.Value).FirstOrDefault();

            try
            {
                string executeTestcaseFlg = ats.DisableTests;
                jc.EXECUTE_TESTCASES_FLAG = Convert.ToBoolean(executeTestcaseFlg) == true ? false : true;
            }
            catch
            {

            }
            if (!buildType.Contains("-RP"))
            {
                try
                {
                    string assemblyfilespec = amt.AssemblyFileSpec.Replace("test", "Test");
                    jc.TESTSOURCE_SPEC = assemblyfilespec;
                }
                catch (Exception ex)
                {

                }
                try
                {
                    string testCaseFilter = amt.TestCaseFilter;
                    if (string.IsNullOrEmpty(testCaseFilter))
                    {
                        testCaseFilter = ConfigurationManager.AppSettings["TestCaseFilter"];
                    }
                    jc.TESTCASE_FILTER = testCaseFilter;
                }
                catch (Exception ex)
                {

                }
                try
                {
                    string testcasePlatform = amt.ExecutionPlatform;
                    jc.TESTCASE_PLATFORM = testcasePlatform;
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                jc.TESTSOURCE_SPEC = "Not Applicable";
                jc.TESTCASE_FILTER = "Not Applicable";
                jc.TESTCASE_PLATFORM = "Not Applicable";
            }

            string msBuildARGS = abs.MSBuildArguments;
            string sfbuildargs = abs.SFMSBuildArguments;
            if (string.IsNullOrEmpty(jc.MSBUILD_ARGS))
            {
                if (string.IsNullOrEmpty(abs.MSBuildArguments))
                {
                    if (jc.SOLUTION_NAME_FULL_PATH.Contains("RevenueLoader"))
                    {
                        string revenueloadermsbuildargs = ConfigurationManager.AppSettings["revenuemsbuildsargs"];
                        jc.MSBUILD_ARGS = revenueloadermsbuildargs;
                        msBuildArgs = revenueloadermsbuildargs;
                    }
                    else
                    {
                        this.log.Error("MsbuildArgument is empty for solution : " + jc.SOLUTION_NAME_FULL_PATH);
                        throw new Exception();
                    }
                }
                else
                {
                    jc.MSBUILD_ARGS = msBuildARGS;
                    msBuildArgs = jc.MSBUILD_ARGS;
                }

                if (!string.IsNullOrEmpty(abs.SFMSBuildArguments))
                {
                    sfBuildArgs = sfbuildargs;
                }

            }

            jc.JOB_CONFIG_ID = Guid.NewGuid();
            jc.ENABLED_FLAG = true;
            jc.OPERATOR_ID = ConfigurationManager.AppSettings["OperatorName"].ToString();
            jc.CREATED_TS = DateTime.Now;
            jc.LAST_MODIFIED_TS = DateTime.Now;

        }
    }
}
