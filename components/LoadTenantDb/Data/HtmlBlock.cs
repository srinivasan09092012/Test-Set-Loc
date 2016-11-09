
//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HP.HSP.UA3.Utilities.LoadTenantDb.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebRefHtmlBlocksMaint = HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public class HtmlBlock
    {
        private WebRefHtmlBlocksMaint.HtmlBlockServiceClient clientLicense = new WebRefHtmlBlocksMaint.HtmlBlockServiceClient();

        public HtmlBlock()
        {
        }

        public MainForm MainForm { get; set; }

        public string ContentId { get; set; }

        public string Id { get; set; }

        public string Description { get; set; }

        public List<HtmlBlockLanguage> HtmlBlockLanguages { get; set; }

        public bool IsActive { get; set; }

        public string TenantModuleId { get; set; }

        private IDbSession session;

        public ConnectionStringSettings ConnectionStringSettings { get; set; }

        public string GetHtmlBlockId(HtmlBlock htmlBlock)
        {
            string id;
            this.ConnectionStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (this.session = new DbSession(this.ConnectionStringSettings.ProviderName, this.ConnectionStringSettings.ConnectionString))
            {
                HtmlBlockDbContext htmlBlockDbContext = new HtmlBlockDbContext(this.session);

                var htmBlockId = (from hb in htmlBlockDbContext.Set<HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities.HtmlBlock>() where hb.ContentId == htmlBlock.ContentId select hb.HtmlBlockId).FirstOrDefault();

                if (htmBlockId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                {
                    id = null;
                }
                else
                {
                    id = htmBlockId.ToString();
                }
            }

            return id;
        }


        public List<Object> GetHtmlBlockLanguages(string htmlBlockId)
        {
            List<Object> localList = null;
            this.ConnectionStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (this.session = new DbSession(this.ConnectionStringSettings.ProviderName, this.ConnectionStringSettings.ConnectionString))
            {

                HtmlBlockDbContext htmlBlockDbContext = new HtmlBlockDbContext(this.session);

                localList = (from hb in htmlBlockDbContext.Set<HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities.HtmlBlockLanguages>()
                                 where hb.HtmlBlockId == new Guid(htmlBlockId)
                                 select new { hb.LocaleId }).ToList<Object>();
            }

            return localList;
        }

        public HtmlBlock AddHtmlBlock(HtmlBlock htmlBlock)
        {
            HtmlBlockService.HtmlBlockAdded response = null;

            HtmlBlockService.AddHtmlBlockCommand command = new HtmlBlockService.AddHtmlBlockCommand()
            {
                HtmlBlock = new HtmlBlockService.HtmlBlock()
                {
                    ContentId = htmlBlock.ContentId,
                    HtmlBlockId  = htmlBlock.Id,
                    Description = htmlBlock.Description,
                    HtmlBlockLanguages = this.SetHtmlBlockLanguages(htmlBlock),
                    IsActive = true,
                    TenantModuleId = htmlBlock.TenantModuleId
                },
                Requestor = new Core.BAS.CQRS.UserMeta.RequestorModel()
                {
                    IdentifierId = "User1",
                    IdentifierIdType = Core.BAS.CQRS.UserMeta.CoreEnumerations.Messaging.IdentifierIdType.User,
                    TenantId = this.MainForm.TenantId,
                    RequestDate = DateTime.UtcNow
                }
            };

            try
            {
                response = this.clientLicense.AddHtmlBlock(command);
            }
            catch (Exception ex)
            {
                return null;
            }

            if (response.HtmlBlockId != null)
            {
                htmlBlock.Id = response.HtmlBlockId;
                return htmlBlock;
            }
            else
            {
                return null;
            }
        }

        public HtmlBlock UpdateHtmlBlock(HtmlBlock htmlBlock)
        {
            HtmlBlockService.HtmlBlockUpdated response = null;

            HtmlBlockService.UpdateHtmlBlockCommand command = new HtmlBlockService.UpdateHtmlBlockCommand()
            {
                HtmlBlock = new HtmlBlockService.HtmlBlock()
                {
                    ContentId = htmlBlock.ContentId,
                    HtmlBlockId = htmlBlock.Id,
                    Description = htmlBlock.Description,
                    HtmlBlockLanguages = this.SetHtmlBlockLanguages(htmlBlock),
                    IsActive = true,
                    TenantModuleId = htmlBlock.TenantModuleId
                },
                Requestor = new Core.BAS.CQRS.UserMeta.RequestorModel()
                {
                    IdentifierId = "User1",
                    IdentifierIdType = Core.BAS.CQRS.UserMeta.CoreEnumerations.Messaging.IdentifierIdType.User,
                    TenantId = this.MainForm.TenantId,
                    RequestDate = DateTime.UtcNow
                }
            };

            try
            {
                response = this.clientLicense.UpdateHtmlBlock(command);
            }
            catch (Exception ex)
            {
                return null;
            }

            if (response.HtmlBlockId != null)
            {
                htmlBlock.Id = response.HtmlBlockId;
                return htmlBlock;
            }
            else
            {
                return null;
            }
        }

        private HtmlBlockService.HtmlBlockLanguage[] SetHtmlBlockLanguages(HtmlBlock htmlBlock)
        {
            List<HtmlBlockLanguage> langList = htmlBlock.HtmlBlockLanguages;
            if (langList != null)
            {
                HtmlBlockService.HtmlBlockLanguage[] result = new HtmlBlockService.HtmlBlockLanguage[] { };
                List<HtmlBlockService.HtmlBlockLanguage> result2 = new List<HtmlBlockService.HtmlBlockLanguage>();
                HtmlBlockService.HtmlBlockLanguage lang = null;

                foreach (HtmlBlockLanguage itemLang in langList)
                {
                    lang = new HtmlBlockService.HtmlBlockLanguage()
                    {
                        HtmlBlockId = htmlBlock.Id,
                        HtmlBlockLanguageUpdated = !string.IsNullOrEmpty(itemLang.HtmlBlockId),
                        Html = itemLang.Html,
                        IsActive = itemLang.IsActive,
                        Locale = itemLang.Locale
                    };

                    result2.Add(lang);
                }

                result = result2.ToArray();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}