﻿// <auto-generated />
using System;
using Fitweb.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(FitwebDbContext))]
    [Migration("20210926200424_ChangeForeignKeysInAthleteFoodProduct")]
    partial class ChangeForeignKeysInAthleteFoodProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Fitweb.Domain.Athletes.Athlete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("NumberOfTrainings")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.Property<int?>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("Fitweb.Domain.Athletes.AthleteFoodProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FoodProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("FoodProductId");

                    b.HasIndex("AthleteId", "FoodProductId");

                    b.ToTable("AthleteFoodProducts");
                });

            modelBuilder.Entity("Fitweb.Domain.Athletes.DietInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("TotalCalories")
                        .HasColumnType("double");

                    b.Property<double>("TotalCarbohydrates")
                        .HasColumnType("double");

                    b.Property<double>("TotalFats")
                        .HasColumnType("double");

                    b.Property<double>("TotalProteins")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId");

                    b.ToTable("DietInformations");
                });

            modelBuilder.Entity("Fitweb.Domain.Exercises.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PartOfBody")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Fitweb.Domain.FoodProducts.FoodProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Group")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FoodProducts");
                });

            modelBuilder.Entity("Fitweb.Domain.Trainings.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("NumberOfReps")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSets")
                        .HasColumnType("int");

                    b.Property<int>("TrainingId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("TrainingId", "ExerciseId");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("Fitweb.Domain.Trainings.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("Fitweb.Domain.Trainings.TrainingExercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("TrainingId")
                        .HasColumnType("int");

                    b.HasKey("ExerciseId", "TrainingId");

                    b.HasIndex("TrainingId");

                    b.ToTable("TrainingExercises");
                });

            modelBuilder.Entity("Fitweb.Infrastructure.Identity.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("RevokedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Fitweb.Infrastructure.Identity.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "65d6d0e9-1f8f-4d34-98f7-890a5d2ab2d8",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMINISTRATOR",
                            PasswordHash = "AQAAAAEAACcQAAAAEMu5R+bTs43elzo4+Te2Bfydc4Bpurrxp5Tk/c1Bq38YPf8jN7AurIRKhZJeECoW9Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "19ae77ac-ca60-4c79-a454-528d49a83b44",
                            TwoFactorEnabled = false,
                            UserName = "administrator"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                            ConcurrencyStamp = "c8b41a5f-3329-4095-ad32-c98389a2634d",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                            ConcurrencyStamp = "790b1318-eb76-42c2-a411-edf812261e16",
                            Name = "Athlete",
                            NormalizedName = "ATHLETE"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                            RoleId = "a792b6cb-8230-4a37-9353-1a05d642ffe2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Fitweb.Domain.Athletes.AthleteFoodProduct", b =>
                {
                    b.HasOne("Fitweb.Domain.Athletes.Athlete", "Athlete")
                        .WithMany("FoodProducts")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fitweb.Domain.FoodProducts.FoodProduct", "FoodProduct")
                        .WithMany()
                        .HasForeignKey("FoodProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("FoodProduct");
                });

            modelBuilder.Entity("Fitweb.Domain.Athletes.DietInformation", b =>
                {
                    b.HasOne("Fitweb.Domain.Athletes.Athlete", "Athlete")
                        .WithMany("DietInformations")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");
                });

            modelBuilder.Entity("Fitweb.Domain.Exercises.Exercise", b =>
                {
                    b.OwnsOne("Fitweb.Domain.ValueObjects.Information", "Information", b1 =>
                        {
                            b1.Property<int>("ExerciseId")
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("longtext")
                                .HasColumnName("Description");

                            b1.Property<string>("Name")
                                .HasColumnType("longtext")
                                .HasColumnName("Name");

                            b1.HasKey("ExerciseId");

                            b1.ToTable("Exercises");

                            b1.WithOwner()
                                .HasForeignKey("ExerciseId");
                        });

                    b.Navigation("Information");
                });

            modelBuilder.Entity("Fitweb.Domain.FoodProducts.FoodProduct", b =>
                {
                    b.OwnsOne("Fitweb.Domain.ValueObjects.Calories", "Calories", b1 =>
                        {
                            b1.Property<int>("FoodProductId")
                                .HasColumnType("int");

                            b1.Property<double>("Value")
                                .HasColumnType("double")
                                .HasColumnName("Calories");

                            b1.HasKey("FoodProductId");

                            b1.ToTable("FoodProducts");

                            b1.WithOwner()
                                .HasForeignKey("FoodProductId");
                        });

                    b.OwnsOne("Fitweb.Domain.ValueObjects.Information", "Information", b1 =>
                        {
                            b1.Property<int>("FoodProductId")
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("longtext")
                                .HasColumnName("Description");

                            b1.Property<string>("Name")
                                .HasColumnType("longtext")
                                .HasColumnName("Name");

                            b1.HasKey("FoodProductId");

                            b1.ToTable("FoodProducts");

                            b1.WithOwner()
                                .HasForeignKey("FoodProductId");
                        });

                    b.OwnsOne("Fitweb.Domain.ValueObjects.Nutrient", "Nutrient", b1 =>
                        {
                            b1.Property<int>("FoodProductId")
                                .HasColumnType("int");

                            b1.Property<double>("Carbohydrate")
                                .HasColumnType("double")
                                .HasColumnName("Carbohydrate");

                            b1.Property<double>("Fat")
                                .HasColumnType("double")
                                .HasColumnName("Fat");

                            b1.Property<double?>("Fiber")
                                .HasColumnType("double")
                                .HasColumnName("Fiber");

                            b1.Property<double>("Protein")
                                .HasColumnType("double")
                                .HasColumnName("Protein");

                            b1.Property<double?>("Salt")
                                .HasColumnType("double")
                                .HasColumnName("Salt");

                            b1.Property<double?>("SaturatedFat")
                                .HasColumnType("double")
                                .HasColumnName("SaturatedFat");

                            b1.Property<double?>("Sugar")
                                .HasColumnType("double")
                                .HasColumnName("Sugar");

                            b1.HasKey("FoodProductId");

                            b1.ToTable("FoodProducts");

                            b1.WithOwner()
                                .HasForeignKey("FoodProductId");
                        });

                    b.Navigation("Calories");

                    b.Navigation("Information");

                    b.Navigation("Nutrient");
                });

            modelBuilder.Entity("Fitweb.Domain.Trainings.Set", b =>
                {
                    b.HasOne("Fitweb.Domain.Trainings.TrainingExercise", "TrainingExercise")
                        .WithMany("Sets")
                        .HasForeignKey("TrainingId", "ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingExercise");
                });

            modelBuilder.Entity("Fitweb.Domain.Trainings.Training", b =>
                {
                    b.HasOne("Fitweb.Domain.Athletes.Athlete", "Athlete")
                        .WithMany("Trainings")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Fitweb.Domain.ValueObjects.Information", "Information", b1 =>
                        {
                            b1.Property<int>("TrainingId")
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("longtext")
                                .HasColumnName("Description");

                            b1.Property<string>("Name")
                                .HasColumnType("longtext")
                                .HasColumnName("Name");

                            b1.HasKey("TrainingId");

                            b1.ToTable("Trainings");

                            b1.WithOwner()
                                .HasForeignKey("TrainingId");
                        });

                    b.Navigation("Athlete");

                    b.Navigation("Information");
                });

            modelBuilder.Entity("Fitweb.Domain.Trainings.TrainingExercise", b =>
                {
                    b.HasOne("Fitweb.Domain.Exercises.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fitweb.Domain.Trainings.Training", "Training")
                        .WithMany("Exercises")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Fitweb.Infrastructure.Identity.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Fitweb.Infrastructure.Identity.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fitweb.Infrastructure.Identity.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Fitweb.Infrastructure.Identity.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fitweb.Domain.Athletes.Athlete", b =>
                {
                    b.Navigation("DietInformations");

                    b.Navigation("FoodProducts");

                    b.Navigation("Trainings");
                });

            modelBuilder.Entity("Fitweb.Domain.Trainings.Training", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Fitweb.Domain.Trainings.TrainingExercise", b =>
                {
                    b.Navigation("Sets");
                });
#pragma warning restore 612, 618
        }
    }
}
