namespace TimeMgmtLibraryCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReminderDateAddedToUserModules : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModules", "ReminderDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserModules", "ReminderDate");
        }
    }
}
