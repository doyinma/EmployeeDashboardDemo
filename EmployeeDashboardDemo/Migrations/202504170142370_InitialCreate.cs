namespace EmployeeDashboardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PositionDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DivisionId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Description = c.String(),
                        CheckboxParkingStall = c.Boolean(nullable: false),
                        RadioDeskPhone = c.Boolean(nullable: false),
                        RadioCellPhone = c.Boolean(nullable: false),
                        RelatedDocument = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Divisions", t => t.DivisionId)
                .Index(t => t.DivisionId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PositionDescriptions", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.PositionDescriptions", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "DivisionId", "dbo.Divisions");
            DropIndex("dbo.PositionDescriptions", new[] { "DepartmentId" });
            DropIndex("dbo.PositionDescriptions", new[] { "DivisionId" });
            DropIndex("dbo.Departments", new[] { "DivisionId" });
            DropTable("dbo.PositionDescriptions");
            DropTable("dbo.Divisions");
            DropTable("dbo.Departments");
        }
    }
}
