﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("AllDay")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Approved")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CardiologistId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CardiologistId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

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

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cardiologists");
                });

            modelBuilder.Entity("Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Documents");
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

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

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

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HouseDoctorId");

                    b.HasIndex("UserId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PredictionResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<float>("Ca")
                        .HasColumnType("REAL");

                    b.Property<float>("Chol")
                        .HasColumnType("REAL");

                    b.Property<float>("Cp")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<float>("Exang")
                        .HasColumnType("REAL");

                    b.Property<float>("Fbs")
                        .HasColumnType("REAL");

                    b.Property<Guid?>("HouseDoctorId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Label")
                        .HasColumnType("REAL");

                    b.Property<float>("OldPeak")
                        .HasColumnType("REAL");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.Property<float>("Percentage")
                        .HasColumnType("REAL");

                    b.Property<float>("RestEcg")
                        .HasColumnType("REAL");

                    b.Property<float>("Slope")
                        .HasColumnType("REAL");

                    b.Property<float>("Thal")
                        .HasColumnType("REAL");

                    b.Property<float>("Thalach")
                        .HasColumnType("REAL");

                    b.Property<float>("TrestBps")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("HouseDoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("PredictionResults");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserRoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Video", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Appointment", b =>
                {
                    b.HasOne("Cardiologist", "Cardiologist")
                        .WithMany("Appointment")
                        .HasForeignKey("CardiologistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Patient", "Patient")
                        .WithMany("Appointment")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cardiologist");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Cardiologist", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany("Cardiologist")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Document", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("HouseDoctor", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany("HouseDoctor")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Patient", b =>
                {
                    b.HasOne("HouseDoctor", "HouseDoctor")
                        .WithMany("Patient")
                        .HasForeignKey("HouseDoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("Patient")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HouseDoctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PredictionResult", b =>
                {
                    b.HasOne("HouseDoctor", "HouseDoctor")
                        .WithMany("PredictionResult")
                        .HasForeignKey("HouseDoctorId");

                    b.HasOne("Patient", "Patient")
                        .WithMany("PredictionResult")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HouseDoctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.HasOne("UserRole", "UserRole")
                        .WithMany("User")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Video", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany("Video")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cardiologist", b =>
                {
                    b.Navigation("Appointment");

                    b.Navigation("Findings");
                });

            modelBuilder.Entity("HouseDoctor", b =>
                {
                    b.Navigation("Findings");

                    b.Navigation("Patient");

                    b.Navigation("PredictionResult");
                });

            modelBuilder.Entity("Patient", b =>
                {
                    b.Navigation("Appointment");

                    b.Navigation("Findings");

                    b.Navigation("PredictionResult");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("Cardiologist");

                    b.Navigation("HouseDoctor");

                    b.Navigation("Patient");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
