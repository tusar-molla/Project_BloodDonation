using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_BloodDonation.Models
{
    public class Bloodgroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Member> Members { get; set; }
    }

    public class Member { 
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string RegistrationDate { get; set; } = DateTime.Now.ToShortDateString();
        [ValidateNever]
        public string UserName { get; set; } = "";
        [ValidateNever]
        public string Password { get; set; } = "";
        [ForeignKey("Bloodgroup"),DisplayName("Blood Group")]
        public int BloodgroupId { get; set; }
        [ValidateNever]
        public Bloodgroup Bloodgroup { get; set; }

        [ForeignKey("Area"),DisplayName("Area")]
        public int AreaId { get; set; }
        [ValidateNever]
        public Area Area { get; set; }

        [ForeignKey("Thana"), DisplayName("Thana")]
        public int ThanaId { get; set; }
        [ValidateNever]
        public Thana Thana { get; set; }
        [ForeignKey("District"), DisplayName("District")]
        public int DistrictId { get; set; }
        [ValidateNever]
        public District District { get; set; }

        [ForeignKey("Division"), DisplayName("Division")]
        public int DivisionId { get; set;}
        [ValidateNever]
        public Division Division { get; set;}

        [ForeignKey("Country"),DisplayName("Country")]
        public int CountryId { get; set;}
        [ValidateNever]
        public Country Country { get; set; }
    }

    public class Area { 
    
        public int Id { get; set; }
        public string Name { get; set; }
     
        [ForeignKey("Thana"), DisplayName("Thana")]
        public int ThanaId { get; set;}
        public Thana Thana { get; set; }
    }

    public class Thana {

        public int Id { get; set; }
        public string Name { get; set; }


        [ForeignKey("District"), DisplayName("District")]
        public int DistricId{ get; set; }
        public District District { get; set; }

        public ICollection<Area> Areas { get; set; }
    }

    public class District {
    
        public int Id { get; set; }
        public string Name { get; set; }
       

        [ForeignKey("Division"), DisplayName("Division")]
        public int DivisionId { get; set; }
        public Division Division { get; set;}

        public ICollection<Thana> Thanas { get; set; }

    }

    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
    
        [ForeignKey("Country"), DisplayName("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set;}

    }
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Division> Divisions { get; set; }
    }

    public class BloodDonationDtls { 
    
        public int Id { get; set; }

        [DisplayName("DonarID")]
        public int DonarId { get; set; }
        [DisplayName("RecieverID")]
        public int RecieverId { get; set; }
        [DisplayName("DonationDate")]
        public DateTime DonationDate { get; set; }
        [DisplayName("DonateQTY")]
        public string DonateQty { get; set;}
        [DisplayName("DonatePlace")]
        public string Donateplace { get; set; }
    }
}
