using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PDX.DAL;

namespace PDX.DAL.Migrations
{
    [DbContext(typeof(PDXContext))]
    partial class PDXContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("PDX.Domain.Account.Menu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Icon")
                        .HasColumnName("icon");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<string>("MenuCode")
                        .IsRequired()
                        .HasColumnName("menu_code");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<int?>("ParentMenuID")
                        .HasColumnName("parent_menu_id");

                    b.Property<int>("Priority")
                        .HasColumnName("priority");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnName("url");

                    b.HasKey("ID");

                    b.HasIndex("ParentMenuID");

                    b.ToTable("menu","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.MenuRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<int>("MenuID")
                        .HasColumnName("menu_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("RoleID")
                        .HasColumnName("role_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("MenuID");

                    b.HasIndex("RoleID");

                    b.ToTable("menu_role","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.Permission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Category")
                        .HasColumnName("category");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("PermissionCode")
                        .IsRequired()
                        .HasColumnName("permission_code");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("permission","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.ReportRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ReportID")
                        .HasColumnName("report_id");

                    b.Property<int>("RoleID")
                        .HasColumnName("role_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("ReportID");

                    b.HasIndex("RoleID");

                    b.ToTable("report_role","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("RoleCode")
                        .IsRequired()
                        .HasColumnName("role_code");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("role","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.RolePermission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("PermissionID")
                        .HasColumnName("permission_id");

                    b.Property<int>("RoleID")
                        .HasColumnName("role_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("PermissionID");

                    b.HasIndex("RoleID");

                    b.ToTable("role_permission","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnName("last_login");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(100);

                    b.Property<string>("Phone2")
                        .HasColumnName("phone2")
                        .HasMaxLength(100);

                    b.Property<string>("Phone3")
                        .HasColumnName("phone3")
                        .HasMaxLength(100);

                    b.Property<int?>("RoleID");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("UserTypeID")
                        .HasColumnName("user_type_id");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("user_name");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserTypeID");

                    b.ToTable("user","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.UserAgent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AgentID")
                        .HasColumnName("agent_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("UserID")
                        .HasColumnName("user_id");

                    b.HasKey("ID");

                    b.HasIndex("AgentID");

                    b.HasIndex("UserID");

                    b.ToTable("user_agent","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.UserLogin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Browser")
                        .HasColumnName("browser");

                    b.Property<string>("BrowserVersion")
                        .HasColumnName("browser_version");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("DeviceType")
                        .HasColumnName("device_type");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnName("login_time");

                    b.Property<string>("LogoutReason")
                        .HasColumnName("logout_reason");

                    b.Property<DateTime?>("LogoutTime")
                        .HasColumnName("logout_time");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("OS")
                        .HasColumnName("os");

                    b.Property<string>("OSVersion")
                        .HasColumnName("os_version");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("UserID")
                        .HasColumnName("user_id");

                    b.HasKey("ID");

                    b.ToTable("user_login","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.UserRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("RoleID")
                        .HasColumnName("role_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("UserID")
                        .HasColumnName("user_id");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("user_role","account");
                });

            modelBuilder.Entity("PDX.Domain.Catalog.Checklist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AnswerTypeID")
                        .HasColumnName("answer_type_id");

                    b.Property<int>("ChecklistTypeID")
                        .HasColumnName("checklist_type_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsSRA")
                        .HasColumnName("is_sra");

                    b.Property<string>("Lable")
                        .HasColumnName("label");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<int>("ParentChecklistID")
                        .HasColumnName("parent_checklist_id");

                    b.Property<int?>("Priority")
                        .HasColumnName("priority");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("AnswerTypeID");

                    b.HasIndex("ChecklistTypeID");

                    b.ToTable("checklist","catalog");
                });

            modelBuilder.Entity("PDX.Domain.Catalog.Option", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<int>("OptionGroupID")
                        .HasColumnName("option_group_id");

                    b.Property<int>("Priority")
                        .HasColumnName("priority");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("OptionGroupID");

                    b.ToTable("option","catalog");
                });

            modelBuilder.Entity("PDX.Domain.Catalog.SubmoduleChecklist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("ChecklistID")
                        .HasColumnName("checklist_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("SubmoduleID")
                        .HasColumnName("submodule_id");

                    b.HasKey("ID");

                    b.HasIndex("ChecklistID");

                    b.HasIndex("SubmoduleID");

                    b.ToTable("submodule_checklist","catalog");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.AdminRoute", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AdminRouteCode")
                        .IsRequired()
                        .HasColumnName("admin_route_code")
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("admin_route","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.AgeGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("age_group","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.AgentProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AgentID")
                        .HasColumnName("agent_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ProductID")
                        .HasColumnName("product_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("AgentID");

                    b.HasIndex("ProductID");

                    b.ToTable("agent_product","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ATC", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("ATCCode")
                        .IsRequired()
                        .HasColumnName("atc_code")
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("atc","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ContainerType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("container_type","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.DosageForm", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("dosage_form","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.DosageUnit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("dosage_unit","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.Excipient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("excipient","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.INN", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("inn","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.PharmacologicalClassification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnName("prefix");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("pharmacological_classification","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.PharmacopoeiaStandard", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("pharmacopoeia_standard","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("AdminRouteID")
                        .HasColumnName("admin_route_id");

                    b.Property<int?>("AgeGroupID")
                        .HasColumnName("age_group_id");

                    b.Property<string>("BrandName")
                        .HasColumnName("brand_name")
                        .HasMaxLength(500);

                    b.Property<int>("CommodityTypeID")
                        .HasColumnName("commodity_type_id");

                    b.Property<int?>("ContainerTypeID")
                        .HasColumnName("container_type_id");

                    b.Property<int?>("CreatedByUserID")
                        .HasColumnName("created_by_user_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<string>("DosageForm")
                        .HasColumnName("dosage_form");

                    b.Property<int?>("DosageFormID")
                        .HasColumnName("dosage_form_id");

                    b.Property<string>("DosageStrength")
                        .HasColumnName("dosage_strength");

                    b.Property<string>("DosageUnit")
                        .HasColumnName("dosage_unit");

                    b.Property<int?>("DosageUnitID")
                        .HasColumnName("dosage_unit_id");

                    b.Property<string>("GenericName")
                        .IsRequired()
                        .HasColumnName("generic_name")
                        .HasMaxLength(500);

                    b.Property<int?>("INNID")
                        .HasColumnName("inn_id");

                    b.Property<string>("IngredientStatement")
                        .HasColumnName("ingredient_statement")
                        .HasMaxLength(500);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<int?>("ModifiedByUserID")
                        .HasColumnName("modified_by_user_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(500);

                    b.Property<int?>("PharmacologicalClassificationID")
                        .HasColumnName("pharmacological_classification_id");

                    b.Property<int?>("PharmacopoeiaStandardID")
                        .HasColumnName("pharmacopoei_standard_id");

                    b.Property<string>("Posology")
                        .HasColumnName("posology")
                        .HasMaxLength(500);

                    b.Property<string>("Presentation")
                        .HasColumnName("presentation")
                        .HasMaxLength(500);

                    b.Property<int?>("ProductCategoryID")
                        .HasColumnName("product_category_id");

                    b.Property<int?>("ProductTypeID")
                        .HasColumnName("product_type_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShelfLife")
                        .HasColumnName("shelf_life");

                    b.Property<int?>("ShelfLifeID")
                        .HasColumnName("shelf_life_id");

                    b.Property<string>("StorageCondition")
                        .HasColumnName("storage_condition")
                        .HasMaxLength(500);

                    b.HasKey("ID");

                    b.HasIndex("AdminRouteID");

                    b.HasIndex("AgeGroupID");

                    b.HasIndex("CommodityTypeID");

                    b.HasIndex("ContainerTypeID");

                    b.HasIndex("CreatedByUserID");

                    b.HasIndex("DosageFormID");

                    b.HasIndex("DosageUnitID");

                    b.HasIndex("INNID");

                    b.HasIndex("ModifiedByUserID");

                    b.HasIndex("PharmacologicalClassificationID");

                    b.HasIndex("PharmacopoeiaStandardID");

                    b.HasIndex("ProductCategoryID");

                    b.HasIndex("ProductTypeID");

                    b.HasIndex("ShelfLifeID");

                    b.ToTable("product","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ProductATC", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("ATCID")
                        .HasColumnName("atc_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ProductID")
                        .HasColumnName("product_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("ATCID");

                    b.HasIndex("ProductID");

                    b.ToTable("product_atc","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ProductCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("product_category","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ProductComposition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("DosageStrength")
                        .HasColumnName("dosage_strength");

                    b.Property<int>("DosageUnitID")
                        .HasColumnName("dosage_unit_id");

                    b.Property<int>("ExcipientID")
                        .HasColumnName("excipient_id");

                    b.Property<int>("INNID")
                        .HasColumnName("inn_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsActiveComposition")
                        .HasColumnName("is_active_composition");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("PharmacopoeiaStandardID")
                        .HasColumnName("pharmacopoeia_tandard_id");

                    b.Property<int>("ProductID")
                        .HasColumnName("product_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("DosageUnitID");

                    b.HasIndex("ExcipientID");

                    b.HasIndex("INNID");

                    b.HasIndex("PharmacopoeiaStandardID");

                    b.HasIndex("ProductID");

                    b.ToTable("product_composition","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ProductManufacturer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<int>("ManufacturerID")
                        .HasColumnName("manufacturer_id");

                    b.Property<int?>("ManufacturerTypeID")
                        .HasColumnName("manufacturer_type_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ProductID")
                        .HasColumnName("product_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("ManufacturerID");

                    b.HasIndex("ManufacturerTypeID");

                    b.HasIndex("ProductID");

                    b.ToTable("product_manufacturer","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ProductType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("product_type","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ShelfLife", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("shelf_life","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.SRA", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("SRACode")
                        .IsRequired()
                        .HasColumnName("sra_code")
                        .HasMaxLength(10);

                    b.Property<int>("SRATypeID")
                        .HasColumnName("sra_type_id");

                    b.HasKey("ID");

                    b.HasIndex("SRATypeID");

                    b.ToTable("sra","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.SupplierProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnName("expiry_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ProductID")
                        .HasColumnName("product_id");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnName("registration_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("SupplierID")
                        .HasColumnName("supplier_id");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SupplierID");

                    b.ToTable("supplier_product","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.UseCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("use_category","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Common.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasMaxLength(100);

                    b.Property<int>("CountryID")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("HouseNumber")
                        .HasColumnName("house_number")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<string>("Kebele")
                        .HasColumnName("kebele")
                        .HasMaxLength(100);

                    b.Property<string>("Line1")
                        .HasColumnName("line1")
                        .HasMaxLength(500);

                    b.Property<string>("Line2")
                        .HasColumnName("line2")
                        .HasMaxLength(500);

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("PlaceName")
                        .HasColumnName("place_name")
                        .HasMaxLength(100);

                    b.Property<string>("Region")
                        .HasColumnName("region")
                        .HasMaxLength(100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("StreetName")
                        .HasColumnName("street_name")
                        .HasMaxLength(100);

                    b.Property<string>("SubCity")
                        .HasColumnName("sub_city")
                        .HasMaxLength(100);

                    b.Property<string>("Woreda")
                        .HasColumnName("woreda")
                        .HasMaxLength(100);

                    b.Property<string>("ZipCode")
                        .HasColumnName("zip_code")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("address","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.AgentLevel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AgentLevelCode")
                        .IsRequired()
                        .HasColumnName("agent_level_code");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("agent_level","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.AgentType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AgentTypeCode")
                        .IsRequired()
                        .HasColumnName("agent_type_code");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("agent_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.AnswerType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AnswerTypeCode")
                        .IsRequired()
                        .HasColumnName("answer_type_code");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("answer_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.ChecklistType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("ChecklistTypeCode")
                        .IsRequired()
                        .HasColumnName("checklist_type_code");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("checklist_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.CommodityType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("CommodityTypeCode")
                        .IsRequired()
                        .HasColumnName("commodity_type_code");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("commodity_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnName("country_code")
                        .HasMaxLength(5);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("country","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.Currency", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.Property<string>("Symbol")
                        .HasColumnName("symbol");

                    b.HasKey("ID");

                    b.ToTable("currency","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.DocumentType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<string>("DocumentTypeCode")
                        .HasColumnName("document_type_code");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsSystemGenerated")
                        .HasColumnName("is_system_generated");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("document_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.FeeType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<int>("CurrencyID")
                        .HasColumnName("currency_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("CurrencyID");

                    b.ToTable("fee_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.ForeignApplicationStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("ForeignApplicationStatusCode")
                        .IsRequired()
                        .HasColumnName("foreign_application_status_code");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("foreign_application_status","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.ImportPermitDelivery", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("import_permit_delivery","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.ImportPermitStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<string>("DisplayName")
                        .HasColumnName("display_name");

                    b.Property<string>("ImportPermitStatusCode")
                        .IsRequired()
                        .HasColumnName("import_permit_status_code");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<int?>("Priority")
                        .HasColumnName("priority");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("import_permit_status","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.ImportPermitType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<string>("ImportPermitTypeCode")
                        .IsRequired()
                        .HasColumnName("import_permit_type_code");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("import_permit_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.ManufacturerType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<string>("ManufacturerTypeCode")
                        .IsRequired()
                        .HasColumnName("manufacturer_type_code");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("manufacturer_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.MAStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("DisplayName")
                        .HasColumnName("display_name");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<string>("MAStatusCode")
                        .IsRequired()
                        .HasColumnName("ma_status_code");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<int?>("Priority")
                        .HasColumnName("priority");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("ma_status","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.MAType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<string>("MATypeCode")
                        .IsRequired()
                        .HasColumnName("ma_type_code");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("ma_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.Module", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("ModuleCode")
                        .HasColumnName("module_code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("module","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.OptionGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("option_group","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.PaymentMode", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("PaymentCode")
                        .IsRequired()
                        .HasColumnName("payment_code");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("payment_mode","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.PaymentTerm", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("PaymentTermCode")
                        .IsRequired()
                        .HasColumnName("payment_term_code");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("payment_term","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.PortOfEntry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("PortCode")
                        .HasColumnName("port_code");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("ShippingMethodID")
                        .HasColumnName("shipping_method_id");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("ShippingMethodID");

                    b.ToTable("port_of_entry","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.ReportType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<string>("ReportTypeCode")
                        .IsRequired()
                        .HasColumnName("report_type_code");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("report_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.ResponderType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("responder_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.ShippingMethod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShippingCode")
                        .HasColumnName("shipping_code");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("shipping_method","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.SRAType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("sra_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.Submodule", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ModuleID")
                        .HasColumnName("module_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.Property<string>("SubmoduleCode")
                        .HasColumnName("submodule_code");

                    b.HasKey("ID");

                    b.HasIndex("ModuleID");

                    b.ToTable("submodule","common");
                });

            modelBuilder.Entity("PDX.Domain.Common.SystemSetting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("DataType")
                        .HasColumnName("data_type")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("SystemSettingCode")
                        .IsRequired()
                        .HasColumnName("system_setting_code");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("value");

                    b.HasKey("ID");

                    b.ToTable("system_setting","settings");
                });

            modelBuilder.Entity("PDX.Domain.Common.UserType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShortName")
                        .HasColumnName("short_name")
                        .HasMaxLength(100);

                    b.Property<string>("UserTypeCode")
                        .IsRequired()
                        .HasColumnName("user_type_code");

                    b.HasKey("ID");

                    b.ToTable("user_type","common");
                });

            modelBuilder.Entity("PDX.Domain.Customer.Agent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AddressID")
                        .HasColumnName("address_id");

                    b.Property<int>("AgentTypeID")
                        .HasColumnName("agent_type_id");

                    b.Property<string>("ContactPerson")
                        .HasColumnName("contact_person")
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(100);

                    b.Property<string>("Fax")
                        .HasColumnName("fax")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsApproved")
                        .HasColumnName("is_approved");

                    b.Property<string>("LicenseNumber")
                        .HasColumnName("license_number")
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(500);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(100);

                    b.Property<string>("Phone2")
                        .HasColumnName("phone2")
                        .HasMaxLength(100);

                    b.Property<string>("Phone3")
                        .HasColumnName("phone3")
                        .HasMaxLength(100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Website")
                        .HasColumnName("website")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.HasIndex("AgentTypeID");

                    b.ToTable("agent","customer");
                });

            modelBuilder.Entity("PDX.Domain.Customer.AgentSupplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AgentID")
                        .HasColumnName("agent_id");

                    b.Property<int>("AgentLevelID")
                        .HasColumnName("agent_level_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnName("end_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Remark")
                        .HasColumnName("remark");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnName("start_date");

                    b.Property<int>("SupplierID")
                        .HasColumnName("supplier_id");

                    b.HasKey("ID");

                    b.HasIndex("AgentID");

                    b.HasIndex("AgentLevelID");

                    b.HasIndex("SupplierID");

                    b.ToTable("agent_supplier","customer");
                });

            modelBuilder.Entity("PDX.Domain.Customer.Manufacturer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("AddressID")
                        .HasColumnName("address_id");

                    b.Property<int>("CountryID")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(100);

                    b.Property<string>("Fax")
                        .HasColumnName("fax")
                        .HasMaxLength(100);

                    b.Property<string>("GMPCertificateNumber")
                        .HasColumnName("gmp_certificate_number")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("GMPInspectedDate")
                        .HasColumnName("gmp_inspected_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool?>("IsGMPInspected")
                        .HasColumnName("is_gmp_inspected");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(300);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(100);

                    b.Property<string>("Phone2")
                        .HasColumnName("phone2")
                        .HasMaxLength(100);

                    b.Property<string>("Phone3")
                        .HasColumnName("phone3")
                        .HasMaxLength(100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Site")
                        .HasColumnName("site");

                    b.Property<string>("Website")
                        .HasColumnName("website")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.HasIndex("CountryID");

                    b.ToTable("manufacturer","customer");
                });

            modelBuilder.Entity("PDX.Domain.Customer.Supplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AddressID")
                        .HasColumnName("address_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(100);

                    b.Property<string>("Fax")
                        .HasColumnName("fax")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(500);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(100);

                    b.Property<string>("Phone2")
                        .HasColumnName("phone2")
                        .HasMaxLength(100);

                    b.Property<string>("Phone3")
                        .HasColumnName("phone3")
                        .HasMaxLength(100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Website")
                        .HasColumnName("website")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.ToTable("supplier","customer");
                });

            modelBuilder.Entity("PDX.Domain.Document.Document", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnName("file_path");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnName("file_type");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ModuleDocumentID")
                        .HasColumnName("module_document_id");

                    b.Property<int>("ReferenceID")
                        .HasColumnName("reference_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("UpdatedBy")
                        .HasColumnName("updated_by");

                    b.HasKey("ID");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModuleDocumentID");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("document","document");
                });

            modelBuilder.Entity("PDX.Domain.Document.Letter", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Footer");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ModuleDocumentID")
                        .HasColumnName("module_document_id");

                    b.Property<string>("OtherText");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("ModuleDocumentID");

                    b.ToTable("letter","document");
                });

            modelBuilder.Entity("PDX.Domain.Document.LetterHeading", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<string>("LogoUrl");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.ToTable("letter_heading","document");
                });

            modelBuilder.Entity("PDX.Domain.Document.ModuleDocument", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<int>("DocumentTypeID")
                        .HasColumnName("document_type_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsRequired")
                        .HasColumnName("is_required");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("SubmoduleID")
                        .HasColumnName("submodule_id");

                    b.HasKey("ID");

                    b.HasIndex("DocumentTypeID");

                    b.HasIndex("SubmoduleID");

                    b.ToTable("module_document","document");
                });

            modelBuilder.Entity("PDX.Domain.Document.PrintLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<int>("DocumentID")
                        .HasColumnName("document_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("PrintedByUserID")
                        .HasColumnName("printed_by_user_id");

                    b.Property<DateTime>("PrintedDate")
                        .HasColumnName("printed_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("DocumentID");

                    b.HasIndex("PrintedByUserID");

                    b.ToTable("print_log","document");
                });

            modelBuilder.Entity("PDX.Domain.Finance.MAPayment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<int>("MAID")
                        .HasColumnName("ma_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<DateTime>("PaidDate")
                        .HasColumnName("paid_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("SubmoduleFeeID")
                        .HasColumnName("submodule_fee_id");

                    b.HasKey("ID");

                    b.HasIndex("SubmoduleFeeID");

                    b.ToTable("ma_payment","finance");
                });

            modelBuilder.Entity("PDX.Domain.Finance.SubmoduleFee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<decimal>("Fee")
                        .HasColumnName("fee");

                    b.Property<int>("FeeTypeID")
                        .HasColumnName("fee_type_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("SubmoduleID")
                        .HasColumnName("submodule_id");

                    b.HasKey("ID");

                    b.HasIndex("FeeTypeID");

                    b.HasIndex("SubmoduleID");

                    b.ToTable("submodule_fee","finance");
                });

            modelBuilder.Entity("PDX.Domain.License.ForeignApplication", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("CertificateNumber")
                        .HasColumnName("certificate_number");

                    b.Property<int>("CountryID")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("DecisionDate")
                        .HasColumnName("decision_date");

                    b.Property<int>("ForeignApplicationStatusID")
                        .HasColumnName("foreign_application_status_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<int>("MAID")
                        .HasColumnName("ma_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.HasIndex("ForeignApplicationStatusID");

                    b.HasIndex("MAID");

                    b.ToTable("foreign_application","license");
                });

            modelBuilder.Entity("PDX.Domain.License.MA", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AgentID")
                        .HasColumnName("agent_id");

                    b.Property<int>("CreatedByUserID")
                        .HasColumnName("created_by_user_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("ExipryDate")
                        .HasColumnName("expiry_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsSRA")
                        .HasColumnName("is_sra");

                    b.Property<string>("MANumber")
                        .IsRequired()
                        .HasColumnName("ma_number")
                        .HasMaxLength(100);

                    b.Property<int>("MAStatusID")
                        .HasColumnName("ma_status_id");

                    b.Property<int>("MATypeID")
                        .HasColumnName("ma_type_id");

                    b.Property<int>("ModifiedByUserID")
                        .HasColumnName("modified_by_user_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnName("registration_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("SupplierID")
                        .HasColumnName("supplier_id");

                    b.HasKey("ID");

                    b.HasIndex("AgentID");

                    b.HasIndex("CreatedByUserID");

                    b.HasIndex("MAStatusID");

                    b.HasIndex("MATypeID");

                    b.HasIndex("ModifiedByUserID");

                    b.HasIndex("SupplierID");

                    b.ToTable("ma","license");
                });

            modelBuilder.Entity("PDX.Domain.License.MAChecklist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Answer")
                        .HasColumnName("answer");

                    b.Property<int>("ChecklistID")
                        .HasColumnName("checklist_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<int>("MAID")
                        .HasColumnName("ma_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int?>("OptionID")
                        .HasColumnName("option_id");

                    b.Property<int>("ResponderID")
                        .HasColumnName("responder_id");

                    b.Property<int>("ResponderTypeID")
                        .HasColumnName("responder_type_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("ChecklistID");

                    b.HasIndex("MAID");

                    b.HasIndex("OptionID");

                    b.HasIndex("ResponderID");

                    b.HasIndex("ResponderTypeID");

                    b.ToTable("ma_checklist","license");
                });

            modelBuilder.Entity("PDX.Domain.License.MALogStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Comment")
                        .HasColumnName("comment")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<int?>("FromStatusID")
                        .HasColumnName("from_status_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsCurrent")
                        .HasColumnName("is_current");

                    b.Property<int>("MAID")
                        .HasColumnName("ma_id");

                    b.Property<int>("ModifiedByUserID")
                        .HasColumnName("modified_by_user_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("ToStatusID")
                        .HasColumnName("to_status_id");

                    b.HasKey("ID");

                    b.HasIndex("FromStatusID");

                    b.HasIndex("MAID");

                    b.HasIndex("ModifiedByUserID");

                    b.HasIndex("ToStatusID");

                    b.ToTable("ma_log_status","license");
                });

            modelBuilder.Entity("PDX.Domain.Logging.StatusLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("ColumnName")
                        .HasColumnName("column_name");

                    b.Property<string>("ColumnType")
                        .HasColumnName("column_type");

                    b.Property<string>("Comment")
                        .HasColumnName("comment");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<int>("EntityID")
                        .HasColumnName("entity_id");

                    b.Property<string>("EntityType")
                        .HasColumnName("entity_type");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<int>("ModifiedByUserID")
                        .HasColumnName("modified_by_user_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("NewValue")
                        .HasColumnName("new_value");

                    b.Property<string>("OldValue")
                        .HasColumnName("old_value");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("ModifiedByUserID");

                    b.ToTable("status_log","logging");
                });

            modelBuilder.Entity("PDX.Domain.Procurement.ImportPermit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AgentID")
                        .HasColumnName("agent_id");

                    b.Property<decimal>("Amount")
                        .HasColumnName("amount");

                    b.Property<DateTime?>("AssignedDate")
                        .HasColumnName("assigned_date");

                    b.Property<int?>("AssignedUserID")
                        .HasColumnName("assigned_user_id");

                    b.Property<int>("CreatedByUserID")
                        .HasColumnName("created_by_user_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<int>("CurrencyID")
                        .HasColumnName("currency_id");

                    b.Property<string>("Delivery")
                        .IsRequired()
                        .HasColumnName("delivery")
                        .HasMaxLength(100);

                    b.Property<decimal?>("Discount")
                        .HasColumnName("discount");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnName("expiry_date");

                    b.Property<decimal>("FreightCost")
                        .HasColumnName("freight_cost");

                    b.Property<string>("ImportPermitNumber")
                        .IsRequired()
                        .HasColumnName("import_permit_number")
                        .HasMaxLength(100);

                    b.Property<int>("ImportPermitStatusID")
                        .HasColumnName("import_permit_status_id");

                    b.Property<int>("ImportPermitTypeID")
                        .HasColumnName("import_permit_type_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("PaymentModeID")
                        .HasColumnName("payment_mode_id");

                    b.Property<int?>("PaymentTermID")
                        .HasColumnName("payment_term_id");

                    b.Property<string>("PerformaInvoiceNumber")
                        .HasColumnName("performa_invoice_number");

                    b.Property<int>("PortOfEntryID")
                        .HasColumnName("port_of_entry_id");

                    b.Property<string>("Remark")
                        .HasColumnName("remark");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("ShippingMethodID")
                        .HasColumnName("shipping_method_id");

                    b.Property<int>("SupplierID")
                        .HasColumnName("supplier_id");

                    b.HasKey("ID");

                    b.HasIndex("AgentID");

                    b.HasIndex("AssignedUserID");

                    b.HasIndex("CreatedByUserID");

                    b.HasIndex("CurrencyID");

                    b.HasIndex("ImportPermitStatusID");

                    b.HasIndex("ImportPermitTypeID");

                    b.HasIndex("PaymentModeID");

                    b.HasIndex("PaymentTermID");

                    b.HasIndex("PortOfEntryID");

                    b.HasIndex("ShippingMethodID");

                    b.HasIndex("SupplierID");

                    b.ToTable("import_permit","procurement");
                });

            modelBuilder.Entity("PDX.Domain.Procurement.ImportPermitDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<decimal?>("Discount")
                        .HasColumnName("discount");

                    b.Property<int>("ImportPermitID")
                        .HasColumnName("import_permit_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<int>("ManufacturerID")
                        .HasColumnName("manufacturer_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ProductID")
                        .HasColumnName("product_id");

                    b.Property<decimal>("Quantity")
                        .HasColumnName("quantity");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnName("unit_price");

                    b.HasKey("ID");

                    b.HasIndex("ImportPermitID");

                    b.HasIndex("ManufacturerID");

                    b.HasIndex("ProductID");

                    b.ToTable("import_permit_detail","procurement");
                });

            modelBuilder.Entity("PDX.Domain.Procurement.ImportPermitLogStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Comment")
                        .HasColumnName("comment")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<int?>("FromStatusID")
                        .HasColumnName("from_status_id");

                    b.Property<int>("ImportPermitID")
                        .HasColumnName("import_permit_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsCurrent")
                        .HasColumnName("is_current");

                    b.Property<int>("ModifiedByUserID")
                        .HasColumnName("modified_by_user_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("ToStatusID")
                        .HasColumnName("to_status_id");

                    b.HasKey("ID");

                    b.HasIndex("FromStatusID");

                    b.HasIndex("ImportPermitID");

                    b.HasIndex("ModifiedByUserID");

                    b.HasIndex("ToStatusID");

                    b.ToTable("import_permit_log_status","procurement");
                });

            modelBuilder.Entity("PDX.Domain.Public.ChangeLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnName("release_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnName("version")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("change_log","public");
                });

            modelBuilder.Entity("PDX.Domain.Public.WIP", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("type")
                        .HasMaxLength(100);

                    b.Property<int>("UserID")
                        .HasColumnName("user_id");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("wip","public");
                });

            modelBuilder.Entity("PDX.Domain.Report.Report", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<string>("FilterColumns")
                        .HasColumnName("filter_columns");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<int?>("MaxRows")
                        .HasColumnName("max_rows");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(500);

                    b.Property<int?>("Priority")
                        .HasColumnName("priority");

                    b.Property<string>("Query")
                        .IsRequired()
                        .HasColumnName("query");

                    b.Property<int>("ReportTypeID")
                        .HasColumnName("report_type_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("SeriesColumns")
                        .HasColumnName("series_columns");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(500);

                    b.Property<int?>("Width")
                        .HasColumnName("width");

                    b.HasKey("ID");

                    b.HasIndex("ReportTypeID");

                    b.ToTable("report","report");
                });

            modelBuilder.Entity("PDX.Domain.Views.vwAgent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AddressID")
                        .HasColumnName("address_id");

                    b.Property<int>("AgentTypeID")
                        .HasColumnName("agent_type_id");

                    b.Property<string>("AgentTypeName")
                        .HasColumnName("agent_type_name");

                    b.Property<string>("ContactPerson")
                        .HasColumnName("contact_person");

                    b.Property<string>("CountryName")
                        .HasColumnName("country_name");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("Fax")
                        .HasColumnName("fax");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsApproved")
                        .HasColumnName("is_approved");

                    b.Property<string>("LicenseNumber")
                        .HasColumnName("license_number");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnName("phone");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Website")
                        .HasColumnName("website");

                    b.HasKey("ID");

                    b.ToTable("vwagent","customer");
                });

            modelBuilder.Entity("PDX.Domain.Views.vwImportPermit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AgentID")
                        .HasColumnName("agent_id");

                    b.Property<string>("AgentName")
                        .HasColumnName("agent_name");

                    b.Property<decimal>("Amount")
                        .HasColumnName("amount");

                    b.Property<DateTime?>("AssignedDate")
                        .HasColumnName("assigned_date");

                    b.Property<string>("AssignedUser")
                        .HasColumnName("assigned_user");

                    b.Property<int?>("AssignedUserID")
                        .HasColumnName("assigned_user_id");

                    b.Property<int>("CreatedByUserID")
                        .HasColumnName("created_by_user_id");

                    b.Property<string>("CreatedByUsername")
                        .HasColumnName("created_by_username");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Currency")
                        .HasColumnName("currency");

                    b.Property<int>("CurrencyID")
                        .HasColumnName("currency_id");

                    b.Property<string>("CurrencySH")
                        .HasColumnName("currency_sh");

                    b.Property<string>("CurrencySymbol")
                        .HasColumnName("currency_symbol");

                    b.Property<DateTime?>("DecisionDate")
                        .HasColumnName("decision_date");

                    b.Property<string>("Delivery")
                        .HasColumnName("delivery");

                    b.Property<decimal?>("Discount")
                        .HasColumnName("discount");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnName("expiry_date");

                    b.Property<decimal>("FreightCost")
                        .HasColumnName("freight_cost");

                    b.Property<string>("ImportPermitNumber")
                        .HasColumnName("import_permit_number");

                    b.Property<string>("ImportPermitStatus")
                        .HasColumnName("import_permit_status");

                    b.Property<string>("ImportPermitStatusCode")
                        .HasColumnName("import_permit_status_code");

                    b.Property<string>("ImportPermitStatusDisplayName")
                        .HasColumnName("import_permit_status_display_name");

                    b.Property<int>("ImportPermitStatusID")
                        .HasColumnName("import_permit_status_id");

                    b.Property<int?>("ImportPermitStatusPriority")
                        .HasColumnName("import_permit_status_priority");

                    b.Property<string>("ImportPermitStatusSH")
                        .HasColumnName("import_permit_status_sh");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("PaymentMode")
                        .HasColumnName("payment_mode");

                    b.Property<int>("PaymentModeID")
                        .HasColumnName("payment_mode_id");

                    b.Property<string>("PaymentModeSH")
                        .HasColumnName("payment_mode_sh");

                    b.Property<string>("PerformaInvoiceNumber")
                        .HasColumnName("performa_invoice_number");

                    b.Property<string>("PortOfEntry")
                        .HasColumnName("port_of_entry");

                    b.Property<int>("PortOfEntryID")
                        .HasColumnName("port_of_entry_id");

                    b.Property<string>("PortOfEntrySH")
                        .HasColumnName("port_of_entry_sh");

                    b.Property<string>("Remark")
                        .HasColumnName("remark");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShippingMethod")
                        .HasColumnName("shipping_method");

                    b.Property<int>("ShippingMethodID")
                        .HasColumnName("shipping_method_id");

                    b.Property<string>("ShippingMethodSH")
                        .HasColumnName("shipping_method_sh");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnName("submission_date");

                    b.Property<string>("SupplierName")
                        .HasColumnName("supplier_name");

                    b.HasKey("ID");

                    b.ToTable("vwimport_permit","procurement");
                });

            modelBuilder.Entity("PDX.Domain.Views.vwPIP", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AgentID")
                        .HasColumnName("agent_id");

                    b.Property<string>("AgentName")
                        .HasColumnName("agent_name");

                    b.Property<decimal>("Amount")
                        .HasColumnName("amount");

                    b.Property<DateTime?>("AssignedDate")
                        .HasColumnName("assigned_date");

                    b.Property<string>("AssignedUser")
                        .HasColumnName("assigned_user");

                    b.Property<int?>("AssignedUserID")
                        .HasColumnName("assigned_user_id");

                    b.Property<int>("CreatedByUserID")
                        .HasColumnName("created_by_user_id");

                    b.Property<string>("CreatedByUsername")
                        .HasColumnName("created_by_username");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Currency")
                        .HasColumnName("currency");

                    b.Property<int>("CurrencyID")
                        .HasColumnName("currency_id");

                    b.Property<string>("CurrencySH")
                        .HasColumnName("currency_sh");

                    b.Property<string>("CurrencySymbol")
                        .HasColumnName("currency_symbol");

                    b.Property<DateTime?>("DecisionDate")
                        .HasColumnName("decision_date");

                    b.Property<string>("Delivery")
                        .HasColumnName("delivery");

                    b.Property<decimal?>("Discount")
                        .HasColumnName("discount");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnName("expiry_date");

                    b.Property<decimal>("FreightCost")
                        .HasColumnName("freight_cost");

                    b.Property<string>("ImportPermitNumber")
                        .HasColumnName("import_permit_number");

                    b.Property<string>("ImportPermitStatus")
                        .HasColumnName("import_permit_status");

                    b.Property<string>("ImportPermitStatusCode")
                        .HasColumnName("import_permit_status_code");

                    b.Property<string>("ImportPermitStatusDisplayName")
                        .HasColumnName("import_permit_status_display_name");

                    b.Property<int>("ImportPermitStatusID")
                        .HasColumnName("import_permit_status_id");

                    b.Property<int?>("ImportPermitStatusPriority")
                        .HasColumnName("import_permit_status_priority");

                    b.Property<string>("ImportPermitStatusSH")
                        .HasColumnName("import_permit_status_sh");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("PaymentMode")
                        .HasColumnName("payment_mode");

                    b.Property<int>("PaymentModeID")
                        .HasColumnName("payment_mode_id");

                    b.Property<string>("PaymentModeSH")
                        .HasColumnName("payment_mode_sh");

                    b.Property<string>("PerformaInvoiceNumber")
                        .HasColumnName("performa_invoice_number");

                    b.Property<string>("PortOfEntry")
                        .HasColumnName("port_of_entry");

                    b.Property<int>("PortOfEntryID")
                        .HasColumnName("port_of_entry_id");

                    b.Property<string>("PortOfEntrySH")
                        .HasColumnName("port_of_entry_sh");

                    b.Property<string>("Remark")
                        .HasColumnName("remark");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShippingMethod")
                        .HasColumnName("shipping_method");

                    b.Property<int>("ShippingMethodID")
                        .HasColumnName("shipping_method_id");

                    b.Property<string>("ShippingMethodSH")
                        .HasColumnName("shipping_method_sh");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnName("submission_date");

                    b.Property<string>("SupplierName")
                        .HasColumnName("supplier_name");

                    b.HasKey("ID");

                    b.ToTable("vwpip","procurement");
                });

            modelBuilder.Entity("PDX.Domain.Views.vwProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AgentName")
                        .HasColumnName("agent_name");

                    b.Property<string>("BrandName")
                        .HasColumnName("brand_name");

                    b.Property<string>("CommodityTypeName")
                        .HasColumnName("commodity_type_name");

                    b.Property<string>("CountryName")
                        .HasColumnName("country_name");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnName("expiry_date");

                    b.Property<string>("FullItemName")
                        .HasColumnName("full_item_name");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<string>("ManufacturerName")
                        .HasColumnName("manufacturer_name");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("ProductID")
                        .HasColumnName("product_id");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnName("registration_date");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShelfLife")
                        .HasColumnName("shelf_life");

                    b.Property<int>("SupplierID")
                        .HasColumnName("supplier_id");

                    b.Property<string>("SupplierName")
                        .HasColumnName("supplier_name");

                    b.HasKey("ID");

                    b.ToTable("vwproduct","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Views.vwSupplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("AddressID")
                        .HasColumnName("address_id");

                    b.Property<int?>("AgentCount")
                        .HasColumnName("agent_count");

                    b.Property<string>("CountryName")
                        .HasColumnName("country_name");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("Fax")
                        .HasColumnName("fax");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnName("phone");

                    b.Property<int?>("ProductCount")
                        .HasColumnName("product_count");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Website")
                        .HasColumnName("website");

                    b.HasKey("ID");

                    b.ToTable("vwsupplier","customer");
                });

            modelBuilder.Entity("PDX.Domain.Views.vwUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AgentName")
                        .HasColumnName("agent_name");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnName("last_login");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Password")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasColumnName("phone");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("UserTypeCode")
                        .HasColumnName("user_type_code");

                    b.Property<int>("UserTypeID")
                        .HasColumnName("user_type_id");

                    b.Property<string>("UserTypeName")
                        .HasColumnName("user_type_name");

                    b.Property<string>("Username")
                        .HasColumnName("user_name");

                    b.HasKey("ID");

                    b.ToTable("vwuser","account");
                });

            modelBuilder.Entity("PDX.Domain.Account.Menu", b =>
                {
                    b.HasOne("PDX.Domain.Account.Menu", "ParentMenu")
                        .WithMany()
                        .HasForeignKey("ParentMenuID");
                });

            modelBuilder.Entity("PDX.Domain.Account.MenuRole", b =>
                {
                    b.HasOne("PDX.Domain.Account.Menu", "Menu")
                        .WithMany("MenuRoles")
                        .HasForeignKey("MenuID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.Role", "Role")
                        .WithMany("MenuRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Account.ReportRole", b =>
                {
                    b.HasOne("PDX.Domain.Report.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.Role", "Role")
                        .WithMany("ReportRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Account.RolePermission", b =>
                {
                    b.HasOne("PDX.Domain.Account.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Account.User", b =>
                {
                    b.HasOne("PDX.Domain.Account.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID");

                    b.HasOne("PDX.Domain.Common.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Account.UserAgent", b =>
                {
                    b.HasOne("PDX.Domain.Customer.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "User")
                        .WithMany("UserAgents")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Account.UserRole", b =>
                {
                    b.HasOne("PDX.Domain.Account.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Catalog.Checklist", b =>
                {
                    b.HasOne("PDX.Domain.Common.AnswerType", "AnswerType")
                        .WithMany()
                        .HasForeignKey("AnswerTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.ChecklistType", "ChecklistType")
                        .WithMany()
                        .HasForeignKey("ChecklistTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Catalog.Option", b =>
                {
                    b.HasOne("PDX.Domain.Common.OptionGroup", "OptionGroup")
                        .WithMany()
                        .HasForeignKey("OptionGroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Catalog.SubmoduleChecklist", b =>
                {
                    b.HasOne("PDX.Domain.Catalog.Checklist", "Checklist")
                        .WithMany()
                        .HasForeignKey("ChecklistID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.Submodule", "Submodule")
                        .WithMany()
                        .HasForeignKey("SubmoduleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Commodity.AgentProduct", b =>
                {
                    b.HasOne("PDX.Domain.Customer.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Commodity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Commodity.Product", b =>
                {
                    b.HasOne("PDX.Domain.Commodity.AdminRoute", "AdminRoute")
                        .WithMany()
                        .HasForeignKey("AdminRouteID");

                    b.HasOne("PDX.Domain.Commodity.AgeGroup", "AgeGroup")
                        .WithMany()
                        .HasForeignKey("AgeGroupID");

                    b.HasOne("PDX.Domain.Common.CommodityType", "CommodityType")
                        .WithMany()
                        .HasForeignKey("CommodityTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Commodity.ContainerType", "ContainerType")
                        .WithMany()
                        .HasForeignKey("ContainerTypeID");

                    b.HasOne("PDX.Domain.Account.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserID");

                    b.HasOne("PDX.Domain.Commodity.DosageForm", "DosageFormObj")
                        .WithMany()
                        .HasForeignKey("DosageFormID");

                    b.HasOne("PDX.Domain.Commodity.DosageUnit", "DosageUnitObj")
                        .WithMany()
                        .HasForeignKey("DosageUnitID");

                    b.HasOne("PDX.Domain.Commodity.INN", "INN")
                        .WithMany()
                        .HasForeignKey("INNID");

                    b.HasOne("PDX.Domain.Account.User", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserID");

                    b.HasOne("PDX.Domain.Commodity.PharmacologicalClassification", "PharmacologicalClassification")
                        .WithMany()
                        .HasForeignKey("PharmacologicalClassificationID");

                    b.HasOne("PDX.Domain.Commodity.PharmacopoeiaStandard", "PharmacopoeiaStandard")
                        .WithMany()
                        .HasForeignKey("PharmacopoeiaStandardID");

                    b.HasOne("PDX.Domain.Commodity.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryID");

                    b.HasOne("PDX.Domain.Commodity.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeID");

                    b.HasOne("PDX.Domain.Commodity.ShelfLife", "ShelfLifeObj")
                        .WithMany()
                        .HasForeignKey("ShelfLifeID");
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ProductATC", b =>
                {
                    b.HasOne("PDX.Domain.Commodity.ATC", "ATC")
                        .WithMany()
                        .HasForeignKey("ATCID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Commodity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ProductComposition", b =>
                {
                    b.HasOne("PDX.Domain.Commodity.DosageUnit", "DosageUnit")
                        .WithMany()
                        .HasForeignKey("DosageUnitID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Commodity.Excipient", "Excipient")
                        .WithMany()
                        .HasForeignKey("ExcipientID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Commodity.INN", "INN")
                        .WithMany()
                        .HasForeignKey("INNID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Commodity.PharmacopoeiaStandard", "PharmacopoeiaStandard")
                        .WithMany()
                        .HasForeignKey("PharmacopoeiaStandardID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Commodity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Commodity.ProductManufacturer", b =>
                {
                    b.HasOne("PDX.Domain.Customer.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.ManufacturerType", "ManufacturerType")
                        .WithMany()
                        .HasForeignKey("ManufacturerTypeID");

                    b.HasOne("PDX.Domain.Commodity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Commodity.SRA", b =>
                {
                    b.HasOne("PDX.Domain.Common.SRAType", "SRAType")
                        .WithMany()
                        .HasForeignKey("SRATypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Commodity.SupplierProduct", b =>
                {
                    b.HasOne("PDX.Domain.Commodity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Customer.Supplier", "Agent")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Common.Address", b =>
                {
                    b.HasOne("PDX.Domain.Common.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Common.FeeType", b =>
                {
                    b.HasOne("PDX.Domain.Common.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Common.PortOfEntry", b =>
                {
                    b.HasOne("PDX.Domain.Common.ShippingMethod", "ShippingMethod")
                        .WithMany()
                        .HasForeignKey("ShippingMethodID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Common.Submodule", b =>
                {
                    b.HasOne("PDX.Domain.Common.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Customer.Agent", b =>
                {
                    b.HasOne("PDX.Domain.Common.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.AgentType", "AgentType")
                        .WithMany()
                        .HasForeignKey("AgentTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Customer.AgentSupplier", b =>
                {
                    b.HasOne("PDX.Domain.Customer.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.AgentLevel", "AgentLevel")
                        .WithMany()
                        .HasForeignKey("AgentLevelID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Customer.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Customer.Manufacturer", b =>
                {
                    b.HasOne("PDX.Domain.Common.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID");

                    b.HasOne("PDX.Domain.Common.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Customer.Supplier", b =>
                {
                    b.HasOne("PDX.Domain.Common.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Document.Document", b =>
                {
                    b.HasOne("PDX.Domain.Account.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Document.ModuleDocument", "ModuleDocument")
                        .WithMany()
                        .HasForeignKey("ModuleDocumentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Document.Letter", b =>
                {
                    b.HasOne("PDX.Domain.Document.ModuleDocument", "ModuleDocument")
                        .WithMany()
                        .HasForeignKey("ModuleDocumentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Document.ModuleDocument", b =>
                {
                    b.HasOne("PDX.Domain.Common.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.Submodule", "Submodule")
                        .WithMany("ModuleDocuments")
                        .HasForeignKey("SubmoduleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Document.PrintLog", b =>
                {
                    b.HasOne("PDX.Domain.Document.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "PrintedByUser")
                        .WithMany()
                        .HasForeignKey("PrintedByUserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Finance.MAPayment", b =>
                {
                    b.HasOne("PDX.Domain.Finance.SubmoduleFee", "SubmoduleFee")
                        .WithMany()
                        .HasForeignKey("SubmoduleFeeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Finance.SubmoduleFee", b =>
                {
                    b.HasOne("PDX.Domain.Common.FeeType", "FeeType")
                        .WithMany()
                        .HasForeignKey("FeeTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.Submodule", "Submodule")
                        .WithMany()
                        .HasForeignKey("SubmoduleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.License.ForeignApplication", b =>
                {
                    b.HasOne("PDX.Domain.Common.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.ForeignApplicationStatus", "ForeignApplicationStatus")
                        .WithMany()
                        .HasForeignKey("ForeignApplicationStatusID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.License.MA", "MA")
                        .WithMany()
                        .HasForeignKey("MAID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.License.MA", b =>
                {
                    b.HasOne("PDX.Domain.Customer.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.MAStatus", "MAStatus")
                        .WithMany()
                        .HasForeignKey("MAStatusID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.MAType", "MAType")
                        .WithMany()
                        .HasForeignKey("MATypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Customer.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.License.MAChecklist", b =>
                {
                    b.HasOne("PDX.Domain.Catalog.Checklist", "Checklist")
                        .WithMany()
                        .HasForeignKey("ChecklistID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.License.MA", "MA")
                        .WithMany()
                        .HasForeignKey("MAID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Catalog.Option", "Option")
                        .WithMany()
                        .HasForeignKey("OptionID");

                    b.HasOne("PDX.Domain.Account.User", "Responder")
                        .WithMany()
                        .HasForeignKey("ResponderID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.ResponderType", "ResponderType")
                        .WithMany()
                        .HasForeignKey("ResponderTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.License.MALogStatus", b =>
                {
                    b.HasOne("PDX.Domain.Common.MAStatus", "FromMAStatus")
                        .WithMany()
                        .HasForeignKey("FromStatusID");

                    b.HasOne("PDX.Domain.License.MA", "MA")
                        .WithMany()
                        .HasForeignKey("MAID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.MAStatus", "ToMAStatus")
                        .WithMany()
                        .HasForeignKey("ToStatusID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Logging.StatusLog", b =>
                {
                    b.HasOne("PDX.Domain.Account.User", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Procurement.ImportPermit", b =>
                {
                    b.HasOne("PDX.Domain.Customer.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "AssignedUser")
                        .WithMany()
                        .HasForeignKey("AssignedUserID");

                    b.HasOne("PDX.Domain.Account.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatedByUserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.ImportPermitStatus", "ImportPermitStatus")
                        .WithMany()
                        .HasForeignKey("ImportPermitStatusID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.ImportPermitType", "ImportPermitType")
                        .WithMany()
                        .HasForeignKey("ImportPermitTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.PaymentMode", "PaymentMode")
                        .WithMany()
                        .HasForeignKey("PaymentModeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.PaymentTerm", "PaymentTerm")
                        .WithMany()
                        .HasForeignKey("PaymentTermID");

                    b.HasOne("PDX.Domain.Common.PortOfEntry", "PortOfEntry")
                        .WithMany()
                        .HasForeignKey("PortOfEntryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.ShippingMethod", "ShippingMethod")
                        .WithMany()
                        .HasForeignKey("ShippingMethodID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Customer.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Procurement.ImportPermitDetail", b =>
                {
                    b.HasOne("PDX.Domain.Procurement.ImportPermit", "ImportPermit")
                        .WithMany("ImportPermitDetails")
                        .HasForeignKey("ImportPermitID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Customer.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Commodity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Procurement.ImportPermitLogStatus", b =>
                {
                    b.HasOne("PDX.Domain.Common.ImportPermitStatus", "FromImportPermitStatus")
                        .WithMany()
                        .HasForeignKey("FromStatusID");

                    b.HasOne("PDX.Domain.Procurement.ImportPermit", "ImportPermit")
                        .WithMany()
                        .HasForeignKey("ImportPermitID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.ImportPermitStatus", "ToImportPermitStatus")
                        .WithMany()
                        .HasForeignKey("ToStatusID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Public.WIP", b =>
                {
                    b.HasOne("PDX.Domain.Account.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Report.Report", b =>
                {
                    b.HasOne("PDX.Domain.Common.ReportType", "ReportType")
                        .WithMany()
                        .HasForeignKey("ReportTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
