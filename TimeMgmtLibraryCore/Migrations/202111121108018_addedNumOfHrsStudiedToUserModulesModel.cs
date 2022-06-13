namespace TimeMgmtLibraryCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNumOfHrsStudiedToUserModulesModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModules", "NumOfHrsStudied", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserModules", "NumOfHrsStudied");
        }
    }
}
