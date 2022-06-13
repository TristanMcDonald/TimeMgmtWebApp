namespace TimeMgmtLibraryCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDbCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModuleModels",
                c => new
                    {
                        ModuleCode = c.String(nullable: false, maxLength: 128),
                        ModuleName = c.String(nullable: false),
                        NoOfCredits = c.Int(nullable: false),
                        ClassHrsPerWeek = c.Double(nullable: false),
                        SelfStudyHrsPerWeek = c.Double(nullable: false),
                        UserModule_Id = c.Int(),
                        UserModule_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.ModuleCode)
                .ForeignKey("dbo.UserModules", t => t.UserModule_Id)
                .ForeignKey("dbo.UserModules", t => t.UserModule_Id1)
                .Index(t => t.UserModule_Id)
                .Index(t => t.UserModule_Id1);
            
            CreateTable(
                "dbo.SemesterModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumOfWeeks = c.Double(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 128),
                        ModuleCode = c.String(nullable: false, maxLength: 128),
                        SelfStudyHrsLeft = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ModuleModels", t => t.ModuleCode, cascadeDelete: true)
                .ForeignKey("dbo.UserModels", t => t.Username, cascadeDelete: true)
                .Index(t => t.Username)
                .Index(t => t.ModuleCode);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.UserSemesters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 128),
                        SemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SemesterModels", t => t.SemesterId, cascadeDelete: true)
                .ForeignKey("dbo.UserModels", t => t.Username, cascadeDelete: true)
                .Index(t => t.Username)
                .Index(t => t.SemesterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSemesters", "Username", "dbo.UserModels");
            DropForeignKey("dbo.UserSemesters", "SemesterId", "dbo.SemesterModels");
            DropForeignKey("dbo.ModuleModels", "UserModule_Id1", "dbo.UserModules");
            DropForeignKey("dbo.UserModules", "Username", "dbo.UserModels");
            DropForeignKey("dbo.ModuleModels", "UserModule_Id", "dbo.UserModules");
            DropForeignKey("dbo.UserModules", "ModuleCode", "dbo.ModuleModels");
            DropIndex("dbo.UserSemesters", new[] { "SemesterId" });
            DropIndex("dbo.UserSemesters", new[] { "Username" });
            DropIndex("dbo.UserModules", new[] { "ModuleCode" });
            DropIndex("dbo.UserModules", new[] { "Username" });
            DropIndex("dbo.ModuleModels", new[] { "UserModule_Id1" });
            DropIndex("dbo.ModuleModels", new[] { "UserModule_Id" });
            DropTable("dbo.UserSemesters");
            DropTable("dbo.UserModels");
            DropTable("dbo.UserModules");
            DropTable("dbo.SemesterModels");
            DropTable("dbo.ModuleModels");
        }
    }
}
