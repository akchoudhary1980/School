﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using School;

namespace School.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220104031342_SectionModel")]
    partial class SectionModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("School.Areas.Admin.Models.BoardModel", b =>
                {
                    b.Property<int>("BoardID")
                        .HasColumnType("int");

                    b.Property<string>("BoardDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BoardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.HasKey("BoardID");

                    b.ToTable("BoardModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.CityModel", b =>
                {
                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateID")
                        .HasColumnType("int");

                    b.HasKey("CityID");

                    b.ToTable("CityModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.ClassModel", b =>
                {
                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<string>("ClassDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.HasKey("ClassID");

                    b.ToTable("ClassModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.CountryModel", b =>
                {
                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryID");

                    b.ToTable("CountryModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.DesginationModel", b =>
                {
                    b.Property<int>("DesginationID")
                        .HasColumnType("int");

                    b.Property<string>("DesginationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.HasKey("DesginationID");

                    b.ToTable("DesginationModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.FeesHeadModel", b =>
                {
                    b.Property<int>("FeesHeadID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeesHeadName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeesHeadType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.HasKey("FeesHeadID");

                    b.ToTable("FeesHeadModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.FeesStructureModel", b =>
                {
                    b.Property<int>("FeesStructureID")
                        .HasColumnType("int");

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<string>("Pictures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.HasKey("FeesStructureID");

                    b.ToTable("FeesStructureModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.FeesStructureTransModel", b =>
                {
                    b.Property<int>("FeesStructureTransID")
                        .HasColumnType("int");

                    b.Property<string>("BillingCycle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("FeesAmount")
                        .HasColumnType("float");

                    b.Property<int>("FeesHeadID")
                        .HasColumnType("int");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.HasKey("FeesStructureTransID");

                    b.ToTable("FeesStructureTransModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.QualificationModel", b =>
                {
                    b.Property<int>("QualificationID")
                        .HasColumnType("int");

                    b.Property<string>("QualificationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.HasKey("QualificationID");

                    b.ToTable("QualificationModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.QualificationTransModel", b =>
                {
                    b.Property<int>("QualificationTransID")
                        .HasColumnType("int");

                    b.Property<int>("BoardID")
                        .HasColumnType("int");

                    b.Property<string>("PassingYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Percent")
                        .HasColumnType("float");

                    b.Property<string>("QualificationFor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QualificationID")
                        .HasColumnType("int");

                    b.Property<double>("RecieptMark")
                        .HasColumnType("float");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.Property<int>("StaffID")
                        .HasColumnType("int");

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.Property<double>("TotalMark")
                        .HasColumnType("float");

                    b.HasKey("QualificationTransID");

                    b.ToTable("QualificationTransModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.SchoolModel", b =>
                {
                    b.Property<int>("SchoolID")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BoardID")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassFromTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPerson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EstablishedYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolAbout")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolBoysOrGirls")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolBuildArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolCampusArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolGroundArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SchoolNoOfClassRoom")
                        .HasColumnType("int");

                    b.Property<int?>("SchoolNoOfLabRoom")
                        .HasColumnType("int");

                    b.Property<int?>("SchoolNoOfSwimmingPool")
                        .HasColumnType("int");

                    b.Property<int?>("SchoolNoOfToilets")
                        .HasColumnType("int");

                    b.Property<string>("SchoolTimmingFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolTimmingTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SchoolID");

                    b.ToTable("SchoolModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.SectionModel", b =>
                {
                    b.Property<int>("SectionID")
                        .HasColumnType("int");

                    b.Property<string>("SectionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.HasKey("SectionID");

                    b.ToTable("SectionModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.SessionYearModel", b =>
                {
                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionYearName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SessionYearRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("SessionYearID");

                    b.ToTable("SessionYearModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.StaffModel", b =>
                {
                    b.Property<int>("StaffID")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfAppointment")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("DesginationID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsPF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PFNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermanetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resume")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Salary")
                        .HasColumnType("float");

                    b.Property<string>("ScanDocuments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffID");

                    b.ToTable("StaffModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.StateModel", b =>
                {
                    b.Property<int>("StateID")
                        .HasColumnType("int");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StateID");

                    b.ToTable("StateModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.SubJectTransModel", b =>
                {
                    b.Property<int>("SubJectTransID")
                        .HasColumnType("int");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.HasKey("SubJectTransID");

                    b.ToTable("SubJectTransModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.SubjectModel", b =>
                {
                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectID");

                    b.ToTable("SubjectModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.TeacherModel", b =>
                {
                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassSection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfAppointment")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("DesginationID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsClassTeacher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsPF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PFNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermanetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resume")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Salary")
                        .HasColumnType("float");

                    b.Property<string>("ScanDocuments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherID");

                    b.ToTable("TeacherModels");
                });

            modelBuilder.Entity("School.Areas.Admin.Models.UserModel", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("AccountStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ReadRights")
                        .HasColumnType("bit");

                    b.Property<int>("SessionYearID")
                        .HasColumnType("int");

                    b.Property<bool>("SettingRights")
                        .HasColumnType("bit");

                    b.Property<bool>("UserCreateRights")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WrightRights")
                        .HasColumnType("bit");

                    b.HasKey("UserID");

                    b.ToTable("UserModels");
                });
#pragma warning restore 612, 618
        }
    }
}
