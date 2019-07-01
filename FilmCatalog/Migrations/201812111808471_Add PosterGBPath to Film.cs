namespace FilmCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPosterGBPathtoFilm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "Country", c => c.String());
            AddColumn("dbo.Films", "PosterBGPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Films", "PosterBGPath");
            DropColumn("dbo.Films", "Country");
        }
    }
}
