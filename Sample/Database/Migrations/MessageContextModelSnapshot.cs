using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Sample;

namespace Sample.Migrations
{
    [DbContext(typeof(MessageContext))]
    partial class MessageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sample.Conversation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Receiver")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Sender")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("Sample.TextMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ConversationId");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Number");

                    b.Property<DateTime?>("Read");

                    b.Property<string>("Receiver")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime>("Saved");

                    b.Property<string>("Sender")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10240);

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Sample.TextMessage", b =>
                {
                    b.HasOne("Sample.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
