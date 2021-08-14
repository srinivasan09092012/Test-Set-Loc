//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using Contracts.Domain;
using Contracts.Queries;
using DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.QueryHandlers
{
    public class DRProviderQueryExecutor : QueryExecutorBase<DRProviderModel, DRProviderQuery>
    {
        private IDbContextBase context = null;
        private System.Diagnostics.Stopwatch watch;

        public DRProviderQueryExecutor(IDbContextBase context)
        {
            this.context = context;
            this.watch = new System.Diagnostics.Stopwatch();
        }

        protected override IQueryable<DRProviderModel> ApplyPaging(IQueryable<DRProviderModel> baseQuery, DRProviderQuery query)
        {
            return base.ApplyPaging(baseQuery, query);
        }

        protected override IQueryable<DRProviderModel> ApplySorting(IQueryable<DRProviderModel> baseQuery, DRProviderQuery query)
        {
            var dynamicSort = this.DynamicSort()
                .SortUsing(p => p._id).ForValue("_id")
                .WithDefaults(def =>
                {
                    def.ByDescending(p => p._id);
                }).Build();

            return baseQuery.OrderBy(dynamicSort, query.SortCriteria);
        }

        protected override void AssignResults(IQueryable<DRProviderModel> baseQuery, DRProviderQuery query)
        {
            query.Results = baseQuery.ToList();

            foreach (DRProviderModel p in query.Results)
            {
                List<DRProviderServiceLocationModel> serviceLocations = this.GetProviderServiceLocations(this.context, p._id);

                foreach (DRProviderServiceLocationModel psl in serviceLocations)
                {
                    List<DRAddressModel> addresses = this.GetAddresses(this.context, psl.PracticeLocationId, psl.ProviderServiceLocationId);
                    List<DRSpecialtyModel> specialties = this.GetSpecialties(this.context, psl.ProviderServiceLocationId);
                    List<DRTaxonomyModel> taxonomies = this.GetTaxonomies(this.context, psl.ProviderServiceLocationId);
                    List<DRIRSTaxIdAssociationsModel> irsTaxAssoc = this.GetIRSTaxIDAssociations(this.context, psl.ProviderServiceLocationId);

                    serviceLocations.Where(x => x.ProviderServiceLocationId == psl.ProviderServiceLocationId).FirstOrDefault().ProviderAddresses.AddRange(addresses);
                    serviceLocations.Where(x => x.ProviderServiceLocationId == psl.ProviderServiceLocationId).FirstOrDefault().ProviderSpecialties.AddRange(specialties);
                    serviceLocations.Where(x => x.ProviderServiceLocationId == psl.ProviderServiceLocationId).FirstOrDefault().ProviderTaxonomies.AddRange(taxonomies);
                    serviceLocations.Where(x => x.ProviderServiceLocationId == psl.ProviderServiceLocationId).FirstOrDefault().ProviderIRSTaxIdAssociations.AddRange(irsTaxAssoc);
                }

                query.Results.Where(x => x._id == p._id).FirstOrDefault().ProviderServiceLocations.AddRange(serviceLocations);
            }

            this.watch.Stop();
            query.TotalMiliseconds = this.watch.ElapsedMilliseconds;
        }

        protected override IQueryable<DRProviderModel> GetBaseQuery(IDbContextBase context, DRProviderQuery query)
        {
            query.Validate();
            this.watch.Start();

            var providers = (from p in context.Set<PROVIDER>()
                             select new
                             {
                                 BaseId = p.BASE_ID,
                                 BusinessName = p.BUSINESS_NAME,
                                 _id = p.PROVIDER_ID,
                                 IsActive = p.IS_ACTIVE,
                                 ProviderCategoryCode = p.PROVIDER_CATEGORY_CD
                             }).AsEnumerable()
                            .Select(x => new DRProviderModel()
                            {
                                BaseId = x.BaseId.ToString(),
                                BusinessName = x.BusinessName,
                                _id = x._id,
                                IsActive = x.IsActive,
                                ProviderCategoryCode = x.ProviderCategoryCode
                            });

            return providers.AsQueryable();
        }

        private List<DRProviderServiceLocationModel> GetProviderServiceLocations(IDbContextBase context, Guid providerId)
        {
                List<DRProviderServiceLocationModel> serviceLocations = (from sl in context.Set<PROVIDER_SERVICE_LOCATION>()
                                                                         where sl.PROVIDER_ID == providerId
                                                                         select new DRProviderServiceLocationModel()
                                                                         {
                                                                             OriginalEffectiveDate = sl.EFFECTIVE_DT,
                                                                             OriginalEndDate = sl.END_DT,
                                                                             ProviderServiceLocationId = sl.PROVIDER_SERVICE_LOCATION_ID,
                                                                             PracticeLocationId = sl.PRACTICE_LOCATION_ID,
                                                                             LongNPI = sl.NPI,
                                                                             ProviderBillingId = sl.PROVIDER_BILLING_ID,
                                                                             ProviderTypeCode = sl.PROVIDER_TYPE_CD,
                                                                             IsActive = sl.IS_ACTIVE
                                                                         }).ToList();

                return serviceLocations;
        }

        private List<DRAddressModel> GetAddresses(IDbContextBase context, Guid practiceLocationId, Guid serviceLocationId)
        {
            List<DRAddressModel> addresses = (from plaa in context.Set<PracticeLocationAddressAssociation>()
                                              join a in context.Set<ADDRESS>()
                                              on plaa.AddressId equals a.ADDRESS_ID
                                              where plaa.PracticeLocationId == practiceLocationId
                                              select new DRAddressModel()
                                              {
                                                  City = a.CITY,
                                                  IsActive = a.IS_ACTIVE,
                                                  PostalCode = a.POSTAL_CD,
                                                  ProviderAddressId = a.ADDRESS_ID,
                                                  State = a.STATE_CD,
                                                  Line1 = a.ADR_LINE_1,
                                                  Line2 = a.ADR_LINE_2,
                                                  OriginalEffectiveDate = plaa.EffectiveDate,
                                                  OriginalEndDate = plaa.EndDate,
                                                  ServiceLocationId = serviceLocationId
                                              }).ToList();

            return addresses;
        }        

        private List<DRSpecialtyModel> GetSpecialties(IDbContextBase context, Guid serviceLocationId)
        {
            List<DRSpecialtyModel> specialties = (from speci in context.Set<PROVIDER_SPECIALTY>()
                                                  where speci.PROVIDER_SERVICE_LOCATION_ID == serviceLocationId
                                                  select new DRSpecialtyModel()
                                                  {
                                                      OriginalEffectiveDate = speci.EFFECTIVE_DT,
                                                      OriginalEndDate = speci.END_DT,
                                                      IsActive = speci.IS_ACTIVE,
                                                      IsPrimary = speci.IS_PRIMARY,
                                                      ProviderSpecialtyId = speci.PROVIDER_SPECIALTY_ID,
                                                      SpecialtyCode = speci.SPECIALTY_CD,
                                                      TaxonomyCode = speci.TAXONOMY_CD
                                                  }).ToList();

            return specialties;
        }

        private List<DRTaxonomyModel> GetTaxonomies(IDbContextBase context, Guid serviceLocationId)
        {
            List<DRTaxonomyModel> taxonomies = (from taxo in context.Set<PROVIDER_TAXONOMY>()
                                                where taxo.PROVIDER_SERVICE_LOCATION_ID == serviceLocationId
                                                select new DRTaxonomyModel()
                                                {
                                                    OriginalEffectiveDate = taxo.EFFECTIVE_DT,
                                                    OriginalEndDate = taxo.END_DT,
                                                    IsActive = taxo.IS_ACTIVE,
                                                    ProviderTaxonomyId = taxo.PROVIDER_TAXONOMY_ID,
                                                    TaxonomyCode = taxo.TAXONOMY_CD
                                                }).ToList();

            return taxonomies;
        }

        private List<DRIRSTaxIdAssociationsModel> GetIRSTaxIDAssociations(IDbContextBase context, Guid serviceLocationId)
        {
            List<DRIRSTaxIdAssociationsModel> irsTaxAssoc = (from irsTax in context.Set<IRSTaxIdAssociation>()
                                                             where irsTax.ServiceLocationId == serviceLocationId
                                                             select new DRIRSTaxIdAssociationsModel()
                                                             {
                                                                 OriginalEffectiveDate = irsTax.EffectiveDate,
                                                                 OriginalEndDate = irsTax.EndDate,
                                                                 IRSTaxIdentificationId = irsTax.IRSTaxIdentificationId,
                                                                 ProviderIRSTaxIdAssociationId = irsTax.IRSTaxIdAssociationId,
                                                                 IsActive = irsTax.IsActive
                                                             }).ToList();

            return irsTaxAssoc;
        }
    }
}