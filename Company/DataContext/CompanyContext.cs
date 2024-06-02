using System.Data;
using Company.Models;
using Microsoft.EntityFrameworkCore;
using Company.ViewModels;

namespace Company.DataContext
{
    public class CompanyContext : DbContext
    {
        public DbSet<Роли> Роли { get; set; } = null!;
        public DbSet<Приложения> Приложения { get; set; } = null!;
        public DbSet<Права_доступа> Права_доступа { get; set; } = null!;
        public DbSet<Статусы> Статусы { get; set; } = null!;
        public DbSet<Сотрудники> Сотрудники { get; set; } = null!;
        public DbSet<Расписание> Расписание { get; set; } = null!;
        public DbSet<Ограничения_доступа> Ограничения_доступа { get; set; } = null!;
        public DbSet<Организации> Организации { get; set; } = null!;
        public DbSet<Отделы> Отделы { get; set; } = null!;
        public DbSet<Периоды_неисполнения_обязанностей> Периоды_неисполнения_обязанностей { get; set; } = null!;
        public DbSet<Кадровые_назначения> Кадровые_назначения { get; set; } = null!;
        public DbSet<Учетные_данные> Учетные_данные { get; set; } = null!;
        public DbSet<Сотрудники_приложения> Сотрудники_приложения { get; set; } = null!;
        public DbSet<Аккаунты> Аккаунты { get; set; } = null!;

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Company.ViewModels.EditEmployeesViewModel> EditEmployeesViewModel { get; set; } = default!;
    }
}