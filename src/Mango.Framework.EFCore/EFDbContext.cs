using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Mango.Entity;
namespace Mango.Framework.EFCore
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
        {

        }

        public EFDbContext(DbContextOptions<EFDbContext> options): base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Core.Configuration.GetItem("ConnectionStrings"));
            }
        }
        #region Entity DbSet<>
        public virtual DbSet<m_AppManager> m_AppManager { get; set; }

        public virtual DbSet<m_UserPlusRecords> m_UserPlusRecords { get; set; }
        public virtual DbSet<m_DocsTheme> m_DocsTheme { get; set; }
        public virtual DbSet<m_Docs> m_Docs { get; set; }

        public virtual DbSet<m_WebSiteConfig> m_WebSiteConfig { get; set; }
        public virtual DbSet<m_WebSiteNavigation> m_WebSiteNavigation { get; set; }

        public virtual DbSet<m_Sms> m_Sms { get; set; }

        public virtual DbSet<m_ManagerAccount> m_ManagerAccount { get; set; }
        public virtual DbSet<m_ManagerMenu> m_ManagerMenu { get; set; }
        public virtual DbSet<m_ManagerPower> m_ManagerPower { get; set; }
        public virtual DbSet<m_ManagerRole> m_ManagerRole { get; set; }
        public virtual DbSet<m_Message> m_Message { get; set; }
        public virtual DbSet<m_Navigation> m_Navigation { get; set; }
        public virtual DbSet<m_NavigationClassify> m_NavigationClassify { get; set; }
        public virtual DbSet<m_Posts> m_Posts { get; set; }
        public virtual DbSet<m_PostsChannel> m_PostsChannel { get; set; }
        public virtual DbSet<m_PostsAnswer> m_PostsAnswer { get; set; }
        public virtual DbSet<m_PostsAttention> m_PostsAttention { get; set; }
        public virtual DbSet<m_PostsComments> m_PostsComments { get; set; }
        public virtual DbSet<m_PostsTags> m_PostsTags { get; set; }
        public virtual DbSet<m_User> m_User { get; set; }
        public virtual DbSet<m_UserGroup> m_UserGroup { get; set; }
        public virtual DbSet<m_UserGroupMenu> m_UserGroupMenu { get; set; }
        public virtual DbSet<m_UserGroupPower> m_UserGroupPower { get; set; }
        #endregion
    }
}
