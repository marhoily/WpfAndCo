using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Sample;

namespace Sample.Migrations
{
    [DbContext(typeof(MessageContext))]
    partial class MessageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
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
                });

            modelBuilder.Entity("Sample.TextMessage", b =>
                {
                    b.HasOne("Sample.Conversation")
                        .WithMany()
                        .HasForeignKey("ConversationId");
                });
        }
    }
}
