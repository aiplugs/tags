using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Models
{
    public class SampleViewModel
    {
        [Display(Name = "String No.1", Description="This is description of String No.1", Prompt = "Enter")]
        public string String1 { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(5)]
        [Display(Name = "String No.2", Description="This is description of String No.2")]
        public string String2 { get; set; } = "String 2";


        [Required]
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"^[a-z]+$", ErrorMessage = "You can use only the alphabet charcters (a-z) ")]
        [Display(Name = "String No.3", Description="This is description of String No.3")]
        public string String3 { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "String No.4", Description="This is description of String No.4", Prompt = "Textarea")]
        public string String4 { get; set; } = "String 4";

        [Display(Name = "Enum No.1", Description="This is description of Enum No.1")]
        public SampleEnum Enum1 { get; set; } = SampleEnum.Second;

        [Display(Name = "Enum No.2", Description="This is description of Enum No.2")]
        public IEnumerable<SampleEnum> Enum2 { get; set; } = new []{SampleEnum.Second};

        [Required]
        [Display(Name = "Enum No.3", Description="This is description of Enum No.3")]
        public IEnumerable<SampleEnum> Enum3 { get; set; } = new []{SampleEnum.Second};

        [Required]
        [Display(Name = "Dictionary No.1", Description="This is description of Dictionary No.3")]
        public IDictionary<string, string> Dictionary1 { get; set; } = new Dictionary<string,string> 
        {
            { "MSG", "Hello, World!" }
        };

        [Display(Name = "Boolean", Description="This is description of Boolean")]        
        public bool Boolean { get; set; } = true;
    }

    public enum SampleEnum
    {
        [Display(Name="1st")]
        First,

        [Display(Name="2nd")]
        Second,

        [Display(Name="3rd")]
        Third
    }
}