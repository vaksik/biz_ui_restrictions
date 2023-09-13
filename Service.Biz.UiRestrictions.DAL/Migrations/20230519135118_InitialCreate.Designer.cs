﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Service.Biz.UiRestrictions.DAL;
using Service.Biz.UiRestrictions.DAL.Entities;

#nullable disable

namespace Service.Biz.UiRestrictions.DAL.Migrations
{
    [DbContext(typeof(BizUiRestrictionsDataContext))]
    [Migration("20230519135118_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "access_restriction_type", new[] { "tariff_not_enough" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "access_type", new[] { "disable", "hidden" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Service.Biz.UiRestrictions.DAL.Entities.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("feature", (string)null);
                });

            modelBuilder.Entity("Service.Biz.UiRestrictions.DAL.Entities.OrganizationProduct", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("organization_id");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<Guid?>("NetworkId")
                        .HasColumnType("uuid")
                        .HasColumnName("network_id");

                    b.HasKey("OrganizationId", "ProductId");

                    b.HasIndex("NetworkId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("organization_product", (string)null);
                });

            modelBuilder.Entity("Service.Biz.UiRestrictions.DAL.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("code");

                    b.Property<byte>("Level")
                        .HasColumnType("smallint")
                        .HasColumnName("level");

                    b.HasKey("Id");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("Service.Biz.UiRestrictions.DAL.Entities.ProductFeatureRestriction", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int>("FeatureId")
                        .HasColumnType("integer")
                        .HasColumnName("feature_id");

                    b.Property<AccessRestrictionType>("AccessRestrictionType")
                        .HasColumnType("access_restriction_type")
                        .HasColumnName("access_restriction_type");

                    b.Property<AccessType>("AccessType")
                        .HasColumnType("access_type")
                        .HasColumnName("access_type");

                    b.Property<string>("Detail")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("detail");

                    b.HasKey("ProductId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("product_feature_access", (string)null);
                });

            modelBuilder.Entity("Service.Biz.UiRestrictions.DAL.Entities.ProductFeatureRestriction", b =>
                {
                    b.HasOne("Service.Biz.UiRestrictions.DAL.Entities.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Biz.UiRestrictions.DAL.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
