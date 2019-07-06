using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PDX.DAL;

namespace PDX.DAL.Migrations
{
    [DbContext(typeof(PDXContext))]
    [Migration("20170314104730_DocumentTypeMigration")]
    partial class DocumentTypeMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

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

                    b.ToTable("permission","account");
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
                        .HasAnnotation("MaxLength", 1000);

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

                    b.Property<int>("ModuleID")
                        .HasColumnName("module_id");

                    b.Property<int>("PermissionID")
                        .HasColumnName("permission_id");

                    b.Property<int>("RoleID")
                        .HasColumnName("role_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("ModuleID");

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
                        .HasColumnName("phone");

                    b.Property<int>("RoleID")
                        .HasColumnName("role_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("user_name");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

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

            modelBuilder.Entity("PDX.Domain.Commodity.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("CommodityTypeID")
                        .HasColumnName("commodity_type_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("DosageForm")
                        .HasColumnName("dosage_form");

                    b.Property<string>("DosageStrength")
                        .HasColumnName("dosage_strength");

                    b.Property<string>("DosageUnit")
                        .HasColumnName("dosage_unit");

                    b.Property<string>("GenericName")
                        .IsRequired()
                        .HasColumnName("generic_name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("ShelfLife")
                        .HasColumnName("shelf_life");

                    b.HasKey("ID");

                    b.HasIndex("CommodityTypeID");

                    b.ToTable("product","commodity");
                });

            modelBuilder.Entity("PDX.Domain.Common.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("CountryID")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("HouseNumber")
                        .HasColumnName("house_number")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<string>("Kebele")
                        .HasColumnName("kebele")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Line1")
                        .HasColumnName("line1")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("Line2")
                        .HasColumnName("line2")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("PlaceName")
                        .HasColumnName("place_name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Region")
                        .HasColumnName("region")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("StreetName")
                        .HasColumnName("street_name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("SubCity")
                        .HasColumnName("sub_city")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Woreda")
                        .HasColumnName("woreda")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("ZipCode")
                        .HasColumnName("zip_code")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("address","common");
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
                        .HasAnnotation("MaxLength", 1000);

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

                    b.ToTable("agent_type","common");
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
                        .HasAnnotation("MaxLength", 1000);

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
                        .HasAnnotation("MaxLength", 5);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 1000);

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

                    b.HasKey("ID");

                    b.ToTable("document_type","common");
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
                        .HasAnnotation("MaxLength", 1000);

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

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

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
                        .HasAnnotation("MaxLength", 1000);

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

                    b.HasKey("ID");

                    b.ToTable("import_permit_type","common");
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
                        .HasAnnotation("MaxLength", 1000);

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

                    b.HasKey("ID");

                    b.ToTable("module","common");
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
                        .HasAnnotation("MaxLength", 1000);

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

                    b.HasKey("ID");

                    b.ToTable("payment_mode","common");
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
                        .HasAnnotation("MaxLength", 1000);

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

                    b.HasKey("ID");

                    b.ToTable("port_of_entry","common");
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
                        .HasAnnotation("MaxLength", 1000);

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

                    b.HasKey("ID");

                    b.ToTable("shipping_method","common");
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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Fax")
                        .HasColumnName("fax")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<string>("LicenseNumber")
                        .HasColumnName("license_number")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Website")
                        .HasColumnName("website")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.HasIndex("AgentTypeID");

                    b.ToTable("agent","customer");
                });

            modelBuilder.Entity("PDX.Domain.Customer.Manufacturer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("CountryID")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

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
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Fax")
                        .HasColumnName("fax")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<string>("Website")
                        .HasColumnName("website")
                        .HasAnnotation("MaxLength", 100);

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

                    b.Property<int>("ModuleID")
                        .HasColumnName("module_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.HasKey("ID");

                    b.HasIndex("DocumentTypeID");

                    b.HasIndex("ModuleID");

                    b.ToTable("module_document","document");
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

                    b.Property<int>("CreatedByUserID")
                        .HasColumnName("created_by_user_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date");

                    b.Property<int>("CurrencyID")
                        .HasColumnName("currency_id");

                    b.Property<string>("Delivery")
                        .IsRequired()
                        .HasColumnName("delivery")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnName("expiry_date");

                    b.Property<bool>("FeeReceived")
                        .HasColumnName("fee_received");

                    b.Property<DateTime?>("FeeReceivedDate")
                        .HasColumnName("fee_received_date");

                    b.Property<decimal>("FreightCost")
                        .HasColumnName("freight_cost");

                    b.Property<string>("GDTID")
                        .HasColumnName("gdt_id");

                    b.Property<string>("ImportPermitNumber")
                        .IsRequired()
                        .HasColumnName("import_permit_number")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("ImportPermitStatusID")
                        .HasColumnName("import_permit_status_id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date");

                    b.Property<int>("PaymentModeID")
                        .HasColumnName("payment_mode_id");

                    b.Property<string>("PerformaInvoiceNumber")
                        .HasColumnName("performa_invoice_number");

                    b.Property<int>("PortOfEntryID")
                        .HasColumnName("port_of_entry_id");

                    b.Property<Guid>("RowGuid")
                        .HasColumnName("rowguid");

                    b.Property<int>("ShippingMethodID")
                        .HasColumnName("shipping_method_id");

                    b.HasKey("ID");

                    b.HasIndex("AgentID");

                    b.HasIndex("CreatedByUserID");

                    b.HasIndex("CurrencyID");

                    b.HasIndex("ImportPermitStatusID");

                    b.HasIndex("PaymentModeID");

                    b.HasIndex("PortOfEntryID");

                    b.HasIndex("ShippingMethodID");

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
                        .HasAnnotation("MaxLength", 1000);

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

            modelBuilder.Entity("PDX.Domain.Account.RolePermission", b =>
                {
                    b.HasOne("PDX.Domain.Common.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Account.User", b =>
                {
                    b.HasOne("PDX.Domain.Account.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Account.UserAgent", b =>
                {
                    b.HasOne("PDX.Domain.Customer.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Account.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Commodity.Product", b =>
                {
                    b.HasOne("PDX.Domain.Common.CommodityType", "CommodityType")
                        .WithMany()
                        .HasForeignKey("CommodityTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Common.Address", b =>
                {
                    b.HasOne("PDX.Domain.Common.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
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

            modelBuilder.Entity("PDX.Domain.Customer.Manufacturer", b =>
                {
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

            modelBuilder.Entity("PDX.Domain.Document.ModuleDocument", b =>
                {
                    b.HasOne("PDX.Domain.Common.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Procurement.ImportPermit", b =>
                {
                    b.HasOne("PDX.Domain.Customer.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentID")
                        .OnDelete(DeleteBehavior.Cascade);

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

                    b.HasOne("PDX.Domain.Common.PaymentMode", "PaymentMode")
                        .WithMany()
                        .HasForeignKey("PaymentModeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.PortOfEntry", "PortOfEntry")
                        .WithMany()
                        .HasForeignKey("PortOfEntryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PDX.Domain.Common.ShippingMethod", "ShippingMethod")
                        .WithMany()
                        .HasForeignKey("ShippingMethodID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PDX.Domain.Procurement.ImportPermitDetail", b =>
                {
                    b.HasOne("PDX.Domain.Procurement.ImportPermit", "ImportPermit")
                        .WithMany()
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
        }
    }
}
