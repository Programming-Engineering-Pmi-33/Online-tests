namespace OnlineTests.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Question = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SubjectId = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        KeyWord = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestSummaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerUserId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseUsers", t => t.OwnerUserId, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.OwnerUserId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.TestItemOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestItemId = c.Int(nullable: false),
                        Answer = c.String(nullable: false, maxLength: 50),
                        IsCorrect = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestItems", t => t.TestItemId, cascadeDelete: true)
                .Index(t => t.TestItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestItemOptions", "TestItemId", "dbo.TestItems");
            DropForeignKey("dbo.TestItems", "TestId", "dbo.Tests");
            DropForeignKey("dbo.TestSummaries", "TestId", "dbo.Tests");
            DropForeignKey("dbo.TestSummaries", "OwnerUserId", "dbo.BaseUsers");
            DropForeignKey("dbo.Tests", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.TestItemOptions", new[] { "TestItemId" });
            DropIndex("dbo.TestSummaries", new[] { "TestId" });
            DropIndex("dbo.TestSummaries", new[] { "OwnerUserId" });
            DropIndex("dbo.Tests", new[] { "SubjectId" });
            DropIndex("dbo.TestItems", new[] { "TestId" });
            DropTable("dbo.TestItemOptions");
            DropTable("dbo.TestSummaries");
            DropTable("dbo.Subjects");
            DropTable("dbo.Tests");
            DropTable("dbo.TestItems");
        }
    }
}
