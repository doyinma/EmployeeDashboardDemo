namespace EmployeeDashboardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPositionModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PositionDescriptions", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.PositionDescriptions", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PositionDescriptions", "Description", c => c.String());
            AlterColumn("dbo.PositionDescriptions", "Name", c => c.String());
        }
    }
}
