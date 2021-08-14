//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Data;
using RightsModels;

namespace RightsDAL
{
    public class RightsDataAccess
    {
        public string ErrorMessage { get; private set; }

        public RightsDataAccess()
        {
        }

        public List<TenantModel> GetTenantList(RightsConnection cn)
        {
            List<TenantModel> results = new List<TenantModel>();

            IDataReader data = cn.ExecuteQuery($"SELECT TENANT_ID, TENANT_NAME FROM {GetQuerySchema(cn)}TENANT WHERE IS_ACTIVE = 1");

            if (data != null)
            {
                while (data.Read())
                {
                    results.Add(new TenantModel() 
                    { 
                        Id = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(0)), 
                        Name = data.GetString(1)
                    });
                }

                data.Close();
                data.Dispose();
            }

            return results;
        }

        public ModuleModel GetTenantModuleForModuleName(RightsConnection cn, string TenantId, string ModuleName)
        {
            ModuleModel result = new ModuleModel();

            IDataReader data = cn.ExecuteQuery("SELECT tm.MODULE_ID, tm.TM_ID, m.MODULE_NAME " + 
                $" FROM {GetQuerySchema(cn)}TENANT_MODULE tm" +
                $" LEFT JOIN {GetQuerySchema(cn)}MODULE m ON (tm.MODULE_ID = m.MODULE_ID AND m.IS_ACTIVE = 1)" +
                $" WHERE tm.TENANT_ID = '{ Conversions.convertToGuid(cn.IsOracle, TenantId) }' AND m.MODULE_NAME = '{ ModuleName }' AND tm.IS_ACTIVE = 1");

            if (data != null)
            {
                data.Read();
                result.ModuleId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(0));
                result.TenantModuleId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(1));
                result.Name = data.GetString(2);

                data.Close();
                data.Dispose();
            }

            return result;
        }

        public List<ModuleModel> GetTenantModuleList(RightsConnection cn, string TenantId)
        {
            List<ModuleModel> results = new List<ModuleModel>();

            IDataReader data = cn.ExecuteQuery("SELECT t.TM_ID, m.MODULE_ID, m.MODULE_NAME FROM TENANT_MODULE t " +
                $" LEFT JOIN {GetQuerySchema(cn)}MODULE m ON(m.MODULE_ID = t.MODULE_ID) " +
                $" WHERE t.TENANT_ID = '{ Conversions.convertToGuid(cn.IsOracle, TenantId) }' AND t.IS_ACTIVE = 1 AND m.IS_ACTIVE = 1 " +
                " ORDER BY m.MODULE_NAME");

            if (data != null)
            {
                while (data.Read())
                {
                    results.Add(new ModuleModel()
                    {
                        TenantModuleId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(0)),
                        ModuleId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(1)),
                        Name = data.GetString(2)
                    });
                }

                data.Close();
                data.Dispose();
            }

            return results;
        }

        public DataListModel GetDataListForContentId(RightsConnection cn, string TenantModuleId, string DataListName)
        {
            DataListModel result = new DataListModel();

            IDataReader data = cn.ExecuteQuery("SELECT TM_DATA_LISTS_ID, TM_ID, CONTENT_ID, TM_DATA_LISTS_NAME, DESCRIPTION " +
                $" FROM {GetQuerySchema(cn)}TM_DATA_LISTS" +
                $" WHERE IS_ACTIVE = 1 AND TM_ID = '{ Conversions.convertToGuid(cn.IsOracle, TenantModuleId) }' AND CONTENT_ID = '{DataListName}'");

            if (data != null)
            {
                data.Read();
                result.DataListId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(0));
                result.TenantModuleId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(1));
                result.ContentId = data.GetString(2);
                result.Name = data.GetString(3);
                result.ContentId = data.GetString(4);
                
                data.Close();
                data.Dispose();
            }

            return result;
        }


        public List<DataListItemModel> GetDataListItems(RightsConnection cn, string DataListId)
        {
            List<DataListItemModel> results = new List<DataListItemModel>();

            IDataReader data = cn.ExecuteQuery("SELECT TM_DATA_LISTS_ITEM_ID, TM_DATA_LISTS_ID, TM_DATA_LISTS_ITEM_KEY, CONTENT_ID, LIST_TYPE " +
                $" FROM {GetQuerySchema(cn)}TM_DATA_LISTS_ITEM " +
                $" WHERE IS_ACTIVE = 1 AND TM_DATA_LISTS_ID = '{ Conversions.convertToGuid(cn.IsOracle, DataListId) }' " +
                " ORDER BY TM_DATA_LISTS_ITEM_KEY");

            if (data != null)
            {
                while (data.Read())
                {
                    results.Add(new DataListItemModel()
                    {
                        DataListItemId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(0)),
                        DataListId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(1)),
                        ItemKey = data.GetString(2),
                        ContentId = data.GetString(3),
                        ListType = data.GetString(4)
                    });
                }

                data.Close();
                data.Dispose();
            }

            return results;
        }

        public List<DataListLinkModel> GetDataListItemLinks(RightsConnection cn, string ParentDataListId, string ChildDataListId)
        {
            List<DataListLinkModel> results = new List<DataListLinkModel>();

            IDataReader data = cn.ExecuteQuery("SELECT Parent_ID_FK, Child_ID_FK " +
                $" FROM {GetQuerySchema(cn)}TM_DATA_LISTS_ITEM_LINK link " +
                $" LEFT JOIN {GetQuerySchema(cn)}TM_DATA_LISTS_ITEM parent ON (link.Parent_ID_FK = parent.TM_DATA_LISTS_ITEM_ID) " +
                $" LEFT JOIN {GetQuerySchema(cn)}TM_DATA_LISTS_ITEM child ON (link.Child_ID_FK = child.TM_DATA_LISTS_ITEM_ID) " +
                " WHERE link.IS_ACTIVE = 1 " +
                $" AND parent.TM_DATA_LISTS_ID = '{ Conversions.convertToGuid(cn.IsOracle, ParentDataListId) }' " +
                $" AND child.TM_DATA_LISTS_ID = '{ Conversions.convertToGuid(cn.IsOracle, ChildDataListId) }'");

            if (data != null)
            {
                while (data.Read())
                {
                    results.Add(new DataListLinkModel()
                    {
                        ParentListId = ParentDataListId,
                        ChildListId = ChildDataListId,
                        ParentListItemId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(0)),
                        ChildListItemId = Conversions.convertToRaw(cn.IsOracle, data.GetGuid(1))
                    });
                }

                data.Close();
                data.Dispose();
            }

            return results;
        }

        private string GetQuerySchema(RightsConnection cn)
        {
            return (cn.IsOracle && !string.IsNullOrEmpty(cn.OracleSchema)) ? $"{cn.OracleSchema}." : "";
        }
    }
}
