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
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.QueryHandlers
{
    public class DRTotalNumberProviderQueryExecutor : QueryExecutorBase<DRProviderModel, DRProviderQuery>
    {
        public DRTotalNumberProviderQueryExecutor()
        {
        }

        protected override void AssignResults(IQueryable<DRProviderModel> baseQuery, DRProviderQuery query)
        {
            query.Results = baseQuery.ToList();
        }

        protected override IQueryable<DRProviderModel> GetBaseQuery(IDbContextBase context, DRProviderQuery query)
        {
            query.Validate();

            var providers = from p in context.Set<PROVIDER>()
                                    select new DRProviderModel()
                                    {
                                        _id = p.PROVIDER_ID
                                    };

            return providers.AsQueryable();
        }
    }
}