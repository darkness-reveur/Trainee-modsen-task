﻿// <auto-generated />
using System;
using MeetupPlatformApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeetupPlatformApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220412142923_AddKeyword")]
    partial class AddKeyword
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MeetupPlatformApi.Domain.Keyword", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("KeywordName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("keyword_name");

                    b.HasKey("Id")
                        .HasName("pk_keywords");

                    b.ToTable("keywords", (string)null);
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.Meetup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_time");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_meetups");

                    b.ToTable("meetups", (string)null);
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.MeetupKeyword", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("KeywordId")
                        .HasColumnType("uuid")
                        .HasColumnName("keyword_id");

                    b.Property<Guid>("MeetupId")
                        .HasColumnType("uuid")
                        .HasColumnName("meetup_id");

                    b.HasKey("Id")
                        .HasName("pk_meetups_keywords");

                    b.HasIndex("KeywordId")
                        .HasDatabaseName("ix_meetups_keywords_keyword_id");

                    b.HasIndex("MeetupId")
                        .HasDatabaseName("ix_meetups_keywords_meetup_id");

                    b.ToTable("meetups_keywords", (string)null);
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_refresh_tokens");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_refresh_tokens_user_id");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ux_users_username");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.MeetupKeyword", b =>
                {
                    b.HasOne("MeetupPlatformApi.Domain.Keyword", "Keyword")
                        .WithMany()
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_keywords_meetups_keywords_keyword_id");

                    b.HasOne("MeetupPlatformApi.Domain.Meetup", "Meetup")
                        .WithMany()
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_meetups_meetups_keywords_meetup_id");

                    b.Navigation("Keyword");

                    b.Navigation("Meetup");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.RefreshToken", b =>
                {
                    b.HasOne("MeetupPlatformApi.Domain.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_refresh_tokens_user_id");
                });

            modelBuilder.Entity("MeetupPlatformApi.Domain.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
