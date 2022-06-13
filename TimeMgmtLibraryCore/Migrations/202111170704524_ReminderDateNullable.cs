namespace TimeMgmtLibraryCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReminderDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserModules", "ReminderDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserModules", "ReminderDate", c => c.DateTime(nullable: false));
        }
    }
}
