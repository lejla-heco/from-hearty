﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240107113601_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Cardiologist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cardiologists");
                });

            modelBuilder.Entity("Finding", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CardiologistId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardiologistNote")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("FeatureA")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("HouseDoctorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("HouseDoctorNote")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsSick")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsSickPrediction")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CardiologistId");

                    b.HasIndex("HouseDoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Findings");
                });

            modelBuilder.Entity("FindingBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AgeCategory")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AlcoholDrinking")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Asthma")
                        .HasColumnType("INTEGER");

                    b.Property<float>("BMI")
                        .HasColumnType("REAL");

                    b.Property<Guid?>("CardiologistId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardiologistNote")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Diabetic")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DiffWalking")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GenHealth")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("HouseDoctorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("HouseDoctorNote")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsSick")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsSickPrediction")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("KidneyDisease")
                        .HasColumnType("INTEGER");

                    b.Property<float>("MentalHealth")
                        .HasColumnType("REAL");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhysicalActivity")
                        .HasColumnType("INTEGER");

                    b.Property<float>("PhysicalHealth")
                        .HasColumnType("REAL");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("SkinCancer")
                        .HasColumnType("INTEGER");

                    b.Property<float>("SleepTime")
                        .HasColumnType("REAL");

                    b.Property<bool>("Smoking")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Stroke")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CardiologistId");

                    b.HasIndex("HouseDoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("FindingsBase");
                });

            modelBuilder.Entity("HouseDoctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HouseDoctors");
                });

            modelBuilder.Entity("Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateRegistered")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("HouseDoctorId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HouseDoctorId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Finding", b =>
                {
                    b.HasOne("Cardiologist", "Cardiologist")
                        .WithMany("Findings")
                        .HasForeignKey("CardiologistId");

                    b.HasOne("HouseDoctor", "HouseDoctor")
                        .WithMany("Findings")
                        .HasForeignKey("HouseDoctorId");

                    b.HasOne("Patient", "Patient")
                        .WithMany("Findings")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cardiologist");

                    b.Navigation("HouseDoctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("FindingBase", b =>
                {
                    b.HasOne("Cardiologist", "Cardiologist")
                        .WithMany()
                        .HasForeignKey("CardiologistId");

                    b.HasOne("HouseDoctor", "HouseDoctor")
                        .WithMany()
                        .HasForeignKey("HouseDoctorId");

                    b.HasOne("Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cardiologist");

                    b.Navigation("HouseDoctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Patient", b =>
                {
                    b.HasOne("HouseDoctor", "HouseDoctor")
                        .WithMany("Patient")
                        .HasForeignKey("HouseDoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HouseDoctor");
                });

            modelBuilder.Entity("Cardiologist", b =>
                {
                    b.Navigation("Findings");
                });

            modelBuilder.Entity("HouseDoctor", b =>
                {
                    b.Navigation("Findings");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Patient", b =>
                {
                    b.Navigation("Findings");
                });
#pragma warning restore 612, 618
        }
    }
}