using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.User
{
    public class User : BaseEntity
    {
        #region Properties
        public User()
        {
            IsActive = true;
        }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        [StringLength(500)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset LoginDate { get; set; }


        #endregion

        #region Relation

       public ICollection<Post.Post> Posts { get; set; } 

        #endregion

    }
    public enum GenderType
    {
        [Display(Name = "مرد")] Male = 1,
        [Display(Name = "زن")] Female = 2
    }
}
