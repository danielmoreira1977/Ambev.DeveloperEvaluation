using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace Ambev.DeveloperEvaluation.Migrator;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly bool mustExecute = true;
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    private static BaseItem[] EmotionalBase =>
        [
            new BaseItem(new Guid("5dbaadcf-7113-4dfd-b585-83ccbb12f4ca"),"Accomplished"),
            new BaseItem(new Guid("a1053977-348f-43ae-a2a2-44d4c8767207"),"Affectionate"),
            new BaseItem(new Guid("a5ac6a51-2990-49b0-9330-9f2cff8dc08e"),"Amazed"),
            new BaseItem(new Guid("af929e43-d01c-4116-a1e8-2ed0f4599b29"),"Angry"),
            new BaseItem(new Guid("21a81ee4-084d-4563-8e9e-13ec91a00578"),"Anguished"),
            new BaseItem(new Guid("7b3f28fc-1960-4bc6-bb83-302a1598e72f"),"Anxious"),
            new BaseItem(new Guid("a4e3df1b-2d8e-484e-9419-e50c83deae14"),"Apathetic"),
            new BaseItem(new Guid("db60f75d-93cf-4713-a7de-26b5eba61654"),"Assertive"),
            new BaseItem(new Guid("e1d4950e-482b-45c5-892d-e72d906afedf"),"Beloved"),
            new BaseItem(new Guid("f0441e58-9356-4d42-b76d-3a6524febec5"),"Bitter"),
            new BaseItem(new Guid("a1aae339-3002-401d-81dc-63530ec7c89f"),"Calm"),
            new BaseItem(new Guid("94e310b8-ccb4-485a-8911-ff634fe82986"),"Caring"),
            new BaseItem(new Guid("2f5ce2ca-bd2c-4084-ae71-2ea470c1b0a5"),"Cheerful"),
            new BaseItem(new Guid("8ee040a2-b656-4df6-ab02-4fe94d9f9ead"),"Committed"),
            new BaseItem(new Guid("844b4d76-4a08-4e61-a291-f4d24005652b"),"Confident"),
            new BaseItem(new Guid("f2c64013-5c40-4927-944f-abcf01e6c5e9"),"Confused"),
            new BaseItem(new Guid("d6112cc8-d129-4619-9e7c-242f91b962ec"),"Content"),
            new BaseItem(new Guid("a308c26f-f312-40fb-9d10-3996c74a67bb"),"Courageous"),
            new BaseItem(new Guid("9d82445e-af72-4317-a0c9-faa07f26b376"),"Curious"),
            new BaseItem(new Guid("c2d747f9-f25b-4761-a671-289de2dd4dc4"),"Desperate"),
            new BaseItem(new Guid("d2b786a8-adba-4d8a-a45f-51d05838a121"),"Determined"),
            new BaseItem(new Guid("654a9895-d5c3-445e-bfea-3f288008e7c0"),"Disappointed"),
            new BaseItem(new Guid("c01edfd9-52f3-40dd-a905-e7514e055096"),"Discouraged"),
            new BaseItem(new Guid("e9dee1fe-cc99-43ed-b40d-7158f5beeb4a"),"Disturbed"),
            new BaseItem(new Guid("28f08f64-ce04-4c2a-93f6-f1fedd7a7f9a"),"Empathetic"),
            new BaseItem(new Guid("5dfda0fd-8852-4cd5-9f52-47fff8185cd2"),"Enchanted"),
            new BaseItem(new Guid("aa2ff9b1-90f2-4c8c-84fd-38f6d5d8be40"),"Enthusiastic"),
            new BaseItem(new Guid("44ee4b1b-101d-4a30-870e-7d10a4b9cc5b"),"Euphoric"),
            new BaseItem(new Guid("50c1941c-abd6-4b7c-9ed0-0c5378f87670"),"Excited"),
            new BaseItem(new Guid("9813cabf-b238-4288-a91d-cf258d90fa11"),"Fearful"),
            new BaseItem(new Guid("8e53274a-e397-4280-8537-28e6bfce0edf"),"Frustrated"),
            new BaseItem(new Guid("6a21672a-282a-4791-946d-b795f7716f8b"),"Funny"),
            new BaseItem(new Guid("55d19edb-0f87-422a-8fcd-ecae68d81be1"),"Grateful"),
            new BaseItem(new Guid("1906d5d8-2187-4ed2-9bf3-142ab1d48743"),"Guilty"),
            new BaseItem(new Guid("5704fdb7-7326-4a76-8f73-8ff54c13f1bf"),"Happy"),
            new BaseItem(new Guid("34ef227c-2614-4115-aede-295bfa4253e2"),"Hopeful"),
            new BaseItem(new Guid("08b98508-cbc1-4a6d-9e2c-b8ceedc738dd"),"Indecisive"),
            new BaseItem(new Guid("96937254-e177-4a2b-9d5f-23787b36a1dd"),"Insecure"),
            new BaseItem(new Guid("ddc8bcf1-6594-4e38-9a4f-8ecf1fae80cc"),"Inspired"),
            new BaseItem(new Guid("1f30daf7-38ab-4232-b3e0-a7f49357a168"),"Intrigued"),
            new BaseItem(new Guid("ac4a0ad5-f2c8-4dc1-8023-d35a7229f9a7"),"Jealous"),
            new BaseItem(new Guid("38f6f68c-e9be-48b1-b0db-00825763c72b"),"Lost"),
            new BaseItem(new Guid("6c8d09b9-b394-47a6-8e89-0c816bc30e56"),"Motivated"),
            new BaseItem(new Guid("d6e26f1b-d38c-478c-bdff-e052e4b9152e"),"Naughty"),
            new BaseItem(new Guid("b74e65a0-d5f1-4a5c-94f7-b16ab3deb946"),"Needy"),
            new BaseItem(new Guid("18e3ee43-519c-47df-b769-b813c26fde02"),"Passionate"),
            new BaseItem(new Guid("2553298d-691f-46cc-a6ac-4122efd995cc"),"Perverted"),
            new BaseItem(new Guid("84488e93-79b6-47ee-87e8-871c958b73db"),"Proactive"),
            new BaseItem(new Guid("825dfc1a-18fa-4d13-b78b-3ee6e6677fa8"),"Protected"),
            new BaseItem(new Guid("0a12ab0b-473a-4e97-955d-18a5ed670859"),"Proud"),
            new BaseItem(new Guid("6a94047c-ec59-437f-8f3c-dbc5555b976e"),"Reflective"),
            new BaseItem(new Guid("d0769910-2ef4-483f-9e9a-b211c9555ce0"),"Relaxed"),
            new BaseItem(new Guid("f7fe04e2-c8e3-491d-99d2-76edd96d8a13"),"Resilient"),
            new BaseItem(new Guid("0babae4f-f3da-4997-9d5c-1bc3d81fe056"),"Sad"),
            new BaseItem(new Guid("dff55f8a-f270-47a5-a887-6cf5384543f9"),"Satisfied"),
            new BaseItem(new Guid("b97ef558-ed7c-4f13-94bd-febaa09abc81"),"Secure"),
            new BaseItem(new Guid("d2fc47f3-31e2-486c-bbda-11b2430c2617"),"Self-fulfilled"),
            new BaseItem(new Guid("a6b1e9ad-e158-4e06-889a-cd437282ce8c"),"Self-sufficient"),
            new BaseItem(new Guid("58b46d2d-faa0-498b-bfec-cabd77ebf199"),"Serene"),
            new BaseItem(new Guid("dfab54d9-354c-4cbc-aac7-9bc759f8082d"),"Shocked"),
            new BaseItem(new Guid("51ce5572-e7d5-4d03-b9f2-ad24699d8b87"),"Surprised"),
            new BaseItem(new Guid("ba7a7f34-1a16-481c-9acb-ed667a75f0f5"),"Tired"),
            new BaseItem(new Guid("6d994198-b547-4f62-8ad0-3cb100c43344"),"Torn"),
            new BaseItem(new Guid("c1060b67-6270-4bee-afd1-f4213dd59373"),"Upset"),
            new BaseItem(new Guid("44580bb2-c6aa-4ba4-84fb-491a83825c3c"),"Worried"),
        ];

    private static BaseItem[] PersonalFeaturesBase =>
        [
            new BaseItem(new Guid("6cbf442b-5be4-4b35-a873-3f1dcda0c668"),"Authenticity"),
            new BaseItem(new Guid("f1352922-4036-4e54-aab4-3cd19fa909c4"),"Beauty"),
            new BaseItem(new Guid("f5cd6852-e424-47df-a0f0-59648669ee51"),"Charisma"),
            new BaseItem(new Guid("7fe936a4-5f9b-4643-97e9-35b74ae3c322"),"Confidence"),
            new BaseItem(new Guid("06ff1bc4-ea07-4390-963a-db4374230112"),"Creativity"),
            new BaseItem(new Guid("d8b150d4-0eb9-484b-bf3f-fffe54632d0b"),"Education"),
            new BaseItem(new Guid("e9094183-7e8d-46b8-833d-3e985f5ae2cd"),"Empathy"),
            new BaseItem(new Guid("3c52b4f5-4390-449d-964d-eac89a084ce4"),"Fidelity"),
            new BaseItem(new Guid("aca000f2-ccd9-4714-91d8-8b5bf45a0aba"),"Good humor"),
            new BaseItem(new Guid("3b7ecc15-5922-4392-8dec-f429c80f75ea"),"Healthy Living"),
            new BaseItem(new Guid("f27bd5c1-c1a9-4cb1-a349-726834b5ab0f"),"Honesty"),
            new BaseItem(new Guid("c15f67a0-c2f6-4608-a40b-4d814bd7cbe9"),"Independence"),
            new BaseItem(new Guid("0a096936-da99-4650-8818-80657e804186"),"Intelligence"),
            new BaseItem(new Guid("5b37fc55-72a4-471f-bbf4-ca23c0789686"),"Responsibility"),
            new BaseItem(new Guid("84074989-e46d-48fd-8a68-a87b3a8a1b4f"),"Sexy")
        ];

    private record ThemeItem(Guid Id, string Name, bool IsPremium);
    private record BaseItem(Guid Id, string Name);

    private static ThemeItem[] ThemesBase =>
        [
            new ThemeItem(new Guid("02f0881e-0ac8-4c4a-8623-779783450534"), "Relationships and Flirting", false),
            new ThemeItem(new Guid("84057581-a1e3-45ff-8e37-b4a0f1564755"), "Relationships and Flirting HOT", true),
            new ThemeItem(new Guid("7037999a-a5a5-493c-bba8-c801a54ceb23"), "Parties and Fun", false),
            new ThemeItem(new Guid("2b0bd1b7-330f-4d59-a86d-1ffb8b4bb5de"), "Parties and Fun HOT", true),
            new ThemeItem(new Guid("d99d2054-a748-4f73-bb3b-bdfa66903116"), "Schools and Work", false),
            new ThemeItem(new Guid("aaff3849-6bab-470b-8732-985d7697e802"), "Schools and Work HOT", true),
            new ThemeItem(new Guid("3b9b3cd1-ca43-4d34-be34-fa993948b7a1"), "Travel and Adventures", false),
            new ThemeItem(new Guid("d6d6fe05-ce11-439e-8dca-72bad763a046"), "Travel and Adventures HOT", true),
            new ThemeItem(new Guid("136c52e2-bf72-454d-9fe2-2f434d884e58"), "Family", false),
            new ThemeItem(new Guid("4e7b8f8c-2646-43a9-b0d4-a0d8e380dd6c"), "Friendships", false),
            new ThemeItem(new Guid("4880f3ae-8573-4f07-889f-94237f2e2fd7"), "Friendships HOT", true),
            new ThemeItem(new Guid("0646dfa0-5eb0-4a25-8a02-dc6585a0a5ca"), "Fears and Paranormal", false),
            new ThemeItem(new Guid("934f330c-c71e-46b0-8a3d-6425a2229406"), "Forbidden or Controversial Things", false),
            new ThemeItem(new Guid("7df233ad-2ffa-4a5f-a6db-0801691096fc"), "Crushes and Celebrities", false),
            new ThemeItem(new Guid("93363112-399e-4cb8-8e70-a6c2fbbdf19f"), "Crushes and Celebrities HOT", true),
            new ThemeItem(new Guid("fad50c58-e2b1-4d48-aa8e-d56d947ffd3e"), "Technology and Social Networks", false),
            new ThemeItem(new Guid("022f534d-f47c-4108-82dc-89eaf4212492"), "Personal Challenges", false),
            new ThemeItem(new Guid("45e032b3-a805-4f8c-95fe-af4492d730ce"), "Love Life", false),
            new ThemeItem(new Guid("ffa8553b-e051-4b29-b3b6-1bb7aec14920"), "Love Life HOT", true),
            new ThemeItem(new Guid("5d770839-3b0e-46e9-a0ec-639b96c0bde8"), "Pop Culture", false),
            new ThemeItem(new Guid("abb3f3bd-65e6-482c-afb2-0e814f00f498"), "Personal Curiosities", false),
            new ThemeItem(new Guid("5fd8e0f6-aebc-4f14-81f7-1c15bc4d1f64"), "Personal Curiosities HOT", true),
            new ThemeItem(new Guid("10452510-9967-4b27-95f5-028f627e5f3f"), "Sexuality", false),
            new ThemeItem(new Guid("0556df95-9eed-4264-a5e7-e848bf1c39b0"), "Sexuality HOT", true),
            new ThemeItem(new Guid("caebf72c-97d1-4f8d-a9e3-547b9cce4feb"), "Sexuality HOTTEST", true),
        ];

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!mustExecute)
        {
            hostApplicationLifetime.StopApplication();
            return;
        }

        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        var contextInterfaces = new[]
        {
            typeof(IDefaultContext)
        };

        try
        {
            using var scope = serviceProvider.CreateScope();

            foreach (var interfaceType in contextInterfaces)
            {
                var service = scope.ServiceProvider.GetRequiredService(interfaceType);

                if (service is not DbContext dbContext)
                {
                    throw new InvalidOperationException($"O serviço {interfaceType.Name} não é um DbContext.");
                }

                await EnsureDatabaseAsync(dbContext, cancellationToken);
                await RunMigrationAsync(dbContext, cancellationToken);
                await SeedDataAsync(dbContext, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            activity?.AddEvent(new ActivityEvent("Migration failed"));
            activity?.SetTag("exception", ex.ToString());
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    //private static ICollection<EmotionalStatus> CreateEmotionals()
    //{
    //    ICollection<EmotionalStatus> emotionalList = [];

    // foreach (var item in EmotionalBase) { var emotionalStatus = new EmotionalStatus(new
    // EmotionalStatusId(item.Id), item.Name);

    // emotionalList.Add(emotionalStatus); }

    //    return emotionalList;
    //}

    //private static ICollection<PersonalFeature> CreatePersonals()
    //{
    //    ICollection<PersonalFeature> personalList = [];

    // foreach (var item in PersonalFeaturesBase) { var personalFeature = new PersonalFeature(new
    // PersonalFeatureId(item.Id), item.Name);

    // personalList.Add(personalFeature); }

    //    return personalList;
    //}

    //private static ICollection<Theme> CreateThemes()
    //{
    //    ICollection<Theme> themeList = [];

    // foreach (var item in ThemesBase) { var theme = new Theme(new ThemeId(item.Id),
    // item.IsPremium, item.Name);

    // themeList.Add(theme); }

    //    return themeList;
    //}

    private static async Task EnsureDatabaseAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        try
        {
            var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

            var strategy = dbContext.Database.CreateExecutionStrategy();
            await
            strategy.ExecuteAsync(async () =>
            {
                var exists = await dbCreator.ExistsAsync(cancellationToken);

                if (!exists)
                {
                    await dbCreator.CreateAsync(cancellationToken);
                }
            });
        }
        catch (Exception ex)
        {
            var err = ex.Message;
            throw;
        }
    }

    //private static async Task InsertEmotionalStatus(DbContext dbContext, CancellationToken cancellationToken)
    //{
    //    if (!await dbContext.EmotionalsStatus.AnyAsync(cancellationToken))
    //    {
    //        var emotionalList = CreateEmotionals();
    //        await dbContext.EmotionalsStatus.AddRangeAsync(emotionalList, cancellationToken);
    //    }
    //}

    //private static async Task InsertPersonalFeatures(DbContext dbContext, CancellationToken cancellationToken)
    //{
    //    if (!await dbContext.PersonalFeatures.AnyAsync(cancellationToken))
    //    {
    //        var personalFeaturesList = CreatePersonals();
    //        await dbContext.PersonalFeatures.AddRangeAsync(personalFeaturesList, cancellationToken);
    //    }
    //}

    //private static async Task InsertThemesStatus(DbContext dbContext, CancellationToken cancellationToken)
    //{
    //    if (!await dbContext.Themes.AnyAsync(cancellationToken))
    //    {
    //        var themeList = CreateThemes();
    //        await dbContext.Themes.AddRangeAsync(themeList, cancellationToken);
    //    }
    //}

    private static async Task RunMigrationAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        /// dotnet ef migrations add InitialCreate --startup-project C:\Content\Work\FortunaFaber\Projects\ISpicy-project\ISpicy\src\MS\Identity\ISpicy.Identity.API
        /// --context ISpicyIdentityReadContext

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            //await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            //await transaction.CommitAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            //await InsertEmotionalStatus(dbContext, cancellationToken);
            //await InsertPersonalFeatures(dbContext, cancellationToken);
            //await InsertThemesStatus(dbContext, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
