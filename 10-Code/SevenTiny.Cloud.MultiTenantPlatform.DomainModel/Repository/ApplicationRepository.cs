﻿using SevenTiny.Cloud.MultiTenantPlatform.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SevenTiny.Cloud.MultiTenantPlatform.DomainModel.Repository
{
    public class ApplicationRepository
    {
        public static List<Application> GetApplications()
        {
            using (var db = new MultiTenantPlatformDbContext())
            {
                return db.QueryList<Application>();
            }
        }
        public static List<Application> GetApplications(Expression<Func<Application,bool>> filter)
        {
            using (var db = new MultiTenantPlatformDbContext())
            {
                return db.QueryList(filter);
            }
        }
        public static void AddApplication(Application application)
        {
            using (var db = new MultiTenantPlatformDbContext())
            {
                db.Add(application);
            }
        }
    }
}
