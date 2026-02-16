namespace GalaxyDelivery.Entities;

public enum DeliveryStatusCode
{
    Planned = 1,
    Started = 2,
    InProgress = 3,
    Completed = 4,
    Failed = 5
}

public static class DeliveryStatusCodeNames
{
    private static readonly string[] Names =
    [
        "",
        "Planned",
        "Started",
        "InProgress",
        "Completed",
        "Failed"
    ];

    public static string GetName(DeliveryStatusCode code) => Names[(int)code];
}
