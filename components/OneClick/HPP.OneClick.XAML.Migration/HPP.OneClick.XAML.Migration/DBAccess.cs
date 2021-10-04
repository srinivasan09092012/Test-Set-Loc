using HPP.OneClick.XAML.Migration.Log4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JOB_CONFIG = HPP.OneClick.XAML.Migration.DevContext.JOB_CONFIG;
using MODULE = HPP.OneClick.XAML.Migration.DevContext.MODULE;
using Environment = HPP.OneClick.XAML.Migration.DevContext.ENVIRONMENT_RELEASE_MAP;
using HPP.OneClick.XAML.Migration.DevContext;
using System.Configuration;

namespace HPP.OneClick.XAML.Migration
{
    public class DBAccess
    {
        private IOneClickLog log;
        public DBAccess()
        {
            this.log = new OneClickLog(MethodBase.GetCurrentMethod().DeclaringType);
        }
        //public bool InsertIntoJobConfig(List<JOB_CONFIG> jobconfig)
        //{
        //  using(Model1 m1 = new Model1())
        //  {
        //    List<MODULE> modl = m1.MODULEs.ToList();
        //    foreach (JOB_CONFIG jc in jobconfig)
        //    {
        //      if (!string.IsNullOrEmpty(jc.SOLUTION_NAME_FULL_PATH))
        //      {
        //        string slnName = jc.SOLUTION_NAME_FULL_PATH;
        //        string[] slnarray = slnName.Split('/');
        //        string moduleName = slnarray[1];
        //        Console.WriteLine(modl.Where(aa => aa.MODULE_NAME == moduleName).Select(aa => aa.MODULE_ID).FirstOrDefault());
        //        Console.WriteLine(slnName);
        //        jc.MODULE_ID = modl.Where(aa => aa.MODULE_NAME == moduleName).Select(aa => aa.MODULE_ID).FirstOrDefault();            
        //        m1.JOB_CONFIG.Add(jc);
        //      }

        //    }
        //    m1.SaveChanges();
        //    return true;
        //  }     
        //}

        public bool DEV_InsertIntoJobConfig(List<JOB_CONFIG> jobconfig)
        {
            string migrationType = ConfigurationManager.AppSettings["SFMigration"];


            this.log.Info("Dev_InsertIntoJobConfig");
            using (DevBuildTypeContext m1 = new DevBuildTypeContext())
            {
                List<JOB_CONFIG> currentList = m1.JOB_CONFIG.ToList();
                List<MODULE> modl = m1.MODULEs.ToList();
                List<BUILD_TYPE> buildtypelist = m1.BUILD_TYPE.ToList();
                List<PRODUCT> productList = m1.PRODUCTs.ToList(); 
                int i = 0;
                List<Environment> erm = m1.ENVIRONMENT_RELEASE_MAP.ToList();
                foreach (JOB_CONFIG jc in jobconfig)
                {
                    Console.WriteLine(++i);
                    this.log.Info("SolutionName : " + jc.SOLUTION_NAME_FULL_PATH);
                    try
                    {
                        if (currentList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).Count() == 0)
                        {
                            if (!string.IsNullOrEmpty(jc.SOLUTION_NAME_FULL_PATH))
                            {
                                string slnName = jc.SOLUTION_NAME_FULL_PATH;
                                string[] slnarray = slnName.Split('/');
                                string moduleName = slnarray[1];
                                string buildName = slnarray[3].ToUpper();
                                if (buildName == "PROVIDERJSONCONVERSION")
                                {
                                    buildName = "BATCH";
                                }
                                jc.BUILD_TYPE_ID = buildtypelist.Where(aa => aa.BUILD_TYPE_NAME == buildName).Select(aa => aa.BUILD_TYPE_ID).FirstOrDefault();
                                Console.WriteLine(modl.Where(aa => aa.MODULE_NAME == moduleName).Select(aa => aa.MODULE_ID).FirstOrDefault());
                                Console.WriteLine(slnName);
                                jc.PRODUCT_ID = productList.Where(aa => aa.PRODUCT_NAME == jc.PRODUCT_NAME).Select(aa => aa.PRODUCT_ID).FirstOrDefault();
                                jc.TARGET_BUILD_ENVRMT = erm.Where(aa => aa.BUILD_RELEASE == jc.TARGET_BUILD_ENVRMT).Select(aa => aa.HPP_RELEASE).FirstOrDefault();
                                jc.MODULE_ID = modl.Where(aa => aa.MODULE_NAME == moduleName).Select(aa => aa.MODULE_ID).FirstOrDefault();
                               
                                if (jc.BUILD_TYPE_ID == Guid.Empty)
                                {
                                    this.log.Error("Empty build type " + jc.SOLUTION_NAME_FULL_PATH);
                                }
                                else
                                {
                                    m1.JOB_CONFIG.Add(jc);
                                }
                                Console.WriteLine(i + jc.SOLUTION_NAME_FULL_PATH);
                            }
                        }
                        else if (migrationType == "SFMigration")
                        {
                            JOB_CONFIG currentjc = currentList.Where(aa => aa.SOLUTION_NAME_FULL_PATH == jc.SOLUTION_NAME_FULL_PATH).FirstOrDefault();
                            currentjc.COMBINED_BUILD_ARGS = jc.COMBINED_BUILD_ARGS;
                            currentjc.LAST_MODIFIED_TS = DateTime.Now;

                            if (currentjc.DEPLOYMENT_MODEL != jc.DEPLOYMENT_MODEL)
                            {
                                currentjc.DEPLOYMENT_MODEL = currentjc.DEPLOYMENT_MODEL + "," + jc.DEPLOYMENT_MODEL;
                            }
                            currentjc.OPERATOR_ID = "SFMigration";
                        }
                    }
                    catch (Exception ex)
                    {
                        this.log.Error(ex.Message);
                    }
                }

                m1.SaveChanges();
                return true;
            }
        }

        public bool InsertSolutions(List<string> slnlist)
        {
            using (DevBuildTypeContext context = new DevBuildTypeContext())
            {
                List<SOLUTION> db_sln = context.SOLUTIONs.ToList();
                foreach (string str in slnlist)
                {
                    string slnName = str;
                    string modelName = slnName.Split('/')[1];
                    Guid modelID = context.MODULEs.Where(aa => aa.MODULE_NAME == modelName).Select(aa => aa.MODULE_ID).FirstOrDefault();
                    if (db_sln.Where(a => a.SOLUTION_NAME == slnName).Count() == 0)
                    {
                        SOLUTION sln = new SOLUTION();
                        sln.MODULE_ID = modelID;
                        sln.SOLUTION_NAME = slnName;
                        sln.OPERATOR_ID = "Migration";
                        sln.CREATED_TS = DateTime.Now;
                        sln.LAST_MODIFIED_TS = DateTime.Now;
                        sln.SOLUTION_ID = Guid.NewGuid();

                        if (modelID == Guid.Empty)
                        {
                            Console.WriteLine("Invalid");
                        }
                        else if (modelID == default(Guid))
                        {
                            Console.WriteLine("Invalid");
                        }

                        context.SOLUTIONs.Add(sln);
                    }

                }
                context.SaveChanges();
            }
            return false;
        }

        public bool InsertPackages(List<string> pkgList)
        {
            List<PACKAGE> pkg = new List<PACKAGE>();
            using (DevBuildTypeContext context = new DevBuildTypeContext())
            {
                List<PACKAGE> db_pkg = context.PACKAGEs.ToList();
                foreach (string s in pkgList)
                {
                    string[] pkgarray = s.Split('$');
                    string pkgName = pkgarray[0];
                    string ModelName = pkgarray[1];
                    if (db_pkg.Where(a => a.PACKAGE_NAME == pkgName).Count() == 0)
                    {
                        PACKAGE pkgobj = new PACKAGE()
                        {
                            PACKAGE_ID = Guid.NewGuid(),
                            PACKAGE_NAME = pkgName,
                            OPERATOR_ID = "Migration",
                            CREATED_TS = DateTime.Now,
                            LAST_MODIFIED_TS = DateTime.Now,
                            MODULE_ID = context.MODULEs.Where(aa => aa.MODULE_NAME == ModelName).Select(aa => aa.MODULE_ID).FirstOrDefault()
                        };

                        if (pkgobj.MODULE_ID == Guid.Empty)
                        {
                            Console.WriteLine("Invalid");
                        }
                        else if (pkgobj.MODULE_ID == default(Guid))
                        {
                            Console.WriteLine("Invalid");
                        }
                        context.PACKAGEs.Add(pkgobj);
                    }
                }
                context.SaveChanges();
                return true;
            }
        }

        public bool InsertDependency(List<DependencyModel> dependencyModel)
        {
            using (DevBuildTypeContext context = new DevBuildTypeContext())
            {
                List<MODULE> modl = context.MODULEs.ToList();
                List<SOLUTION> slns = context.SOLUTIONs.ToList();
                List<PACKAGE> pkgs = context.PACKAGEs.ToList();
                List<SLTN_PKG_DEPENDENCY> slndep = context.SLTN_PKG_DEPENDENCY.ToList();
                foreach (DependencyModel dm in dependencyModel)
                {
                    //Case solution
                    //Guid slnGuid = context.SOLUTIONs.Where(aa => aa.SOLUTION_NAME.ToLower() == dm.SolutionName.ToLower()).Select(aa => aa.SOLUTION_ID).FirstOrDefault();

                    Guid slnGuid = slns.Where(aa => aa.SOLUTION_NAME.ToLower() == dm.SolutionName.ToLower()).Select(aa => aa.SOLUTION_ID).FirstOrDefault();

                    if (slnGuid == Guid.Empty || slnGuid == default(Guid))
                    {
                        Console.WriteLine("Sln not exist");
                        string slnName = dm.SolutionName;
                        string[] slnarray = slnName.Split('/');
                        string moduleName = slnarray[1];

                        slnGuid = Guid.NewGuid();
                        SOLUTION sln = new SOLUTION()
                        {
                            SOLUTION_ID = slnGuid,
                            SOLUTION_NAME = slnName,
                            OPERATOR_ID = "Dependency Migration",
                            MODULE_ID = modl.Where(aa => aa.MODULE_NAME == moduleName).Select(aa => aa.MODULE_ID).FirstOrDefault(),
                            CREATED_TS = DateTime.Now,
                            LAST_MODIFIED_TS = DateTime.Now
                        };
                    }

                    //case package
                    foreach (string pkgname in dm.PackageName)
                    {

                        string modelName = string.Empty;
                        //Guid pkgGuid = context.PACKAGEs.Where(aa => aa.PACKAGE_NAME.ToLower() == pkgname.ToLower()).Select(aa => aa.PACKAGE_ID).FirstOrDefault();
                        Guid pkgGuid = pkgs.Where(aa => aa.PACKAGE_NAME.ToLower() == pkgname.ToLower()).Select(aa => aa.PACKAGE_ID).FirstOrDefault();

                        if (pkgGuid == Guid.Empty || pkgGuid == default(Guid))
                        {
                            if (pkgname.ToLower().StartsWith("hpe.") || pkgname.ToLower().StartsWith("hp."))
                            {
                                string[] pkgarray = pkgname.Split('.');
                                modelName = pkgarray[3];
                            }
                            else if (pkgname.ToLower().StartsWith("hpp."))
                            {
                                string[] pkgarray = pkgname.Split('.');
                                modelName = pkgarray[1];
                            }
                            else
                            {
                                modelName = "ThirdPartySource";
                            }
                            pkgGuid = Guid.NewGuid();
                            context.PACKAGEs.Add(new PACKAGE()
                            {
                                PACKAGE_ID = pkgGuid,
                                PACKAGE_NAME = pkgname,
                                OPERATOR_ID = "DependencyMigration",
                                CREATED_TS = DateTime.Now,
                                LAST_MODIFIED_TS = DateTime.Now,
                                MODULE_ID = modl.Where(aa => aa.MODULE_NAME == modelName).Select(aa => aa.MODULE_ID).FirstOrDefault(),
                            });
                        }

                        Guid pkgdepGuid = slndep.Where(aa => aa.PACKAGE_ID == pkgGuid && aa.SOLUTION_ID == slnGuid).Select(aa => aa.SLTN_PKG_DEPENDENCY_ID).FirstOrDefault();

                        if (pkgdepGuid == Guid.Empty || pkgdepGuid == default(Guid))
                        {
                            context.SLTN_PKG_DEPENDENCY.Add(new SLTN_PKG_DEPENDENCY()
                            {
                                SLTN_PKG_DEPENDENCY_ID = Guid.NewGuid(),
                                PACKAGE_ID = pkgGuid,
                                SOLUTION_ID = slnGuid,
                                OPERATOR_ID = "DependencyMigration",
                                CREATED_TS = DateTime.Now,
                                LAST_MODIFIED_TS = DateTime.Now
                            });
                        }                     

                    }
                    // dependency
                }
                context.SaveChanges();
                return false;
            }
        }
    }
}