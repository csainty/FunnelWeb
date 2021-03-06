using System;
using FunnelWeb.Model;
using FunnelWeb.Repositories.Queries;
using FunnelWeb.Tests.Helpers;
using NUnit.Framework;

namespace FunnelWeb.Tests.Integration.Queries
{
    [TestFixture]
    public class GetEntriesQueryCanReturnAllStatuses : QueryIntegrationTest
    {
        public GetEntriesQueryCanReturnAllStatuses()
            : base(TheDatabase.MustBeFresh)
        {
        }

        public override void TestQuery()
        {
            var name = "test-" + Guid.NewGuid();

            Database.WithRepository(
                repo =>
                    {
                        var entry1 = new Entry { Name = name, Author = "A1", Status = EntryStatus.PublicBlog };
                        var revision1 = entry1.Revise();
                        revision1.Body = "Hello";
                        repo.Add(entry1);

                        var entry2 = new Entry { Name = name, Author = "A1", Status = EntryStatus.Private };
                        var revision2 = entry2.Revise();
                        revision2.Body = "Goodbye";
                        repo.Add(entry2);

                        var entry3 = new Entry { Name = name, Author = "A1", Status = EntryStatus.PublicPage };
                        var revision3 = entry3.Revise();
                        revision3.Body = "Goodbye";
                        repo.Add(entry3);
                    });

            Database.WithRepository(
                repo =>
                    {
                        var result = repo.Find(new GetEntriesQuery(EntryStatus.All), 0, 3);
                        Assert.AreEqual(3, result.Count);
                        Assert.GreaterOrEqual(result.TotalResults, 3);
                    });
        }
    }
}