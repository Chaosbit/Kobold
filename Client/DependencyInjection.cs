namespace Kobold.Client;

public static class DependencyInjection
{
    public static void AddKoboldClient(this IServiceCollection services)
    {
        services.AddAntDesign();
    }
}