namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredFieldsAndMaxLens : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "EmployeeId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employees", "MiddleName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "LastName", c => c.String());
            AlterColumn("dbo.Employees", "MiddleName", c => c.String());
            AlterColumn("dbo.Employees", "FirstName", c => c.String());
            AlterColumn("dbo.Employees", "EmployeeId", c => c.String());
        }
    }
}
