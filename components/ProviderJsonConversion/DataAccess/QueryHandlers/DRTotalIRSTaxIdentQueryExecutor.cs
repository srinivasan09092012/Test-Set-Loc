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
    public class DRTotalIRSTaxIdentQueryExecutor : QueryExecutorBase<DRIRSTaxIdentificationModel, DRIRSTaxIdentificationQuery>
    {
        public DRTotalIRSTaxIdentQueryExecutor()
        {
        }

        protected override void AssignResults(IQueryable<DRIRSTaxIdentificationModel> baseQuery, DRIRSTaxIdentificationQuery query)
        {
            query.Results = baseQuery.ToList();
        }

        protected override IQueryable<DRIRSTaxIdentificationModel> GetBaseQuery(IDbContextBase context, DRIRSTaxIdentificationQuery query)
        {
            query.Validate();

            var irsTaxIdentifications = from i in context.Set<IRSTaxIdentification>()
                                    select new DRIRSTaxIdentificationModel()
                                    {
                                        _id = i.IRSTaxIdentificationId
                                    };

            return irsTaxIdentifications.AsQueryable();
        }
    }
}