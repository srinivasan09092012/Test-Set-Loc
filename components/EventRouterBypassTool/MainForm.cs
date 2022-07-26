//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using EventRouterByPassTool.Common;
using EventRouterByPassTool.Entities;
using EventRouterByPassTool.Entities.ConsumedEvents;
using EventRouterByPassTool.Entities.PublishedEvents;
using EventRouterByPassTool.Repository;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Base.BulkTransactions;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace EventRouterByPassTool
{
    public partial class MainForm : Form
    {
        #region Private Objects
        private XmlDocument payloadDocument;
        private static int threadCount = 10;
        private string selectedFolderForMultipleFiles = string.Empty;
        private string connectionString = string.Empty;
        private string connectionProviderName = string.Empty;
        private string connectionSchemaName = string.Empty;
        private int masiveLoad;
        private string aggregateRootIdElement = string.Empty;
        private string moduleName = string.Empty;
        private List<ExternalReq> externalReqList = new List<ExternalReq>();
        private List<ExternalRes> externalResList = new List<ExternalRes>();
        private List<EventSubscriptionError> eventSubsErrList = new List<EventSubscriptionError>();
        private Stopwatch timeWatch;
        private string eventTypeName = string.Empty;
        private string eventNameSpace = string.Empty;
        private string eventFilterMetadata = string.Empty;
        private Guid groupId;
        private bool isLastFromGroup;
        private string aggregateRootId = string.Empty;
        private string aggregateRootType = string.Empty;
        private byte[] eventPayload;
        private byte status;
        #endregion

        #region Properties
        [Dependency]
        public IBulkTransaction BulkTransaction { get; set; }
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Protected Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.FillConnectionStringsDropDownList();
            this.FillModulesDropDownList();

            txtBoxGroupId.Enabled = false;
            numericMasive.Enabled = false;
            btnCleanTables.Enabled = false;
            txtBoxAggregate.Enabled = false;
        }
        #endregion

        #region Private Methods
        private void FillModulesDropDownList()
        {
            ListItemSection moduleIdsSection = ConfigurationManager.GetSection(EventRouterBypassConstants.DatabaseRelated.MyModulesSection) as ListItemSection;
            for (int i = 0; i < moduleIdsSection.Values.Count; i++)
            {
                this.moduleIDs.Items.Add(new ListItem()
                {
                    Name = moduleIdsSection.Values[i].Name,
                    Value = moduleIdsSection.Values[i].Value
                });
            }

            this.moduleIDs.SelectedIndex = 0;
        }

        private void FillConnectionStringsDropDownList()
        {
            ListItemSection connectionStringSection = ConfigurationManager.GetSection(EventRouterBypassConstants.DatabaseRelated.MyConnectionStringsSection) as ListItemSection;
            for (int i = 0; i < connectionStringSection.Values.Count; i++)
            {
                this.cbEndpoint.Items.Add(new ListItem()
                {
                    Name = connectionStringSection.Values[i].Name,
                    Value = connectionStringSection.Values[i].Value
                });
            }

            this.cbEndpoint.SelectedIndex = 0;
        }

        private void LoadPayload(string filePath)
        {
            string formatted = GetFormattedXml(filePath);

            tbFileName.Text = filePath;
            tbPayloadContent.Text = formatted;
            wbXML.Navigate(filePath);
        }

        private string GetFormattedXml(string filePath)
        {
            string result = string.Empty;

            try
            {
                this.payloadDocument = new XmlDocument();
                this.payloadDocument.Load(filePath);

                using (var ms = new MemoryStream())
                {
                    using (var writer = new XmlTextWriter(ms, Encoding.UTF8))
                    {
                        writer.Formatting = System.Xml.Formatting.Indented;
                        this.payloadDocument.WriteContentTo(writer);
                        writer.Flush();
                        ms.Flush();

                        ms.Position = 0;
                        using (var reader = new StreamReader(ms))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = string.Format(EventRouterBypassConstants.Messages.ErrorReadingXml, ex.Message);
                tbError.Text += (msg);
            }

            return result;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string reqResult;
            string resResult;
            string eventSubsErrResult;

            this.ClearLists();
            BuildConnectionParameters();

            EventSubscribersReadOnly subscribers = new EventSubscribersReadOnly(this.connectionString, this.connectionProviderName, EventRouterBypassConstants.DatabaseRelated.INXSchema);

            timeWatch = Stopwatch.StartNew();

            Cursor cursor = this.Cursor;
            Cursor.Current = Cursors.WaitCursor;
            this.status = radioButtonStatus1.Checked ? Byte.Parse("1") : radioButtonStatus3.Checked ? Byte.Parse("3") : Byte.Parse("0");
            buttonNormalTest.Enabled = false;
            this.GetStaticInformationFromPayload();

            reqResult = HandleExternalReq();
            tbError.Text = string.Format(EventRouterBypassConstants.Messages.TimeTakenReq, timeWatch.Elapsed.ToString());

            this.BuildExternalResList(subscribers, externalResList);
            resResult = this.AddBulkResTable(this.moduleName, externalResList);

            this.BuildEventSubscriptionErrorList();
            eventSubsErrResult = this.AddBulkEventSubsErrTable(this.moduleName, eventSubsErrList);

            tbError.Text = tbError.Text + string.Format(EventRouterBypassConstants.Messages.TimeTakenRes, timeWatch.Elapsed.ToString());

            timeWatch.Stop();
            MessageBox.Show(string.Format(EventRouterBypassConstants.Messages.TimeTakenOverall, timeWatch.Elapsed.ToString()), EventRouterBypassConstants.Messages.BulkInserSuccessful);
            buttonNormalTest.Enabled = true;
            Cursor.Current = cursor;
            this.selectedFolderForMultipleFiles = string.Empty;
        }

        private string HandleExternalReq()
        {
            string reqResult;
            if (checkboxMasive.Checked)
            {
                masiveLoad = int.Parse(numericMasive.Value.ToString());

                for (int i = 1; i <= masiveLoad; i++)
                {
                    this.BuildExternalReqList(new ExternalReq(), i);
                }
            }
            else
            {
                this.BuildExternalReqList(new ExternalReq());
            }

            reqResult = this.AddBulkReqTable(this.moduleName, externalReqList);
            return reqResult;
        }

        private void ClearLists()
        {
            this.externalReqList.Clear();
            this.externalResList.Clear();
            this.eventSubsErrList.Clear();
        }

        private void BuildEventSubscriptionErrorList()
        {
            foreach (ExternalRes externalRes in externalResList)
            {
                if (externalRes.SubscriptionStatus == 3)
                {
                    EventSubscriptionError eventSubscriptionErr = new EventSubscriptionError
                    {
                        ID = Guid.NewGuid(),
                        EventNamespace = externalReqList.First().EventNamespace,
                        EventTypeName = externalRes.EventTypeName,
                        EventPayload = externalReqList.First().EventPayload,
                        CommitID = externalRes.CommitId,
                        AggregateRootID = this.GetAggregateIdFromList(externalRes.CommitId),
                        AggregateRootType = externalReqList.First().AggregateRootType,
                        Version = "0",
                        EventCreated = externalRes.CreatedTS,
                        ModuleName = externalRes.ModuleName,
                        SubscriberName = externalRes.SubscriberName,
                        ErrorMessage = EventRouterBypassConstants.Messages.ErrorMessage,
                        RetryCount = 10,
                        DeadFlag = true,
                        ErrorCode = EventRouterBypassConstants.Messages.ErrorCodeBusinessException,
                        SuccessFlag = false,
                        ModifiedDate = DateTime.UtcNow,
                        IsRetryOverride = false,
                        GroupId = externalRes.GroupId
                    };

                    this.eventSubsErrList.Add(eventSubscriptionErr);
                }
            }
        }

        private string GetAggregateIdFromList(Guid commitId)
        {
            string aggregateId = externalReqList.Where(e => e.CommitId == commitId).Select(c => c.AggregateRootId).FirstOrDefault();
            return aggregateId;
        }

        private void BuildConnectionParameters()
        {
            if (string.IsNullOrEmpty(this.connectionProviderName) && string.IsNullOrEmpty(this.connectionString) && string.IsNullOrEmpty(connectionSchemaName) && string.IsNullOrEmpty(this.moduleName))
            {
                if (((ListItem)this.cbEndpoint.SelectedItem).Name.Contains(EventRouterBypassConstants.DatabaseRelated.Medicaid))
                {
                    this.connectionProviderName = EventRouterBypassConstants.DatabaseRelated.OracleProviderName;
                }

                if (((ListItem)this.cbEndpoint.SelectedItem).Name.Contains(EventRouterBypassConstants.DatabaseRelated.Commercial))
                {
                    this.connectionProviderName = EventRouterBypassConstants.DatabaseRelated.SqlServerProviderName;
                }

                this.moduleName = ((ListItem)this.moduleIDs.SelectedItem).Name;
                this.connectionString = ((ListItem)this.cbEndpoint.SelectedItem).Value;
                this.connectionSchemaName = EventRouterBypassConstants.DatabaseRelated.INXSchema;
            }
        }

        private void BuildExternalResList(EventSubscribersReadOnly subscribers, List<ExternalRes> externalResList)
        {
            List<EventSubscriptionModel> eventSubscribers = subscribers.GetSubscribers(externalReqList.First().EventTypeName, externalReqList.First().ModuleName);

            if (eventSubscribers.Any())
            {
                foreach (ExternalReq entity in externalReqList)
                {
                    foreach (EventSubscriptionModel subscription in eventSubscribers)
                    {
                        ExternalRes externalRes = new ExternalRes
                        {
                            CommitId = entity.CommitId,
                            EventTypeName = entity.EventTypeName,
                            ModuleName = entity.ModuleName,
                            SubscriberName = subscription.SubscriberName,
                            SubscriptionStatus = entity.SubscriptionStatus,
                            GroupId = entity.GroupId,
                            CreatedTS = DateTime.UtcNow,
                            LastModifiedTs = DateTime.UtcNow
                        };

                        externalResList.Add(externalRes);
                    }
                }
            }
            
        }

        private byte RandomizeStatus()
        {
            Random r = new Random();
            byte succeed = 1;
            byte failed = 3;
            byte[] values = new[] { succeed, failed };
            byte x = values[r.Next(values.Length)];
            return x;
        }

        private void GetStaticInformationFromPayload()
        {
            this.aggregateRootIdElement = tenantIds.Text;
            this.moduleName = ((ListItem)this.moduleIDs.SelectedItem).Name;
            this.eventTypeName = this.GetEventTypeName();
            this.eventNameSpace = this.GetEventNameSpace();
            this.eventFilterMetadata = this.GetEventFilterMetadata();
            this.groupId = this.GetGroupId();
            this.isLastFromGroup = this.GetIsLastFromGroup();
            this.aggregateRootId = this.GetAggregateRootId();
            this.aggregateRootType = this.GetAggregateRootType();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string fileDirectory = ConfigurationManager.AppSettings[EventRouterBypassConstants.Messages.FileDirectoryAppSetting];
            this.tenantIds.Items.Clear();
            if (string.IsNullOrWhiteSpace(fileDirectory))
            {
                fileDirectory = Environment.CurrentDirectory;
            }

            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = EventRouterBypassConstants.XmlAttributes.XmlFilter;
                dialog.InitialDirectory = fileDirectory;
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.LoadPayload(dialog.FileName);
                    foreach (XmlNode node in this.payloadDocument.FirstChild.ChildNodes)
                    {
                        this.tenantIds.Items.Add(new ListItem()
                        {
                            Name = node.Name,
                            Value = node.Value
                        });
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.payloadDocument == null)
            {
                this.payloadDocument = new XmlDocument();
            }
            this.payloadDocument.LoadXml(tbPayloadContent.Text);
            using (var sfd = new SaveFileDialog())
            {
                string fileDirectory = ConfigurationManager.AppSettings[EventRouterBypassConstants.Messages.FileDirectoryAppSetting];
                if (!string.IsNullOrWhiteSpace(fileDirectory))
                {
                    sfd.InitialDirectory = fileDirectory;
                }

                sfd.Filter = EventRouterBypassConstants.XmlAttributes.XmlFilter;
                sfd.FilterIndex = 1;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.payloadDocument.Save(sfd.FileName);
                    this.LoadPayload(sfd.FileName);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.LoadPayload(tbFileName.Text);
        }

        private void BuildExternalReqList(ExternalReq em, int i = 0)
        {
            tbError.Text += string.Empty;
            buttonNormalTest.Enabled = false;
            Guid commitId = Guid.NewGuid();

            if (checkboxMasive.Checked)
            {
                em.IsLastFromGroup = i == masiveLoad;
                em.AggregateRootId = string.IsNullOrEmpty(this.aggregateRootId) ? i.ToString() : this.aggregateRootId + i;
            }
            else
            {
                em.IsLastFromGroup = this.isLastFromGroup;
                em.AggregateRootId = this.aggregateRootId;
            }

            em.CommitId = commitId;
            em.EventTypeName = this.eventTypeName;
            em.EventNamespace = this.eventNameSpace;
            em.EventFilterMetadata = this.eventFilterMetadata;
            em.GroupId = this.groupId;
            em.Version = "0";
            em.SubscriptionStatus = this.status == 0 ? this.RandomizeStatus() : this.status;
            em.AggregateRootType = this.aggregateRootType;
            em.CreatedTS = DateTime.UtcNow;
            em.ModuleName = this.moduleName;
            em.LastModifiedTS = DateTime.UtcNow;
            em.EventPayload = this.BuildEventPayload(commitId);

            externalReqList.Add(em);
        }

        private byte[] BuildEventPayload(Guid commitId)
        {
            StringBuilder sb = new StringBuilder();
            Guid groupId = Guid.Empty;
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("  "),
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true
            };

            if (checkBoxOverwriteGroupId.Checked && Guid.TryParse(txtBoxGroupId.Text, out groupId))
            {
                foreach (XmlNode node in this.payloadDocument.FirstChild.ChildNodes)
                {
                    if (node.Name.ToLower().Contains(EventRouterBypassConstants.XmlAttributes.INGroupIdToLower))
                    {
                        node.InnerText = groupId.ToString();
                        node.InnerXml = groupId.ToString();
                        break;
                    }
                }
            }

            if (chkBoxAggregate.Checked && !string.IsNullOrEmpty(txtBoxAggregate.Text))
            {
                foreach (XmlNode node in this.payloadDocument.FirstChild.ChildNodes)
                {
                    if (node.Name.ToLower().Contains(EventRouterBypassConstants.XmlAttributes.AggregateRootTypeLower))
                    {
                        node.InnerText = txtBoxAggregate.Text;
                        node.InnerXml = txtBoxAggregate.Text;
                        break;
                    }
                }
            }

            foreach (XmlNode node in this.payloadDocument.FirstChild.ChildNodes)
            {
                if (node.Name.ToLower().Contains(EventRouterBypassConstants.XmlAttributes.CommitIdLower))
                {
                    node.InnerText = commitId.ToString();
                    node.InnerXml = commitId.ToString();
                }
            }


            using (var writer = XmlWriter.Create(sb, settings))
            {
                this.payloadDocument.WriteContentTo(writer);
                writer.Flush();
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private string GetAggregateRootType()
        {
            string agregateRootTypeInPayload;

            if (chkBoxAggregate.Checked && !string.IsNullOrEmpty(txtBoxAggregate.Text))
            {
                return txtBoxAggregate.Text;
            }
            else
            {
                XmlNodeList AggregateRootType = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.AggregateRootType);
                if (AggregateRootType == null || AggregateRootType.Count == 0)
                {
                    AggregateRootType = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.AggregateRootTypePayloadFormat);
                }

                agregateRootTypeInPayload = AggregateRootType == null ? null : AggregateRootType.Count == 0 ? null : AggregateRootType[0].InnerXml == string.Empty ? null : AggregateRootType[0].InnerXml;

                return string.IsNullOrEmpty(agregateRootTypeInPayload) ? EventRouterBypassConstants.XmlAttributes.AggregateRootTypeDefault : agregateRootTypeInPayload;
            }   
        }

        private string GetAggregateRootId()
        {
            XmlNodeList AggregateRootID = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.AggregateRootId);
            if (AggregateRootID == null || AggregateRootID.Count == 0)
            {
                AggregateRootID = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.AggregateRootIdPayloadFormat);
            }
            if (AggregateRootID == null || AggregateRootID.Count == 0)
            {
                AggregateRootID = this.payloadDocument.GetElementsByTagName(aggregateRootIdElement);
            }
            return AggregateRootID == null ? null : AggregateRootID.Count == 0 ? null : AggregateRootID[0].InnerXml == string.Empty ? null : AggregateRootID[0].InnerXml;
        }

        private bool GetIsLastFromGroup()
        {
            XmlNodeList IsLastFromGroup = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.IsLastFromGroup);
            if (IsLastFromGroup == null || IsLastFromGroup.Count == 0)
            {
                IsLastFromGroup = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.IsLastFromGroupPayloadFormat);
            }
            return IsLastFromGroup == null ? false : IsLastFromGroup.Count == 0 ? false : IsLastFromGroup[0].InnerXml == string.Empty ? false : bool.Parse(IsLastFromGroup[0].InnerXml);
        }

        private Guid GetGroupId()
        {
            Guid overwriteGroupId;

            if (checkBoxOverwriteGroupId.Checked && Guid.TryParse(txtBoxGroupId.Text, out overwriteGroupId))
            {
                return overwriteGroupId;
            }
            else
            {
                XmlNodeList INGroupIdList = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.INGroupId);
                if (INGroupIdList == null || INGroupIdList.Count == 0)
                {
                    INGroupIdList = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.INGroupIdPayloadFormat);
                }

                return INGroupIdList == null ? Guid.Empty : INGroupIdList.Count == 0 ? Guid.Empty : INGroupIdList[0].InnerXml == string.Empty ? Guid.Empty : Guid.Parse(INGroupIdList[0].InnerXml);
            }
        }

        private string GetEventFilterMetadata()
        {
            XmlNodeList eventFilterMetadataNodeList = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.EventFilterMetadata);
            if (eventFilterMetadataNodeList == null || eventFilterMetadataNodeList.Count == 0)
            {
                eventFilterMetadataNodeList = this.payloadDocument.GetElementsByTagName(EventRouterBypassConstants.XmlAttributes.EventFilterMetadataPayloadFormat);
            }
            return eventFilterMetadataNodeList == null ? null : eventFilterMetadataNodeList.Count == 0 ? null : eventFilterMetadataNodeList[0].InnerXml == string.Empty ? null : eventFilterMetadataNodeList[0].OuterXml.ToString();
        }

        private string GetEventNameSpace()
        {
            var eventNameSpaceAttribute = ConfigurationManager.AppSettings[EventRouterBypassConstants.XmlAttributes.EventNameSpaceAttribute];

            if (this.payloadDocument.FirstChild.Attributes[eventNameSpaceAttribute] != null)
            {
                string nm = this.payloadDocument.FirstChild.Attributes[eventNameSpaceAttribute].Value;
                return  nm.Substring(nm.LastIndexOf('/') + 1);
            }
            else
            {
                var nm = this.payloadDocument.FirstChild.Attributes[EventRouterBypassConstants.XmlAttributes.XmlnsAttribute].Value;
                return nm.Substring(nm.LastIndexOf('/') + 1);
            }
        }

        private string GetEventTypeName()
        {
            var eventNameAttribute = ConfigurationManager.AppSettings[EventRouterBypassConstants.XmlAttributes.EventNameAttribute];

            if (this.payloadDocument.FirstChild.Attributes[eventNameAttribute] != null)
            {
                var nm = this.payloadDocument.FirstChild.Attributes[eventNameAttribute].Value;
                return nm.Substring(nm.LastIndexOf(':') + 1);
            }
            else
            {
                return  this.payloadDocument.FirstChild.Name;
            }  
        }

        private string AddBulkReqTable(string moduleName, List<ExternalReq> values)
        {
            string returnStatus = EventRouterBypassConstants.Messages.SuccessStatus;

            using (IDbSession session = new DbSession(this.connectionProviderName, this.connectionString, this.connectionSchemaName))
            {
                try
                {
                    using (var dataBaseContext = new IntegrationDbContext(session))
                    {
                        returnStatus = new BulkTransactionHelper(dataBaseContext).ExecuteBulkReqProcedure(moduleName, values);
                    }
                }
                catch (Exception ex)
                {
                    returnStatus = string.Format(EventRouterBypassConstants.Messages.FailStatus, ex.Message);
                }
            }

            return returnStatus;
        }

        private string AddBulkResTable(string moduleName, List<ExternalRes> values)
        {
            string returnStatus = EventRouterBypassConstants.Messages.SuccessStatus;

            using (IDbSession session = new DbSession(this.connectionProviderName, this.connectionString, this.connectionSchemaName))
            {
                try
                {
                    using (var dataBaseContext = new IntegrationDbContext(session))
                    {
                        returnStatus = new BulkTransactionHelper(dataBaseContext).ExecuteBulkResProcedure(moduleName, values);
                    }
                }
                catch (Exception ex)
                {
                    returnStatus = string.Format(EventRouterBypassConstants.Messages.FailStatus, ex.Message);
                }
            }

            return returnStatus;
        }

        private string AddBulkEventSubsErrTable(string moduleName, List<EventSubscriptionError> values)
        {
            string returnStatus = EventRouterBypassConstants.Messages.SuccessStatus;

            using (IDbSession session = new DbSession(this.connectionProviderName, this.connectionString, this.connectionSchemaName))
            {
                try
                {
                    using (var dataBaseContext = new IntegrationDbContext(session))
                    {
                        returnStatus = new BulkTransactionHelper(dataBaseContext).ExecuteBulkEventSubscriptionErrProcedure(moduleName, values);
                    }
                }
                catch (Exception ex)
                {
                    returnStatus = string.Format(EventRouterBypassConstants.Messages.FailStatus, ex.Message);
                }
            }

            return returnStatus;
        }

        private string DeleteBulkInReqTable(string tableType = EventRouterBypassConstants.DatabaseRelated.EventSubscriptionsErrorTable)
        {
            string returnStatus = EventRouterBypassConstants.Messages.SuccessStatus;

            using (IDbSession session = new DbSession(this.connectionProviderName, this.connectionString, this.connectionSchemaName))
            {
                try
                {
                    using (var dataBaseContext = new IntegrationDbContext(session))
                    {
                        returnStatus = new BulkTransactionHelper(dataBaseContext).ExecuteBulkDeleteProcedure(this.groupId, this.moduleName, tableType);
                    }
                }
                catch (Exception ex)
                {
                    returnStatus = string.Format(EventRouterBypassConstants.Messages.FailStatus, ex.Message);
                }
            }

            return returnStatus;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnbrowseFolder_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(this.folderBrowserDialog1.SelectedPath))
                this.selectedFolderForMultipleFiles = this.folderBrowserDialog1.SelectedPath;
            this.buttonNormalTest.Enabled = true;
        }

        private void checkBoxOverwriteGroupId_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxGroupId.Enabled = checkBoxOverwriteGroupId.Checked;
        }

        private void checkboxMasive_CheckedChanged(object sender, EventArgs e)
        {
            numericMasive.Enabled = checkboxMasive.Checked;
        }

        private void tenantIds_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonNormalTest.Enabled = true;
        }

        private void txtBoxGroupId_TextChanged(object sender, EventArgs e)
        {
            btnCleanTables.Enabled = true;
        }

        private void btnCleanTables_Click(object sender, EventArgs e)
        {
            Cursor cursor = this.Cursor;
            Cursor.Current = Cursors.WaitCursor;

            this.BuildConnectionParameters();
            if(this.groupId == Guid.Empty)
            {
                this.groupId = this.GetGroupId();
            }
            this.DeleteBulkInReqTable();
            this.DeleteBulkInReqTable(EventRouterBypassConstants.DatabaseRelated.ResTable);
            this.DeleteBulkInReqTable(EventRouterBypassConstants.DatabaseRelated.ReqTable);

            Cursor.Current = cursor;

            MessageBox.Show(EventRouterBypassConstants.Messages.DeleteDataMessage, EventRouterBypassConstants.Messages.DeleteData);
        }

        private void chkBoxAggregate_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxAggregate.Enabled = chkBoxAggregate.Checked;
        }
        #endregion
    }
}
