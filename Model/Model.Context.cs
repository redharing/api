﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class mangaEntities : DbContext
    {
        public mangaEntities()
            : base("name=mangaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Manga> Manga { get; set; }
        public virtual DbSet<MangaChapter> MangaChapter { get; set; }
        public virtual DbSet<MangaImage> MangaImage { get; set; }
        public virtual DbSet<NewReleaseManga> NewReleaseManga { get; set; }
    }
}
