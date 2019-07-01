namespace FilmCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class My : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.FilmGenres", "FilmId");
            CreateIndex("dbo.FilmGenres", "GenreId");
            AddForeignKey("dbo.FilmGenres", "FilmId", "dbo.Films", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FilmGenres", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilmGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.FilmGenres", "FilmId", "dbo.Films");
            DropIndex("dbo.FilmGenres", new[] { "GenreId" });
            DropIndex("dbo.FilmGenres", new[] { "FilmId" });
        }
    }
}
