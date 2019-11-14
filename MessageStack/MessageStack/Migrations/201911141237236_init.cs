namespace MessageStack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChatId = c.Guid(nullable: false),
                        SenderId = c.Guid(nullable: false),
                        SenderName = c.String(),
                        Text = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .Index(t => t.ChatId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        OwnerAccountId = c.Guid(nullable: false),
                        ContactAccountId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.ContactAccountId)
                .ForeignKey("dbo.Accounts", t => t.OwnerAccountId, cascadeDelete: true)
                .Index(t => t.OwnerAccountId)
                .Index(t => t.ContactAccountId);
            
            CreateTable(
                "dbo.ChatAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChatId = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .Index(t => t.ChatId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.ChatAccount1",
                c => new
                    {
                        Chat_Id = c.Guid(nullable: false),
                        Account_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Chat_Id, t.Account_Id })
                .ForeignKey("dbo.Chats", t => t.Chat_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Chat_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatAccounts", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.ChatAccounts", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Contacts", "OwnerAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Contacts", "ContactAccountId", "dbo.Accounts");
            DropForeignKey("dbo.ChatAccount1", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.ChatAccount1", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropIndex("dbo.ChatAccount1", new[] { "Account_Id" });
            DropIndex("dbo.ChatAccount1", new[] { "Chat_Id" });
            DropIndex("dbo.ChatAccounts", new[] { "AccountId" });
            DropIndex("dbo.ChatAccounts", new[] { "ChatId" });
            DropIndex("dbo.Contacts", new[] { "ContactAccountId" });
            DropIndex("dbo.Contacts", new[] { "OwnerAccountId" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropTable("dbo.ChatAccount1");
            DropTable("dbo.ChatAccounts");
            DropTable("dbo.Contacts");
            DropTable("dbo.Messages");
            DropTable("dbo.Chats");
            DropTable("dbo.Accounts");
        }
    }
}
