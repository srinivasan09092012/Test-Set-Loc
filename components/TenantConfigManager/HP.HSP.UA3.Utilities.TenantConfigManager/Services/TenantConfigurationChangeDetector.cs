
using HP.HSP.UA3.Core.UX.Data.Configuration;
using HP.HSP.UA3.Core.UX.Data.Navigation;
using HP.HSP.UA3.Core.UX.Data.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using uxcommon = HP.HSP.UA3.Core.UX.Data.Common;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Services
{
    public class TenantConfigurationChangeDetector
    {
        private int _order = 0;
        private string _tenant;
        private string _module;

        public List<ConfigurationChange> GetChanges(List<TenantConfigurationModel> original, List<TenantConfigurationModel> changed)
        {
            List<ConfigurationChange> results = new List<ConfigurationChange>();

            _order = 0;
            _tenant = null;
            _module = null;

            DetectChanges(original, changed, "Tenant configuration setting", results, _order++, p => p.TenantId,
                new Func<TenantConfigurationModel, object>[]
                {
                    p => p.TenantId
                },
                (o, c) =>
                {
                    _module = null;
                    DetectChanges(o.Modules, c.Modules, results, _order++);
                });

            return results.OrderBy(p => p.Order).ToList();
        }

        #region resuable methods among all change detection

        private void DetectChanges<T>(List<T> original, List<T> changed, string itemType, List<ConfigurationChange> results, int order, Func<T, object> idSelectorFunc, Func<T, object>[] propertyChecks, Action<T, T> additionalPerItemCheck = null) where T : class
        {
            List<ConfigurationChange> tempChanges = new List<ConfigurationChange>();

            var originalById = original.ToDictionary(o => idSelectorFunc(o).ToString());
            var changedById = changed.ToDictionary(c => idSelectorFunc(c).ToString());

            foreach (KeyValuePair<string, T> originalPair in originalById)
            {
                T changedItem = null;

                changedById.TryGetValue(originalPair.Key, out changedItem);

                if (changedItem == null)
                {
                    var change = ConfigurationChange.Deleted(originalPair.Value);
                    change.ItemId = originalPair.Key;
                    change.ItemType = itemType;
                    change.Module = _module;
                    change.Order = order++;
                    change.Tenant = _tenant;
                    tempChanges.Add(change);
                    continue;
                }

                if (AnyAreDifferent<T>(originalPair.Value, changedItem, propertyChecks))
                {
                    var change = ConfigurationChange.Updated(originalPair.Value, changedItem);
                    change.ItemId = originalPair.Key;
                    change.ItemType = itemType;
                    change.Module = _module;
                    change.Order = order++;
                    change.Tenant = _tenant;
                    tempChanges.Add(change);
                }

                if (additionalPerItemCheck != null)
                    additionalPerItemCheck(originalPair.Value, changedItem);
            }

            var originalIds = original.Select(p => idSelectorFunc(p)).Distinct();
            var addedItems = changed.Where(p => originalIds.Contains(idSelectorFunc(p)) == false).ToList();

            foreach (var added in addedItems)
            {
                var addedId = idSelectorFunc(added);

                var change = ConfigurationChange.Created(added);
                change.ItemId = addedId == null ? string.Empty : addedId.ToString();
                change.ItemType = itemType;
                change.Module = _module;
                change.Order = order++;
                change.Tenant = _tenant;
                tempChanges.Add(change);
            }

            if (tempChanges.Count > 0)
            {
                tempChanges.ForEach(p =>
                {
                    p.Tenant = _tenant;
                    p.Module = _module;
                });

                results.AddRange(tempChanges);
            }
        }

        private static bool AnyAreDifferent<T>(T original, T changed, params Func<T, object>[] checks) where T : class
        {
            bool result = false;

            foreach (var check in checks)
            {
                var originalVal = check(original);
                var changedVal = check(changed);

                var originalString = originalVal == null ? string.Empty : originalVal.ToString();
                var changedString = changedVal == null ? string.Empty : changedVal.ToString();

                Debug.WriteLine("Comparing '{0}' and '{1}': Equals == {2}", originalString, changedString, originalString.Equals(changedString));

                if (!originalString.Equals(changedString))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        #endregion resuable methods among all change detection

        private void DetectChanges(List<ModuleConfigurationModel> original, List<ModuleConfigurationModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Module configuration setting", results, _order, p => p.ModuleId,
                new Func<ModuleConfigurationModel, object>[]
                {
                    p => p.Name,
                    p => p.ModuleId,
                    p => p.IocConfiguration.OuterXml
                },
                (o, c) =>
                {
                    _module = c.Name;
                    DetectChanges(o.DisplayConfiguration.DisplaySizes, c.DisplayConfiguration.DisplaySizes, results, _order++);
                    DetectChanges(o.LocalizationConfiguration.Locales, c.LocalizationConfiguration.Locales, results, _order++);
                    DetectChanges(o.ModelDefinitionConfiguration, c.ModelDefinitionConfiguration, results, _order++);
                });
        }

        private void DetectChanges(List<uxcommon.ContactModel> original, List<uxcommon.ContactModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Tenant Contact", results, _order, p => p.ContactId,
                new Func<uxcommon.ContactModel, object>[]
                {
                    p => p.FaxNumber,
                    p => p.FirstName,
                    p => p.LastName,
                    p => p.MiddelName,
                    p => p.PhoneNumber,
                    p => p.Position
                },
                (o, c) =>
                {
                    DetectChanges(o.Addresses, c.Addresses, results, _order++);
                    DetectChanges(o.EmailAddresses, c.EmailAddresses, results, _order++);
                });
        }

        private void DetectChanges(List<uxcommon.EmailAddressModel> original, List<uxcommon.EmailAddressModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Tenant Contact Email Addreess", results, _order, p => p.EmailAddressId,
                new Func<uxcommon.EmailAddressModel, object>[]
                {
                    p => p.EmailAddressId,
                    p => p.EmailAddressVerified,
                    p => p.PersonName == null ? string.Empty : p.PersonName.DisplayName,
                    p => p.PersonName == null ? string.Empty : p.PersonName.First,
                    p => p.PersonName == null ? string.Empty : p.PersonName.Last,
                    p => p.PersonName == null ? string.Empty : p.PersonName.Middle,
                    p => p.PersonName == null ? string.Empty : p.PersonName.MiddleInitial,
                    p => p.PersonName == null ? string.Empty : p.PersonName.NameId,
                    p => p.PersonName == null ? string.Empty : p.PersonName.Suffix,
                    p => p.Type == null ? string.Empty : p.Type.Name,
                    p => p.Type == null ? string.Empty : p.Type.ID,
                });
        }

        private void DetectChanges(List<uxcommon.AddressModel> original, List<uxcommon.AddressModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Tenant Contact Address", results, _order, p => p.AddressId,
                new Func<uxcommon.AddressModel, object>[]
                {
                    p => p.City,
                    p => p.Country,
                    p => p.Line1,
                    p => p.Line2,
                    p => p.State,
                    p => p.Type == null ? string.Empty : p.Type.Name,
                    p => p.Type == null ? string.Empty : p.Type.ID,
                });
        }

        private void DetectChanges(List<ModelDefinitionModel> original, List<ModelDefinitionModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Model definition", results, _order, p => p.Id,
                new Func<ModelDefinitionModel, object>[]
                {
                    p => p.DisplaySize,
                    p => p.Scope,
                    p => p.Type
                },
                (o, c) =>
                {
                    DetectChanges(o.ModelProperties, c.ModelProperties, results, _order++);
                });
        }

        private void DetectChanges(List<ModelPropertyModel> original, List<ModelPropertyModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Model Definition property", results, _order, p => p.Id,
                new Func<ModelPropertyModel, object>[]
                {
                    p => p.AccessKey,
                    p => p.CompareTo,
                    p => p.CompareToMsgContentId,
                    p => p.CompareToMsgContentIdSpecified,
                    p => p.CompareToSpecified,
                    p => p.DataRestrictionType,
                    p => p.DataType,
                    p => p.DefaultText,
                    p => p.DisplayType,
                    p => p.EditSecurityRightContentId,
                    p => p.EditSecurityRightContentIdSpecified,
                    p => p.Height,
                    p => p.HintType,
                    p => p.IgnoreDirtyData,
                    p => p.IsRequired,
                    p => p.LabelContentId,
                    p => p.MaxLength,
                    p => p.Name,
                    p => p.ViewSecurityRightContentId,
                    p => p.ViewSecurityRightContentIdSpecified
                });
        }

        private void DetectChanges(List<LocaleConfigurationModel> original, List<LocaleConfigurationModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Locale", results, _order, p => p.Id,
                new Func<LocaleConfigurationModel, object>[]
                {
                    p => p.LocaleId,
                    p => p.Name,
                    p => p
                },
                (o, c) =>
                {
                    DetectChanges(o.LocaleDataLists, c.LocaleDataLists, results, _order++);
                    DetectChanges(o.LocaleEmailTemplates, c.LocaleEmailTemplates, results, _order++);
                    DetectChanges(o.LocaleLabels, c.LocaleLabels, results, _order++);
                    DetectChanges(o.LocaleMessages, c.LocaleMessages, results, _order++);
                });
        }

        private void DetectChanges(List<LocaleConfigurationLabelModel> original, List<LocaleConfigurationLabelModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Localized Label", results, _order, p => p.Id,
                new Func<LocaleConfigurationLabelModel, object>[]
                {
                    p => p.ContentId,
                    p => p.LocaleId,
                    p => p.Text,
                    p => p.Tooltip
                });
        }

        private void DetectChanges(List<LocaleConfigurationMessageModel> original, List<LocaleConfigurationMessageModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Localized Message", results, _order, p => p.Id,
                new Func<LocaleConfigurationMessageModel, object>[]
                {
                    p => p.ContentId,
                    p => p.LocaleId,
                    p => p.MessageType,
                    p => p.Text
                });
        }

        private void DetectChanges(List<LocaleConfigurationEmailTemplateModel> original, List<LocaleConfigurationEmailTemplateModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Localized Email Template", results, _order, p => p.Id,
                new Func<LocaleConfigurationEmailTemplateModel, object>[]
                {
                    p => p.Body.OuterXml,
                    p => p.ContentId,
                    p => p.EndDate,
                    p => p.LocaleId,
                    p => p.Name,
                    p => p.Priority,
                    p => p.StartDate,
                    p => p.Subject
                },
                (o, c) =>
                {
                    DetectChanges(o.Addresses, c.Addresses, results, _order++);
                });
        }

        private void DetectChanges(List<LocaleConfigurationEmailTemplateAddressModel> original, List<LocaleConfigurationEmailTemplateAddressModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Localized Email Address Template Address", results, _order, p => p.Id,
                new Func<LocaleConfigurationEmailTemplateAddressModel, object>[]
                {
                    p => p.DisplayName,
                    p => p.EmailAddress,
                    p => p.Type
                });
        }

        private void DetectChanges(List<LocaleConfigurationDataListModel> original, List<LocaleConfigurationDataListModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Localized Data List", results, _order, p => p.Id,
                new Func<LocaleConfigurationDataListModel, object>[]
                {
                    p => p.ContentId,
                    p => p.LocaleId,
                    p => p.Name
                },
                (o, c) =>
                {
                    DetectChanges(o.LocaleDataListItems, c.LocaleDataListItems, results, _order++);
                });
        }

        private void DetectChanges(List<LocaleConfigurationDataListItemModel> original, List<LocaleConfigurationDataListItemModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Localized Data List Item", results, _order, p => p.Id,
                new Func<LocaleConfigurationDataListItemModel, object>[]
                {
                    p => p.ContentId,
                    p => p.LocaleId,
                    p => p.Order,
                    p => p.Text,
                    p => p.Value
                });
        }

        private void DetectChanges(List<DisplaySizeConfigurationModel> original, List<DisplaySizeConfigurationModel> changed, List<ConfigurationChange> results, int order)
        {
            DetectChanges(original, changed, "Display Size configuration", results, _order, p => p.Id,
                new Func<DisplaySizeConfigurationModel, object>[]
                {
                    p => p.IsDefault,
                    p => p.MaxHeight,
                    p => p.MaxWidth,
                    p => p.Name
                });
        }
    }
}