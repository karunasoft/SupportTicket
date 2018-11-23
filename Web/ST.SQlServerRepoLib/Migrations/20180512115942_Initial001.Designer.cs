﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace ST.SQLServerRepoLib.Migrations
{
    [DbContext(typeof(SupportTicketContext))]
    [Migration("20180512115942_Initial001")]
    partial class Initial001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview1-28290")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ST.SharedEntitiesLib.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ST.SharedEntitiesLib.Severity", b =>
                {
                    b.Property<int>("SeverityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("SeverityId");

                    b.ToTable("Severity");
                });

            modelBuilder.Entity("ST.SharedEntitiesLib.Tickets", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Problem")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<int>("ProductId");

                    b.Property<int>("SeverityId");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("TicketId");

                    b.HasIndex("ProductId")
                        .HasName("IX_ProductId");

                    b.HasIndex("SeverityId")
                        .HasName("IX_SeverityId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ST.SharedEntitiesLib.Tickets", b =>
                {
                    b.HasOne("ST.SharedEntitiesLib.Product", "Product")
                        .WithMany("Tickets")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_dbo.Ticket_dbo.Product_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ST.SharedEntitiesLib.Severity", "Severity")
                        .WithMany("Tickets")
                        .HasForeignKey("SeverityId")
                        .HasConstraintName("FK_dbo.Ticket_dbo.Severity_SeverityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
