using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaiy_vet_DTO.Models
{
    public class ApplicationContext : DbContext
    {
        //Объявление переменных, через которые осуществляется доступ к таблицами базы данных, средствами EntityFramework
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Breed> Breeds { get; set; }

        //Конструктор класса с целью создания таблицы БД
        public ApplicationContext()
        {
            //Если требуется пересоздать базу данных, то необходимо снять комментарий со следующей строчки:
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        //Задание строки подключения к БД
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=root;database=usersdb5;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
    }
}
