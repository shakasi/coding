﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

using OpenAuth.Domain;

namespace OpenAuth.Repository.Models.Mapping
{
    public partial class ModuleMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            // table
            ToTable("Module", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .IsRequired();
            Property(t => t.CascadeId)
                .HasColumnName("CascadeId")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Url)
                .HasColumnName("Url")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.HotKey)
                .HasColumnName("HotKey")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.ParentId)
                .HasColumnName("ParentId");
            Property(t => t.IsLeaf)
                .HasColumnName("IsLeaf")
                .IsRequired();
            Property(t => t.IsAutoExpand)
                .HasColumnName("IsAutoExpand")
                .IsRequired();
            Property(t => t.IconName)
                .HasColumnName("IconName")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Status)
                .HasColumnName("Status")
                .IsRequired();
            Property(t => t.ParentName)
                .HasColumnName("ParentName")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.Vector)
                .HasColumnName("Vector")
                .HasMaxLength(255)
                .IsRequired();
            Property(t => t.SortNo)
                .HasColumnName("SortNo")
                .IsRequired();

            // Relationships
        }
    }
}
