//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Task2.DB
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial class ItemParams
    {
        public int Id { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
    
        public virtual Item Item { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"ID: {Id}");
            sb.AppendLine($"Height: {Height}");
            sb.AppendLine($"Width: {Width}");
            sb.AppendLine($"Depth: {Depth}");
            sb.AppendLine($"Weight: {Weight}");

            return sb.ToString();
        }
    }
}
