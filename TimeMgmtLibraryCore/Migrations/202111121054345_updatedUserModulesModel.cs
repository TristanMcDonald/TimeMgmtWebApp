namespace TimeMgmtLibraryCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedUserModulesModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ModuleModels", "UserModule_Id", "dbo.UserModules");
            DropForeignKey("dbo.ModuleModels", "UserModule_Id1", "dbo.UserModules");
            DropIndex("dbo.ModuleModels", new[] { "UserModule_Id" });
            DropIndex("dbo.ModuleModels", new[] { "UserModule_Id1" });
            DropColumn("dbo.ModuleModels", "UserModule_Id");
            DropColumn("dbo.ModuleModels", "UserModule_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModuleModels", "UserModule_Id1", c => c.Int());
            AddColumn("dbo.ModuleModels", "UserModule_Id", c => c.Int());
            CreateIndex("dbo.ModuleModels", "UserModule_Id1");
            CreateIndex("dbo.ModuleModels", "UserModule_Id");
            AddForeignKey("dbo.ModuleModels", "UserModule_Id1", "dbo.UserModules", "Id");
            AddForeignKey("dbo.ModuleModels", "UserModule_Id", "dbo.UserModules", "Id");
        }
    }
}
