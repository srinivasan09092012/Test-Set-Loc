﻿using Common.Interfaces;
using Common.ReportModels;
using FileHelpers;
using System.Collections.Generic;
using Common.ExtensionMethods;
using System.IO;
using System;

namespace Controller.Helpers.Scan
{
    public class MissingTagsHelper
    {
        private List<ServiceMissingTagsModel> ServiceWithMissingDescription = new List<ServiceMissingTagsModel>();
        private List<CommandMissingTagsModel> CommandsWithMissingDescription = new List<CommandMissingTagsModel>();
        private List<EventMissingTagsModel> EventsWithMissingDescription = new List<EventMissingTagsModel>();
        private List<ModelMissingTagsModel> ModelsWithMissingDescription = new List<ModelMissingTagsModel>();
        private List<QueryMissingTagsModel> QueryWithMissingDescription = new List<QueryMissingTagsModel>();
        private List<DTOMissingTagsModel> DTOWithMissingDescription = new List<DTOMissingTagsModel>();
        private readonly ILogger loggerEngine;

        public MissingTagsHelper(ILogger loggerEngineInstance)
        {
            loggerEngine = loggerEngineInstance;
        }

        private void AddMissingDTOTag(DTOMissingTagsModel mst)
        {
            if (mst.DTODescription.Contains("[Missing &lt;summary&gt; documentation for"))
            {
                mst.DTODescription = string.Empty;
                DTOWithMissingDescription.Add(mst);
            }
        }

        private void AddMissingQueryTag(QueryMissingTagsModel mst)
        {
            if (mst.QueryDescription.Contains("[Missing &lt;summary&gt; documentation for"))
            {
                mst.QueryDescription = string.Empty;
                QueryWithMissingDescription.Add(mst);
            }
        }

        private void AddMissingServiceTag(ServiceMissingTagsModel mst)
        {
            if (mst.ServiceOperationDescription == string.Empty)
            {
                ServiceWithMissingDescription.Add(mst);
            }
        }

        private void AddMissingCommandTag(CommandMissingTagsModel mst)
        {
            if (mst.CommandDescription.Contains("[Missing &lt;summary&gt; documentation for"))
            {
                mst.CommandDescription = string.Empty;
                CommandsWithMissingDescription.Add(mst);
            }
        }

        private void AddMissingEventTag(EventMissingTagsModel mst)
        {
            if (mst.EventDescription.Contains("[Missing &lt;summary&gt; documentation for"))
            {
                mst.EventDescription = string.Empty;
                EventsWithMissingDescription.Add(mst);
            }
        }

        private void AddMissingModelTag(ModelMissingTagsModel mst)
        {
            if (mst.ModelDescription.Contains("[Missing &lt;summary&gt; documentation for"))
            {
                mst.ModelDescription = string.Empty;
                ModelsWithMissingDescription.Add(mst);
            }
        }

        public void GetDTOSource(Dictionary<string, string> source, string ModuleName, string StorageDrive)
        {
            this.DTOWithMissingDescription.Add(
                new DTOMissingTagsModel()
                {
                    ModuleName = "Module Name",
                    DTOName = "DTO Name",
                    DTODescription = "DTO Description",
                    ActionNeeded = "Action Needed"
                });

            foreach (var item in source)
            {
                this.AddMissingDTOTag(
                new DTOMissingTagsModel()
                {
                    ModuleName = ModuleName,
                    DTOName = item.Key,
                    DTODescription = item.Value,
                    ActionNeeded = "Developer must revisit the code and enter Sandcastle XML comment to the definition of the DTO"
                });
            }

            this.writeDTOFile(ModuleName, StorageDrive);
        }

        public void GetQuerySource(Dictionary<string, string> source, string ModuleName, string StorageDrive)
        {
            this.QueryWithMissingDescription.Add(
                new QueryMissingTagsModel()
                {
                    ModuleName = "Module Name",
                    QueryName = "Query Parameters Name",
                    QueryDescription = "Query Parameters Description",
                    ActionNeeded = "Action Needed"
                });

            foreach (var item in source)
            {
                this.AddMissingQueryTag(
                new QueryMissingTagsModel()
                {
                    ModuleName = ModuleName,
                    QueryName = item.Key,
                    QueryDescription = item.Value,
                    ActionNeeded = "Developer must revisit the code and enter Sandcastle XML comment to the definition of the Query Parameters"
                });
            }

            this.writeQueryFile(ModuleName, StorageDrive);
        }

        public void GetModelsSource(Dictionary<string, string> source, string ModuleName, string StorageDrive)
        {
            this.ModelsWithMissingDescription.Add(
                new ModelMissingTagsModel()
                {
                    ModuleName = "Module Name",
                    ModelName = "Model Name",
                    ModelDescription = "Model Description",
                    ActionNeeded = "Action Needed"
                });

            foreach (var item in source)
            {
                this.AddMissingModelTag(
                new ModelMissingTagsModel()
                {
                    ModuleName = ModuleName,
                    ModelName = item.Key,
                    ModelDescription = item.Value,
                    ActionNeeded = "Developer must revisit the code and enter Sandcastle XML comment to the definition of the Model"
                });
            }

            this.writeModelFile(ModuleName, StorageDrive);
        }

        public void GetEventsSource(Dictionary<string, string> source, string ModuleName, string StorageDrive)
        {
            this.EventsWithMissingDescription.Add(
                new EventMissingTagsModel()
                {
                    ModuleName = "Module Name",
                    EventName = "Event Name",
                    EventDescription = "Event Description",
                    ActionNeeded = "Action Needed"
                });

            foreach (var item in source)
            {
                this.AddMissingEventTag(
                new EventMissingTagsModel()
                {
                    ModuleName = ModuleName,
                    EventName = item.Key,
                    EventDescription = item.Value,
                    ActionNeeded = "Developer must revisit the code and enter Sandcastle XML comment to the definition of the Event"
                });
            }

            this.writeEventFile(ModuleName, StorageDrive);
        }

        public void GetCommandsSource(Dictionary<string, string> source, string ModuleName, string StorageDrive)
        {
            this.CommandsWithMissingDescription.Add(
                new CommandMissingTagsModel()
                {
                    ModuleName = "Module Name",
                    CommandName = "Command Name",
                    CommandDescription = "Command Description",
                    ActionNeeded = "Action Needed"
                });

            foreach (var item in source)
            {
                this.AddMissingCommandTag(
                new CommandMissingTagsModel()
                {
                    ModuleName = ModuleName,
                    CommandName = item.Key,
                    CommandDescription = item.Value,
                    ActionNeeded = "Developer must revisit the code and enter Sandcastle XML comment to the definition of the Command"
                });
            }

            this.writeCommandFile(ModuleName, StorageDrive);
        }

        public void GetServicesSource(List<List<string>> source, string ModuleName, string ServiceName, string StorageDrive)
        {
            this.ServiceWithMissingDescription.Add(
               new ServiceMissingTagsModel()
               {
                   ModuleName = "Module",
                   ServiceName = "Service Name",
                   ServiceOperationName = "Operation Name",
                   ServiceOperationDescription = "Operation Description",
                   ActionNeeded = "Action Needed"
               });

            foreach (var item in source)
            {
                    this.AddMissingServiceTag(
                    new ServiceMissingTagsModel()
                    {
                        ModuleName = ModuleName,
                        ServiceName = ServiceName,
                        ServiceOperationName = item[0],
                        ServiceOperationDescription = item[1],
                        ActionNeeded = "Developer must revisit the code and enter Sandcastle XML comment to the definition of the method"
                    });
                
            }

            writeServiceFile(ModuleName, StorageDrive);
        }

        private void writeDTOFile(string ModuleName, string StorageDrive)
        {
            if (DTOWithMissingDescription.Count > 1)
            {
                string reportPath = (StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\" + DateTime.Now.ToString("MMddyyyy") + @"\ViewDTO\").CreateDirectory() + "DTO.csv";
                loggerEngine.writeEntry(string.Format("Generating excel list for DTO with missing XML Comments to folder {0}", reportPath), LogginSeetings.LevelType.InformationApplication, 9000, 9);
                var engine = new FileHelperAsyncEngine<DTOMissingTagsModel>();
                using (engine.BeginWriteFile(reportPath))
                {
                    foreach (var operation in DTOWithMissingDescription)
                    {
                        engine.WriteNext(operation);
                    }
                }

                engine.Close();
            }
        }

        private void writeServiceFile(string ModuleName, string StorageDrive)
        {
            if (ServiceWithMissingDescription.Count > 1)
            {
                string reportPath = (StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\" + DateTime.Now.ToString("MMddyyyy") + @"\Services\").CreateDirectory() + "Service - " + ServiceWithMissingDescription[1].ServiceName + ".csv";
                loggerEngine.writeEntry(string.Format("Generating excel list for Services with missing XML Comments to folder {0}", reportPath), LogginSeetings.LevelType.InformationApplication,9000,9);
                var engine = new FileHelperAsyncEngine<ServiceMissingTagsModel>();
                using (engine.BeginWriteFile(reportPath))
                {
                    foreach (var operation in ServiceWithMissingDescription)
                    {
                        engine.WriteNext(operation);
                    }
                }

                engine.Close();
            }
        }

        private void writeCommandFile(string ModuleName, string StorageDrive)
        {
            if (CommandsWithMissingDescription.Count > 1)
            {
                string reportPath = (StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\" + DateTime.Now.ToString("MMddyyyy") + @"\Commands\").CreateDirectory() + "Commands - " + ".csv";
                loggerEngine.writeEntry(string.Format("Generating excel list for Commands with missing XML Comments to folder {0}",reportPath), LogginSeetings.LevelType.InformationApplication, 9000, 9);
                var engine = new FileHelperAsyncEngine<CommandMissingTagsModel>();
                using (engine.BeginWriteFile(reportPath))
                {
                    foreach (var operation in CommandsWithMissingDescription)
                    {
                        engine.WriteNext(operation);
                    }
                }

                engine.Close();
            }
        }

        private void writeEventFile(string ModuleName, string StorageDrive)
        {
            if (EventsWithMissingDescription.Count > 1)
            {
                string reportPath = (StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\" + DateTime.Now.ToString("MMddyyyy") + @"\Events\").CreateDirectory() + "Events" + ".csv";
                loggerEngine.writeEntry(string.Format("Generating excel list for Events with missing XML Comments to folder {0}", reportPath), LogginSeetings.LevelType.InformationApplication, 9000, 9);
                var engine = new FileHelperAsyncEngine<EventMissingTagsModel>();
                using (engine.BeginWriteFile(reportPath))
                {
                    foreach (var operation in EventsWithMissingDescription)
                    {
                        engine.WriteNext(operation);
                    }
                }

                engine.Close();
            }
        }

        private void writeModelFile(string ModuleName, string StorageDrive)
        {
            if (ModelsWithMissingDescription.Count > 1)
            {
                string reportPath = (StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\" + DateTime.Now.ToString("MMddyyyy") + @"\Models\").CreateDirectory() + "Models" + ".csv";
                loggerEngine.writeEntry(string.Format("Generating excel list for Models with missing XML doc input to folder {0}", reportPath), LogginSeetings.LevelType.InformationApplication, 9000, 9);
                var engine = new FileHelperAsyncEngine<ModelMissingTagsModel>();
                using (engine.BeginWriteFile(reportPath))
                {
                    foreach (var operation in ModelsWithMissingDescription)
                    {
                        engine.WriteNext(operation);
                    }
                }

                engine.Close();
            }
        }

        private void writeQueryFile(string ModuleName, string StorageDrive)
        {
            if (QueryWithMissingDescription.Count > 1)
            {
                string reportPath = (StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\" + DateTime.Now.ToString("MMddyyyy") + @"\QueryParams\").CreateDirectory() + "QueryParams" + ".csv";
                loggerEngine.writeEntry(string.Format("Generating excel list for QueryParams with missing XML Comments to folder {0}", reportPath), LogginSeetings.LevelType.InformationApplication,9000,9);
                var engine = new FileHelperAsyncEngine<QueryMissingTagsModel>();
                using (engine.BeginWriteFile(reportPath))
                {
                    foreach (var operation in QueryWithMissingDescription)
                    {
                        engine.WriteNext(operation);
                    }
                }

                engine.Close();
            }
        }
    }
}
