//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoDinar
{
    using System;
    using System.Collections.Generic;
    
    public partial class SkladFurnituri
    {
        public int Id { get; set; }
        public string IdFurnituri { get; set; }
        public string Kolichestvo { get; set; }
    
        public virtual Furnitura Furnitura { get; set; }
    }
}
