using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST_API.Models
{
    public class Users
    {
        /// <summary>
        /// gets or sets Id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// gets or sets FirstName.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// gets or sets LastName.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// gets or sets UidSerie.
        /// </summary>
        public string UidSerie { get; set; }
        /// <summary>
        /// gets or sets Email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// gets or sets BirthDate.
        /// </summary>
        public DateTimeOffset BirthDate { get; set; }
        /// <summary>
        /// gets or sets Gender.
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// gets or sets userName.
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// gets or sets Password.
        /// </summary>
        public string Password { get; set; }
    }
}