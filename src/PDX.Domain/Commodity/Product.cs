using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using PDX.Domain.Account;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Commodity {
    [Table ("product", Schema = "commodity")]
    public class Product : BaseEntity {
        public Product () {
            SupplierProducts = new List<SupplierProduct> ();
        }

        [Required]
        [Column ("name")]
        [MaxLength (500)]
        public string Name { get; set; }

        [Required]
        [Column ("generic_name")]
        [MaxLength (500)]
        public string GenericName { get; set; }

        [Column ("brand_name")]
        [MaxLength (500)]
        public string BrandName { get; set; }

        [Column ("description")]
        [MaxLength (1000)]
        public string Description { get; set; }

        [Column ("shelf_life")]
        public string ShelfLife { get; set; }

        [Column ("dosage_form")]
        public string DosageForm { get; set; }

        [Column ("dosage_unit")]
        public string DosageUnit { get; set; }

        [Column ("dosage_strength")]
        public string DosageStrength { get; set; }

        [MaxLength (500)]
        [Column ("posology")]
        public string Posology { get; set; }

        [MaxLength (500)]
        [Column ("ingredient_statement")]
        public string IngredientStatement { get; set; }

        [Column ("indication")]
        public string Indication { get; set; }

        [MaxLength (500)]
        [Column ("storage_condition")]
        public string StorageCondition { get; set; }

        [Column ("age_group_id")]
        public int? AgeGroupID { get; set; }

        [Column ("inn_id")]
        public int? INNID { get; set; }

        [Column ("container_type")]
        public string ContainerType { get; set; }

        [Column ("product_type_id")]
        public int? ProductTypeID { get; set; }

        [Column ("product_category_id")]
        public int? ProductCategoryID { get; set; }

        [Column ("pharmacopoei_standard_id")]
        public int? PharmacopoeiaStandardID { get; set; }

        [Column ("shelf_life_id")]
        public int? ShelfLifeID { get; set; }

        [Column ("admin_route_id")]
        public int? AdminRouteID { get; set; }

        [Column ("dosage_form_id")]
        public int? DosageFormID { get; set; }

        [Column ("dosage_strength_id")]
        public int? DosageStrengthID { get; set; }

        [Column ("dosage_unit_id")]
        public int? DosageUnitID { get; set; }

        [Column ("pharmacological_classification_id")]
        public int? PharmacologicalClassificationID { get; set; }

        [Column ("use_category_id")]
        public int? UseCategoryID { get; set; }

        [Column ("created_by_user_id")]
        public int? CreatedByUserID { get; set; }

        [Column ("modified_by_user_id")]
        public int? ModifiedByUserID { get; set; }

        [Column ("product_md_id")]
        public int? ProductMDID { get; set; }

        [Column ("type_of_technology")]
        public string TypeOfTechnology { get; set; }

        [Column ("food_ingredients")]
        public string FoodIngredients { get; set; }

        [NotMapped]
        public DateTime? RegistrationDate { get; set; }

        [NotMapped]
        public DateTime? ExpiryDate { get; set; }

        [NotMapped]
        public bool? IsExpired { get { return ExpiryDate == null ? (bool?) null : ExpiryDate < DateTime.UtcNow; } }

        [NotMapped]
        public string FullItemName { get { return GenericName + "-" + DosageStrength + "-" + DosageUnitObj?.Name + "-" + DosageFormObj?.Name; } }

        [NavigationProperty]
        [ForeignKey ("AgeGroupID")]
        public virtual AgeGroup AgeGroup { get; set; }

        [NavigationProperty]
        [ForeignKey ("INNID")]
        public virtual INN INN { get; set; }

        [NavigationProperty]
        [ForeignKey ("ProductTypeID")]
        public virtual ProductType ProductType { get; set; }

        [NavigationProperty]
        [ForeignKey ("ProductCategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }

        [NavigationProperty]
        [ForeignKey ("PharmacopoeiaStandardID")]
        public virtual PharmacopoeiaStandard PharmacopoeiaStandard { get; set; }

        [NavigationProperty]
        [ForeignKey ("ShelfLifeID")]
        public virtual ShelfLife ShelfLifeObj { get; set; }

        [NavigationProperty]
        [ForeignKey ("AdminRouteID")]
        public virtual AdminRoute AdminRoute { get; set; }

        [NavigationProperty]
        [ForeignKey ("DosageFormID")]
        public virtual DosageForm DosageFormObj { get; set; }

        [NavigationProperty]
        [ForeignKey ("DosageStrengthID")]
        public virtual DosageStrength DosageStrengthObj { get; set; }

        [NavigationProperty]
        [ForeignKey ("DosageUnitID")]
        public virtual DosageUnit DosageUnitObj { get; set; }

        [NavigationProperty]
        [ForeignKey ("PharmacologicalClassificationID")]
        public virtual PharmacologicalClassification PharmacologicalClassification { get; set; }

        [NavigationProperty]
        [ForeignKey ("UseCategoryID")]
        public virtual UseCategory UseCategory { get; set; }

        [ForeignKey ("CreatedByUserID")]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey ("ModifiedByUserID")]
        public virtual User ModifiedByUser { get; set; }
        
        [NavigationProperty]
        [ForeignKey ("ProductMDID")]
        public virtual ProductMD ProductMD { get; set; }

        [NavigationProperty]
        public virtual ICollection<ProductComposition> ProductCompositions { get; set; }

        [NavigationProperty]
        public virtual ICollection<ProductATC> ProductATCs { get; set; }

        [NavigationProperty]
        public virtual ICollection<ProductManufacturer> ProductManufacturers { get; set; }

        [NavigationProperty]
        public virtual ICollection<Presentation> Presentations { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        [NavigationProperty]
        public virtual ICollection<MDModelSize> MDModelSizes { get; set; }
        [NavigationProperty]
        public virtual ICollection<DeviceAccessories> DeviceAccessories { get; set; }

        [NavigationProperty]
        public virtual ICollection<FoodComposition> FoodCompositions { get; set; }

        #region Computed Properties
        [NotMapped]
        public string AgeGroupStr { get { return this.AgeGroup?.Name; } }

        [NotMapped]
        public string INNStr { get { return INN?.Name; } }

        [NotMapped]
        public string ProductTypeStr { get { return ProductType?.Name; } }

        [NotMapped]
        public string ProductCategoryStr { get { return ProductCategory?.Name; } }

        [NotMapped]
        public string PharmacopoeiaStandardStr { get { return PharmacopoeiaStandard?.Name; } }

        [NotMapped]
        public string ShelfLifeStr { get { return ShelfLifeObj?.Name; } }

        [NotMapped]
        public string AdminRouteStr { get { return this.AdminRoute?.Name; } }

        [NotMapped]
        public string DosageFormStr { get { return DosageFormObj?.Name; } }

        [NotMapped]
        public string DosageStrengthStr { get { return DosageStrengthObj?.Name; } }

        [NotMapped]
        public string DosageUnitName { get { return DosageUnitObj?.Name; } }

        [NotMapped]
        public string PharmacologicalClassificationStr { get { return PharmacologicalClassification?.Name; } }

        [NotMapped]
        public string UseCategoryStr { get { return UseCategory?.Name; } }

        [NotMapped]
        public string ProductManufacturersStr { get { return ProductManufacturers?.Aggregate<ProductManufacturer, string> (String.Empty, (i, j) => i + $"{j?.ManufacturerAddress?.Manufacturer?.Name} - {j?.ManufacturerType?.Name}"); } }

        [NotMapped]
        public string Presentation { get { return Presentations?.Aggregate<Presentation, string> (String.Empty, (i, j) => i + $"{j?.PackSize?.Name}"); } }

        [NotMapped]
        public string ProductStatus { get; set; }

        #endregion

    }
}