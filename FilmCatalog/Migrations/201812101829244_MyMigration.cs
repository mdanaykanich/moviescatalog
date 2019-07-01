namespace FilmCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilmGenres", "FilmId", "dbo.Films");
            DropForeignKey("dbo.FilmGenres", "GenreId", "dbo.Genres");
            DropIndex("dbo.FilmGenres", new[] { "FilmId" });
            DropIndex("dbo.FilmGenres", new[] { "GenreId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.FilmGenres", "GenreId");
            CreateIndex("dbo.FilmGenres", "FilmId");
            AddForeignKey("dbo.FilmGenres", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FilmGenres", "FilmId", "dbo.Films", "Id", cascadeDelete: true);
        }
    }
}
