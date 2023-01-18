using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HPE.HSP.UA3.Core.API.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PECopyEnrollment
{
    public class EnrollmentCopier
    {
        private string tenantId;
        private string sourceConnectionString;
        private string sourceConnectionProvider;

        public void Copy(string trackingId, int tenantCode)
        {

            this.GetSourceTenant(tenantCode);

            EnrollmentApplication application = this.GetSourceApplication(trackingId);

            this.CopyToDestination(application);
        }

        private void GetSourceTenant(int tenantCode)
        {
            switch (tenantCode)
            {
                case 1:
                    this.tenantId = ConfigurationManager.AppSettings["TenantIdMedicaid"];
                    sourceConnectionString = ConfigurationManager.AppSettings["SourceConnectionStringMedicaid"];
                    sourceConnectionProvider = ConfigurationManager.AppSettings["SourceConnectionProviderMedicaid"];
                    break;
                case 2:
                    this.tenantId = ConfigurationManager.AppSettings["TenantIdCommercial"];
                    sourceConnectionString = ConfigurationManager.AppSettings["SourceConnectionStringCommercial"];
                    sourceConnectionProvider = ConfigurationManager.AppSettings["SourceConnectionProviderCommercial"];
                    break;
            }
        }

        /// <summary>
        /// Copy Enrollment in to target DB
        /// </summary>
        private void CopyToDestination(EnrollmentApplication application)
        {
            if (application != null)
            {
                this.LoadAppSettings();

                //Target part            
                TenantBasConfiguration settings = TenantBasConfiguration.GetLocal(this.tenantId);
                using (IDbSession session = new DbSession(settings.ConnectionStringProvider, settings.ConnectionString))
                {
                    using (ProviderEnrollmentDbContext context = new ProviderEnrollmentDbContext(session))
                    {
                        if (this.CheckTargetApplication(application.APPLICATION_RESUME_ID, context) == 0)
                        {
                            Console.WriteLine($"Inserting Enrollment into Target DB...");
                            if (this.InsertIntoTable(application, session) > 0)
                            {
                                Console.WriteLine($"Data inserted into Target DB : " + application.APPLICATION_RESUME_ID);
                                LoggerManager.Logger.LogInformational("Data inserted into Target DB : " + application.APPLICATION_RESUME_ID);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Data already exists in Target DB : " + application.APPLICATION_RESUME_ID);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Insert Enrollment in target DB into PE table
        /// </summary>
        private int InsertIntoTable(EnrollmentApplication application, IDbSession dataBaseSession)
        {
            var commandText = this.BuildInsertCommandText().ToString();

            DbParameter[] parameters = this.BuildInsertParameters(application, dataBaseSession);

            string buildPrefix = this.GetBindPrefix(dataBaseSession);
            commandText = Regex.Replace(commandText, @"@", buildPrefix);

            return dataBaseSession.ExecuteNonQuery(CommandType.Text, commandText, parameters);
        }

        private DbParameter[] BuildInsertParameters(EnrollmentApplication application, IDbSession dataBaseSession)
        {
            DbParameter[] parameters = new DbParameter[30];
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataBaseSession.ProviderInvariantName);

            DbParameter pPROVIDER_ENROLLMENT_ID = factory.CreateParameter();
            pPROVIDER_ENROLLMENT_ID.ParameterName = "provider_enrollment_id";
            pPROVIDER_ENROLLMENT_ID.DbType = DbType.Binary;
            pPROVIDER_ENROLLMENT_ID.IsNullable = false;
            pPROVIDER_ENROLLMENT_ID.Value = application.PROVIDER_ENROLLMENT_ID.ToByteArray();
            parameters[0] = pPROVIDER_ENROLLMENT_ID;

            DbParameter pPROVIDER_TAX_ID = factory.CreateParameter();
            pPROVIDER_TAX_ID.ParameterName = "provider_tax_id";
            pPROVIDER_TAX_ID.DbType = DbType.Int64;
            pPROVIDER_TAX_ID.Size = 15;
            pPROVIDER_TAX_ID.IsNullable = true;
            pPROVIDER_TAX_ID.Value = application.PROVIDER_TAX_ID == null ? (object)DBNull.Value : application.PROVIDER_TAX_ID;
            parameters[1] = pPROVIDER_TAX_ID;

            DbParameter pPROVIDER_NPID = factory.CreateParameter();
            pPROVIDER_NPID.ParameterName = "provider_npid";
            pPROVIDER_NPID.DbType = DbType.String;
            pPROVIDER_NPID.Size = 50;
            pPROVIDER_NPID.IsNullable = true;
            pPROVIDER_NPID.Value = application.PROVIDER_NPID == null ? (object)DBNull.Value : application.PROVIDER_NPID;
            parameters[2] = pPROVIDER_NPID;

            DbParameter pENROLLMENT_STATUS_ID = factory.CreateParameter();
            pENROLLMENT_STATUS_ID.ParameterName = "enrollment_status_id";
            pENROLLMENT_STATUS_ID.DbType = DbType.Int64;
            pENROLLMENT_STATUS_ID.Size = 30;
            pENROLLMENT_STATUS_ID.IsNullable = false;
            pENROLLMENT_STATUS_ID.Value = application.ENROLLMENT_STATUS_ID == null ? (object)DBNull.Value : application.ENROLLMENT_STATUS_ID;
            parameters[3] = pENROLLMENT_STATUS_ID;

            DbParameter pENROLLMENT_STATUS_RSN = factory.CreateParameter();
            pENROLLMENT_STATUS_RSN.ParameterName = "enrollment_status_rsn";
            pENROLLMENT_STATUS_RSN.DbType = DbType.String;
            pENROLLMENT_STATUS_RSN.Size = 400;
            pENROLLMENT_STATUS_RSN.IsNullable = true;
            pENROLLMENT_STATUS_RSN.Value = string.IsNullOrWhiteSpace(application.ENROLLMENT_STATUS_RSN) ? (object)DBNull.Value : application.ENROLLMENT_STATUS_RSN;
            parameters[4] = pENROLLMENT_STATUS_RSN;

            DbParameter pCREATED_TS = factory.CreateParameter();
            pCREATED_TS.ParameterName = "created_ts";
            pCREATED_TS.DbType = DbType.DateTime;
            pCREATED_TS.IsNullable = false;
            pCREATED_TS.Value = application.CREATED_TS;
            parameters[5] = pCREATED_TS;

            DbParameter pSUBMITTED_TS = factory.CreateParameter();
            pSUBMITTED_TS.ParameterName = "submitted_ts";
            pSUBMITTED_TS.DbType = DbType.DateTime;
            pSUBMITTED_TS.IsNullable = true;
            pSUBMITTED_TS.Value = application.SUBMITTED_TS == null ? (object)DBNull.Value : application.SUBMITTED_TS;
            parameters[6] = pSUBMITTED_TS;

            DbParameter pRESPONSE_TS = factory.CreateParameter();
            pRESPONSE_TS.ParameterName = "response_ts";
            pRESPONSE_TS.DbType = DbType.DateTime;
            pRESPONSE_TS.IsNullable = true;
            pRESPONSE_TS.Value = application.RESPONSE_TS == null ? (object)DBNull.Value : application.RESPONSE_TS;
            parameters[7] = pRESPONSE_TS;

            DbParameter pAPPLICATION_RESUME_CD = factory.CreateParameter();
            pAPPLICATION_RESUME_CD.ParameterName = "application_resume_cd";
            pAPPLICATION_RESUME_CD.DbType = DbType.String;
            pAPPLICATION_RESUME_CD.Size = 20;
            pAPPLICATION_RESUME_CD.IsNullable = true;
            pAPPLICATION_RESUME_CD.Value = (object)DBNull.Value;
            parameters[8] = pAPPLICATION_RESUME_CD;

            DbParameter pLAST_MODIFIED_TS = factory.CreateParameter();
            pLAST_MODIFIED_TS.ParameterName = "last_modified_ts";
            pLAST_MODIFIED_TS.DbType = DbType.DateTime;
            pLAST_MODIFIED_TS.IsNullable = false;
            pLAST_MODIFIED_TS.Value = application.LAST_MODIFIED_TS;
            parameters[9] = pLAST_MODIFIED_TS;

            DbParameter pLAST_UPDATE_BY = factory.CreateParameter();
            pLAST_UPDATE_BY.ParameterName = "last_update_by";
            pLAST_UPDATE_BY.DbType = DbType.String;
            pLAST_UPDATE_BY.Size = 50;
            pLAST_UPDATE_BY.IsNullable = true;
            pLAST_UPDATE_BY.Value = application.LAST_UPDATE_BY;
            parameters[10] = pLAST_UPDATE_BY;

            DbParameter pAPPLICATION_RESUME_ID = factory.CreateParameter();
            pAPPLICATION_RESUME_ID.ParameterName = "application_resume_id";
            pAPPLICATION_RESUME_ID.DbType = DbType.String;
            pAPPLICATION_RESUME_ID.Size = 12;
            pAPPLICATION_RESUME_ID.IsNullable = true;
            pAPPLICATION_RESUME_ID.Value = application.APPLICATION_RESUME_ID;
            parameters[11] = pAPPLICATION_RESUME_ID;

            DbParameter pEMAIL_ADR = factory.CreateParameter();
            pEMAIL_ADR.ParameterName = "email_adr";
            pEMAIL_ADR.DbType = DbType.String;
            pEMAIL_ADR.Size = 150;
            pEMAIL_ADR.IsNullable = true;
            pEMAIL_ADR.Value = application.EMAIL_ADR == null ? (object)DBNull.Value : application.EMAIL_ADR;
            parameters[12] = pEMAIL_ADR;

            DbParameter pPROVIDER_CITY = factory.CreateParameter();
            pPROVIDER_CITY.ParameterName = "provider_city";
            pPROVIDER_CITY.DbType = DbType.String;
            pPROVIDER_CITY.Size = 100;
            pPROVIDER_CITY.IsNullable = true;
            pPROVIDER_CITY.Value = application.PROVIDER_CITY == null ? (object)DBNull.Value : application.PROVIDER_CITY;
            parameters[13] = pPROVIDER_CITY;

            DbParameter pPROVIDER_STATE = factory.CreateParameter();
            pPROVIDER_STATE.ParameterName = "provider_state";
            pPROVIDER_STATE.DbType = DbType.String;
            pPROVIDER_STATE.Size = 100;
            pPROVIDER_STATE.IsNullable = true;
            pPROVIDER_STATE.Value = string.IsNullOrWhiteSpace(application.PROVIDER_STATE) ? (object)DBNull.Value : application.PROVIDER_STATE;
            parameters[13] = pPROVIDER_STATE;

            DbParameter pPROVIDER_FIRST_NAME = factory.CreateParameter();
            pPROVIDER_FIRST_NAME.ParameterName = "provider_first_name";
            pPROVIDER_FIRST_NAME.DbType = DbType.String;
            pPROVIDER_FIRST_NAME.Size = 100;
            pPROVIDER_FIRST_NAME.IsNullable = true;
            pPROVIDER_FIRST_NAME.Value = string.IsNullOrWhiteSpace(application.PROVIDER_FIRST_NAME) ? (object)DBNull.Value : application.PROVIDER_FIRST_NAME;
            parameters[14] = pPROVIDER_FIRST_NAME;

            DbParameter pPROVIDER_LAST_NAME = factory.CreateParameter();
            pPROVIDER_LAST_NAME.ParameterName = "provider_last_name";
            pPROVIDER_LAST_NAME.DbType = DbType.String;
            pPROVIDER_LAST_NAME.Size = 100;
            pPROVIDER_LAST_NAME.IsNullable = true;
            pPROVIDER_LAST_NAME.Value = string.IsNullOrWhiteSpace(application.PROVIDER_LAST_NAME) ? (object)DBNull.Value : application.PROVIDER_LAST_NAME;
            parameters[15] = pPROVIDER_LAST_NAME;

            DbParameter pPROVIDER_MIDDLE_NAME = factory.CreateParameter();
            pPROVIDER_MIDDLE_NAME.ParameterName = "provider_middle_name";
            pPROVIDER_MIDDLE_NAME.DbType = DbType.String;
            pPROVIDER_MIDDLE_NAME.Size = 100;
            pPROVIDER_MIDDLE_NAME.IsNullable = true;
            pPROVIDER_MIDDLE_NAME.Value = string.IsNullOrWhiteSpace(application.PROVIDER_MIDDLE_NAME) ? (object)DBNull.Value : application.PROVIDER_MIDDLE_NAME;
            parameters[16] = pPROVIDER_MIDDLE_NAME;

            DbParameter pPROVIDER_BUSINESS_NAME = factory.CreateParameter();
            pPROVIDER_BUSINESS_NAME.ParameterName = "provider_business_name";
            pPROVIDER_BUSINESS_NAME.DbType = DbType.String;
            pPROVIDER_BUSINESS_NAME.Size = 100;
            pPROVIDER_BUSINESS_NAME.IsNullable = true;
            pPROVIDER_BUSINESS_NAME.Value = string.IsNullOrWhiteSpace(application.PROVIDER_BUSINESS_NAME) ? (object)DBNull.Value : application.PROVIDER_BUSINESS_NAME;
            parameters[17] = pPROVIDER_BUSINESS_NAME;

            DbParameter pLEGACY_PROVIDER_ID = factory.CreateParameter();
            pLEGACY_PROVIDER_ID.ParameterName = "legacy_provider_id";
            pLEGACY_PROVIDER_ID.DbType = DbType.String;
            pLEGACY_PROVIDER_ID.Size = 14;
            pLEGACY_PROVIDER_ID.IsNullable = true;
            pLEGACY_PROVIDER_ID.Value = string.IsNullOrWhiteSpace(application.LEGACY_PROVIDER_ID) ? (object)DBNull.Value : application.LEGACY_PROVIDER_ID;
            parameters[18] = pLEGACY_PROVIDER_ID;

            DbParameter pAPPLICATION_TYPE = factory.CreateParameter();
            pAPPLICATION_TYPE.ParameterName = "application_type";
            pAPPLICATION_TYPE.DbType = DbType.String;
            pAPPLICATION_TYPE.Size = 2;
            pAPPLICATION_TYPE.IsNullable = true;
            pAPPLICATION_TYPE.Value = application.APPLICATION_TYPE == null ? (object)DBNull.Value : application.APPLICATION_TYPE;
            parameters[19] = pAPPLICATION_TYPE;

            DbParameter pPROVIDER_TYPE = factory.CreateParameter();
            pPROVIDER_TYPE.ParameterName = "provider_type";
            pPROVIDER_TYPE.DbType = DbType.String;
            pPROVIDER_TYPE.Size = 50;
            pPROVIDER_TYPE.IsNullable = true;
            pPROVIDER_TYPE.Value = application.PROVIDER_TYPE == null ? (object)DBNull.Value : application.PROVIDER_TYPE;
            parameters[20] = pPROVIDER_TYPE;

            DbParameter pENROLLMENT_TYPE = factory.CreateParameter();
            pENROLLMENT_TYPE.ParameterName = "enrollment_type";
            pENROLLMENT_TYPE.DbType = DbType.String;
            pENROLLMENT_TYPE.Size = 50;
            pENROLLMENT_TYPE.IsNullable = true;
            pENROLLMENT_TYPE.Value = application.ENROLLMENT_TYPE == null ? (object)DBNull.Value : application.ENROLLMENT_TYPE;
            parameters[21] = pENROLLMENT_TYPE;

            DbParameter pAPPLICATION_DTL = factory.CreateParameter();
            pAPPLICATION_DTL.ParameterName = "application_dtl";
            pAPPLICATION_DTL.DbType = DbType.Binary;
            pAPPLICATION_DTL.IsNullable = true;
            pAPPLICATION_DTL.Value = application.APPLICATION_DTL == null ? (object)DBNull.Value : application.APPLICATION_DTL;
            parameters[22] = pAPPLICATION_DTL;

            DbParameter pPOSTAL_CD = factory.CreateParameter();
            pPOSTAL_CD.ParameterName = "postal_cd";
            pPOSTAL_CD.DbType = DbType.String;
            pPOSTAL_CD.Size = 15;
            pPOSTAL_CD.IsNullable = true;
            pPOSTAL_CD.Value = application.POSTAL_CD == null ? (object)DBNull.Value : application.POSTAL_CD;
            parameters[23] = pPOSTAL_CD;

            DbParameter pREASON_EXPIRED = factory.CreateParameter();
            pREASON_EXPIRED.ParameterName = "reason_expired";
            pREASON_EXPIRED.DbType = DbType.String;
            pREASON_EXPIRED.Size = 200;
            pREASON_EXPIRED.IsNullable = true;
            pREASON_EXPIRED.Value = string.IsNullOrWhiteSpace(application.REASON_EXPIRED) ? (object)DBNull.Value : application.REASON_EXPIRED;
            parameters[24] = pREASON_EXPIRED;

            DbParameter pTAX_ID_TYPE = factory.CreateParameter();
            pTAX_ID_TYPE.ParameterName = "tax_id_type";
            pTAX_ID_TYPE.DbType = DbType.String;
            pTAX_ID_TYPE.Size = 20;
            pTAX_ID_TYPE.IsNullable = true;
            pTAX_ID_TYPE.Value = application.TAX_ID_TYPE == null ? (object)DBNull.Value : application.TAX_ID_TYPE;
            parameters[25] = pTAX_ID_TYPE;

            DbParameter pPREFERRED_COMM_LOCALE_ID = factory.CreateParameter();
            pPREFERRED_COMM_LOCALE_ID.ParameterName = "preferred_comm_locale_id";
            pPREFERRED_COMM_LOCALE_ID.DbType = DbType.String;
            pPREFERRED_COMM_LOCALE_ID.Size = 5;
            pPREFERRED_COMM_LOCALE_ID.IsNullable = true;
            pPREFERRED_COMM_LOCALE_ID.Value = application.PREFERRED_COMM_LOCALE_ID == null ? (object)DBNull.Value : application.PREFERRED_COMM_LOCALE_ID;
            parameters[26] = pPREFERRED_COMM_LOCALE_ID;

            DbParameter pFINALIZED_TS = factory.CreateParameter();
            pFINALIZED_TS.ParameterName = "finalized_ts";
            pFINALIZED_TS.DbType = DbType.DateTime;
            pFINALIZED_TS.IsNullable = true;
            pFINALIZED_TS.Value = application.FINALIZED_TS == null ? (object)DBNull.Value : application.FINALIZED_TS;
            parameters[27] = pFINALIZED_TS;

            DbParameter pIS_RTP = factory.CreateParameter();
            pIS_RTP.ParameterName = "is_rtp";
            pIS_RTP.DbType = DbType.Int64;
            pIS_RTP.IsNullable = true;
            pIS_RTP.Value = application.IS_RTP == null ? (object)DBNull.Value : application.IS_RTP;
            parameters[28] = pIS_RTP;

            DbParameter pENROLLMENT_UPDATED_TS = factory.CreateParameter();
            pENROLLMENT_UPDATED_TS.ParameterName = "ENROLLMENT_UPDATED_TS";
            pENROLLMENT_UPDATED_TS.DbType = DbType.DateTime;
            pENROLLMENT_UPDATED_TS.IsNullable = false;
            pENROLLMENT_UPDATED_TS.Value = application.ENROLLMENT_UPDATED_TS == null ? (object)DBNull.Value : application.ENROLLMENT_UPDATED_TS;
            parameters[29] = pENROLLMENT_UPDATED_TS;

            return parameters;
        }

        private StringBuilder BuildInsertCommandText()
        {
            return new StringBuilder()
                .Append(@"INSERT INTO UA3_PROVIDER_ENROLL.provider_enrollment (")
              .Append(@"provider_enrollment_id, ")
              .Append(@"provider_tax_id, ")
              .Append(@"provider_npid, ")
              .Append(@"enrollment_status_id, ")
              .Append(@"enrollment_status_rsn, ")
              .Append(@"created_ts, ")
              .Append(@"submitted_ts, ")
              .Append(@"response_ts, ")
              .Append(@"application_resume_cd, ")
              .Append(@"last_modified_ts, ")
              .Append(@"last_update_by, ")
              .Append(@"application_resume_id, ")
              .Append(@"email_adr, ")
              .Append(@"provider_state, ")
              .Append(@"provider_first_name, ")
              .Append(@"provider_last_name, ")
              .Append(@"provider_middle_name, ")
              .Append(@"provider_business_name, ")
              .Append(@"legacy_provider_id, ")
              .Append(@"application_type, ")
              .Append(@"provider_type, ")
              .Append(@"enrollment_type, ")
              .Append(@"application_dtl, ")
              .Append(@"postal_cd, ")
              .Append(@"reason_expired, ")
              .Append(@"tax_id_type, ")
              .Append(@"preferred_comm_locale_id, ")
              .Append(@"finalized_ts, ")
              .Append(@"is_rtp, ")
              .Append(@"ENROLLMENT_UPDATED_TS) VALUES (")
              .Append(@"@provider_enrollment_id, ")
              .Append(@"@provider_tax_id, ")
              .Append(@"@provider_npid, ")
              .Append(@"@enrollment_status_id, ")
              .Append(@"@enrollment_status_rsn, ")
              .Append(@"@created_ts, ")
              .Append(@"@submitted_ts, ")
              .Append(@"@response_ts, ")
              .Append(@"@application_resume_cd, ")
              .Append(@"@last_modified_ts, ")
              .Append(@"@last_update_by, ")
              .Append(@"@application_resume_id, ")
              .Append(@"@email_adr, ")
              .Append(@"@provider_state, ")
              .Append(@"@provider_first_name, ")
              .Append(@"@provider_last_name, ")
              .Append(@"@provider_middle_name, ")
              .Append(@"@provider_business_name, ")
              .Append(@"@legacy_provider_id, ")
              .Append(@"@application_type, ")
              .Append(@"@provider_type, ")
              .Append(@"@enrollment_type, ")
              .Append(@"@application_dtl, ")
              .Append(@"@postal_cd, ")
              .Append(@"@reason_expired, ")
              .Append(@"@tax_id_type, ")
              .Append(@"@preferred_comm_locale_id, ")
              .Append(@"@finalized_ts, ")
              .Append(@"@is_rtp, ")
              .Append(@"@ENROLLMENT_UPDATED_TS)");
        }

        private string GetBindPrefix(IDbSession dataBaseSession)
        {
            string bindPrefix = string.Empty;
            if (dataBaseSession != null && !string.IsNullOrEmpty(dataBaseSession.ProviderInvariantName) && dataBaseSession.ProviderInvariantName.ToLower().Contains("oracle"))
            {
                bindPrefix = ":";
            }
            else
            {
                bindPrefix = "@";
            }

            return bindPrefix;
        }

        /// <summary>
        /// Check Enrollment exists in target DB
        /// </summary>        
        private int CheckTargetApplication(string ID, ProviderEnrollmentDbContext context)
        {
            Console.WriteLine($"Checking Enrollment from Target DB...");

            string query = "SELECT PROVIDER_ENROLLMENT_ID FROM UA3_PROVIDER_ENROLL.PROVIDER_ENROLLMENT WHERE APPLICATION_RESUME_ID = '" + ID + "'";

            return context.Database.SqlQuery<EnrollmentApplication>(query).ToList().Count;
        }

        /// <summary>
        /// Get Enrollment from Source DB
        /// </summary>
        private EnrollmentApplication GetSourceApplication(string ID)
        {
            List<EnrollmentApplication> applications = new List<EnrollmentApplication>();

            Console.WriteLine($"Loading enrollment from source DB.....");

            //Source part

            using (IDbSession session = new DbSession(this.sourceConnectionProvider, this.sourceConnectionString))
            {
                using (ProviderEnrollmentDbContext context = new ProviderEnrollmentDbContext(session))
                {
                    string query = "SELECT * FROM UA3_PROVIDER_ENROLL.PROVIDER_ENROLLMENT WHERE ENROLLMENT_STATUS_ID = '4' and APPLICATION_RESUME_ID = '" + ID + "'";
                    applications = context.Database.SqlQuery<EnrollmentApplication>(query).ToList();
                }
            }

            if (applications.Any())
            {
                Console.WriteLine($"Loaded " + applications.Count + " unsubmitted enrollment from source DB");
            }
            else
            {
                Console.WriteLine($"Unsubmitted enrollment does not exist in source DB/ not in Partial status.");
            }
            Console.WriteLine($"----------------------------------------------");

            return applications.FirstOrDefault();
        }

        private void LoadAppSettings()
        {
            if (ApplicationConfigurationManager.AppSettings == null)
            {
                Console.WriteLine($"Loading Application Configuration from Tenant DB for Tenant: " + this.tenantId);

                ApplicationConfigurationManager.LoadApplicationConfiguration(this.tenantId, new CacheManager());

                Console.WriteLine($"Loaded Application Configuration from Tenant DB!");
                Console.WriteLine("\n");
            }
        }
    }
}
