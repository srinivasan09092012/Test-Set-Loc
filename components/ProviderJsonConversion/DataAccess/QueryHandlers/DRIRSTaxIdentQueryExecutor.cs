//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using Contracts.Domain;
using Contracts.Queries;
using DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System.Linq;

namespace DataAccess.QueryHandlers
{
    public class DRIRSTaxIdentQueryExecutor : QueryExecutorBase<DRIRSTaxIdentificationModel, DRIRSTaxIdentificationQuery>
    {
        public DRIRSTaxIdentQueryExecutor()
        {
        }

        protected override IQueryable<DRIRSTaxIdentificationModel> ApplyPaging(IQueryable<DRIRSTaxIdentificationModel> baseQuery, DRIRSTaxIdentificationQuery query)
        {
            return base.ApplyPaging(baseQuery, query);
        }

        protected override IQueryable<DRIRSTaxIdentificationModel> ApplySorting(IQueryable<DRIRSTaxIdentificationModel> baseQuery, DRIRSTaxIdentificationQuery query)
        {
            var dynamicSort = this.DynamicSort()
                .SortUsing(p => p._id).ForValue("_id")
                .WithDefaults(def =>
                {
                    def.ByDescending(p => p._id);
                }).Build();

            return baseQuery.OrderBy(dynamicSort, query.SortCriteria);
        }

        protected override void AssignResults(IQueryable<DRIRSTaxIdentificationModel> baseQuery, DRIRSTaxIdentificationQuery query)
        {
            query.Results = baseQuery.ToList();
        }

        protected override IQueryable<DRIRSTaxIdentificationModel> GetBaseQuery(IDbContextBase context, DRIRSTaxIdentificationQuery query)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            query.Validate();

            var irsTaxIds = from i in context.Set<IRSTaxIdentification>()
                             select new DRIRSTaxIdentificationModel()
                             {
                                 _id = i.IRSTaxIdentificationId,
                                 OriginalEffectiveDate = i.EffectiveDate,
                                 OriginalEndDate = i.EndDate,
                                 IRSTaxId = i.IRSTaxId,
                                 IsValid = i.IsActive,
                                 TaxIdName = i.TaxIdName,
                                 TaxIdTypeCode = i.TaxIdTypeCode,
                             };
            
            watch.Stop();
            query.TotalMiliseconds = watch.ElapsedMilliseconds;

            return irsTaxIds;
        }
    }
}