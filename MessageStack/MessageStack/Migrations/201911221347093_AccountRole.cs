namespace MessageStack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountRoles");
        }
    }
}
