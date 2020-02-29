﻿using SevenTiny.Bantina;
using SevenTiny.Cloud.MultiTenant.Domain.DataAccess;
using SevenTiny.Cloud.MultiTenant.Domain.Entity;
using SevenTiny.Cloud.MultiTenant.Domain.Enum;
using SevenTiny.Cloud.MultiTenant.Domain.RepositoryContract;
using System;
using System.Collections.Generic;

namespace SevenTiny.Cloud.MultiTenant.Domain.Repository
{
    public class MetaObjectCommonRepositoryBase<TEntity> : CommonRepositoryBase<TEntity>, IMetaObjectCommonRepositoryBase<TEntity> where TEntity : MetaObjectCommonBase
    {
        public MetaObjectCommonRepositoryBase(MultiTenantPlatformDbContext multiTenantPlatformDbContext) : base(multiTenantPlatformDbContext)
        {
            dbContext = multiTenantPlatformDbContext;
        }

        MultiTenantPlatformDbContext dbContext;

        public void DeleteByMetaObjectId(Guid metaObjectId)
        {
            dbContext.Delete<TEntity>(t => t.MetaObjectId == metaObjectId);
        }

        public List<TEntity> GetEntitiesByMetaObjectId(Guid metaObjectId)
            => dbContext.Queryable<TEntity>().Where(t => t.MetaObjectId == metaObjectId).ToList();

        public List<TEntity> GetEntitiesDeletedByMetaObjectId(Guid metaObjectId)
            => dbContext.Queryable<TEntity>().Where(t => t.IsDeleted == (int)IsDeleted.Deleted && t.MetaObjectId == metaObjectId).ToList();

        public List<TEntity> GetEntitiesUnDeletedByMetaObjectId(Guid metaObjectId)
            => dbContext.Queryable<TEntity>().Where(t => t.IsDeleted == (int)IsDeleted.UnDeleted && t.MetaObjectId == metaObjectId).ToList();

        /// <summary>
        /// 获取同对象下的编码或者名称相同的数据
        /// </summary>
        /// <param name="metaObjectId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity GetByCodeOrNameWithSameMetaObjectIdAndNotSameId(Guid metaObjectId, Guid id, string code, string name)
            => dbContext.Queryable<TEntity>().Where(t => t.MetaObjectId == metaObjectId && t.Id != id && (t.Code.Equals(code) || t.Name.Equals(name))).FirstOrDefault();
    }
}
