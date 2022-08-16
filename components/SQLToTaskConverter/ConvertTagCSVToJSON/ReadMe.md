SQLToTaskConverter

Definition

Utility when configured will execute queries written to return output to be input into the Task Management TaskBulkUpload process to create tasks.  This process should be executed after the Managed Care TaskMemberError process.

Configuration

The utility requires that the following parameters be supplied  Input directory where queries and their corresponding parameter file exist.  This directory should contain a Summary subfolder where output status along with their execution time will be reported for each file within a summary file.  A work directory the utility requires in execution.  An output directory where the json files will be written to.  This location should be the input location for the Task Management TaskBulkUpload process.
An Example of these parameters would look as follows:
C:\Temp\Input C:\Temp\Work C:\Temp\Output

The following application setting will need to be adjusted when the utility is deployed
<add key="CacheConnectionOptions" value="" />
<add key="TenantId" value=""/>
<add key="DefaultConnectionString" value="" />
<add key="DefaultConnectionProvider" value="Oracle.ManagedDataAccess.Client" />
 <add key="DefaultConnectionSchema" value="" />
 <add key="EnableStringDataTypeOverride" value="false"/>

The connection properties to the Tenant will also need to be configured for the environment you are running on.

Execution

Execution of the utility will read in each .sql file within the directory, search for a corresponding parameter file, if one exists, perform string replacements within the query replacing the parameter name with the parameter value.  The query will then be executed and converted into input strings necessary for the Task Management TaskBulkUpload process.
The following is the format of the parameter file
[
  {
    "ParameterName": "BEGINMONTHMINUSONEDAY",
    "ParameterValue": "20220331"
  },
  {
    "ParameterName": "BEGINMONTH",
    "ParameterValue": "20220401"
  },
  {
    "ParameterName": "ENDMONTH",
    "ParameterValue": "20220430"
  },
  {
    "ParameterName": "EXECUTIONDAY",
    "ParameterValue": "20220407"
  }
]
 
Before execution if the user chooses, they can replace the values in the parameter file with values to be substituted at execution time.
To bypass the execution of a query simply place the following characters [-–] in front of the sql file.  The utility will bypass execution of the query and will report it out as being bypassed.
New Queries 
For new queries to be executed the following format below is required to be output from the query in order to ensure proper processing of the query and it to be successfully parse into the format required for the Task Management BulkUploadProcess. Please note it is important that the output of the query be aliased with the statement as Task as shown below.  This will ensure the output is processed properly.
'"' || lower(ua3_managed_care.rawtoguid(sys_guid())) || '",' ||
'"' || mbr.individual_id || '",' ||
'"MC",' ||
'"ASG",' ||
'"044",' ||
 '"Member ID: ' 
 ||  mbr.member_id
 ||  ',Member Name :' 
 ||  mbrname.FIRST_NAME
 ||  ' ' 
 ||  mbrname.MIDDLE_NAME
 ||   ' ' 
 ||  mbrname.LAST_NAME 
 ||  ',Case ID: ' 
 || case.MEDICAID_CASE_ID
 || ',Tag Code: NA' 
 || ',Benefit Plan: ' 
 || mbp.benefit_plan_cd
 || ',Eligibility Effective Date: ' 
|| TO_CHAR(mbp.eff_dt, 'MM/DD/YYYY')
|| ',Eligibility End Date: ' 
|| TO_CHAR(mbp.end_dt, 'MM/DD/YYYY')
|| ',Description of the Error: Error Description of task that needs to be addressed(WorkAround)"'
 as Task

Summary Reporting

Summary reporting for each execution will be reported in JSON format and will be available within the Input parameter Summary subfolder.
