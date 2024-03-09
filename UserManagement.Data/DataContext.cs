﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("UserManagement.Data.DataContext");

    protected override void OnModelCreating(ModelBuilder model)
        => model.Entity<User>().HasData(new[]
        {
            new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", DateofBirth = new DateTime(1990,3,5), IsActive = true},
            new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", DateofBirth = new DateTime(1996,7,14), IsActive = true},
            new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", DateofBirth = new DateTime(2000,8,5), IsActive = false},
            new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", DateofBirth = new DateTime(2002,3,5), IsActive = true},
            new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", DateofBirth = new DateTime(2019,11,6), IsActive = true},
            new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", DateofBirth = new DateTime(2000,6,25), IsActive = true},
            new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", DateofBirth = new DateTime(2022,8,4), IsActive = false},
            new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", DateofBirth = new DateTime(1990,7,19), IsActive = false},
            new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", DateofBirth = new DateTime(1991,9,13), IsActive = false},
            new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", DateofBirth = new DateTime(1991,9,30), IsActive = true},
            new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", DateofBirth = new DateTime(1990,12,30), IsActive = true},
        });

    public DbSet<User>? Users { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }
}
