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
    
    public partial class SkladTkani
    {
        public int Id { get; set; }
        public string IdTkani { get; set; }
        public string Kolichestvo { get; set; }
    
        public virtual Tkani Tkani { get; set; }
    }
}