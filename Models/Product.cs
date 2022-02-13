namespace Pasticceria
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        private Model2 db = new Model2();
        [Key]
        public int idProduct { get; set; }

        [StringLength(50)]
        [DisplayName("Nome Prodotto")]
        public string ProdName { get; set; }

        [StringLength(200)]
        [DisplayName("Ingredienti")]
        public string Ingredients { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}",ApplyFormatInEditMode =true,ConvertEmptyStringToNull =false)]
        [DisplayName("Data Produzione")]
        public DateTime? ProdDate { get; set; }

        [StringLength(50)]
        [DisplayName("Dettagli")]
        public string Details { get; set; }

        [StringLength(50)]
        [DisplayName("Foto")]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Prezzo/Pz")]
        [DataType(DataType.Currency)]
        
        public decimal? Price{ get; set; }

        [DisplayName("Pezzi Disponibili")]
        [DefaultValue("1")]
        public int? NPieces { get; set; }
    }
}
