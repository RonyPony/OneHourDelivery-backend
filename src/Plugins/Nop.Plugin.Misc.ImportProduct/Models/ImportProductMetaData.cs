using Nop.Core.Domain.Catalog;
using Nop.Services.ExportImport;
using Nop.Services.ExportImport.Help;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.ImportProduct.Models
{
    /// <summary>
    /// ImportProductMetadata configuration class
    /// </summary>
    public class ImportProductMetadata
    {
        /// <summary>
        /// Here read the row at the end.
        /// </summary>
        public int EndRow { get; internal set; }
        /// <summary>
        /// Here import the property like the productTag,Image,Product,etc.
        /// </summary>
        public PropertyManager<Product> Manager { get; internal set; }
        /// <summary>
        /// Import the property by name like ProductType.
        /// </summary>
        public IList<PropertyByName<Product>> Properties { get; set; }
        /// <summary>
        /// Count of the product in the file.
        /// </summary>
        public int CountProductsInFile => ProductsInFile.Count;
        /// <summary>
        /// Import the Attribute of the product.
        /// </summary>
        public PropertyManager<ExportProductAttribute> ProductAttributeManager { get; internal set; }
        /// <summary>
        /// Import the Specification of Attribute.
        /// </summary>
        public PropertyManager<ExportSpecificationAttribute> SpecificationAttributeManager { get; internal set; }
        /// <summary>
        /// Identify the Cell where are the sku.
        /// </summary>
        public int SkuCellNum { get; internal set; }
        /// <summary>
        /// Get all the product that have sku and imported.
        /// </summary>
        public List<string> AllSku { get; set; }
        /// <summary>
        /// Identify the Products of file.
        /// </summary>
        public List<int> ProductsInFile { get; set; }
    }
}
