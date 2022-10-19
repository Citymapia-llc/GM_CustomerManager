using System;
using System.ComponentModel.DataAnnotations;

namespace GM.Store.Shared.Models
{
    public class AddressModel
    {

        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Zip { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int ApplicationId { get; set; }
    }
    public class AddOrUpdateAddressModel
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        public string Area { get; set; }
        [Required]
        public string Zip { get; set; }
    }
    public class DeleteRequestModel
    {
        public int Id { get; set; }
    }
}
