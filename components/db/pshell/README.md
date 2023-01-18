# PowerShell Scripts

Back to [Project](../../../README.md) | [Components](../../README.md) | [Databases](../README.md)

---

## Intro

This directory contains [PowerShell](https://docs.microsoft.com/en-us/powershell/) scripts to support database maintenance, scripting, documentation, and/or automation capabilities.

TODO: These scripts may need to moved into a more centralized repo
TODO: Need to add more parameterization before moving into centralized repo

### **reverse_engr_mssql.ps1**

This script does the following:

1. Connects to the requested SQL Server instance
2. Searches the SQL Server instance looking for the requested database(s)
3. For each database found
   1. Reverse engineers a SINGLE FILE, create database script
   2. Reverse engineers FILE PER OBJECT, database object DEFINITION (DDL) scripts
   3. Reverse engineers FILE PER OBJECT database table DATA (DML - Insert) scripts
   4. Organizes the files in subdirectories by database OBJECT TYPE
4. Places the files in the `Backup\{yyyymmdd}` subdirectory
   1. TODO: Need to determine how/where to place files
   2. TODO: May need to update .gitignore based upon decision above

The following are HARD-CODED values in the script per module at this time:

1. Input parameter default values
   1. $Port
   2. $User
   3. $IncludeDBs
   4. $PythoneExe
2. Root PATH to place the generated files
3. Objects to EXCLUDE for DATA script generation
   1. One per database
   2. Will need to be DYNAMIC based upon number of DBs assocaited with module
4. Value for MAXIMUM filesize for DATA scripts

### **xcopy_gen_dbobj_files.ps1**

TODO: Add input parameters

This script does the following:

1. Copies all generated database script files from the SOURCE temporary path to the corresponding `db\mssql\{database}\{objtype}` TARGET directories
2. Deletes the contents of the TARGET directories first before copying the files
   1.  Does NOT touch the `_Changes` directory
   2.  Does NOT impact the README.md file
   3.  Will CREATE a database directory, if new
   4.  TODO: Handle scenario where object type is no longer in SOURCE, but is in TARGET

The following are HARD-CODED values in the script per module at this time:

1. Root PATH to place the generated files
2. TARGET path set to `..\db\mssql`
