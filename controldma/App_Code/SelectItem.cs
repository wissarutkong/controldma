using System;
using System.ComponentModel.DataAnnotations;

namespace controldma.service
{
    internal class SelectItem
    {
        [Display(Name = "Check")]
        public Boolean Check { get; set; }
        [Display(Name = "Value")]
        public string Value { get; set; }
        [Display(Name = "Label")]
        public string Label { get; set; }

        public SelectItem()
        {

        }
        public SelectItem(Boolean check, string value, string label)
        {
            this.Check = check;
            this.Value = value;
            this.Label = label;
        }
    }
}