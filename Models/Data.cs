using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TVM.Models
{
    
    
        [Table("users")]
        public class Data
        {
            [Key]
            [Column("user_id")]
            public int Id { get; set; }

        [Column("email")]
        public string? Email { get; set; }



        [Column("password")]
        public string? Password { get; set; }

            [Column("firstname")]
            public string? Firstname { get; set; }

            [Column("lastname")]
            public string? Lastname { get; set; }

            [Column("maritalstatus")]
            public string? Marital_status { get; set; }

           

            [Column("pancardno")]
            public string? Pancard_no { get; set; }

            [Column("temp_address")]
            public string? Temp_address { get; set; }

            [Column("aadhar_no")]
            public string? Aadhar_no { get; set; }

            [Column("perm_address")]
            public string? Pnt_address { get; set; }

            [Column("ph_no")]
            public string? Phone_no { get; set; }

            [Column("gender")]
            public string? Gender { get; set; }

            [Column("blood_grp")]
            public string? Blood_group { get; set; }
        }
    
}
