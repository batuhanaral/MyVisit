namespace ProjeA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Number = c.String(),
                        Email = c.String(),
                        Adress = c.String(),
                        Photo = c.String(),
                        Iban = c.String(),
                        LinkedIn = c.String(),
                        Company = c.String(),
                        Title = c.String(),
                        Instagram = c.String(),
                        Facebook = c.String(),
                        Tiktok = c.String(),
                        WebSite = c.String(),
                        NameDurum = c.String(),
                        SurnameDurum = c.String(),
                        NumberDurum = c.String(),
                        EmailDurum = c.String(),
                        AdressDurum = c.String(),
                        PhotoDurum = c.String(),
                        IbanDurum = c.String(),
                        LinkedInDurum = c.String(),
                        CompanyDurum = c.String(),
                        TitleDurum = c.String(),
                        InstagramDurum = c.String(),
                        FacebookDurum = c.String(),
                        TiktokDurum = c.String(),
                        WebSiteDurum = c.String(),
                        User_Id = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Information");
        }
    }
}
