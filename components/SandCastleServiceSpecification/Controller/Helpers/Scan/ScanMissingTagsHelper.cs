using Common;
using System.Collections.Generic;
using FileHelpers;
using System;

namespace APISvcSpec.Helpers.Scan
{
    public class ScanMissingTagsHelper
    {
        List<ServiceMissingTagsModel> ServiceWithMissingDescription = new List<ServiceMissingTagsModel>();
        List<CommandMissingTagsModel> CommandsWithMissingDescription = new List<CommandMissingTagsModel>();
        List<EventMissingTagsModel> EventsWithMissingDescription = new List<EventMissingTagsModel>();
        List<ModelMissingTagsModel> ModelsWithMissingDescription = new List<ModelMissingTagsModel>();
        List<QueryMissingTagsModel> QueryWithMissingDescription = new List<QueryMissingTagsModel>();
        List<DTOMissingTagsModel> DTOWithMissingDescription = new List<DTOMissingTagsModel>();

        private void AddMissingDTOTag(DTOMissingTagsModel mst)
        {
            DTOWithMissingDescription.Add(mst);
        }

        private void AddMissingQueryTag(QueryMissingTagsModel mst)
        {
            QueryWithMissingDescription.Add(mst);
        }

        private void AddMissingServiceTag(ServiceMissingTagsModel mst)
        {
            ServiceWithMissingDescription.Add(mst);
        }

        private void AddMissingCommandTag(CommandMissingTagsModel mst)
        {
            CommandsWithMissingDescription.Add(mst);
        }

        private void AddMissingEventTag(EventMissingTagsModel mst)
        {
            EventsWithMissingDescription.Add(mst);
        }

        private void AddMissingModelTag(ModelMissingTagsModel mst)
        {
            ModelsWithMissingDescription.Add(mst);
        }

        public void GetDTOSource(Dictionary<string, string> source, string ModuleName, string StorageDrive)
        {
            this.AddMissingDTOTag(
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
            this.AddMissingQueryTag(
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
            this.AddMissingModelTag(
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
            this.AddMissingEventTag(
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
            this.AddMissingCommandTag(
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
                    CommandName =item.Key,
                    CommandDescription = item.Value,
                    ActionNeeded = "Developer must revisit the code and enter Sandcastle XML comment to the definition of the Command"
                });
            }

            this.writeCommandFile(ModuleName, StorageDrive);
        }

        public void GetServicesSource(List<List<string>> source, string ModuleName, string ServiceName, string StorageDrive)
        {
            this.AddMissingServiceTag(
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
                var engine = new FileHelperAsyncEngine<DTOMissingTagsModel>();
                //TODO: REMOVE HARCODE, INCLUDE IN SETTING XML FILE
                using (engine.BeginWriteFile(StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\MissingXMLInput\ViewDTO\ContractViewDTO.csv"))
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
                var engine = new FileHelperAsyncEngine<ServiceMissingTagsModel>();
                //TODO: REMOVE HARCODE, INCLUDE IN SETTING XML FILE
                using (engine.BeginWriteFile(StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\MissingXMLInput\Services\Service" + ServiceWithMissingDescription[1].ServiceName + ".csv"))
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
                var engine = new FileHelperAsyncEngine<CommandMissingTagsModel>();
                //TODO: REMOVE HARCODE, INCLUDE IN SETTING XML FILE
                using (engine.BeginWriteFile(StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\MissingXMLInput\Commands\ContractCommands.csv"))
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
                var engine = new FileHelperAsyncEngine<EventMissingTagsModel>();
                //TODO: REMOVE HARCODE, INCLUDE IN SETTING XML FILE
                using (engine.BeginWriteFile(StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\MissingXMLInput\Events\ContractEvents.csv"))
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
                var engine = new FileHelperAsyncEngine<ModelMissingTagsModel>();
                //TODO: REMOVE HARCODE, INCLUDE IN SETTING XML FILE
                using (engine.BeginWriteFile(StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\MissingXMLInput\Models\ContractModels.csv"))
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
                var engine = new FileHelperAsyncEngine<QueryMissingTagsModel>();
                //TODO: REMOVE HARCODE, INCLUDE IN SETTING XML FILE
                using (engine.BeginWriteFile(StorageDrive + @"\UA3\SandCastleCustomizationTool\CustomServiceSpecs\" + ModuleName + @"\Reports\MissingXMLInput\QueryParams\ContractQueryParams.csv"))
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
