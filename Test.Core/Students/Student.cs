using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.StudyHouses;

namespace Test.Core.Students
{
    public class Student
    {
        public int Id { get; set; }
        public int StudyHouseId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Identification { get; set; }
        [Required]
        public int Age { get; set; }


    }
}
