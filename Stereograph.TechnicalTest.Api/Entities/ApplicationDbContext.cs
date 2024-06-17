using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Stereograph.TechnicalTest.Api.Entities;
using Stereograph.TechnicalTest.Api.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stereograph.TechnicalTest.Api.Models;

public class ApplicationDbContext : DbContext
{
    private readonly ApplicationDbSeeder _seeder;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ApplicationDbSeeder seeder) : base(options)
    {
        _seeder = seeder;
    }

    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var data = _seeder.Seed();
        modelBuilder.Entity<Person>().HasData(data);
    }
}
